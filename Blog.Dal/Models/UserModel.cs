using Blog.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Dal.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string AvatarUrl { get; set; }
        public IEnumerable<string> Roles { get; set; }
        public DateTime RegistrationDate { get; set; }

        public UserModel() { }

        public UserModel(User user)
        {
            Id = user.Id;
            FullName = user.FullName;
            Username = user.Username;
            Email = user.Email;
            Password = user.Password;
            AvatarUrl = user.AvatarUrl;
            RegistrationDate = user.RegistrationDate;
            Roles = user.UserRole.Select(r => r.Role.Name).ToList();
        }
    }
}
