using Blog.Repository.Managers;
using Blog.Repository.Models;
using Blog.Repository.Repositories;
using Blog.WebUI.Code;
using Blog.WebUI.Code.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.WebUI.Controllers
{
    [AllowAnonymous]
    public class TestController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IArticleManager _articleManager;
        private readonly ITagRepository _tagRepository;
        private readonly ITransliterationService _transliterationService;

        public TestController(IUserRepository userRepository, IArticleManager articleManager, ITagRepository tagRepository, ITransliterationService transliterationService)
        {
            _userRepository = userRepository;
            _articleManager = articleManager;
            _tagRepository = tagRepository;
            _transliterationService = transliterationService;
        }

        public ActionResult ClearUsers()
        {
            _userRepository.RemoveAll();
            return View();
        }

        public ActionResult FriendlyUrl(string source)
        {
            string friendly = _transliterationService.ToFriendlyUrl(source);
            return View("Index", friendly);
        }

        public ActionResult ClearTags()
        {
            _tagRepository.RemoveAll();
            return View();
        }

        public ActionResult RemoveTag(string tag)
        {
            _tagRepository.Remove(new TagModel { Name = tag });
            return View();
        }

        public ActionResult ClearArticles()
        {
            _articleManager.RemoveAll();
            return View();
        }

        public ActionResult CreateTags()
        {            
            _tagRepository.InsertRange(new TagModel[]
            {
                new  TagModel
                {
                    Name = "Lorem"
                },
                new  TagModel
                {
                    Name = "Ipsum"
                },
                new  TagModel
                {
                    Name = "Dolor"
                },
                new  TagModel
                {
                    Name = "Sit"
                },
                new  TagModel
                {
                    Name = "Amet"
                },
                new  TagModel
                {
                    Name = "Consectetuer"
                },
                new  TagModel
                {
                    Name = "Adipiscing"
                },
                new  TagModel
                {
                    Name = "Elit"
                },
                new  TagModel
                {
                    Name = "Sed"
                }
            });

            return View();
        }
    }
}
