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
        //
        // GET: /Article/

        public ActionResult Index(int id)
        {
            ViewBag.Id = id;
            ViewBag.Article =  new ArticleContentModel
            {
                Title = "ArticleTitle",
                Body = "body"          
            };
            ViewBag.Comments = new List<CommentModel>
            {
                new CommentModel
                {
                    Body = "comment#1"    
                },
                new CommentModel
                {
                    Body = "comment#2"    
                },
                new CommentModel
                {
                    Body = "comment#3"    
                },
                new CommentModel
                {
                    Body = "comment#4"    
                }
            };

            return View();
        }
    }
}
