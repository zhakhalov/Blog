using Blog.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.WebUI.Controllers
{
    [AllowAnonymous]
    public class ArticleController : Controller
    {
        //
        // GET: /Article/

        public ActionResult Index(int id)
        {           
            return View();
        }
    }
}
