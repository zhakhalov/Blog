using Blog.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Dal.Repositories.Concrete
{
    class TagRepository : Repository
    {
        public TagRepository(string connectionString) : base(connectionString) { }

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
