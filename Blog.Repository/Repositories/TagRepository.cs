using Blog.Repository.Models;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Repository.Repositories
{
    class TagRepository : Repository<TagModel>
    {
        public TagRepository(string connectionString) : base(connectionString, "blog", "tags")
        {
            Collection.CreateIndex(new IndexKeysBuilder<TagModel>().Ascending(t => t.Name), IndexOptions.SetUnique(true));
        }
    }
}
