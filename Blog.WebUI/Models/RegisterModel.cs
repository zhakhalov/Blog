using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Blog.WebUI.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage="Username required")]
        [RegularExpression(@"/[A-Za-z0-9]{3,10}/", ErrorMessage="Allowed only letters and numbers")]        
        public string Username { get; set; }
        [Required(ErrorMessage = "E-mail required")]
        [RegularExpression(@"/^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4})(\]?)$/", ErrorMessage = "Allowed only letters and numbers")]        
        public string Email { get; set; }
        [Required(ErrorMessage = "Password required")]
        [RegularExpression(@"/^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,16}$/", ErrorMessage = "Allowed only letters and numbers")]        
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "Password doesn't match confirmation")]
        public string ConfirmPassword { get; set; }
    }
}