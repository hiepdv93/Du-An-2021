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
            name: "Khoa hoc danh muc",
            url: "danh-muc/{id}",
            defaults: new { controller = "ProductSite", action = "Index", id = UrlParameter.Optional }
         );
            routes.MapRoute(
            name: "Khoa hoc chi tiet",
            url: "san-pham/{id}",
            defaults: new { controller = "ProductSite", action = "Detail", id = UrlParameter.Optional, active = "" }
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
            name: "linh vuc",
            url: "linh-vuc/{id}",
            defaults: new { controller = "HomeSite", action = "Department", id = UrlParameter.Optional }
        );
            routes.MapRoute(
             name: "Lien he",
             url: "lien-he",
             defaults: new { controller = "HomeSite", action = "Contact", id = UrlParameter.Optional }
         );
            routes.MapRoute(
            name: "Giang vien",
            url: "giang-vien",
            defaults: new { controller = "TeacherSite", action = "Index", id = UrlParameter.Optional }
        );
            routes.MapRoute(
         name: "Giang vien ct",
         url: "xem-giang-vien/{id}",
         defaults: new { controller = "TeacherSite", action = "Detail", id = UrlParameter.Optional }
     );
            routes.MapRoute(
            name: "Tim-kiem khoa hoc",
            url: "tim-kiem/{id}",
            defaults: new { controller = "ProductSite", action = "Search", id = UrlParameter.Optional }
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
            name: "Giohang",
            url: "Gio-hang",
            defaults: new { controller = "Carts", action = "Index", id = UrlParameter.Optional }
          );
            routes.MapRoute(
            name: "DangKy",
            url: "dang-ky",
            defaults: new { controller = "CustomerSite", action = "Register", id = UrlParameter.Optional }
            );
            routes.MapRoute(
            name: "DangNhap",
            url: "dang-nhap",
            defaults: new { controller = "CustomerSite", action = "Login", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "HomeSite", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
