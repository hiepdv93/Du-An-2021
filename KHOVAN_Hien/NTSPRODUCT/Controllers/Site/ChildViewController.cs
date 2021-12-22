using System;
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
        public ActionResult ChildHeader(string lang, string path)
        {
            bool isHome = false;
            if (path.Equals("") || path.Equals("/"))
            {
                isHome = true;
                var slide = db.Slides.Where(u => u.slang.Equals(lang) && u.active == true).OrderBy(u => u.numberOder).ToList();
                ViewBag.slide = slide;
            }
            Config conf;
            if (ConfigModel.listConfig == null)
            {
                ConfigModel.listConfig = db.Configs.ToList();
            }
            conf = ConfigModel.listConfig.FirstOrDefault(u => u.conLang.Equals(lang));
            var menu = db.Menus.Where(u => u.mLang.Equals(lang) && u.mPosition == 1 && u.active == true).ToList();
            ViewBag.conf = conf;
            ViewBag.isHome = isHome;
            return PartialView(menu);
        }
        //1 ngay
        //  [OutputCache(Duration = 86400, VaryByParam = "lang")]
        public ActionResult ChildHome(string lang)
        {
            Config conf;
            if (ConfigModel.listConfig == null)
            {
                ConfigModel.listConfig = db.Configs.ToList();
            }
            conf = ConfigModel.listConfig.FirstOrDefault(u => u.conLang.Equals(lang));
            var whyChoose = db.WhyChooseUsses.Where(u => u.slang.Equals(lang) && u.active == true).OrderBy(u => u.slang).ToList();
            var slogan = db.Sologans.Where(u => u.lang.Equals(lang) && u.status == true).OrderBy(u => u.dOrder).ToList();
            var pn = db.Partners.Where(u => u.lang.Equals(lang) && u.type.Equals(Constants.Partner)).OrderBy(u => u.numberOder).ToList();
            var sl = db.Solutions.Where(u => u.lang.Equals(lang)).OrderBy(u => u.solutionOrder).ToList();
            ViewBag.sl = sl;
            ViewBag.pn = pn;
            ViewBag.conf = conf;
            ViewBag.whyChoose = whyChoose;
            return PartialView(slogan);
        }
        //  [OutputCache(Duration = ClassExten.timeCacheChild, VaryByParam = "lang")]
        public ActionResult ChildHomeTop(string lang)
        {
            Config conf;
            if (ConfigModel.listConfig == null)
            {
                ConfigModel.listConfig = db.Configs.ToList();
            }
            conf = ConfigModel.listConfig.FirstOrDefault(u => u.conLang.Equals(lang));
            ViewBag.conf = conf;
            var catepro = db.Categorys.Where(u => u.cateLang.Equals(lang) && u.cateType == ClassExten.typePro && u.cateActive == true && u.catepar_id.Equals("-1")).ToList();
            var slide = db.Slides.Where(u => u.slang.Equals(lang) && u.active == true).OrderBy(u => u.numberOder).ToList();
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
            conf = ConfigModel.listConfig.FirstOrDefault(u => u.conLang.Equals(lang));
            ViewBag.conf = conf;
            var tag = db.Tagproes.Where(u => u.lang.Equals(lang)).OrderBy(u => u.tagOrder).ToList();
            ViewBag.tags = tag;

            var chooseWe = db.WhyChooseUsses.Where(u => u.slang.Equals(lang) && u.active == true).OrderBy(u => u.numberOder).ToList();
            var partner = (from a in db.Partners.AsNoTracking()
                           orderby a.numberOder
                           select a).ToList();
            ViewBag.partner = partner;
            ViewBag.chooseWe = chooseWe;
            return PartialView();
        }
        // [OutputCache(Duration = ClassExten.timeCacheChild, VaryByParam = "lang")]
        public ActionResult ChildFooter(string lang)
        {
            //var isMb = Request.Browser.IsMobileDevice;
            //ViewBag.isMb = isMb;
            Config conf;
            if (ConfigModel.listConfig == null)
            {
                ConfigModel.listConfig = db.Configs.ToList();
            }
            conf = ConfigModel.listConfig.FirstOrDefault(u => u.conLang.Equals(lang));
            ViewBag.conf = conf;
            var menu = db.Menus.Where(u => u.mLang.Equals(lang) && u.mPosition == 2 && u.par_id.Equals(ClassExten.cateParent) && u.active == true).OrderBy(u => u.mOrder).ToList();
            return PartialView(menu);
        }

        #region[view con bên trang tin]
        //[OutputCache(Duration = ClassExten.timeCacheChild, VaryByParam = "lang")]
        public ActionResult PageNewLeft(string lang)
        {
            var cateNew = db.Categorys.Where(u => u.cateLang.Equals(lang) && u.cateActive == true && u.cateType == ClassExten.typeNew).ToList();
            ViewBag.cateNew = cateNew;
            var newsNew = db.News.Where(u => u.newLang.Equals(lang) && u.status == Constants.Active && u.newNew == true).OrderBy(u => u.newOrder).Take(6).ToList();
            return PartialView(newsNew);
        }
        #endregion

        #region[bên trái trang sản phẩm]
        //[OutputCache(Duration = ClassExten.timeCacheChild, VaryByParam = "lang")]
        public ActionResult PageProLeft(string lang,int type)
        {
            var pType = type.ToString();
            var proHot = db.Products.Where(u => u.pLang.Equals(lang) && u.active == true && u.pro_hot == true && u.pType.Equals(pType)).OrderBy(u => u.proOrder).Take(6).ToList();
            var catepro = db.Categorys.Where(u => u.cateLang.Equals(lang) && u.cateType == type && u.cateActive == true).ToList();
            ViewBag.catepro = catepro;
            ViewBag.pType = pType;
            return PartialView(proHot);
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

        public ActionResult ChildTeacherImg(string lang)
        {
            var lstImage = db.Advs.Where(u => u.advLang.Equals(lang) && u.advActive == true).OrderBy(u => u.advOrder).ToList();
            return PartialView(lstImage);
        }
    }
}