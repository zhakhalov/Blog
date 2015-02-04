using Blog.Repository.Models;
using Mvc.Mailer;
using System.Net.Mail;
using System.Text;

namespace Registration.Mailers
{ 
    public class UserMailer : MailerBase, IUserMailer 	
	{
		public UserMailer(UserModel user)
		{
			MasterName="_Layout";
		}

        public MvcMailMessage Welcome(UserModel user)
        {
            ViewBag.User = user;
            var mailMessage = Populate(x =>
            {
                x.Subject = "Welcome!";
                x.ViewName = "Welcome";
                x.To.Add(user.Email);
            });

            return mailMessage;
        }

        public MvcMailMessage PasswordReset(UserModel user)
        {
            ViewBag.User = user;
            var mailMessage = Populate(x =>
            {
                x.Subject = "New password";
                x.ViewName = "PasswordReset";
                x.To.Add(user.Email);
            });

            return mailMessage;
        }
    }
}