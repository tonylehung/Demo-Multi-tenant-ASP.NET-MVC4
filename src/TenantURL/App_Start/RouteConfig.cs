using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TenantURL
{
    public class RouteConfig
    {


        public static void RegisterRoutes(RouteCollection routes)
        {

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Home",
                "Home/{action}/{id}",
                new { controller = "home", action = "index", id = "" }
            );

            routes.MapRoute(
                "Account",
                "Account/{action}/{id}",
                new { controller = "account", action = "index", id = "" }
            );
            routes.MapRoute(
                "Tenant",
                "tenant/{action}/{id}",
                new { controller = "tenant", action = "index", id = "" }
            );

            routes.MapRoute("0", "{tenant}", new { tenant = "", controller = "home", action = "index", id = "" });
            routes.MapRoute("1", "{tenant}/", new { tenant = "", controller = "home", action = "index", id = "" });
            routes.MapRoute("3", "{tenant}/news", new { tenant = "", controller = "news", action = "index", id = "" });
            routes.MapRoute("4", "{tenant}/news/", new { tenant = "", controller = "news", action = "index", id = "" });
            routes.MapRoute("5", "{tenant}/{controller}/{id}/{action}", new { tenant = "", controller = "news", action = "index" });


        }
    }
}