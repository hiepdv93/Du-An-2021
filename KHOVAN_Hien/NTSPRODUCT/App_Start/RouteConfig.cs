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
            name: "Menu noi dung",
            url: "sites/{id}",
            defaults: new { controller = "NewSite", action = "SiteContent", id = UrlParameter.Optional }
        );
            routes.MapRoute(
            name: "san-pham",
            url: "san-pham/{id}",
            defaults: new { controller = "ProductSite", action = "Detail", id = UrlParameter.Optional, type = ClassExten.typePro }
         );
            routes.MapRoute(
            name: "thiet-bi",
            url: "thiet-bi/{id}",
            defaults: new { controller = "ProductSite", action = "Detail", id = UrlParameter.Optional, type = ClassExten.typeDevice }
         );
            routes.MapRoute(
            name: "danh muc tb",
            url: "danh-muc/{id}",
            defaults: new { controller = "ProductSite", action = "Device", id = UrlParameter.Optional }
         );
            routes.MapRoute(
            name: "danh muc moduler",
            url: "moduler/{id}",
            defaults: new { controller = "ProductSite", action = "Index", id = UrlParameter.Optional, active = "" }
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
            name: "giai-phap",
            url: "giai-phap/{id}",
            defaults: new { controller = "HomeSite", action = "Solution", id = UrlParameter.Optional }
        );
            routes.MapRoute(
             name: "Lien he",
             url: "lien-he",
             defaults: new { controller = "HomeSite", action = "Contact", id = UrlParameter.Optional }
         );
            routes.MapRoute(
            name: "ung dung",
            url: "ung-dung",
            defaults: new { controller = "HomeSite", action = "App", id = UrlParameter.Optional }
        );

            routes.MapRoute(
             name: "the tags",
             url: "tags/{id}",
             defaults: new { controller = "ProductSite", action = "Tags", id = UrlParameter.Optional }
             );

            routes.MapRoute(
            name: "Tim-kiem tin",
            url: "tim-kiem/{id}",
            defaults: new { controller = "ProductSite", action = "Search", id = UrlParameter.Optional }
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
