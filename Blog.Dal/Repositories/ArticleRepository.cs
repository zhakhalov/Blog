using Blog.Dal.Models;
using Blog.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Dal.Repositories.Concrete
{
    class ArticleRepository : Repository
    {
        public ArticleRepository(string connectionString) : base(connectionString) { }

        public ArticleModel GetById(int id)
        {
            ArticleModel articleModel = null;

            using(var context = CreateContext())
            {
                Article article = context.Set<Article>().Where(a => id == a.Id).FirstOrDefault();
            }

            return articleModel;
        }
    }
}
