using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Blog.WebUI.Code.Services
{
    public class ArticleConfigService : IArticleConfigService
    {
        public int TitleLimit { get; private set; }
        public int CommentLimit { get; private set; }
        public int ShortContentLimit { get; private set; }
        public int AsideLimit { get; private set; }
        public int ItemsPerPage { get; private set; }
        public int MaxNumbers { get; private set; }

        public ArticleConfigService(
            int titleLimit,
            int itemsPerPage,
            int maxNumbers,
            int commentLimit,
            int shortContentLimit,
            int asideLimit)
        {
            TitleLimit = titleLimit;
            ItemsPerPage = itemsPerPage;
            MaxNumbers = maxNumbers;
            CommentLimit = commentLimit;
            ShortContentLimit = shortContentLimit;
            AsideLimit = asideLimit;
        }

        public string ShortifyContent(string content)
        {
            if (string.IsNullOrEmpty(content))
            {
                return "";
            }
            //HtmlDocument doc = new HtmlDocument();
            //doc.LoadHtml(content.Substring(0, ShortContentLimit) + "...");
            //System.IO.StringWriter sw = new System.IO.StringWriter();
            //System.Xml.XmlTextWriter xw = new System.Xml.XmlTextWriter(sw);
            //doc.Save(xw);
            //return sw.ToString();
            return new Regex("<[^>]*>").Replace(content, "").Substring(0, ShortContentLimit) + "...";
        }
    }
}