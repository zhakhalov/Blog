using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Repository.Repositories
{
    public interface IRepository<T>
    {
        List<T> Get(IMongoQuery query, int skip, int take);
        T GetById(ObjectId id);
        T GetOne(IMongoQuery query);
        long Count(IMongoQuery query);
        List<T> GetAll();
        void InsertRange(IEnumerable<T> models);
        void Save(T model);
        void RemoveAll();
        void Remove(T model);
    }
}
