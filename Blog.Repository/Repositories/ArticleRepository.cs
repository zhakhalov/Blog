using Blog.Repository.Models;
using MongoDB.Bson;
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

        public List<ArticleModel> GetByUser(string username, int skip, int limit)
        {
            return Get(Query<UserModel>.Where(u => username == u.Username), skip, limit);
        }

        public ArticleModel GetById(ObjectId id)
        {
            return Collection.FindOneById(id);
        }

        public List<ArticleModel> GetByTag(string tag, int skip, int take)
        {
            return Collection
                .Find(Query<ArticleModel>.EQ(a => a.Tags, tag))
                .SetFields(Fields.Exclude("Comments"))
                .OrderBy(a => a.CreateDate)
                .Skip(skip)
                .Take(take)
                .ToList();
        }

        public List<ArticleModel> GetNewest(int skip, int take)
        {
            return Collection
                .FindAll()
                .SetFields(Fields.Exclude("Comments"))
                .OrderBy(a => a.CreateDate)
                .Skip(skip)
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

        public List<ArticleModel> GetMostViewed(int take)
        {
            return Collection
                .FindAll()
                .OrderBy(a => a.Viewed)
                .Take(take)
                .ToList();
        }

        public void AddComment(CommentModel comment, string articleId)
        {
            ObjectId id = new ObjectId(articleId);
            Collection.Update(Query<ArticleModel>.EQ(a => a._id, id), Update<ArticleModel>.AddToSet(a => a.Comments, comment));
        }

        public void IncreaseViewed(int count, string articleId)
        {
            ObjectId id = new ObjectId(articleId);
            Collection.Update(Query<ArticleModel>.EQ(a => a._id, id), Update<ArticleModel>.Inc(a => a.Viewed, count));
        }
    }
}
