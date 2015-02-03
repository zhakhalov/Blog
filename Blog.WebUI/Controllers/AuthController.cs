using Blog.Repository.Models;
using Blog.Repository.Repositories;
using Blog.WebUI.Code;
using Blog.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Blog.WebUI.Controllers
{
    [AllowAnonymous]
    public class AuthController : Controller
    {
        private readonly IUserRepository _userRepository;

        public AuthController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View("Register");
        }

        [HttpPost]
        public ActionResult Register(RegisterModel user)
        {
            if (ModelState.IsValid)
            {
                UserModel userModel = new UserModel
                {
                    Username = user.Username,
                    Email = user.Email,
                    FullName = user.FullName,
                    Password = user.Password,
                    Roles = new List<string> { "user" }
                };
                _userRepository.Save(userModel);
                LoginUser(userModel);
                return RedirectToAction("Index", "Article");
            }
            return View("Register");
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View("Login");
        }

        [HttpPost]
        public ActionResult Login(UserModel user, string ReturnUrl)
        {
            var userModel = _userRepository.GetByLogin(user.Username);
            bool incorrectLogin = null == userModel;
            bool incorrectPassword = !incorrectLogin && user.Password != userModel.Password;
            if (incorrectLogin || incorrectPassword)
            {
                ViewBag.IncorrectLogin = incorrectLogin;
                ViewBag.IncorrectPassword = incorrectPassword;
                return View("Login");
            }
            LoginUser(userModel);

            return Redirect(ReturnUrl);
        }

        [HttpGet]
        public ActionResult Logout(string ReturnUrl)
        {
            FormsAuthentication.SignOut();
            HttpContext.Session["user"] = null;
            return Redirect(ReturnUrl);
        }

        #region Private

        private void LoginUser(UserModel user)
        {
            FormsAuthentication.SetAuthCookie(user.Username, true);
            System.Web.HttpContext.Current.User = new GenericPrincipal(new GenericIdentity(user.Username), user.Roles.ToArray());
            System.Web.HttpContext.Current.Session["user"] = user;
        }

        #endregion Private
    }
}
