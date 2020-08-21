using Band_Web.Models;
using Dapper;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace Band_Web.Controllers
{
    public class signUpPageController : Controller
    {
        // GET: signUpPage
        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(SignUpModel signUpModel)
        {
            int returnResult;
            try
            {
                using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["BandProject"].ConnectionString))
                {
                    CreateHash createHash = new CreateHash();
                    byte[] salt = createHash.getSalt();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@UserAccount", signUpModel.UserAccount, DbType.String, ParameterDirection.Input);
                    parameters.Add("@UserPassword", createHash.passWordeEncryption(signUpModel.UserPassword, salt), DbType.Binary, ParameterDirection.Input);
                    parameters.Add("@UserEmail", signUpModel.UserEmail, DbType.String, ParameterDirection.Input);
                    parameters.Add("@UserSexual", signUpModel.UserSexual, DbType.String, ParameterDirection.Input);
                    parameters.Add("@UserBirthday", signUpModel.UserBirthday, DbType.DateTime, ParameterDirection.Input);
                    parameters.Add("@UserCountry", signUpModel.UserCountry, DbType.String, ParameterDirection.Input);
                    parameters.Add("@UserAddress", signUpModel.UserAddress, DbType.String, ParameterDirection.Input);
                    parameters.Add("@UserPhone", signUpModel.UserPhone, DbType.String, ParameterDirection.Input);
                    parameters.Add("@UserSalt", salt, DbType.Binary, ParameterDirection.Input);
                    parameters.Add("@LastEditDate", signUpModel.LastEditDate, DbType.DateTime, ParameterDirection.Input);
                    parameters.Add("@RETURN_VALUE", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

                    db.Execute("UserSignUpPrecedure", parameters, commandType: CommandType.StoredProcedure);

                    returnResult = parameters.Get<int>("@RETURN_VALUE");
                }
                TempData["result"] = returnResult;
                return View();
            }
            catch (Exception ex)
            {
                ex.ToString();
                TempData["result"] = 2;
                return View();
            }
        }
    }
}