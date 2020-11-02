using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Band_Web.Controllers
{
    public class shopPageController : Controller
    {
        // GET: ShopPage
        public ActionResult AlbumShop()
        {
            return View();
        }

        public ActionResult T_shirtShop()
        {
            return View();
        }

        public ActionResult BookShop()
        {
            return View();
        }
    }
}