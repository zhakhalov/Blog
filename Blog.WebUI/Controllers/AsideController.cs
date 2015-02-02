using Blog.Repository.Managers;
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
    [AllowAnonymous]
    public class AsideController : Controller
    {
        private readonly IArticleManager _articleManager;
        private readonly ITagRepository _tagRepository;

        public AsideController(IArticleManager articleManager, ITagRepository tagRepository)
        {
            _articleManager = articleManager;
            _tagRepository = tagRepository;
        }

        public ActionResult Index()
        {
            int limit = 5;
            ViewBag.ArticleLists = new List<ArticleListModel>
            {                
                new ArticleListModel
                {
                    Title = "Newest",
                    Articles = _articleManager.GetNewest(0,limit).OrderByDescending(a => a.CreateDate).ToList()
                },
                new ArticleListModel
                {
                    Title = "Most Viewed",
                    Articles = _articleManager.GetMostViewed(limit).OrderByDescending(a => a.Viewed).ToList()
                }
            };
            ViewBag.Tags = _tagRepository.GetAll().Select(t => t.Name).ToList();
            ViewBag.CanCteateArticle = HttpContext.User.Identity.IsAuthenticated;
            return View();
        }

        private string GetShortContent(string content, int limit)
        {
            return content.Substring(0, 250) + ((content.Length > 250) ? "..." : "");
        }
    }
}
