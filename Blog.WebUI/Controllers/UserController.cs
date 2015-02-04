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
        private readonly IAvatarService _avatarService;

        public UserController(IUserRepository userRepository, IAvatarService avatarService)
        {
            _userRepository = userRepository;
            _avatarService = avatarService;
        }

        [AllowAnonymous]
        public ActionResult Public()
        {
            return View("Private");
        }

        [Authorize]
        public ActionResult Private()
        {
            ViewBag.User = Session["user"];
            ViewBag.AvatarUrl =_avatarService.ResolveUrl("Default.png");
            return View("Private");
        }

        public ActionResult UploadAvatar(string file)
        {
            string extension = Path.GetExtension(Request.Files["avatar"].FileName);
            string filename = User.Identity.Name + extension;
            Request.Files["avatar"].SaveAs(_avatarService.ResolvePath(filename));
            return Json(new { url = "" });
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
