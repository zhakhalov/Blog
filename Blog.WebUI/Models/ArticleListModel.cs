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
        public PaginationModel Pagination { get; set; }
    }
}