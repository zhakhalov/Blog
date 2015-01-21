using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Dal.Repositories
{
    abstract class Repository
    {
        private readonly string _connectionString;
        protected Repository(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected DbContext CreateContext()
        {
            return new DbContext(_connectionString);
        }
    }
}
