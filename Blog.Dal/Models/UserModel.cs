﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Dal.Models
{
    public class UserModel
    {
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string AvatarUrl { get; set; }
        public string[] Roles { get; set; }
    }
}
