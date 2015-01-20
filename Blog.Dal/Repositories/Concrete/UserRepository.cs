using Blog.Dal.Models;
using Blog.Dal.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Dal.Repositories.Concrete
{
    class UserRepository : Repository<UserModel>
    {
        public UserRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
    }
}
