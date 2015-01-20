using Blog.Dal.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Dal.Repositories.Abstract
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        public string ConnectionString { get; set; }

        public void Add(T model)
        {
            using(DbContext context = CreateDBContext())
            {
                context.Set<T>().Add(model);
                context.SaveChanges();
            }     
        }

        public void Remove(T model)
        {
            using (DbContext context = CreateDBContext())
            {
                context.Set<T>().Remove(model);
                context.SaveChanges();
            }   
        }

        public IEnumerable<T> GetAll()
        {
            List<T> models = null;
            using (DbContext context = CreateDBContext())
            {
                models = context.Set<T>().ToList();                
            }

            return models;
        }

        protected DbContext CreateDBContext()
        {
            return new DbContext(ConnectionString);
        }
    }
}
