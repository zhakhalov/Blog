using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.WebUI.Code.Services
{
    public interface ITransliterationService
    {
        string ToFriendlyUrl(string text);
    }
}