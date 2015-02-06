using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.WebUI.Models
{
    public class PaginationModel
    {
        public string Action { get; set; }
        public int ItemsPerPage { get; set; }       // angular-pagination property
        public int ItemsCount { get; set; }         // angular-pagination property
        public int MaxNumbers { get; set; }         // angular-pagination property
        public int StartPage { get; set; }          // angular-pagination property
    }
}