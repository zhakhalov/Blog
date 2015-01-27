using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Repository.Models
{
    class TagModel
    {
        public ObjectId _id { get; set; }
        public string Name { get; set; }
    }
}
