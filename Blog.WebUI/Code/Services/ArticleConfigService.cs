using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.WebUI.Code.Services
{
    public class ArticleConfigService : IArticleConfigService
    {
        public int TitleLimit { get; private set; }
        public int CommentLimit { get; private set; }
        public int ShortContentLimit { get; private set; }
        public int AsideLimit { get; private set; }
        public int PageLimit { get; private set; }

        public ArticleConfigService(
            int titleLimit,
            int pageLimit,
            int commentLimit,
            int shortContentLimit,
            int asideLimit)
        {
            TitleLimit = titleLimit;
            PageLimit = pageLimit;
            CommentLimit = commentLimit;
            ShortContentLimit = shortContentLimit;
            AsideLimit = asideLimit;
        }

        public string ShortifyContent(string content)
        {
            return content.Substring(0, ShortContentLimit) + ((content.Length > ShortContentLimit) ? "..." : "");
        }
    }
}