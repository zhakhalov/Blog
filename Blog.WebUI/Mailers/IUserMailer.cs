using Blog.Repository.Models;
using Mvc.Mailer;

namespace Registration.Mailers
{ 
    public interface IUserMailer
    {
        MvcMailMessage Welcome(UserModel user);
        MvcMailMessage PasswordReset(UserModel user);
	}
}