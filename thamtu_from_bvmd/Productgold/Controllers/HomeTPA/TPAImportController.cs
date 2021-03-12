using Productgold.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Productgold.Controllers.HomeTPA
{
    public class TPAImportController : Controller
    {

        // GET: TPAImport
        WEBTPAEntities db = new WEBTPAEntities();
        string lang = ClassExten.GetLangSite();
        string siteOption = ClassExten.siteOption;

        MenuClass mn2;
        MenuClass mn3;
        MenuClass mn4;
        List<Menu> lst;
        List<Menu> lst3;
        string iconSub = "<span class='click-here'></span>";
        public ActionResult GenFooter()
        {

            Config config;
            #region[kiem tra cache]
            if (ConfigModel.conf == null)
            {
                config = db.Configs.FirstOrDefault(u => u.conLang == lang && u.siteTPA == siteOption);
                ConfigModel.conf = config;
                ConfigModel.date = DateTime.Now;
            }
            else
            {
                config = ConfigModel.conf;
            }
            #endregion
            #region[lay logo duoi]
            var logoduoi = db.Advs.FirstOrDefault(u => u.active == true && u.advLang == lang && u.siteTPA == siteOption && u.atype == 1);
            if (logoduoi != null)
            {
                ViewBag.logoduoi = "<a href='" + logoduoi.link + "' title='" + logoduoi.advName + "'><img src='" + logoduoi.images + "' alt='" + logoduoi.advName + "'></a>";
            }
            #endregion
            ViewBag.chantrangl = config.conTact;

            return PartialView();
        }

        public ActionResult GenHeader()
        {
            Config config;
            ViewBag.tiengviet = ClassExten.GetLangKey("key_tiengviet");
            ViewBag.tienganh = ClassExten.GetLangKey("key_tienganh");
            ViewBag.timkiem = ClassExten.GetLangKey("key_timkiem");

            var banner = db.Advs.OrderBy(u => u.advOrder).FirstOrDefault(u => u.advLang == lang && u.active == true && u.atype == 2);
            ViewBag.banner = banner;
            #region[lay logo tren]
            var logotren = db.Advs.FirstOrDefault(u => u.active == true && u.advLang == lang && u.siteTPA == siteOption && u.atype == 0);
            if (logotren != null)
            {
                ViewBag.logotren = "<a href='" + logotren.link + "' title='" + logotren.advName + "'><img  class='img-responsive' src='" + logotren.images + "' alt='" + logotren.advName + "'></a>";
                //ViewBag.logotrenMobile = "<a href='" + logotren.link + "' title='" + logotren.advName + "'><img class='img - fluid' src='" + logotren.images + "' alt='" + logotren.advName + "'></a>";
            }
            #endregion
            #region[kiem tra cache]
            if (ConfigModel.conf == null)
            {
                config = db.Configs.FirstOrDefault(u => u.conLang == lang && u.siteTPA == siteOption);
                ConfigModel.conf = config;
                ConfigModel.date = DateTime.Now;
            }
            else
            {
                config = ConfigModel.conf;
            }
            #endregion
            #region[lay menu tren]
            string StrmenuTren = "";
            string StrmenuTrenMB = "";
            MenuClass mn;

            var menutren = db.Menus.Where(u => u.active == true && u.mLang == lang && u.siteTPA == siteOption && u.mPosition == 1 && u.par_id == -1 && u.mcap == 1).OrderBy(u => u.mOrder).ToList();
            foreach (var item in menutren)
            {
                mn = GenMenuTren2(item.id);
                StrmenuTren += "<li><a href='" + item.link + "' title='" + item.name + "'>" + item.name + "</a>";
                StrmenuTrenMB += "<li class='pro-dc'><div class='top-pro-dc'><a href='" + item.link + "' title='" + item.name + "'>" + item.name + "</a>" + (string.IsNullOrEmpty(mn.menuPc) ? "" : iconSub) + "</div>";
                if (!string.IsNullOrEmpty(mn.menuPc))
                {
                    StrmenuTren += mn.menuPc;
                    StrmenuTrenMB += mn.menuMb;
                }
                StrmenuTren += "</li>";
                StrmenuTrenMB += "</li>";
            }
            ViewBag.StrmenuTren = StrmenuTren;
            ViewBag.StrmenuTrenMB = StrmenuTrenMB;
            #endregion
            return PartialView(config);
        }

        public MenuClass GenMenuTren2(int idp)
        {
            lst = db.Menus.Where(u => u.active == true && u.mLang == lang && u.siteTPA == siteOption && u.mPosition == 1 && u.par_id == idp && u.mcap == 2).OrderBy(u => u.mOrder).ToList();
            mn2 = new MenuClass();
            if (lst.Count > 0)
            {
                mn2.menuPc += "<ul>";
                mn2.menuMb += "<ul class='kid-dc cap2'>";
                foreach (var item in lst)
                {
                    mn3 = GenMenuTren3(item.id);
                    mn2.menuPc += "<li><a href='" + item.link + "' title='" + item.name + "'>" + item.name + "</a>";
                    mn2.menuMb += "<li class='pro-dc'><div class='top-pro-dc'><a href='" + item.link + "' title='" + item.name + "'>" + item.name + "</a>" + (string.IsNullOrEmpty(mn3.menuPc) ? "" : iconSub) + "</div>";
                    if (!string.IsNullOrEmpty(mn3.menuPc))
                    {
                        mn2.menuPc += mn3.menuPc;
                        mn2.menuMb += mn3.menuMb;
                    }
                    mn2.menuPc += "</li>";
                    mn2.menuMb += "</li>";
                }
                mn2.menuPc += "</ul>";
                mn2.menuMb += "</ul>";
            }
            return mn2;
        }

        public MenuClass GenMenuTren3(int idp)
        {
            lst3 = db.Menus.Where(u => u.active == true && u.mLang == lang && u.siteTPA == siteOption && u.mPosition == 1 && u.par_id == idp && u.mcap == 3).OrderBy(u => u.mOrder).ToList();
            mn4 = new MenuClass();
            if (lst3.Count > 0)
            {
                mn4.menuPc += "<ul>";
                mn4.menuMb += "<ul class='kid-dc cap3'>";
                foreach (var item in lst3)
                {
                    mn4.menuPc += "<li><a href='" + item.link + "' title='" + item.name + "'>" + item.name + "</a></li>";
                    mn4.menuMb += "<li><a href='" + item.link + "' title='" + item.name + "'>" + item.name + "</a></li>";
                }
                mn4.menuPc += "</ul>";
                mn4.menuMb += "</ul>";
            }
            return mn4;
        }

        public ActionResult GenDanhmuc()
        {
            string typeCate = "1";
            string path = HttpContext.Request.Url.AbsolutePath.ToLower();
            if (path.Contains("/san-pham"))
            {
                typeCate = "0";
            }
            ViewBag.key_danhmucsp = ClassExten.GetLangKey("key_danhmucsp");
            var qcLeft = db.Advs.Where(u => u.advLang == lang && u.siteTPA == siteOption && u.active == true && u.atype == 3).OrderBy(u => u.advOrder).ToList();
            string chuoiqLeft = "";
            foreach (var item in qcLeft)
            {
                chuoiqLeft += "<div class='qc'><a href = '" + item.link + "' title='" + item.advName + "'><img src='" + item.images + "' alt='" + item.advName + "'></a></div>";
            }
            List<Category> all = db.Categorys.Where(u => u.cateLang == lang && u.ctype == 1 && u.siteTPA == siteOption && u.catepar_id == -1 && u.active == true && u.cateLevel == typeCate).OrderBy(u => u.cateOrder).ToList();
            ViewBag.chuoiqLeft = chuoiqLeft;
            return PartialView(all);
        }

        public ActionResult GenNewRight()
        {
            ViewBag.key_tinmoi = ClassExten.GetLangKey("key_tinmoi");
            ViewBag.key_tinnoibat = ClassExten.GetLangKey("key_tinnoibat");
            Config config;
            #region[kiem tra cache]
            if (ConfigModel.conf == null)
            {
                config = db.Configs.FirstOrDefault(u => u.conLang == lang && u.siteTPA == siteOption);
                ConfigModel.conf = config;
                ConfigModel.date = DateTime.Now;
            }
            else
            {
                config = ConfigModel.conf;
            }
            #endregion
            ViewBag.videoRight = config.googleAnalytics;
            int numBer = 8;
            try
            {
                // var spNumber = db.sp_Config_SelectViewNumber(lang, siteOption).ToList();
                string[] mang = config.viewPro.Split(';');
                numBer = int.Parse(mang[0]);
            }
            catch (Exception)
            {

            }

            var List_New = db.News.Where(u => u.active == true && u.newLang == lang).OrderByDescending(u => u.createdate).Take(numBer).ToList();
            ViewBag.List_New = List_New;
            var List_NB = db.News.Where(u => u.ex1.Equals("1") && u.active == true && u.newLang == lang).OrderBy(u=>u.newOrder).Take(numBer).ToList();
            return PartialView(List_NB);
        }
    }
}