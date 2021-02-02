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
        List<Category> allCate;
        // GET: ChildView
        //  [OutputCache(Duration = ClassExten.timeCacheChild, VaryByParam = "lang", VaryByCustom = "browser")]
        public ActionResult ChildHeader(string lang)
        {
            int countSP = 0;
            Config conf;
            if (ConfigModel.listConfig == null)
            {
                ConfigModel.listConfig = db.Configs.ToList();
            }
            conf = ConfigModel.listConfig.FirstOrDefault(u => u.conLang.Equals(lang));

            var menu = db.Menus.Where(u => u.mLang.Equals(lang) && u.mPosition == 1 && u.active == true).ToList();
            ViewBag.conf = conf;

            if (Session["ShoppingCart"] != null)
            {
                var cart = (ShoppingCartViewModel)Session["ShoppingCart"];
                foreach (var item in cart.CartItems)
                {
                    countSP += item.count;
                }
            }

            ViewBag.countSP = countSP;
            return PartialView(menu);
        }
        // [OutputCache(Duration = ClassExten.timeCacheChild, VaryByParam = "lang", VaryByCustom = "browser")]
        public ActionResult ChildHome(string lang)
        {
            Config conf;
            if (ConfigModel.listConfig == null)
            {
                ConfigModel.listConfig = db.Configs.ToList();
            }
            conf = ConfigModel.listConfig.FirstOrDefault(u => u.conLang.Equals(lang));
            int numPro = conf.viewProPageHome != null ? conf.viewProPageHome.Value : 9;
            int numNew = conf.viewNewPageHome != null ? conf.viewNewPageHome.Value : 5;

            var allPro = db.Products.Where(u => u.pLang.Equals(lang) && u.active == true).OrderBy(u => u.proOrder).Take(numPro).ToList();

            var advs = db.Advs.Where(u => u.advLang.Equals(lang) && u.advActive == true).OrderBy(u => u.advOrder).Take(4).ToList();
            var newNew = db.News.Where(u => u.newLang.Equals(lang) && u.status == 1 && u.newNew == true).OrderBy(u => u.newOrder).ThenByDescending(u => u.createDate).Take(3).ToList();

            ViewBag.newNew = newNew;
            ViewBag.advs = advs;
            ViewBag.lang = lang;
            ViewBag.conf = conf;

            return PartialView(allPro);
        }
        public List<string> GetListId(string idp, int cap, string lang)
        {
            List<string> list = new List<string>();
            if (cap == 1)
            {
                if (allCate == null)
                {
                    allCate = db.Categorys.Where(u => u.cateLang.Equals(lang) && u.cateActive == true && u.cateType == ClassExten.typeCareer).ToList();
                }
                list = allCate.Where(u => u.cateType == ClassExten.typeCareer && u.cateLang.Equals(lang) && u.catepar_id.Equals(idp) && u.cate_cap == 2 && u.cateActive == true).Select(u => u.id).ToList();
                var listCap3 = allCate.Where(u => u.cateType == ClassExten.typeCareer && u.cateLang.Equals(lang) && u.cate_cap == 3 && u.cateActive == true && list.Contains(u.catepar_id)).Select(u => u.id).ToList();
                foreach (var item in listCap3)
                {
                    list.Add(item);
                }
            }
            else
            {
                list = allCate.Where(u => u.cateType == ClassExten.typeCareer && u.cateLang.Equals(lang) && u.catepar_id.Equals(idp) && u.cate_cap == 3 && u.cateActive == true).Select(u => u.id).ToList();
            }
            list.Add(idp);
            return list;
        }
        //  [OutputCache(Duration = ClassExten.timeCacheChild, VaryByParam = "lang", VaryByCustom = "browser")]
        public ActionResult ChildFooter(string lang)
        {
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
        //  [OutputCache(Duration = ClassExten.timeCacheChild, VaryByParam = "lang", VaryByCustom = "browser")]
        public ActionResult PageNewLeft(string lang)
        {
            var cateNew = db.Categorys.Where(u => u.cateLang.Equals(lang) && u.cateActive == true && u.cateType == ClassExten.typeNew && u.catepar_id.Equals("-1")).ToList();
            ViewBag.cateNew = cateNew;
            var supports = db.Supports.OrderBy(u => u.phone).ToList();
            return PartialView(supports);
        }
        #endregion

        #region[bên trái trang sản phẩm]
        //  [OutputCache(Duration = ClassExten.timeCacheChild, VaryByParam = "lang;type", VaryByCustom = "browser")]
        public ActionResult PageProLeft(string lang, string typePage)
        {
            Config conf;
            if (ConfigModel.listConfig == null)
            {
                ConfigModel.listConfig = db.Configs.ToList();
            }
            conf = ConfigModel.listConfig.FirstOrDefault(u => u.conLang.Equals(lang));
            int numPro = conf.viewProPageHome != null ? conf.viewProPageHome.Value : 9;
            int numNew = conf.viewNewPageHome != null ? conf.viewNewPageHome.Value : 5;

            var allPro = db.Products.Where(u => u.pLang.Equals(lang) && u.active == true).OrderBy(u => u.proOrder).Take(numPro).ToList();

            ViewBag.typePage = typePage;
            var supports = db.Supports.OrderBy(u => u.phone).OrderBy(u => u.numberOder).ToList();
            ViewBag.allPro = allPro;

            return PartialView(supports);
        }
        #endregion

        public ActionResult SBULeft(string lang, string type)
        {
            if (type.Equals("1"))
            {
                ViewBag.urlPro = "Khach-hang";
                ViewBag.groupName = ClassExten.GetLangKey("key_khachhang");
            }
            else
            {
                ViewBag.urlPro = "Doi-tac";
                ViewBag.groupName = ClassExten.GetLangKey("key_doitac");
            }
            var cate = db.GroupPartners.Where(u => u.lang.Equals(lang) && u.gtype.Equals(type)).OrderBy(u => u.number).ToList();
            return PartialView(cate);
        }

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
                            join b in db.GroupPartners.AsNoTracking() on a.groupId equals b.id
                            where b.gtype.Equals("1") && a.lang.Equals(lang)
                            select a).OrderBy(u => u.numberOder).ToList();
            return PartialView(customer);
        }
        public ActionResult ChildPartner(string lang)
        {
            var pn = (from a in db.Partners.AsNoTracking()
                      join b in db.GroupPartners.AsNoTracking() on a.groupId equals b.id
                      where b.gtype.Equals("2") && a.lang.Equals(lang)
                      select a).OrderBy(u => u.numberOder).ToList();
            return PartialView(pn);
        }

        public ActionResult ChildTeacherImg(string lang)
        {
            var teacher = db.Teachers.Where(u => u.lang.Equals(lang) && u.thot == true && u.status == true).OrderBy(u => u.tOrder).ToList();
            ViewBag.teacher = teacher;
            var lstImage = db.Advs.Where(u => u.advLang.Equals(lang) && u.advActive == true).OrderBy(u => u.advOrder).ToList();
            return PartialView(lstImage);
        }
    }
}