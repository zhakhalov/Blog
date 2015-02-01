using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Repository.Models
{
    public class CommentModel
    {
        public ObjectId _id { get; set; }
        public string Username { get; set; }
        public string Author { get; set; }
        public string Content { get; set; }
        public DateTime CreateDate { get; set; }
        public List<RateModel> Raters { get; set; }
        public List<CommentModel> Comments { get; set; }

        public CommentModel()
        {
            CreateDate = DateTime.UtcNow;
            Comments = new List<CommentModel>();
        }

        public bool CanRate(string username)
        {
            return (Author != username) && (!Raters.Any(r => r.Username == username));
        }
    }
}
