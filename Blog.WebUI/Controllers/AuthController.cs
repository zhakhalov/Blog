using Blog.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.WebUI.Controllers
{
    public class AuthController : Controller
    {
        //
        // GET: /Auth/

        [HttpGet]
        public ActionResult Register()
        {
            return View("Register");
        }

        [HttpPost]
        public ActionResult Register(RegisterModel user)
        {
            return View("Register");
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View("Login");
        }

        [HttpPost]
        public ActionResult Login(RegisterModel user)
        {
            return RedirectToAction("Index", "Home");
        }
    }
}
