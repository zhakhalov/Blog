using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Repository.Models
{
    public class ArticleModel
    {
        public ObjectId _id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Content { get; set; }
        public int Rating { get; set; }
        public DateTime CreateDate { get; set; }
        public List<RateModel> Raters { get; set; }
        public List<CommentModel> Comments { get; set; }
        public List<string> Tags { get; set; }

        public ArticleModel()
        {
            Raters = new List<RateModel>();
            Comments = new List<CommentModel>();
            CreateDate = DateTime.UtcNow;
        }        

        public bool CanRate(UserModel user)
        {
            return (Author != user.Username) && (!Raters.Any(r => r.Username == user.Username));
        }
    }
}
