using Band_Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
        public ActionResult SignUp(signUpModel signUpModel)
        {
            return RedirectToRoute("");
        }
    }
}