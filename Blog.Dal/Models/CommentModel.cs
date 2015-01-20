using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Dal.Models
{
    class CommentModel
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public UserModel Author { get; set; }
        public int Mark { get; set; }
        public List<CommentModel> Comments { get; set; }
    }
}
