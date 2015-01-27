using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Repository.Models
{
    public class UserModel
    {
        public ObjectId _id { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreateDate { get; set; }
        public List<string> Roles { get; set; }

        public UserModel()
        {
            CreateDate = DateTime.UtcNow;
            Roles = new List<string>();
        }
    }
}
