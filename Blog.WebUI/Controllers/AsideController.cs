using Blog.Repository.Managers;
using Blog.Repository.Models;
using Blog.Repository.Repositories;
using Blog.WebUI.Code;
using Blog.WebUI.Code.Services;
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
        private readonly IArticleConfigService _articleConfigService;

        public AsideController(
            IArticleManager articleManager,
            ITagRepository tagRepository,
            IArticleConfigService articleConfigService)
        {
            _articleManager = articleManager;
            _tagRepository = tagRepository;
            _articleConfigService = articleConfigService;
        }

        public ActionResult Index()
        {
            ViewBag.ArticleLists = new List<ArticleListModel>
            {                
                new ArticleListModel
                {
                    Title = "Newest",
                    Articles = _articleManager.GetNewest(0, _articleConfigService.AsideLimit).OrderByDescending(a => a.CreateDate).ToList()
                },
                new ArticleListModel
                {
                    Title = "Most Viewed",
                    Articles = _articleManager.GetMostViewed(0, _articleConfigService.AsideLimit).OrderByDescending(a => a.Viewed).ToList()
                },
                 new ArticleListModel
                {
                    Title = "Top Rated",
                    Articles = _articleManager.GetTopRated(0, _articleConfigService.AsideLimit).OrderByDescending(a => a.Viewed).ToList()
                }
            };
            ViewBag.Tags = _tagRepository.GetAll().Select(t => t.Name).ToList();
            ViewBag.CanCteateArticle = HttpContext.User.Identity.IsAuthenticated;
            return View();
        }
    }
}
