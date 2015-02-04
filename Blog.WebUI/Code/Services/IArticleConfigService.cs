using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.WebUI.Code.Services
{
    public interface IArticleConfigService
    {
        int TitleLimit { get; }
        int CommentLimit { get; }
        int ShortContentLimit { get; }
        int AsideLimit { get; }
        int PageLimit { get; }
        string ShortifyContent(string content);
        
    }
}
