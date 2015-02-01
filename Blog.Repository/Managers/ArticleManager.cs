using Blog.Repository.Models;
using Blog.Repository.Repositories;
using MongoDB.Bson;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Repository.Managers
{
    public class ArticleManager : ArticleRepository, IArticleManager
    {
        public ArticleManager(string connectionString) : base(connectionString) { }

        public List<ArticleModel> GetByUser(string username, int skip, int limit)
        {
            return Get(Query<UserModel>.Where(u => username == u.Username), skip, limit);
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
            comment._id = ObjectId.GenerateNewId();
            ObjectId id = new ObjectId(articleId);
            Collection.Update(
                Query<ArticleModel>.EQ(a => a._id, id),
                Update<ArticleModel>.AddToSet(a => a.Comments, comment));
        }

        public void IncreaseViewed(int count, string articleId)
        {
            ObjectId id = new ObjectId(articleId);
            Collection.Update(
                Query<ArticleModel>.EQ(a => a._id, id),
                Update<ArticleModel>.Inc(a => a.Viewed, count));
        }

        public void RateArticle(RateModel rate, string articleId)
        {
            ObjectId id = new ObjectId(articleId);
            Collection.Update(
                Query<ArticleModel>.EQ(a => a._id, id),
                Update.Combine(
                    Update<ArticleModel>.Inc(a => a.Rating, (rate.Like) ? 1 : -1),
                    Update<ArticleModel>.AddToSet(a => a.Raters, rate)));

        }

        public void RateComment(RateModel rate, string commentId)
        {
            ObjectId id = new ObjectId(commentId);
            Collection.Update(
                Query.ElemMatch("Comments", Query<CommentModel>.EQ(c => c._id, id)),
                Update.PushWrapped("Comments.$.Raters", rate));
        }


        public void AddSubComment(CommentModel comment,  string commentId)
        {
            comment._id = ObjectId.GenerateNewId();
            ObjectId id = new ObjectId(commentId);
            Collection.Update(
                Query.ElemMatch("Comments", Query<CommentModel>.EQ(c => c._id, id)),
                Update.PushWrapped("Comments.$.Raters", comment));
        }
    }
}
