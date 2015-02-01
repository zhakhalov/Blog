using Blog.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Repository.Repositories
{
    public interface IArticleRepository : IRepository<ArticleModel>
    {
    }
}
