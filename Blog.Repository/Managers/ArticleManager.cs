using Blog.Repository.Models;
using Blog.Repository.Repositories;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
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

        public ArticleModel GetByUrl(string url)
        {
            return GetOne(Query<ArticleModel>.EQ(a => a.Url, url));
        }

        public List<ArticleModel> GetByUser(string username, int skip, int take)
        {
            return Collection
                .Find(Query<ArticleModel>.EQ(a => a.Username, username))
                .SetFields(Fields.Exclude("Comments"))
                .OrderByDescending(a => a.CreateDate)
                .Skip(skip)
                .Take(take)
                .ToList();
        }        

        public List<ArticleModel> GetByTag(string tag, int skip, int take)
        {
            return Collection
                .Find(Query<ArticleModel>.EQ(a => a.Tags, tag))
                .SetFields(Fields.Exclude("Comments"))
                .OrderByDescending(a => a.CreateDate)
                .Skip(skip)
                .Take(take)
                .ToList();
        }        

        public List<ArticleModel> GetNewest(int skip, int take)
        {
            return Collection
                .FindAll()
                .SetFields(Fields.Exclude("Comments"))
                .OrderByDescending(a => a.CreateDate)
                .Skip(skip)
                .Take(take)
                .ToList();
        }        

        public List<ArticleModel> GetTopRated(int skip, int take)
        {
            return Collection
                .FindAll()
                .OrderByDescending(a => a.Rating)
                .Skip(skip)
                .Take(take)
                .ToList();
        }

        public List<ArticleModel> GetMostViewed(int skip, int take)
        {
            return Collection
                .FindAll()
                .OrderByDescending(a => a.Viewed)
                .Skip(skip)
                .Take(take)
                .ToList();
        }

        public List<ArticleModel> Search(string search)
        {
            CommandResult result = Collection.Database.RunCommand(new CommandDocument
            {
                { "text", Collection.Name },
                { "search", search }
            });
            return result.Response["results"].AsBsonArray
                .Select(row => row.AsBsonDocument)
                .Select(item => item.AsBsonDocument)
                .OrderByDescending(r => r["score"])
                .Select(doc => BsonSerializer.Deserialize<ArticleModel>(doc["obj"].AsBsonDocument))
                .ToList();
        }

        public long CountByUser(string username)
        {
            return Collection.Count(Query<ArticleModel>.EQ(a => a.Username, username));
        }

        public long CountByTag(string tag)
        {
            return Collection.Count(Query<ArticleModel>.EQ(a => a.Tags, tag));
        }

        public long Count()
        {
            return Collection.Count();
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

        public bool ExistsTitleOrUrl(string title, string url)
        {
            return Collection
                .Count(Query.Or(
                    Query<ArticleModel>.EQ(a => a.Title, title),
                    Query<ArticleModel>.EQ(a => a.Url, url))) > 0;
        }
    }
}
