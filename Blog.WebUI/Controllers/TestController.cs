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
        public ActionResult ClearUsers()
        {
            UserRepository repository = new UserRepository(Constants.BlogNoSQL);
            repository.RemoveAll();
            return View();
        }

        public ActionResult ClearTags()
        {
            TagRepository repository = new TagRepository(Constants.BlogNoSQL);
            repository.RemoveAll();
            return View();
        }

        public ActionResult ClearArticles()
        {
            ArticleRepository repository = new ArticleRepository(Constants.BlogNoSQL);
            repository.RemoveAll();
            return View();
        }

        public ActionResult CreateTags()
        {
            TagRepository repository = new TagRepository(Constants.BlogNoSQL);
            repository.InsertRange(new TagModel[]
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
