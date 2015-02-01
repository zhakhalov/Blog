using Blog.Repository.Models;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Repository.Repositories
{
    public class ArticleRepository : Repository<ArticleModel>, IArticleRepository
    {
        public ArticleRepository(string connectionString) : base(connectionString, "blog", "articles") { }

        public override void Remove(ArticleModel model)
        {
            Collection.Remove(Query<ArticleModel>.EQ(t => t._id, model._id), RemoveFlags.None);
        }
    }
}
