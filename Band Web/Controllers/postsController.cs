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
        public async Task<ActionResult> create(PostModel postModel)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["BandProject"].ConnectionString))
                {
                    for (int i = 0; i < postModel.PostTag.Length; i++)
                    {
                        int errorValue = 0;
                        DynamicParameters parameters = new DynamicParameters();
                        parameters.Add("@PostTitle", postModel.PostTitle, DbType.String, ParameterDirection.Input);
                        parameters.Add("@PostContent", postModel.PostContent, DbType.String, ParameterDirection.Input);
                        parameters.Add("@PostLikeCount", postModel.PostLikeCount, DbType.Int32, ParameterDirection.Input);
                        parameters.Add("@PostDislikeCount", postModel.PostDislikeCount, DbType.Int32, ParameterDirection.Input);
                        parameters.Add("@PostReplyCount", postModel.PostReplyCount, DbType.Int32, ParameterDirection.Input);
                        parameters.Add("@PostUserId", postModel.PostUserId, DbType.Int32, ParameterDirection.Input);
                        parameters.Add("@PostTag", postModel.PostTag[i], DbType.String, ParameterDirection.Input);
                        parameters.Add("@PostBackGroundImage", postModel.PostBackGroundImage_post_back, DbType.Binary, ParameterDirection.Input);
                        parameters.Add("@PostContentImage", postModel.PostContentImage_post_back, DbType.Binary, ParameterDirection.Input);
                        parameters.Add("@LastEditDate", DateTime.Now, DbType.DateTime, ParameterDirection.Input);
                        parameters.Add("@RETRUN_VALUE", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
                        db.Execute("CreatePostProcedure", parameters, commandType: CommandType.StoredProcedure);
                        errorValue = parameters.Get<int>("@RETRUN_VALUE");
                    }

                    return View();
                }
            }
            catch
            {
                return View();
            }
        }
    }
}