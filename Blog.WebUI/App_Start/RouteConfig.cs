using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Blog.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Read",
                url: "read/{url}",
                defaults: new { controller = "Article", action = "Read" }
            );
            routes.MapRoute(
                name: "All",
                url: "all/{page}",
                defaults: new { controller = "Article", action = "All", page = 1 }
            );
            routes.MapRoute(
                name: "Search",
                url: "search",
                defaults: new { controller = "Article", action = "Search" }
            );
            routes.MapRoute(
                name: "Tags",
                url: "tag/{tag}/{page}",
                defaults: new { controller = "Article", action = "Tag", page = 1 }
            );
            routes.MapRoute(
                name: "Profile",
                url: "profile/{username}/{page}",
                defaults: new { controller = "User", action = "Public", page = 1 }
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{info}",
                defaults: new { controller = "Article", action = "Index", info = UrlParameter.Optional }
            );
        }
    }
}