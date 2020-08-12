using Band_Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Band_Web.Controllers
{
    public class tourController : Controller
    {
        private BandProjectEntities db = new BandProjectEntities();

        // GET: Tour
        public ActionResult List()
        {
            return View(db.tTicketDetail.ToList());
        }
    }
}