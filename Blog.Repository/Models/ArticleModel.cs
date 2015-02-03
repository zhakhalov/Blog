using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Repository.Models
{
    public class ArticleModel
    {
        [BsonId]
        public ObjectId _id { get; set; }
        [BsonRequired]
        public string Title { get; set; }
        [BsonRequired]
        public string Url { get; set; }
        [BsonRequired]
        public string Username { get; set; }
        [BsonRequired]
        public string Author { get; set; }
        [BsonRequired]
        public string Content { get; set; }
        public int Rating { get; set; }
        public int Viewed { get; set; }
        public DateTime CreateDate { get; set; }
        public List<RateModel> Raters { get; set; }
        public List<CommentModel> Comments { get; set; }
        public List<string> Tags { get; set; }                

        public ArticleModel()
        {
            Raters = new List<RateModel>();
            Tags = new List<string>();
            Comments = new List<CommentModel>();
            CreateDate = DateTime.UtcNow;
        }

        public bool CanRate(string username)
        {
            return (Author != username) && (!Raters.Any(r => r.Username == username));
        }
    }
}
