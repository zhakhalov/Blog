using Blog.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Dal.Models
{
    class ArticleModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public UserModel Author { get; set; }
        public DateTime Timestamp { get; set; }
        public int Mark { get; set; }
        public IEnumerable<CommentModel> Comments { get; set; }
        public IEnumerable<string> Tags { get; set; }

        public ArticleModel(Article article)
        {
            Id = article.Id;
            Title = article.Title;
            Content = article.Post.Content;
            Author = new UserModel(article.Post.User);
            Tags = article.ArticleTag.Select(t => t.Tag.Name).ToList();
        }
    }
}
