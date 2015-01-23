using Blog.Dal.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Blog.WebUI.Code.ValidationAttributes
{
    public class UniqueUsernameAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return !new UserRepository("Blog").ContainsUsername(value.ToString());
        }
    }
}