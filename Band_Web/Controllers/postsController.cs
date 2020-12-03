using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Band_Web.Models;
using Dapper;
using ClassLibrary;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Imgur.API.Authentication;
using System.Net.Http;
using Imgur.API.Endpoints;
using System.Data.Entity.Infrastructure;
using StackExchange.Redis;
using System.Web.UI;

namespace Band_Web.Controllers
{
    public class postsController : Controller
    {
        // GET: posts
        public async Task<ActionResult> List()
        {
            List<PostModel> results = new List<PostModel>();
            try
            {
                using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["BandProject"].ConnectionString))
                {
                    string strConnect = @"Execute GetPostsProcedure";

                    results = (List<PostModel>)await db.QueryAsync<PostModel>(strConnect);
                }

                //Redis Db Testing
                RedisConnection.Init("127.0.0.1:6379");
                ConnectionMultiplexer redisConnectionMultiplexer = RedisConnection.RedisConnectionInstance.ConnectionMultiplexer;
                IDatabase redisDb = redisConnectionMultiplexer.GetDatabase();
                foreach (var item in results)
                {
                    redisDb.StringSet($"Post{item.PostId}LikeCount", item.PostLikeCount);
                    redisDb.StringSet($"Post{item.PostId}ReplyCount", item.PostReplyCount);
                }

                return View(results);
            }
            catch (Exception exeption)
            {
                TempData["Error"] = exeption.ToString();
                return RedirectToRoute("Default");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //Enable input with html tag
        [ValidateInput(false)]
        public ActionResult create(string PostContent, HttpPostedFileBase PostMainImage)
        {
            try
            {
                int errorValue = 0;
                string filetime = DateTime.Now.ToString("yyyyMMddhhmmssfff");
                string imagePath = "/Content/Images/PostsImage/" + filetime + PostMainImage.FileName;
                PostMainImage.SaveAs(Server.MapPath("~/Content/Images/PostsImage/" + filetime + PostMainImage.FileName));

                //Encoding the PostContent to avoid sql attack
                string PostContentEncode = HttpUtility.HtmlEncode(PostContent);

                using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["BandProject"].ConnectionString))
                {
                    DynamicParameters parameters = new DynamicParameters();

                    parameters.Add("@PostContent", PostContentEncode, DbType.String, ParameterDirection.Input);
                    parameters.Add("@PostUserAccount", "MCR", DbType.String, ParameterDirection.Input);
                    parameters.Add("@PostMainImage_Path", imagePath, DbType.String, ParameterDirection.Input);
                    parameters.Add("@RETRUN_VALUE", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
                    db.Execute("CreatePostProcedure", parameters, commandType: CommandType.StoredProcedure);
                    errorValue = parameters.Get<int>("@RETRUN_VALUE");
                    if (errorValue != 1)
                    {
                        TempData["SaveError"] = "Something Wrong When Saving...";
                    }
                }

                return RedirectToAction("List");
            }
            catch
            {
                TempData["SaveError"] = "Something Wrong When Saving...";
                return RedirectToAction("List");
            }
        }

        [HttpPost]
        public JsonResult UploadImage(HttpPostedFileBase upload)
        {
            string imageUrl = "";
            string filetime = DateTime.Now.ToString("yyyyMMddhhmmssfff");
            if (upload != null && upload.ContentLength > 0)
            {
                upload.SaveAs(Server.MapPath("~/Content/Images/PostsImage/" + filetime + upload.FileName));

                imageUrl = Url.Content("~/Content/Images/PostsImage/" + filetime + upload.FileName);
            }
            return Json(new { uploaded = "true", url = imageUrl });
        }

        [HttpPost]
        public JsonResult getAccountList(string id, string type)
        {
            List<dynamic> likeList = new List<dynamic>();
            try
            {
                using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["BandProject"].ConnectionString))
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@PostId", int.Parse(id), DbType.Int32, ParameterDirection.Input);
                    parameters.Add("@Type", type, DbType.String, ParameterDirection.Input);
                    likeList = db.Query("GetAccountListProcedure", parameters, commandType: CommandType.StoredProcedure).ToList();
                }
                return Json(likeList);
            }
            catch (Exception ex)
            {
                return Json(new { Error = ex.ToString() });
            }
        }

        [HttpPost]
        public JsonResult userAction(string id, int cancel)
        {
            string RETURNVALUE = "ERROR";
            RedisConnection.Init("127.0.0.1:6379");
            ConnectionMultiplexer redisConnectionMultiplexer = RedisConnection.RedisConnectionInstance.ConnectionMultiplexer;
            if (Session[SessionDictionary.User_Account] == null)
            {
                return Json(new { Error = "Please Login!" });
            }
            else
            {
                try
                {
                    //Redis Db Testing
                    if (cancel == 1)
                    {
                        IDatabase redisDb = redisConnectionMultiplexer.GetDatabase();
                        redisDb.StringIncrement($"Post{id}LikeCount", 1, StackExchange.Redis.CommandFlags.FireAndForget);
                        return Json("");
                    }
                    else
                    {
                        IDatabase redisDb = redisConnectionMultiplexer.GetDatabase();
                        redisDb.StringDecrement($"Post{id}LikeCount", 1, StackExchange.Redis.CommandFlags.FireAndForget);
                        return Json("");
                    }

                    using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["BandProject"].ConnectionString))
                    {
                        DynamicParameters parameters = new DynamicParameters();
                        parameters.Add("", int.Parse(id), DbType.Int32, ParameterDirection.Input);
                        parameters.Add("", cancel, DbType.Int32, ParameterDirection.Input);
                        parameters.Add("@RETURN_VALUE", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
                        db.Execute("PostUserActionProcedure", parameters, commandType: CommandType.StoredProcedure);
                        RETURNVALUE = parameters.Get<string>("@RETURN_VALUE");
                    }

                    if (RETURNVALUE == "ERROR")
                    {
                        return Json(new { Error = "Something Wrong..." });
                    }
                }
                catch (Exception ex)
                {
                    return Json(new { Error = ex.ToString() });
                }
            }
        }
    }
}