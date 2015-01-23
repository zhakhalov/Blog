using Blog.Dal.Models;
using Blog.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Dal.Repositories
{
    public class ArticleRepository : Repository
    {
        public ArticleRepository(string connectionString) : base(connectionString) { }

        public ArticleModel GetById(int id)
        {
            ArticleModel articleModel = null;

            using (var context = CreateContext())
            {
                Article article = context.Set<Article>().Where(a => id == a.Id).FirstOrDefault();
            }

            return articleModel;
        }

        public List<ArticleModel> GetNewest(int limit)
        {
            List<ArticleModel> articleModels = null;

            using (var context = CreateContext())
            {
                context.Set<Article>()
                    .Include(a => a.Post);
            }
            return articleModels;
        }

        public List<ArticleModel> GetByTag(string tagName)
        {
            return GetByTag(tagName, 0, int.MaxValue);
        }

        public List<ArticleModel> GetByTag(string tagName, int skip, int limit)
        {
            List<ArticleModel> articleModels = null;

            using (var context = CreateContext())
            {
                List<ArticleTag> articleTag = context.Set<ArticleTag>()
                    .Include(a => a.Tag)
                    .Include(a => a.Article)
                    .Include(a => a.Article.Post)
                    .Where(a => tagName == a.Tag.Name)
                    .OrderBy(a => a.Article.Post.Timestamp)
                    .Skip(skip)
                    .Take(limit)
                    .ToList();
                if (null != articleTag)
                {
                    articleModels = articleTag.Select(a => new ArticleModel(a.Article)).ToList();
                }
            }

            return articleModels;
        }

        public List<ArticleModel> GetByUsername(string username)
        {
            List<ArticleModel> articleModels = new List<ArticleModel>();

            using (var context = CreateContext())
            {
                articleModels = context.Set<Article>()
                    .Include(a => a.Post)
                    .Include(a => a.Post.User)
                    .Include(a => a.Comment)
                    .Include(a => a.Comment.Select(c => c.Post))
                    .Include(a => a.Comment.Select(c => c.Post.User))
                    .Where(a => username == a.Post.User.Username)
                    .OrderBy(a => a.Post.Timestamp)
                    .Select(a => new ArticleModel(a))
                    .ToList();
            }

            return articleModels;
        }

        public int AddTag(ArticleModel article, string tagName)
        {
            return AddTags(article, new List<string>(){tagName}).First();
        }

        public List<int> AddTags(ArticleModel article, List<string> tagNames)
        {
            List<int> ids = null;
            article.Tags.AddRange(tagNames);
            article.Tags.Distinct();

            using (var context = CreateContext())
            {
                List<Tag> tags = context.Set<Tag>().Where(t => tagNames.Contains(t.Name)).ToList();
                List<ArticleTag> articleTags = tags.Select(t => new ArticleTag
                {
                    ArticleId = article.Id,
                    TagId = t.Id
                }).ToList();
                context.Set<ArticleTag>().AddRange(articleTags);
                context.SaveChanges();
                ids = articleTags.Select(a => a.Id).ToList();
            }

            return ids;
        }

        public int AddArticle(ArticleModel articleModel)
        {
            using (var context = CreateContext())
            {
                Post post = new Post
                {
                    UserId = articleModel.Author.Id,
                    Content = articleModel.Content,
                    Timestamp = DateTime.UtcNow
                };
                context.Set<Post>().Add(post);
                context.SaveChanges();

                Article article = new Article
                {
                    Title = articleModel.Title,
                    PostId = post.Id
                };
                context.Set<Article>().Add(article);
                context.SaveChanges();
                articleModel.Id = article.Id;
                List<Tag> tags = context.Set<Tag>()
                    .Where(t => articleModel.Tags.Contains(t.Name))
                    .ToList();
                List<ArticleTag> articleTags = tags.Select(t => new ArticleTag
                    {
                        TagId = t.Id,
                        ArticleId = article.Id
                    }).ToList();
                context.Set<ArticleTag>().AddRange(articleTags);
                context.SaveChanges();
            }

            return articleModel.Id;
        }
    }
}
