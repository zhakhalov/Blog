using Blog.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.WebUI.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            ViewBag.User = (User.Identity.IsAuthenticated) ? Session["user"] : null;
            return View();            
        }

    }
}
