using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Productgold.Models;

namespace Productgold.Controllers
{
    public class ImportViewController : Controller
    {
        //
        // GET: /ImportView/
        WEBTPAEntities db = new WEBTPAEntities();

        #region[ham lay menu]
        public ActionResult Index()
        {
            string chuoi = "";
            var menucha = db.Menus.Where(u=>u.active==true && u.par_id==0).ToList();
            for (int i = 0; i < menucha.Count; i++)
            {
                  chuoi+=" <li class=\"dropdown\">";

                  chuoi += " <a href=\"" + menucha[i].link + "\" class=\"dropdown-toggle\" >" + menucha[i].name + "</a>";
                  int idcha =menucha[i].id;
                var menucon = db.Menus.Where(u=>u.active==true && u.par_id==idcha).ToList();
                  if (menucon.Count>0)
                  {
                      chuoi += " <div class=\"dropdown-menu\">";
                      chuoi += "  <ul class=\"mega-menu-links\">";
                      for (int j = 0; j < menucon.Count; j++)
                      {
                          chuoi += "  <li>";
                          chuoi += "  <a href=\"" + menucon[j].link + "\">" + menucon[j].name + "</a>";
                          chuoi += "  </li>";
                      }
                      chuoi += "   </ul>";
                      chuoi += " </div>";
                  }
                    chuoi+=" </li>";
            }
            ViewBag.menu = chuoi;
            return PartialView();
        }
        #endregion

        #region[lay slide]
      
        #endregion

        #region[ham lay thuong hieu]
        public ActionResult GenBrand()
        {
            string chuoi = "";
            var data = db.Brands.Take(10).ToList();
            if (data.Count > 5)
            {
                chuoi += "<li>";
                chuoi += " <ul class=\"brand_item\">";
                for (int j = 0; j < 5; j++)
                {
                    chuoi += " <li>";
                    chuoi += "<a href=\"/Sanpham/Thuonghieu/" + data[j].id + "\" title=\""+data[j].name+"\">";
                    chuoi += " <div class=\"brand-logo\"><img src=\"" + data[j].images + "\" alt=\"" + data[j].name + "\"></div>";
                    chuoi += "</a>";
                    chuoi += " </li>";
                }
                chuoi += "</ul>";
                chuoi += " </li>";
              
                chuoi += "<li>";
                chuoi += " <ul class=\"brand_item\">";
                for (int h = 5; h < data.Count; h++)
                {
                    chuoi += " <li>";
                    chuoi += "<a href=\"/Sanpham/Thuonghieu/" + data[h].id + "\" title=\"" + data[h].name + "\">";
                    chuoi += " <div class=\"brand-logo\"><img src=\"" + data[h].images + "\" alt=\"" + data[h].name + "\"></div>";
                    chuoi += "</a>";
                    chuoi += " </li>";
                }
                chuoi += "</ul>";
                chuoi += " </li>";
                ViewBag.thuonghieu = chuoi;
            }
            else
            {
                 chuoi+="<li>";
                        chuoi+=" <ul class=\"brand_item\">";
                        for (int i = 0; i < data.Count; i++)
                        {
                            chuoi += " <li>";
                            chuoi += "<a href=\"/Sanpham/Thuonghieu/"+data[i].id+"\">";
                            chuoi += " <div class=\"brand-logo\"><img src=\"" + data[i].images + "\" alt=\"" + data[i].name + "\"></div>";
                            chuoi += "</a>";
                            chuoi += " </li>";
                        }
                         chuoi+="</ul>";
                     chuoi+=" </li>";
                     ViewBag.thuonghieu = chuoi;
            }
            return PartialView();
        }
        #endregion

        #region[ham lay top3 quang cao trang chu]
        public ActionResult GenAdv()
        {
            var data = db.Advs.Take(3).Where(u=>u.active==true && u.atype==0).OrderByDescending(u=>u.id).ToList();
            return PartialView(data);
        }
        #endregion

        #region[ham lay top5 quang cao slide san pham]
        public ActionResult GenAdvSlide()
        {
            var data = db.Advs.Take(5).Where(u => u.active == true && u.atype==1).OrderByDescending(u => u.id).ToList();
            return PartialView(data);
        }
        #endregion

        #region[ham lay top1 quang cao]
        public ActionResult GenAdvtop1()
        {
            var data = db.Advs.Take(1).Where(u => u.active == true && u.atype == 2).OrderByDescending(u => u.id).FirstOrDefault();
            return PartialView(data);
        }
        #endregion

        #region[logo]
        public ActionResult GenLogo()
        {
            var data = db.Configs.FirstOrDefault();
            ViewBag.logo = "<a href=\""+data.link+"\"><img src=\"" + data.logotop + "\" alt=\"" + data.shopname + "\"></a>";
            return PartialView();
        }
        #endregion

        #region[footer]
        public ActionResult Genfooter()
        {
            var data = db.Configs.FirstOrDefault();
            return PartialView(data);
        }
        #endregion

        #region[ham gen nhom san pham]
        public ActionResult Gencate()
        {
            var data = db.Categorys.Where(u=>u.active==true ).ToList();
            return PartialView(data);
        }
        #endregion

        #region[ham gen tin]
        public ActionResult GenTopNew()
        {
            var data = db.News.Where(u => u.active == true ).Take(6).OrderByDescending(u=>u.id).ToList();
            return PartialView(data);
        }
        #endregion

        #region[ham gen brand]
        public ActionResult GenNhomBrand()
        {
            var data = db.Brands.ToList();
            return PartialView(data);
        }
        #endregion

        #region[ham gen cart]
        public ActionResult Gencart()
        {
            var cart = (ShoppingCartViewModel)Session["ShoppingCart"];
            return PartialView(cart);
        }
        #endregion

        #region[ham quag cao khac]
        public ActionResult Genadvother()
        {
            string chuoi = "";
            var data = db.Advs.Where(u => u.active == true && u.atype == 2).ToList();
            if (data.Count > 0)
            {
                chuoi += " <div class=\"leftbanner\">";
                chuoi += " <img src=\"" + data[0].images + "\" alt=\"" + data[0].link + "\">";
                chuoi += "</div>";
            }
            ViewBag.other = chuoi;
            return PartialView();
        }
        #endregion
    }
}
