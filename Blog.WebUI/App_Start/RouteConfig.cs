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
                name: "Search",
                url: "search",
                defaults: new { controller = "Article", action = "Search" }
            );
            routes.MapRoute(
                name: "Tags",
                url: "tag/{tag}",
                defaults: new { controller = "Article", action = "Tag" }
            );
            routes.MapRoute(
                name: "Profile",
                url: "profile/{username}",
                defaults: new { controller = "User", action = "Public" }
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{info}",
                defaults: new { controller = "Article", action = "Index", info = UrlParameter.Optional }
            );
        }
    }
}