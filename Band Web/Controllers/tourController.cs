using Band_Web.Models;
using Band_Web.ViewModels;
using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;

namespace Band_Web.Controllers
{
    public class tourController : Controller
    {
        // GET: Tour
        public ActionResult List()
        {
            List<TicketDetailModel> results = new List<TicketDetailModel>();
            try
            {
                using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["BandProject"].ConnectionString))
                {
                    string strConnect = @"select * from tTicketDetail";

                    results = db.Query<TicketDetailModel>(strConnect).ToList();
                }
                return View(results);
            }
            catch
            {
                return View();
            }
        }

        public ActionResult TicketPurchase(int id)
        {
            TicketPurchaseViewModel viewModel = new TicketPurchaseViewModel();
            try
            {
                using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["BandProject"].ConnectionString))
                {
                    string strConnect = $"select * from tTicketDetail where TicketDetailId = {id}; " +
                                        $"select * from tUser where UserAccount = {SecctionDictionary.UserAccount};";
                    using (var results = db.QueryMultiple(strConnect))
                    {
                        var ticketDetail = results.Read<tTicketDetail>().FirstOrDefault();
                        var user = results.Read<tUser>().FirstOrDefault();

                        viewModel.tticketDetail = ticketDetail;
                        viewModel.tUser = user;
                    }
                }
            }
            catch
            {
                TempData["Error"] = "System Error...";
            }
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult TicketPurchase(TicketPurchaseViewModel model)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["BandProject"].ConnectionString))
            {
            }
            return View();
        }
    }
}