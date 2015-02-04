using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Blog.WebUI.Code.Services
{
    public class AvatarService : IAvatarService
    {
        private readonly string _path;

        public AvatarService(string path)
        {
            _path = path;
        }

        public string ResolvePath(string filename)
        {
            return Path.Combine(HttpContext.Current.Server.MapPath(_path), filename);
        }

        public string ResolveUrl(string filename)
        {
            return Path.Combine(_path, filename).Replace("\\", "/");
        }
    }
}