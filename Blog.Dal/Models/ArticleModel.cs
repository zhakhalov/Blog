using Blog.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Dal.Models
{
    public class ArticleModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public UserModel Author { get; set; }
        public DateTime Timestamp { get; set; }
        public int Mark { get; set; }
        public List<CommentModel> Comments { get; set; }
        public List<string> Tags { get; set; }

        public ArticleModel(Article article)
        {
            Id = article.Id;
            Title = article.Title;
            Content = article.Post.Content;
            Timestamp = article.Post.Timestamp;
            
            Author = new UserModel(article.Post.User);
            
            Tags = article.ArticleTag.OrderBy(a => a.Id).Select(t => t.Tag.Name).ToList();
            Comments = article.Comment.Select(t => new CommentModel(t)).ToList();
            Mark = article.Post.Mark.Sum(m => m.Direction);
        }
    }
}
