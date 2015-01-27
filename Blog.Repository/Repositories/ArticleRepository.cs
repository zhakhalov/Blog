using Blog.Repository.Models;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Repository.Repositories
{
    public class ArticleRepository : Repository<ArticleModel>
    {
        public ArticleRepository(string connectionString) : base(connectionString, "blog", "articles") { }

        public List<ArticleModel> GetByUser(string username, int skip, int take)
        {
            return Get(Query<UserModel>.Where(u => username == u.Username), skip, take);
        }

        public List<ArticleModel> GetByTag(string tag, int skip, int take)
        {
            return Get(Query<List<string>>.Where(t => t.Contains(tag)), skip, take);
        }

        public List<ArticleModel> GetNewest(int take)
        {
            return Collection
                .FindAll()
                .OrderBy(a => a.CreateDate)
                .Take(take)
                .ToList();
        }

        public List<ArticleModel> GetTopRated(int take)
        {
            return Collection
                .FindAll()
                .OrderBy(a => a.Rating)
                .Take(take)
                .ToList();
        }
    }
}
