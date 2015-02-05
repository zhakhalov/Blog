using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Blog.WebUI.Code.Services
{
    public class UserConfigService : IUserConfigService
    {
        private readonly string _path;
        public int SummaryLimit { get; private set; }
        public string DefaultAvatar { get; private set; }

        public UserConfigService(string path, string defaultAvatar, int summaryLimit)
        {
            _path = path;
            DefaultAvatar = defaultAvatar;
            SummaryLimit = summaryLimit;
        }

        public string ResolveAvatarPath(string filename)
        {
            return Path.Combine(HttpContext.Current.Server.MapPath(_path), filename);
        }

        public string ResolveAvatarUrl(string filename)
        {
            return Path.Combine(_path, filename).Replace("\\", "/");
        }
    }
}