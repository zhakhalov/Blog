using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Dal.Repositories.Abstract
{
    interface IRepository<T>
    {
        void Add(T model);
        void Remove(T model);
        IEnumerable<T> GetAll();
    }
}
