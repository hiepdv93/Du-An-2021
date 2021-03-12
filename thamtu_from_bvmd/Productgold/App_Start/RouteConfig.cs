using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Productgold
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            //viet moi rewrite 
            routes.MapRoute(
         name: "Trangchu",
         url: "Trang-chu",
         defaults: new { controller = "BVMDHome", action = "Index", id = UrlParameter.Optional }
         );
            routes.MapRoute(
       name: "Trangchu2",
         url: "",
         defaults: new { controller = "BVMDHome", action = "Index", id = UrlParameter.Optional }
         );
            routes.MapRoute(
 name: "Menu noi dung",
   url: "Sites/{id}",
   defaults: new { controller = "TPANews", action = "Menunoidung", id = UrlParameter.Optional }
   );
            routes.MapRoute(
name: "Hoi dap",
url: "Hoi-dap",
defaults: new { controller = "BVMDHome", action = "ListQuestion", id = UrlParameter.Optional }
);
            routes.MapRoute(
        name: "Tra loi",
url: "Tra-loi/hoi-dap-{id}",
defaults: new { controller = "BVMDHome", action = "DetailQuestion", id = UrlParameter.Optional }
);
            routes.MapRoute(
          name: "Sanpham",
          url: "San-pham/{id}",
          defaults: new { controller = "TPAProduct", action = "Index", id = UrlParameter.Optional }
          );
            routes.MapRoute(
    name: "chi tiet",
    url: "Chi-tiet/{id}",
    defaults: new { controller = "TPAProduct", action = "Chitiet", id = UrlParameter.Optional }
    );
            routes.MapRoute(
name: "tin tuc",
url: "Tin-tuc/{id}",
defaults: new { controller = "BVMDHome", action = "NewList", id = UrlParameter.Optional }
);
            routes.MapRoute(
        name: "chi tiet tin tuc",
url: "Tin/{id}",
defaults: new { controller = "BVMDHome", action = "Detail", id = UrlParameter.Optional }
);

            routes.MapRoute(
name: "lienhe",
url: "Lien-he",
defaults: new { controller = "TPAProduct", action = "Lienhe", id = UrlParameter.Optional }
);
            routes.MapRoute(
name: "khach hang",
url: "Khach-hang",
defaults: new { controller = "TPAProduct", action = "Khachhang", id = UrlParameter.Optional }
);
            routes.MapRoute(
name: "thu vien",
url: "Thu-vien",
defaults: new { controller = "TPANews", action = "Thuvien", id = UrlParameter.Optional }
);
            routes.MapRoute(
name: "tim kiem tin",
url: "Tim-tin/{id}",
defaults: new { controller = "BVMDHome", action = "NewSearch", id = UrlParameter.Optional }
);

            //het
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "BVMDHome", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
