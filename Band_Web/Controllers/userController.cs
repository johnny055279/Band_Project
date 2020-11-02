using Band_Web.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using Dapper;
using System.Data.SqlClient;
using System.Web.Mvc;
using System.Data;
using System.Linq;

namespace Band_Web.Controllers
{
    public class userController : Controller
    {
        // GET: user
        public ActionResult UserEdit()
        {
            BandProjectEntities db = new BandProjectEntities();
            if (Session[SessionDictionary.User_Account] != null)
            {
                try
                {
                    var account = Session[SessionDictionary.User_Account].ToString();
                    var results = db.tUser.Select(n => n).Where(n => n.UserAccount == account).FirstOrDefault();
                    return View(results);
                }
                catch
                {
                    return RedirectToRoute("Default");
                }
            }
            else
            {
                return RedirectToRoute("Default");
            }
        }

        [HttpPost]
        public ActionResult UserEdit(string UserPassword, string UserEmail, DateTime UserBirthday, string UserCountry, string UserAddress, string UserPhone)
        {
            string UserAccount = Session[SessionDictionary.User_Account].ToString();
            try
            {
                int returnResult = 0;
                using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["BandProject"].ConnectionString))
                {
                    CreateHash createHash = new CreateHash();
                    byte[] salt = createHash.getSalt();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@UserAccount", UserAccount, DbType.String, direction: ParameterDirection.Input);
                    if (UserPassword != null)
                    {
                        parameters.Add("@UserPassword", createHash.passWordeEncryption(UserPassword, salt), DbType.Binary, direction: ParameterDirection.Input);
                    }
                    parameters.Add("@UserEmail", UserEmail, DbType.String, direction: ParameterDirection.Input);
                    parameters.Add("@UserBirthday", UserBirthday, DbType.DateTime, direction: ParameterDirection.Input);
                    parameters.Add("@UserCountry", UserCountry, DbType.String, direction: ParameterDirection.Input);
                    parameters.Add("@UserAddress", UserAddress, DbType.String, direction: ParameterDirection.Input);
                    parameters.Add("@UserPhone", UserPhone, DbType.String, direction: ParameterDirection.Input);
                    parameters.Add("@UserSalt", salt, DbType.Binary, direction: ParameterDirection.Input);
                    parameters.Add("@LastEditDate", DateTime.Now, DbType.Date, direction: ParameterDirection.Input);
                    parameters.Add("@RETURN_VALUE", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

                    db.Execute("UserEditProcedure", parameters, commandType: CommandType.StoredProcedure);

                    returnResult = parameters.Get<int>("@RETURN_VALUE");
                }
                return RedirectToRoute("Default");
            }
            catch
            {
                TempData["Error"] = "Woops...Something Wrong...";
                return View();
            }
        }

        public JsonResult checkPassword(string password)
        {
            string UserAccount = Session[SessionDictionary.User_Account].ToString();
            int returnValue = 0;
            try
            {
                using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["BandProject"].ConnectionString))
                {
                    DynamicParameters parameters = new DynamicParameters();
                    CreateHash createHash = new CreateHash();
                    string strConnection = $"select UserSalt from tUser where UserAccount='{UserAccount}'";
                    byte[] salt = db.Query<byte[]>(strConnection).FirstOrDefault();
                    parameters.Add("@UserAccount", UserAccount, DbType.String, direction: ParameterDirection.Input);
                    parameters.Add("@UserPassword", createHash.passWordeEncryption(password, salt), DbType.Binary, direction: ParameterDirection.Input);
                    parameters.Add("@RETURN_VALUE", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

                    db.Execute("UserLoginProcedure", parameters, commandType: CommandType.StoredProcedure);

                    returnValue = parameters.Get<int>("@RETURN_VALUE");
                    return Json(returnValue);
                }
            }
            catch
            {
                TempData["Error"] = "Something went wong...";
                return Json(returnValue);
            }
        }
    }
}