namespace AdList.Web
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;

    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Get ads by user",
                url: "user/{id}",
                defaults: new { controller = "User", action = "GetById" });

            routes.MapRoute(
                name: "Get ads by category",
                url: "ads/tagged/{tag}",
                defaults: new { controller = "Ads", action = "GetByTag" });

            routes.MapRoute(
                name: "Show ad details",
                url: "ads/details/{id}",
                defaults: new { controller = "Ads", action = "Details" });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional });
        }
    }
}
