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
                name: "Article",
                url: "article/{id}",
                defaults: new { controller = "Article", action = "Article" }
            );
            routes.MapRoute(
                name: "Tag",
                url: "tag/{tag}",
                defaults: new { controller = "Article", action = "Tag" }
            );
            routes.MapRoute(
                name: "User",
                url: "user/{username}",
                defaults: new { controller = "User", action = "Public" }
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Article", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}