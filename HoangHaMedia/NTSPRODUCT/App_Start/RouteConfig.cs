using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace NTSPRODUCT
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            //cấu hình router
            routes.MapRoute(
            name: "Trangchu",
            url: "",
            defaults: new { controller = "HomeSite", action = "Index", id = UrlParameter.Optional }
        );
            routes.MapRoute(
            name: "Intro",
            url: "intro",
            defaults: new { controller = "NewSite", action = "Intro", id = UrlParameter.Optional }
        );
            routes.MapRoute(
            name: "Menu noi dung",
            url: "sites/{id}",
            defaults: new { controller = "NewSite", action = "SiteContent", id = UrlParameter.Optional }
        );
            routes.MapRoute(
            name: "Doi tac",
            url: "doi-tac",
            defaults: new { controller = "HomeSite", action = "Partner", id = UrlParameter.Optional }
         );
            routes.MapRoute(
            name: "Tin tuc",
            url: "tin-tuc/{id}",
            defaults: new { controller = "NewSite", action = "Index", id = UrlParameter.Optional }
         );
            routes.MapRoute(
            name: "Tin tuc chi tiet",
            url: "tin/{id}",
            defaults: new { controller = "NewSite", action = "Detail", id = UrlParameter.Optional }
         );

            routes.MapRoute(
            name: "Lien he",
            url: "lien-he",
            defaults: new { controller = "HomeSite", action = "Contact", id = UrlParameter.Optional }
        );
            routes.MapRoute(
            name: "su-kien",
            url: "su-kien/{id}",
            defaults: new { controller = "ProductSite", action = "Sukien", id = UrlParameter.Optional }
        );
            routes.MapRoute(
            name: "hoi truong",
            url: "hoi-truong/{id}",
            defaults: new { controller = "ProductSite", action = "Index", id = UrlParameter.Optional }
        );
            routes.MapRoute(
            name: "Tim-kiem tin",
            url: "tim-kiem/{id}",
            defaults: new { controller = "ProductSite", action = "Search", id = UrlParameter.Optional }
        );
            routes.MapRoute(
            name: "Tag",
            url: "tag/{id}",
            defaults: new { controller = "ProductSite", action = "Tags", id = UrlParameter.Optional }
        );
            routes.MapRoute(
            name: "Site map",
            url: "sitemap",
            defaults: new { controller = "HomeSite", action = "SiteMap", id = UrlParameter.Optional }
        );
            routes.MapRoute(
            name: "Default",
            url: "{controller}/{action}/{id}",
            defaults: new { controller = "HomeSite", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
