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
                name: "Create ad",
                url: "ads/create",
                defaults: new { controller = "Ads", action = "Create" });

            routes.MapRoute(
                name: "Get questions by tag",
                url: "ads/tagged/{tag}",
                defaults: new { controller = "Ads", action = "GetByTag" });

            routes.MapRoute(
                name: "Display question",
                url: "ads/{id}",
                defaults: new { controller = "Ads", action = "Details" });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional });
        }
    }
}
