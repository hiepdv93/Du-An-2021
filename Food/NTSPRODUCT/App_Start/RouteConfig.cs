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
            name: "Gio hang",
            url: "gio-hang",
            defaults: new { controller = "Carts", action = "index", id = UrlParameter.Optional }
        );

            routes.MapRoute(
            name: "Lien he",
            url: "lien-he",
            defaults: new { controller = "HomeSite", action = "Contact", id = UrlParameter.Optional }
        );
            routes.MapRoute(
            name: "san pham",
            url: "san-pham/{id}",
            defaults: new { controller = "ProductSite", action = "Detail", id = UrlParameter.Optional }
        );
            routes.MapRoute(
            name: "Danh muc",
            url: "danh-muc/{id}",
            defaults: new { controller = "ProductSite", action = "Index", id = UrlParameter.Optional }
        );
            routes.MapRoute(
            name: "KM",
            url: "khuyen-mai",
            defaults: new { controller = "ProductSite", action = "Index", id = "km" }
        );
            routes.MapRoute(
            name: "hot",
            url: "top-ban-chay",
            defaults: new { controller = "ProductSite", action = "Index", id = "bc" }
        );
            routes.MapRoute(
            name: "Tim-kiem tin",
            url: "tim-kiem/{id}",
            defaults: new { controller = "ProductSite", action = "Search", id = UrlParameter.Optional }
        );
            routes.MapRoute(
            name: "Thuong-hieu",
            url: "thuong-hieu/{id}",
            defaults: new { controller = "ProductSite", action = "ThuongHieu", id = UrlParameter.Optional }
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
