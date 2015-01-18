using Blog.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.WebUI.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            ViewBag.Articles = new List<ArticleContentModel>
            {
                new ArticleContentModel
                {
                    Title = "Article #1",
                    Body = "Body #1"
                },
                new ArticleContentModel
                {
                    Title = "Article #2",
                    Body = "Body #1"
                },
                new ArticleContentModel
                {
                    Title = "Article #3",
                    Body = "Body #1"
                },
                new ArticleContentModel
                {
                    Title = "Article #4",
                    Body = "Body #1"
                }
            };

            return View();            
        }

    }
}
