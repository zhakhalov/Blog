using Blog.Repository.Managers;
using Blog.Repository.Models;
using Blog.Repository.Repositories;
using Blog.WebUI.Code;
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

        public TestController(IUserRepository userRepository, IArticleManager articleManager, ITagRepository tagRepository)
        {
            _userRepository = userRepository;
            _articleManager = articleManager;
            _tagRepository = tagRepository;
        }

        public ActionResult ClearUsers()
        {
            _userRepository.RemoveAll();
            return View();
        }

        public ActionResult ClearTags()
        {
            _tagRepository.RemoveAll();
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
