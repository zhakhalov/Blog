using Blog.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.WebUI.Models
{
    public class ArticleListModel
    {
        public string Title { get; set; }
        public List<ArticleModel> Articles { get; set; }
        public bool UsePagination { get; set; }
        public string Action { get; set; }
        public string Controller { get; set; }
        public long TotalPages { get; set; }
        public long CurrentPage { get; set; }
    }
}