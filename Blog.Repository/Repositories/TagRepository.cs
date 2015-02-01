using Blog.Repository.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Repository.Repositories
{
    public class TagRepository : Repository<TagModel>, ITagRepository
    {
        public TagRepository(string connectionString) : base(connectionString, "blog", "tags")
        {
            Collection.CreateIndex(new IndexKeysBuilder<TagModel>().Ascending(t => t.Name), IndexOptions.SetUnique(true));
        }

        override public void Remove(TagModel tag)
        {
            Collection.Remove(Query<TagModel>.EQ(t => t.Name, tag.Name), RemoveFlags.None);
        }
    }
}
