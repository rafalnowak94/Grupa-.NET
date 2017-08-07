using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ShopASP
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
              name: "ItemDescription",
              url: "produkt/i-{id}",
              defaults: new { controller = "Shop", action = "Description" }
          );


            routes.MapRoute(
              name: "ItemsList",
              url: "kategoria/{categoryname}",
              defaults: new { controller = "Shop", action = "List" }
          );

            routes.MapRoute(
            name: "StaticPages",
            url: "firma/{page}",
            defaults: new { controller = "Home", action = "StaticPage" }
          );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
          );
        }
    }
}
