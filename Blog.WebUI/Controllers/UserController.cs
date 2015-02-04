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
        public ActionResult Public()
        {
            return View("Private");
        }

        [Authorize]
        public ActionResult Private()
        {
            var user = (UserModel)Session["user"];
            ViewBag.User = user;
            ViewBag.SummaryLimit = _userConfigService.SummaryLimit;
            ViewBag.AvatarUrl =_userConfigService.ResolveAvatarUrl("Default.png");
            ViewBag.Articles = _articleManager.GetByUser(user.Username, 0, int.MaxValue);
            return View("Private");
        }

        [Authorize]
        public ActionResult UploadAvatar(string file)
        {
            string extension = Path.GetExtension(Request.Files["avatar"].FileName);
            string filename = User.Identity.Name + extension;
            Request.Files["avatar"].SaveAs(_userConfigService.ResolveAvatarPath(filename));
            return Json(new { url = _userConfigService.ResolveAvatarUrl(filename) });
        }

        [HttpPost]
        [Authorize]
        public ActionResult Summary(string summary)
        {
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
