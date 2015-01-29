using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Repository.Models
{
    class TagModel
    {
        [BsonId]
        public ObjectId _id { get; set; }
        [BsonRequired]
        public string Name { get; set; }
    }
}
