﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NTSPRODUCT.Models;
namespace NTSPRODUCT.Controllers.Site
{
    public class ChildViewController : Controller
    {

        NTSWEBEntities db = new NTSWEBEntities();
        // GET: ChildView

        [OutputCache(Duration = ClassExten.timeCacheChild, VaryByParam = "lang")]
        public ActionResult ChildHeader(string lang)
        {

            int countSp = 0;
            var cartGet = ClassExten.GetCokiesCart();
            if (cartGet != null)
            {
                countSp = cartGet.CartItems.Count;
            }
            ViewBag.countSp = countSp;

            Config conf;
            if (ConfigModel.listConfig == null)
            {
                ConfigModel.listConfig = db.Configs.ToList();
            }
            conf = ConfigModel.listConfig.FirstOrDefault(u => u.conLang.Equals(lang));

            var menu = db.Menus.Where(u => u.mLang.Equals(lang) && u.mPosition == 1 && u.active == true).ToList();
            ViewBag.conf = conf;
            return PartialView(menu);
        }

        [OutputCache(Duration = ClassExten.timeCacheChild, VaryByParam = "lang")]
        public ActionResult ChildHome(string lang)
        {
            //danh mục hiển thị trang chủ là nhóm cấp 1 dc check  hiển thị trang chủ
            //sản phẩm hiển thị theo nhóm là những nhóm cấp 2 check hiển thị trang chủ
            Config conf;
            if (ConfigModel.listConfig == null)
            {
                ConfigModel.listConfig = db.Configs.ToList();
            }
            conf = ConfigModel.listConfig.FirstOrDefault();
            var allCate = db.Categorys.Where(u => u.cateActive == true && u.cateType == ClassExten.typeProduct).ToList();

            var advs = db.Advs.FirstOrDefault(u => u.advActive == true && u.advType == 1);//quảng cáo dưới slide
            var slide = db.Slides.Where(u => u.active == true).OrderBy(u => u.numberOder).ToList();
            int numPro = conf.viewProPageHome != null ? conf.viewProPageHome.Value : 9;


            var sologan = db.WhyChooseUsses.Where(u => u.active == true).OrderBy(u => u.numberOder).Take(4).ToList();//slogan cam kết
            var SayWe = db.SayWes.Where(u => u.active == true).OrderBy(u => u.numberOder).Take(4).ToList();

            ViewBag.SayWe = SayWe;
            ViewBag.advs = advs;
            ViewBag.sologan = sologan;

            ViewBag.slide = slide;
            ViewBag.lang = lang;
            ViewBag.conf = conf;

            var proHome = db.Products.Where(u => u.pro_home == true && u.active == true).OrderBy(u => u.proOrder).Take(numPro).ToList();
            ViewBag.proHome = proHome;
            return PartialView(allCate);
        }

        public List<string> GetListId(string idp, int cap, string lang)
        {
            List<string> list = new List<string>();
            //if (cap == 1)
            //{
            //    if (allCate == null)
            //    {
            //        allCate = db.Categorys.Where(u => u.cateActive == true && u.cateType == ClassExten.typeProduct).ToList();
            //    }
            //    list = allCate.Where(u => u.cateType == ClassExten.typeProduct && u.catepar_id.Equals(idp) && u.cate_cap == 2 && u.cateActive == true).Select(u => u.id).ToList();
            //    var listCap3 = allCate.Where(u => u.cateType == ClassExten.typeProduct && u.cate_cap == 3 && u.cateActive == true && list.Contains(u.catepar_id)).Select(u => u.id).ToList();
            //    foreach (var item in listCap3)
            //    {
            //        list.Add(item);
            //    }
            //}
            //else
            //{
            //    list = allCate.Where(u => u.cateType == ClassExten.typeProduct && u.catepar_id.Equals(idp) && u.cate_cap == 3 && u.cateActive == true).Select(u => u.id).ToList();
            //}
            //list.Add(idp);
            return list;
        }

        [OutputCache(Duration = ClassExten.timeCacheChild, VaryByParam = "lang")]
        public ActionResult ChildFooter(string lang)
        {
            Config conf;
            if (ConfigModel.listConfig == null)
            {
                ConfigModel.listConfig = db.Configs.ToList();
            }
            conf = ConfigModel.listConfig.FirstOrDefault();
            ViewBag.conf = conf;
            var menu = db.Menus.Where(u => u.mPosition == 2 && u.active == true).OrderBy(u => u.mOrder).ToList();
            return PartialView(menu);
        }

        #region[view con bên trang tin]
        [OutputCache(Duration = ClassExten.timeCacheChild, VaryByParam = "lang")]
        public ActionResult PageNewLeft(string lang)
        {
            var cateNew = db.Categorys.Where(u => u.cateLang.Equals(lang) && u.cateActive == true && u.cateType == ClassExten.typeNew && u.catepar_id.Equals("-1")).ToList();
            ViewBag.cateNew = cateNew;
            var supports = db.Supports.OrderBy(u => u.phone).ToList();
            return PartialView(supports);
        }
        #endregion

        #region[bên trái trang sản phẩm]
        [OutputCache(Duration = ClassExten.timeCacheChild, VaryByParam = "lang")]
        public ActionResult PageProLeft(string lang, string typePage)
        {
            ViewBag.typePage = typePage;
            var supports = db.Supports.OrderBy(u => u.phone).OrderBy(u => u.numberOder).ToList();
            var catepro = db.Categorys.Where(u => u.cateType == ClassExten.typeProduct && u.cateActive == true && u.catepar_id.Equals(ClassExten.cateParent)).ToList();
            ViewBag.catepro = catepro;

            return PartialView(supports);
        }
        #endregion

        public ActionResult ChildSlogan(string lang)
        {
            Config conf;
            if (ConfigModel.listConfig == null)
            {
                ConfigModel.listConfig = db.Configs.ToList();
            }
            conf = ConfigModel.listConfig.FirstOrDefault(u => u.conLang.Equals(lang));
            ViewBag.Slogan = conf.shopName;
            return PartialView();
        }


    }
}