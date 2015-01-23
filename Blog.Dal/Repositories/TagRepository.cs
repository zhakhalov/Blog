using Blog.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Dal.Repositories
{
    public class TagRepository : Repository
    {
        public TagRepository(string connectionString) : base(connectionString) { }

        public List<string> GetAll()
        {
            List<string> tags = null;

            using(var context = CreateContext())
            {
                tags = context.Set<Tag>().Select(t => t.Name).ToList();
            }

            return tags;
        }

        public int AddTag(string name)
        {
            var tag = new Tag
            {
                Name = name
            };
            using(var context = CreateContext())
            {
                context.Set<Tag>().Add(tag);
                context.SaveChanges();
            }

            return tag.Id;
        }

        public void RemoveTag(string name)
        {
            using (var context = CreateContext())
            {
                context.Set<Tag>().Remove(context.Set<Tag>().Where(t => name == t.Name).First());
                context.SaveChanges();
            }
        }
    }
}
