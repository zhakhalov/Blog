using Blog.Repository.Managers;
using Blog.Repository.Models;
using Blog.Repository.Repositories;
using Blog.WebUI.Code.Services;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.WebUI.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IArticleManager _articleManager;
        private readonly IUserConfigService _userConfigService;

        public UserController(
            IUserRepository userRepository,
            IArticleManager articleManager,
            IUserConfigService userConfigService)
        {
            _userRepository = userRepository;
            _articleManager = articleManager;
            _userConfigService = userConfigService;
        }

        [AllowAnonymous]
        public ActionResult Public(string username)
        {
            var user = _userRepository.GetByLogin(username);
            ViewBag.User = user;
            ViewBag.AvatarUrl = _userConfigService.ResolveAvatarUrl(
                string.IsNullOrWhiteSpace(user.AvatarUrl) ? _userConfigService.DefaultAvatar : user.AvatarUrl);
            ViewBag.Articles = _articleManager.GetByUser(user.Username, 0, int.MaxValue);
            return View("Public");
        }

        [Authorize]
        public ActionResult Private()
        {
            Session["user"] = _userRepository.GetByLogin(User.Identity.Name);
            var user = (UserModel)Session["user"];
            ViewBag.User = user;
            ViewBag.SummaryLimit = _userConfigService.SummaryLimit;
            ViewBag.AvatarUrl =_userConfigService.ResolveAvatarUrl(
                string.IsNullOrWhiteSpace(user.AvatarUrl) ? _userConfigService.DefaultAvatar : user.AvatarUrl);
            ViewBag.Articles = _articleManager.GetByUser(user.Username, 0, int.MaxValue);
            return View("Private");
        }

        [Authorize]
        [HttpPost]
        public ActionResult UploadAvatar(string file)
        {
            string extension = Path.GetExtension(Request.Files["file"].FileName);
            string filename = User.Identity.Name + extension;
            Request.Files["file"].SaveAs(_userConfigService.ResolveAvatarPath(filename));
            _userRepository.UpdateAvatar(User.Identity.Name, filename);
            Session["user"] = _userRepository.GetByLogin(User.Identity.Name);
            return Json(new { url = _userConfigService.ResolveAvatarUrl(filename) });
        }

        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(string oldPassword, string newPassword)
        {
            var user = (UserModel)Session["user"];
            var ok = user.Password == oldPassword;
            if (ok)
            {
                _userRepository.ChangePassword(User.Identity.Name, newPassword);
            }           
            return Json(new { ok = ok });
        }

        [HttpPost]
        [Authorize]
        public ActionResult Summary(string summary)
        {
            _userRepository.UpdateSummary(User.Identity.Name, summary);
            Session["user"] = _userRepository.GetByLogin(User.Identity.Name);
            return Json(new { ok = true });
        }

        [AllowAnonymous]
        public ActionResult NavBar()
        {
            ViewBag.ReturnUrl = Request.Url.ToString();    
            
            if (User.Identity.IsAuthenticated)
            {
                if (null == Session["user"])
                {
                    Session["user"] = _userRepository.GetByLogin(User.Identity.Name);
                }
                ViewBag.User = Session["user"];
                return View("Partial/User");
            }
            return View("Partial/Login");
        }
    }
}
