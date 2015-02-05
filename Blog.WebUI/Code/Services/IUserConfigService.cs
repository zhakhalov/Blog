using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.WebUI.Code.Services
{
    public interface IUserConfigService
    {
        int SummaryLimit { get; }
        string DefaultAvatar { get; }
        string ResolveAvatarPath(string filename);
        string ResolveAvatarUrl(string filename);
    }
}
