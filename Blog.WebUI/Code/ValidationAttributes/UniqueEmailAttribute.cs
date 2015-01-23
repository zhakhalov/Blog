using Blog.Dal.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Blog.WebUI.Code.ValidationAttributes
{
    public class UniqueEmailAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return !new UserRepository("Blog").ContainsEmail(value.ToString());
        }
    }
}