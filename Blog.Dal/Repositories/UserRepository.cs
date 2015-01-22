using Blog.Dal.Models;
using Blog.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Dal.Repositories.Concrete
{
    public class UserRepository : Repository
    {
        public UserRepository(string connectionString) : base(connectionString) { }
        public int Create(UserModel userModel)
        {
            userModel.RegistrationDate = DateTime.UtcNow;
            User user = new User
            {
                Username = userModel.Username,
                Password = userModel.Password,
                Email = userModel.Email,
                FullName = userModel.FullName,
                AvatarUrl = userModel.AvatarUrl,
            };

            using (var context = CreateContext())
            {
                context.Set<User>().Add(user);
                context.SaveChanges();

                List<Role> roles = context.Set<Role>().Where(r => userModel.Roles.Contains(r.Name)).ToList();
                foreach (var r in roles)
                {
                    context.Set<UserRole>().Add(new UserRole
                    {
                        RoleId = r.Id,
                        UserId = user.Id
                    });
                }
                context.SaveChanges();
            }

            return user.Id;
        }

        public UserModel GetById(int id)
        {
            UserModel userModel = null;
            using (var context = CreateContext())
            {
                User user = context.Set<User>()
                    .Include(u => u.UserRole)
                    .Include(u => u.UserRole.Select(r => r.Role))
                    .Where(r => id == r.Id)
                    .FirstOrDefault();
                if (null != user)
                {
                    userModel = new UserModel(user);
                }
            }
            return userModel;
        }

        public bool ContainsUsername(string username)
        {
            bool containts = false;

            using (var context = CreateContext())
            {
                containts = null != context.Set<User>().Where(r => username == r.Username).FirstOrDefault();
            }

            return containts;
        }

        public bool ContainsEmail(string email)
        {
            bool containts = false;

            using (var context = CreateContext())
            {
                containts = null != context.Set<User>().Where(r => email == r.Email).FirstOrDefault();
            }

            return containts;
        }

        public UserModel GetByLogin(string login)
        {
            UserModel userModel = null;
            using (var context = CreateContext())
            {
                User user = context.Set<User>()
                    .Include(u => u.UserRole)
                    .Include(u => u.UserRole.Select(r => r.Role))
                    .Where(r => ((login == r.Username) || (login == r.Email)))
                    .FirstOrDefault();
                if (null != user)
                {
                    userModel = new UserModel(user);
                }
            }
            return userModel;
        }

        public List<UserModel> Search(string fragment)
        {
            List<UserModel> userModels = new List<UserModel>();
            using (var context = CreateContext())
            {
                List<User> users = context.Set<User>()
                    .Include(u => u.UserRole)
                    .Include(u => u.UserRole.Select(r => r.Role))
                    .Where(r =>
                        (r.Username.ToLower().Contains(fragment.ToLower())
                        || r.FullName.ToLower().Contains(fragment.ToLower())
                        || r.Email.ToLower().Contains(fragment.ToLower())))
                    .ToList();
                foreach (var u in users)
                {
                    userModels.Add(new UserModel(u));
                }
            }
            return userModels;
        }
    }
}
