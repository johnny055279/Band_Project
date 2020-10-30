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
                    string strConnect = @"Select * from tPost order by LastEditDate desc";

                    results = (List<PostModel>)await db.QueryAsync<PostModel>(strConnect);
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
        public ActionResult create(PostModel postModel)
        {
            try
            {
                int errorValue = 0;
                string filetime = DateTime.Now.ToString("yyyyMMddhhmmssfff");
                string imagePath = Server.MapPath("~/Content/Images/PostsImage/" + filetime + postModel.PostMainImage.FileName);

                using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["BandProject"].ConnectionString))
                {
                    DynamicParameters parameters = new DynamicParameters();

                    parameters.Add("@PostContent", postModel.PostContent, DbType.String, ParameterDirection.Input);
                    parameters.Add("@PostUserId", postModel.PostUserId, DbType.Int32, ParameterDirection.Input);
                    parameters.Add("@PostMainImage_Path", imagePath, DbType.String, ParameterDirection.Input);
                    parameters.Add("@LastEditDate", DateTime.Now, DbType.DateTime, ParameterDirection.Input);
                    parameters.Add("@RETRUN_VALUE", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
                    db.Execute("CreatePostProcedure", parameters, commandType: CommandType.StoredProcedure);
                    errorValue = parameters.Get<int>("@RETRUN_VALUE");
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
    }
}