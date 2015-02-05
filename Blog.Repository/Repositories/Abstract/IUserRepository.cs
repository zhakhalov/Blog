using Blog.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Repository.Repositories
{
    public interface IUserRepository : IRepository<UserModel>
    {
        bool ContainsUsername(string username);
        bool ContainsEmail(string email);
        UserModel GetByLogin(string login);
        void UpdateSummary(string username, string summary);
        void UpdateAvatar(string username, string avatarUrl);
    }
}
