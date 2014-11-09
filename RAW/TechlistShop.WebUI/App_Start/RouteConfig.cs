using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TechlistShop.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                null,
                "Admin",
                new { Controller = "Admin", action = "Index" }
            );

            routes.MapRoute(
                null,
                "نتایج-{pageNumber}",
                new { Controller = "Product", action = "Search", category = (string)null },
                new { pageNumber = @"\d+" }
            );

            routes.MapRoute(
                null,
                "",
                new
                {
                    Controller = "Product",
                    action = "List",
                    category = (string)null,
                    pageNumber = 1
                }
            );

            routes.MapRoute(
                null,
                "صفحه-{pageNumber}",
                new { Controller = "Product", action = "List", category = (string)null },
                new { pageNumber = @"\d+" }
            );

            routes.MapRoute(
                null,
                "{category}",
                new { Controller = "Product", action = "List", pageNumber = 1 }
            );

            routes.MapRoute(
                null,
                "{category}/صفحه-{pageNumber}",
                new { Controller = "Product", action = "List" },
                new { pageNumber = @"\d+" }
            );

            routes.MapRoute(
                null,
                "{controller}/{action}"
            );
        }
    }
}