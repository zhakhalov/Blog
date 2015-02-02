using Blog.Repository.Models;
using Blog.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.WebUI.Controllers
{
    public class TagController : Controller
    {
        private readonly ITagRepository _tagRepository;

        public TagController(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        [Authorize]
        [HttpPost]
        public ActionResult Create(string tag)
        {
            ViewBag.Tag = tag;
            _tagRepository.Save(new TagModel { Name = tag });
            return Json(new {tag = tag });
        }
    }
}
