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
    public abstract class Repository<T>
    {
        private readonly string _connectionString;
        private readonly string _database;
        private readonly string _collection;

        protected MongoCollection<T> Collection
        {
            get
            {
                return new MongoClient(_connectionString)
                    .GetServer()
                    .GetDatabase(_database)
                    .GetCollection<T>(_collection);
            }
        }

        public Repository(string connectionString, string database, string collection)
        {
            _connectionString = connectionString.Contains("mongodb://")
                ? connectionString
                : ConfigurationManager.ConnectionStrings[connectionString].ConnectionString;
            _database = database;
            _collection = collection;
        }

        public virtual List<T> Get(IMongoQuery query, int skip, int take)
        {
            return Collection
                .Find(query)
                .SetFields(Fields.Exclude("Comments"))
                .Skip(skip)
                .Take(take)
                .ToList();
        }

        public virtual List<T> GetAll()
        {
            return Collection.FindAll().ToList();
        }

        public virtual void Insert(T model)
        {
            Collection.Insert(model);
        }

        public virtual void Save(T model)
        {
            Collection.Save(model);
        }
    }
}
