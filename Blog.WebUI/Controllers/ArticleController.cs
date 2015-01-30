using Blog.Repository.Models;
using Blog.Repository.Repositories;
using Blog.WebUI.Code;
using Blog.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.WebUI.Controllers
{
    public class ArticleController : Controller
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            ArticleRepository repository = new ArticleRepository(Constants.BlogNoSQL);
            List<ArticleModel> articles = repository.GetNewest(0, int.MaxValue);
            ViewBag.Articles = articles;
            return View("Articles");
        }

        [AllowAnonymous]
        public ActionResult Article(string id)
        {
            ArticleRepository articleRepository = new ArticleRepository(Constants.BlogNoSQL);
            ArticleModel article = articleRepository.GetById(new MongoDB.Bson.ObjectId(id));
            articleRepository.IncreaseViewed(1, article._id.ToString());

            article.Comments = article.Comments.OrderByDescending(c => c.CreateDate).ToList();

            ViewBag.Article = article;
            ViewBag.isCommentEnabled = HttpContext.User.Identity.IsAuthenticated;
            return View("Article");
        }

        [AllowAnonymous]
        public ActionResult Tag(string tag)
        {
            ArticleRepository repository = new ArticleRepository(Constants.BlogNoSQL);
            List<ArticleModel> articles = repository.GetByTag(tag, 0, int.MaxValue);
            ViewBag.Title = "Article by tag " + tag;
            ViewBag.Articles = articles;
            return View("Articles");
        }

        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Tags = new TagRepository(Constants.BlogNoSQL).GetAll().Select(t => t.Name).ToList();
            return View("Create");
        }

        [Authorize]
        [HttpPost]
        public ActionResult Create(ArticleModel article, string tags)
        {
            article.Author = ((UserModel)Session["user"]).FullName;
            article.Username = ((UserModel)Session["user"]).Username;
            article.Tags = tags.Split(',').ToList();
            ArticleRepository repository = new ArticleRepository(Constants.BlogNoSQL);
            repository.Save(article);
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public ActionResult Comment(string id, string comment)
        {
            CommentModel model = new CommentModel
            {
                Author = ((UserModel)HttpContext.Session["user"]).FullName,
                Content = comment
            };
            ArticleRepository repository = new ArticleRepository(Constants.BlogNoSQL);
            repository.AddComment(model, id);
            return View("Partial/Comment", model);
        }
    }
}
