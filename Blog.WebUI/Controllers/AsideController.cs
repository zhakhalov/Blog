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
        public ActionResult Index()
        {
            int limit = 5;
            ArticleManager repository = new ArticleManager(Constants.BlogNoSQL);
            ViewBag.ArticleLists = new List<ArticleListModel>
            {                
                new ArticleListModel
                {
                    Title = "Newest",
                    Articles = repository.GetNewest(0,limit).OrderByDescending(a => a.CreateDate).ToList()
                },
                new ArticleListModel
                {
                    Title = "Most Viewed",
                    Articles = repository.GetMostViewed(limit).OrderByDescending(a => a.Viewed).ToList()
                }
            };
            ViewBag.Tags = new TagRepository(Constants.BlogNoSQL).GetAll().Select(t => t.Name).ToList();
            ViewBag.CanCteateArticle = HttpContext.User.Identity.IsAuthenticated;
            return View();
        }

    }
}
