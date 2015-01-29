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
    public class TestController : Controller
    {
        //
        // GET: /Test/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateUser()
        {
            UserRepository userRepository = new UserRepository(Constants.BlogNoSQL);
            userRepository.Save(new UserModel
            {
                FullName = "Admin",
                Username = "admin",
                Email = "monolith1143@gmail.com",
                Password = "zQ123456",
                Roles = new List<string> {"user", "admin"}
            });
            return View();
        }

        public ActionResult CreateArticle()
        {
            ArticleRepository articleRepository = new ArticleRepository(Constants.BlogNoSQL);
            articleRepository.Save(new ArticleModel
            {
                Author = "admin",
                Content = "Article content",
                Tags = new List<string> { "general", "test"}
            });
            return View();
        }

        public ActionResult AddComment()
        {
            ArticleRepository articleRepository = new ArticleRepository(Constants.BlogNoSQL);
            articleRepository.GetByUser("admin", 0, 1)[0].Comments.Add(new CommentModel
                {
                    Author = "admin",
                    Content = "Comment content"
                });
            return View();
        }

    }
}
