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
        int ItemsPerPage { get; }                   // angular-pagination property
        int MaxNumbers { get; }                     // angular-pagination property
        string ShortifyContent(string content);
        
    }
}
