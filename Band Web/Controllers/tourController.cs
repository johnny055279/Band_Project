using Band_Web.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
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
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["BandProject"].ConnectionString))
            {
                string strConnect = @"select * from tTicketDetail";

                results = db.Query<TicketDetailModel>(strConnect).ToList();
            }
            return View(results);
        }
    }
}