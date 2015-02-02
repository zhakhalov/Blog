using Blog.Repository.Managers;
using Blog.Repository.Models;
using Blog.Repository.Repositories;
using Blog.WebUI.Code;
using Blog.WebUI.Models;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.WebUI.Controllers
{
    public class ArticleController : Controller
    {
        private readonly IArticleManager _articleManager;
        private readonly ITagRepository _tagRepository;

        public ArticleController(IArticleManager articleManager, ITagRepository tagRepository)
        {
            _articleManager = articleManager;
            _tagRepository = tagRepository;
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            List<ArticleModel> articles = _articleManager.GetNewest(0, int.MaxValue);
            ViewBag.Articles = articles;
            return View("Articles");
        }

        [AllowAnonymous]
        public ActionResult Article(string id)
        {
            ArticleModel article = null;

            try { article = _articleManager.GetById(new ObjectId(id)); }
            catch { } // invalid id

            if (null == article) { return View("Error/404"); }

            _articleManager.IncreaseViewed(1, article._id.ToString());
            article.Comments = article.Comments.OrderByDescending(c => c.CreateDate).ToList();

            ViewBag.Article = article;
            ViewBag.isCommentEnabled = HttpContext.User.Identity.IsAuthenticated;
            return View("Article");
        }

        [AllowAnonymous]
        public ActionResult Tag(string tag)
        {
            List<ArticleModel> articles = _articleManager.GetByTag(tag, 0, int.MaxValue);
            ViewBag.Title = "Article by tag " + tag;
            ViewBag.Articles = articles;
            return View("Articles");
        }

        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Tags = _tagRepository.GetAll().Select(t => t.Name).ToList();
            return View("Create");
        }

        [Authorize]
        [HttpPost]
        public ActionResult Create(ArticleModel article, string tags)
        {
            article.Author = ((UserModel)Session["user"]).FullName;
            article.Username = ((UserModel)Session["user"]).Username;
            article.Tags = tags.Split(',').ToList();
            _articleManager.Save(article);
            return RedirectToAction("Article", new { id = article._id.ToString() });
        }

        [Authorize]
        public ActionResult Comment(string id, string comment)
        {
            CommentModel model = new CommentModel
            {
                Author = ((UserModel)HttpContext.Session["user"]).FullName,
                Content = comment
            };            
            _articleManager.AddComment(
                comment: model,
                articleId: id);
            return View("Partial/Comment", model);
        }

        [Authorize]
        public ActionResult Rate(string articleId, bool like)
        {
            _articleManager.RateArticle(
                rate: new RateModel
                {
                    Like = like,
                    Username = HttpContext.User.Identity.Name
                },
                articleId: articleId);
            return Json(new { like = like });
        }
    }
}
