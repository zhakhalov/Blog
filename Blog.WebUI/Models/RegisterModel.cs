using Blog.WebUI.Code.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Blog.WebUI.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Username required")]
        [RegularExpression(@"[A-Za-z0-9]{3,10}", ErrorMessage = "Allowed only letters and numbers")]
        [UniqueUsername(ErrorMessage = "Username already used")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Username required")]
        [RegularExpression(@"[A-Za-z ]{3,64}", ErrorMessage = "Allowed only upper and lower cases letters and spaces")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "E-mail required")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4})(\]?)$", ErrorMessage = "E-mail is bad formated")]
        [UniqueEmail(ErrorMessage = "E-mail already used")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password required")]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,20}$", ErrorMessage = "Password must contain from 8 to 20 characters, uppercase letter, lowercase letter and number")]
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "Password doesn't match confirmation")]
        public string ConfirmPassword { get; set; }
    }
}