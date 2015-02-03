using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.WebUI.Controllers
{
    public class UserController : Controller
    {
        [AllowAnonymous]
        public ActionResult Public()
        {
            return View("Private");
        }

        [Authorize]
        public ActionResult Private()
        {
            return View("Private");
        }

        [AllowAnonymous]
        public ActionResult NavBar()
        {
            ViewBag.ReturnUrl = Request.Url.ToString();
            ViewBag.User = Session["user"];
            if (User.Identity.IsAuthenticated) { return View("Partial/User"); }
            return View("Partial/Login");
        }
    }
}
