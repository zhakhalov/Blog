using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.WebUI.Code.Services
{
    public interface IAvatarService
    {
        string ResolvePath(string filename);
        string ResolveUrl(string filename);
    }
}
