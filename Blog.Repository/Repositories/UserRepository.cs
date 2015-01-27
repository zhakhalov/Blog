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
    public class UserRepository : Repository<UserModel>
    {
        public UserRepository(string connectionString) : base(connectionString, "blog", "users") { }

        public bool ContainsUsername(string username)
        {
            return Get(Query<UserModel>.Where(u => username == u.Username), 0, 1).Count > 0;
        }

        public bool ContainsEmail(string email)
        {
            return Get(Query<UserModel>.Where(u => email == u.Email), 0, 1).Count > 0;
        }

        public override void Insert(UserModel model)
        {
            if (ContainsUsername(model.Username) || ContainsEmail(model.Email))
            {
                throw new MongoDuplicateKeyException("username or email already used", new WriteConcernResult(new BsonDocument()));
            }
            base.Insert(model);
        }
    }
}
