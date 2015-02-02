using Blog.Repository;
using Blog.Repository.Repositories;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.WebUI.Code.ValidationAttributes
{
    public class UniqueUsernameAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {            
            return !DependencyResolver.Current.GetService<IUserRepository>().ContainsUsername(value.ToString());
        }
    }
}