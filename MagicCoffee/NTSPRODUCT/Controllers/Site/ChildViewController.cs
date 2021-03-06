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
        // [OutputCache(Duration = ClassExten.timeCacheChild, VaryByParam = "lang")]
        public ActionResult ChildHeader(bool isMobile)
        {
            Config conf;
            if (ConfigModel.listConfig == null)
            {
                ConfigModel.listConfig = db.Configs.ToList();
            }
            conf = ConfigModel.listConfig.FirstOrDefault();
            ViewBag.conf = conf;
            var product = db.Products.Where(u => u.active == true).ToList();
            ViewBag.pro = product;

            var cate = db.Categorys.Where(u => u.cateActive == true && u.cateType == ClassExten.typePro).ToList();
            ViewBag.cate = cate;

            var menu = db.Menus.Where(u => u.mPosition == 1 && u.active == true).ToList();
            return PartialView(menu);
        }

        public ActionResult RightMenu()
        {
            var menu = db.Menus.Where(u => u.mPosition == 1 && u.active == true).ToList();
            return PartialView(menu);
        }

        public ActionResult LeftCate()
        {
            var cate = db.Categorys.Where(u => u.cateType == ClassExten.typePro && u.cateActive == true).OrderBy(u => u.cateOrder).ToList();
            return PartialView(cate);
        }

        public ActionResult ChildHeaderBottom(string path)
        {
            bool isHome = false;
            if (path.Equals("") || path.Equals("/"))
            {
                isHome = true;
                var slide = db.Slides.Where(u => u.active == true).OrderBy(u => u.numberOder).ToList();
                ViewBag.slide = slide;
            }
            ViewBag.isHome = isHome;
            return PartialView();
        }
        //1 ngay
        //  [OutputCache(Duration = 86400, VaryByParam = "lang")]
        public ActionResult ChildHome()
        {
            Config conf;
            if (ConfigModel.listConfig == null)
            {
                ConfigModel.listConfig = db.Configs.ToList();
            }
            conf = ConfigModel.listConfig.FirstOrDefault();
            ViewBag.conf = conf;
            var catepro = db.Categorys.Where(u => u.cateActiveHome == true && u.cateActive == true && u.catepar_id.Equals("-1")).OrderBy(u => u.cateType).ThenBy(u => u.cateOrder).ToList();
            var slide = db.Slides.Where(u => u.active == true).OrderBy(u => u.numberOder).ToList();
            ViewBag.slide = slide;
            return PartialView(catepro);
        }
      

        //  [OutputCache(Duration = ClassExten.timeCacheChild, VaryByParam = "lang")]
        public ActionResult ChildHomeTop(string lang)
        {
            Config conf;
            if (ConfigModel.listConfig == null)
            {
                ConfigModel.listConfig = db.Configs.ToList();
            }
            conf = ConfigModel.listConfig.FirstOrDefault();
            ViewBag.conf = conf;
            var catepro = db.Categorys.Where(u => u.cateActiveHome == true && u.cateActive == true && u.catepar_id.Equals("-1")).OrderBy(u => u.cateType).ThenBy(u => u.cateOrder).ToList();
            var slide = db.Slides.Where(u => u.active == true).OrderBy(u => u.numberOder).ToList();
            ViewBag.slide = slide;
            ViewBag.catepro = catepro;
            return PartialView();
        }
        // [OutputCache(Duration = ClassExten.timeCacheChild, VaryByParam = "lang")]
        public ActionResult ChildHomeBottom(string lang)
        {
            Config conf;
            if (ConfigModel.listConfig == null)
            {
                ConfigModel.listConfig = db.Configs.ToList();
            }
            conf = ConfigModel.listConfig.FirstOrDefault();
            ViewBag.conf = conf;
            var tag = db.Tagproes.OrderBy(u => u.tagOrder).ToList();
            ViewBag.tags = tag;

            var chooseWe = db.WhyChooseUsses.Where(u => u.active == true).OrderBy(u => u.numberOder).ToList();
            var partner = (from a in db.Partners.AsNoTracking()
                           orderby a.numberOder
                           select a).ToList();
            ViewBag.partner = partner;
            ViewBag.chooseWe = chooseWe;
            return PartialView();
        }
        // [OutputCache(Duration = ClassExten.timeCacheChild, VaryByParam = "lang")]
        public ActionResult ChildFooter(string path)
        {
            string groupNewID = ClassExten.cateParent;

            string url = HttpContext.Request.Url.AbsoluteUri;
            string pathq = HttpContext.Request.Url.AbsolutePath;
            if (pathq.Contains("/tin-tuc/") || pathq.Contains("/nhom-thuc-don/"))
            {
                var arr = pathq.Split('/').ToArray();
                string key = arr[1];
                if (!string.IsNullOrEmpty(key))
                {
                    var cate = db.Categorys.FirstOrDefault(u => u.cateKey.Equals(key));
                    if (cate != null)
                    {
                        groupNewID = cate.id;
                    }
                }
            }
            Config conf;
            if (ConfigModel.listConfig == null)
            {
                ConfigModel.listConfig = db.Configs.ToList();
            }
            conf = ConfigModel.listConfig.FirstOrDefault();
            ViewBag.conf = conf;
            var news = db.News.Where(u => u.status == Constants.Active && (groupNewID == ClassExten.cateParent || u.groupId.Equals(groupNewID))).OrderByDescending(u => u.createDate).Take(3).ToList();
            int tinThieu = 3 - news.Count();
            if (tinThieu > 0)
            {
                var lstId = news.Select(u => u.id).ToList();
                var lstThieu = db.News.Where(u => u.status == Constants.Active && !lstId.Contains(u.id)).OrderByDescending(u => u.createDate).Take(tinThieu).ToList();
                if (lstThieu.Count > 0)
                {
                    news.AddRange(lstThieu);
                }
            }
            return PartialView(news);
        }

        #region[view con bên trang tin]
        //[OutputCache(Duration = ClassExten.timeCacheChild, VaryByParam = "lang")]
        public ActionResult PageNewLeft(string lang)
        {
            var cateNew = db.Categorys.Where(u => u.cateActive == true && u.cateType == ClassExten.typeNew).ToList();
            ViewBag.cateNew = cateNew;
            var newsNew = db.News.Where(u => u.status == Constants.Active && u.newNew == true).OrderBy(u => u.newOrder).Take(6).ToList();
            return PartialView(newsNew);
        }
        #endregion

 

        public ActionResult ChildSlogan(string lang)
        {
            Config conf;
            if (ConfigModel.listConfig == null)
            {
                ConfigModel.listConfig = db.Configs.ToList();
            }
            conf = ConfigModel.listConfig.FirstOrDefault();
            ViewBag.Slogan = conf.shopName;
            return PartialView();
        }

        public ActionResult ChildCustomer(string lang)
        {
            var customer = (from a in db.Partners.AsNoTracking()
                            select a).OrderBy(u => u.numberOder).ToList();
            return PartialView(customer);
        }
        public ActionResult ChildPartner(string lang)
        {
            var pn = (from a in db.Partners.AsNoTracking()
                      select a).OrderBy(u => u.numberOder).ToList();
            return PartialView(pn);
        }


    }
}