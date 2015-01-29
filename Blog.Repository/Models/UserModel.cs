using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Repository.Models
{
    public class UserModel
    {
        [BsonId]
        public ObjectId _id { get; set; }
        [BsonRequired]
        public string Username { get; set; }
        [BsonRequired]
        public string FullName { get; set; }
        [BsonRequired]
        public string Email { get; set; }
        [BsonRequired]
        public string Password { get; set; }
        [BsonDefaultValue("Default.jpg")]
        public string AvatarUrl { get; set; }
        [BsonDefaultValue("")]
        public string Summary { get; set; }
        public DateTime CreateDate { get; set; }
        public List<string> Roles { get; set; }

        public UserModel()
        {
            CreateDate = DateTime.UtcNow;
            Roles = new List<string>();
        }
    }
}
