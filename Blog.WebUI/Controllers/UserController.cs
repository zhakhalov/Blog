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
            return View("Partial/NavBar");
        }
    }
}
