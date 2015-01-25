using Blog.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Dal.Models
{
    public class CommentModel
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public UserModel Author { get; set; }
        public DateTime Timestamp { get; set; }
        public int Mark { get; set; }
        public List<CommentModel> Comments { get; set; }

        public CommentModel() { }

        public CommentModel(Comment comment)
        {
            Id = comment.Id;
            Author = new UserModel(comment.Post.User);
            Timestamp = comment.Post.Timestamp;
            Mark = comment.Post.Mark.Sum(m => m.Direction);
        }
    }
}
