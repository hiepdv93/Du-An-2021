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
        // [OutputCache(Duration = ClassExten.timeCacheChild, VaryByParam = "lang", VaryByCustom = "browser")]
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
            allCate = db.Categorys.Where(u => u.cateActive == true && u.cateType == ClassExten.typeProduct).ToList();
            var allPro = db.Products.Where(u => u.active == true).ToList();

            //var advs = db.Advs.Where(u => u.advActive == true && u.advType == 1).OrderBy(u => u.advOrder).Take(4).ToList();//quảng cáo dưới slide
            var anhCty = db.Advs.Where(u => u.advActive == true && u.advType == 2).OrderBy(u => u.advOrder).Take(4).ToList();
            var slide = db.Slides.Where(u => u.active == true).OrderBy(u => u.numberOder).ToList();
            int numNew = conf.viewNewPageHome != null ? conf.viewNewPageHome.Value : 5;
            int numPro = conf.viewProPageHome != null ? conf.viewProPageHome.Value : 9;
            var newNew = db.News.Where(u => u.status == 1 && u.newNew == true).OrderBy(u => u.newOrder).ThenByDescending(u => u.createDate).Take(3).ToList();
            var newHot = db.News.Where(u => u.status == 1 && u.newHot == true).OrderBy(u => u.newOrder).ThenByDescending(u => u.createDate).Take(4).ToList();
            var catepro = allCate.Where(u => u.cateType == ClassExten.typeProduct && u.cateActive == true && u.catepar_id.Equals(ClassExten.cateParent)).ToList();
            List<ProductModel> listPro = new List<ProductModel>();
            ProductModel itemPro;
            List<string> cateid;
            var cateHome = allCate.Where(u => u.cateActiveHome == true).ToList();
            foreach (var item in cateHome)
            {
                cateid = new List<string>();
                if (item.cate_cap != 3)
                {
                    cateid = GetListId(item.id, item.cate_cap.Value, lang);
                }
                else
                {
                    cateid.Add(item.id);
                }
                itemPro = new ProductModel();
                itemPro.cate = item;
                itemPro.pro = allPro.Where(u => cateid.Contains(u.cateId)).OrderBy(u => u.proOrder).Take(numPro).ToList();
                listPro.Add(itemPro);
            }

            //var sologan = db.WhyChooseUsses.Where(u => u.active == true).OrderBy(u => u.numberOder).Take(4).ToList();//slogan cam kết
            var SayWe = db.SayWes.Where(u => u.active == true).OrderBy(u => u.numberOder).Take(4).ToList();
            var doiTac = db.Partners.OrderBy(u => u.numberOder).Take(8).ToList();

            ViewBag.doiTac = doiTac;
            ViewBag.SayWe = SayWe;
            //ViewBag.advs = advs;
            //ViewBag.sologan = sologan;

            ViewBag.catepro = catepro;
            ViewBag.newNew = newNew;
            ViewBag.newHot = newHot;
            ViewBag.slide = slide;
            ViewBag.anhCty = anhCty;
            ViewBag.lang = lang;
            ViewBag.conf = conf;

            var proSale = allPro.Where(u => u.pro_hot == true).OrderBy(u => u.proOrder).ThenByDescending(u => u.createDate).Take(4).ToList();
            ViewBag.proSale = proSale;
            return PartialView(listPro);
        }
        public List<string> GetListId(string idp, int cap, string lang)
        {
            List<string> list = new List<string>();
            if (cap == 1)
            {
                if (allCate == null)
                {
                    allCate = db.Categorys.Where(u => u.cateActive == true && u.cateType == ClassExten.typeProduct).ToList();
                }
                list = allCate.Where(u => u.cateType == ClassExten.typeProduct && u.catepar_id.Equals(idp) && u.cate_cap == 2 && u.cateActive == true).Select(u => u.id).ToList();
                var listCap3 = allCate.Where(u => u.cateType == ClassExten.typeProduct && u.cate_cap == 3 && u.cateActive == true && list.Contains(u.catepar_id)).Select(u => u.id).ToList();
                foreach (var item in listCap3)
                {
                    list.Add(item);
                }
            }
            else
            {
                list = allCate.Where(u => u.cateType == ClassExten.typeProduct && u.catepar_id.Equals(idp) && u.cate_cap == 3 && u.cateActive == true).Select(u => u.id).ToList();
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
            conf = ConfigModel.listConfig.FirstOrDefault();
            ViewBag.conf = conf;
            var menu = db.Menus.Where(u => u.mPosition == 2 && u.par_id.Equals(ClassExten.cateParent) && u.active == true).OrderBy(u => u.mOrder).ToList();
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