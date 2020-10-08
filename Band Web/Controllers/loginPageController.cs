using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using ClassLibrary;
using Dapper;

namespace Band_Web.Models
{
    public class loginPageController : Controller
    {
        private BandProjectEntities bandProjectEntities = new BandProjectEntities();

        // GET: login
        public ActionResult loginPage()
        {
            return View();
        }

        [HttpPost]
        public ActionResult loginPage(LoginModel loginModel)
        {
            int returnResult;
            try
            {
                var salt = (from n in bandProjectEntities.tUser
                            where n.UserAccount == loginModel.UserAccount
                            select n.UserSalt).FirstOrDefault();

                using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["BandProject"].ConnectionString))
                {
                    CreateHash createHash = new CreateHash();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@UserAccount", loginModel.UserAccount, DbType.String, ParameterDirection.Input);
                    parameters.Add("@UserPassword", createHash.passWordeEncryption(loginModel.UserPassword, salt), DbType.Binary, ParameterDirection.Input);
                    parameters.Add("@RETURN_VALUE", DbType.Int32, direction: ParameterDirection.ReturnValue);

                    db.Execute("UserLoginProcedure", parameters, commandType: CommandType.StoredProcedure);

                    returnResult = parameters.Get<int>("@RETURN_VALUE");
                }

                if (returnResult != 1)
                {
                    TempData["AccountNotExit"] = "Sorry, account or assword not match,\nplease try again!";
                    return View();
                }
                else
                {
                    Session[SessionDictionary.User_Account] = loginModel.UserAccount;
                    return RedirectToAction("HomePage", "homePage");
                }
            }
            catch (Exception ex)
            {
                TempData["Exception"] = ex.ToString();
                return View();
            }
        }

        public int getEmail(string email)
        {
            int returnResult;

            try
            {
                using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["BandProject"].ConnectionString))
                {
                    DynamicParameters parameters = new DynamicParameters();
                    CreateHash createHash = new CreateHash();
                    parameters.Add("@UserEmail", email, DbType.String, ParameterDirection.Input);
                    parameters.Add("@RETURN_VALUE", DbType.Int32, direction: ParameterDirection.ReturnValue);

                    db.Execute("UserForgetEmailProcedure", parameters, commandType: CommandType.StoredProcedure);

                    returnResult = parameters.Get<int>("@RETURN_VALUE");

                    if (returnResult == 1)
                    {
                        string newPassword = SendEmail.SendUserEmail(email);
                        byte[] salt = createHash.getSalt();
                        try
                        {
                            var q = (from n in bandProjectEntities.tUser
                                     where n.UserEmail == email
                                     select n.UserPassword).FirstOrDefault();

                            q = createHash.passWordeEncryption(newPassword, salt);

                            bandProjectEntities.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            ex.ToString();
                        }

                        return 1;
                    }
                }
            }
            catch
            {
                return 0;
            }
            return returnResult;
        }
    }
}