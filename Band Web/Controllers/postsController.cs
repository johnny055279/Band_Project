using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Band_Web.Controllers
{
    public class postsController : Controller
    {
        // GET: posts
        public ActionResult List()
        {
            return View();
        }
    }
}