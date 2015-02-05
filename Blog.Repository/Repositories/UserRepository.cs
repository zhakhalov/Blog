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
    public class UserRepository : Repository<UserModel>, IUserRepository
    {
        public UserRepository(string connectionString)
            : base(connectionString, "blog", "users")
        {
            Collection.CreateIndex(new IndexKeysBuilder<UserModel>().Ascending(u => u.Username), IndexOptions.SetUnique(true));
            Collection.CreateIndex(new IndexKeysBuilder<UserModel>().Ascending(u => u.Email), IndexOptions.SetUnique(true));
        }

        public bool ContainsUsername(string username)
        {
            return Get(Query<UserModel>.Where(u => u.Username == username), 0, 1).Count > 0;
        }

        public bool ContainsEmail(string email)
        {
            return Get(Query<UserModel>.Where(u => u.Email == email), 0, 1).Count > 0;
        }

        public UserModel GetByLogin(string login)
        {
            return Get(Query.Or(Query<UserModel>.EQ(u => u.Username, login), Query<UserModel>.EQ(u => u.Email, login)), 0, 1).FirstOrDefault();
        }

        public override void Remove(UserModel model)
        {
            Collection.Remove(Query<UserModel>.EQ(t => t._id, model._id), RemoveFlags.None);
        }

        public void UpdateSummary(string username, string summary)
        {
            Collection.Update(
                Query<UserModel>.EQ(u => u.Username, username),
                Update<UserModel>.Set(u => u.Summary, summary));
        }

        public void UpdateAvatar(string username, string avatarUrl)
        {
            Collection.Update(
                Query<UserModel>.EQ(u => u.Username, username),
                Update<UserModel>.Set(u => u.AvatarUrl, avatarUrl));
        }
    }
}
