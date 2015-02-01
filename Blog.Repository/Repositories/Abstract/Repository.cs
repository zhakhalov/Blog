using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Repository.Repositories
{
    public abstract class Repository<T> : IRepository<T>
    {
        private readonly string _connectionString;
        private readonly string _database;

        protected MongoCollection<T> Collection { get; private set; }

        public Repository(string connectionString, string database, string collection)
        {
            _connectionString = connectionString.StartsWith("mongodb://")
                ? connectionString
                : ConfigurationManager.ConnectionStrings[connectionString].ConnectionString;
            _database = database;

            Collection = new MongoClient(_connectionString)
                    .GetServer()
                    .GetDatabase(_database)
                    .GetCollection<T>(collection);
        }

        public virtual List<T> Get(IMongoQuery query, int skip, int take)
        {
            return Collection
                .Find(query)
                .Skip(skip)
                .Take(take)
                .ToList();
        }

        public virtual T GetOne(IMongoQuery query)
        {
            return Collection.FindOne(query);
        }

        public virtual long Count(IMongoQuery query)
        {
            return Collection
                .Find(query)
                .Count();
        }

        public virtual List<T> GetAll()
        {
            return Collection.FindAll().ToList();
        }

        public virtual void Save(T model)
        {
            Collection.Save(model);
        }

        public virtual void InsertRange(IEnumerable<T> models)
        {
            Collection.InsertBatch<T>(models);
        }

        public virtual void RemoveAll()
        {
            Collection.RemoveAll();
        }

        public abstract void Remove(T model);
    }
}
