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

            var doiTac = db.Partners.OrderBy(u => u.numberOder).Take(8).ToList();
            var slide = db.Slides.Where(u => u.active == true).OrderBy(u => u.numberOder).ToList();

            //int numNew = conf.viewNewPageHome != null ? conf.viewNewPageHome.Value : 5;
            int numPro = conf.viewProPageHome != null ? conf.viewProPageHome.Value : 3;
            var numHoiTruong = conf.numberThueHT != null ? conf.numberThueHT.Value : 3;

            var newNew = db.News.Where(u => u.status == 1 && u.newNew == true).OrderBy(u => u.newOrder).ThenByDescending(u => u.createDate).Take(3).ToList();
            var newHot = db.News.Where(u => u.status == 1 && u.newHot == true).OrderBy(u => u.newOrder).ThenByDescending(u => u.createDate).Take(4).ToList();
            var sologan = db.WhyChooseUsses.Where(u => u.active == true).OrderBy(u => u.numberOder).ToList();//slogan cam kết
            var SayWe = db.SayWes.Where(u => u.active == true).OrderBy(u => u.numberOder).ToList();//khách hàng nói gì

            ViewBag.SayWeHoiTruong = SayWe.Where(u => u.type.Equals(ClassExten.HoiTruong)).Take(5).ToList();
            ViewBag.SayWeSukien = SayWe.Where(u => u.type.Equals(ClassExten.SuKien)).Take(5).ToList();

            ViewBag.sologanSukien = sologan.Where(u => u.type.Equals(ClassExten.SuKien)).Take(5).ToList();
            ViewBag.sologanHoiTruong = sologan.Where(u => u.type.Equals(ClassExten.HoiTruong)).Take(5).ToList();

            //hoi truog
            var hoitruong = db.Products.Where(u => u.active == true && u.pro_home == true && u.pLang.Equals(ClassExten.HoiTruong)).OrderBy(u => u.proOrder).Take(numHoiTruong).ToList();
            //su kien
            var sukien = db.Products.Where(u => u.active == true && u.pro_home == true && u.pLang.Equals(ClassExten.SuKien)).OrderBy(u => u.proOrder).Take(numPro).ToList();

            //qua tang
            var quaTang = db.Categorys.Where(u => u.cateActive == true && u.cateActiveHome == true && u.cateType == ClassExten.typeNew).OrderBy(u => u.cateOrder).Take(4).ToList();
            var quaId = quaTang.Select(u => u.id).ToList();
            var quaChild = db.News.Where(u => u.status == 1 && u.newNew == true && quaId.Contains(u.groupId)).ToList();
            List<News> lstQua = new List<News>();
            foreach (var item in quaId)
            {
                lstQua.AddRange(quaChild.Where(u => u.groupId.Equals(item)).OrderByDescending(u => u.createDate).Take(2).ToList());
            }

            ViewBag.quaTang = quaTang;
            ViewBag.lstQua = lstQua;
            ViewBag.hoitruong = hoitruong;
            ViewBag.doiTac = doiTac;
            ViewBag.newNew = newNew;
            ViewBag.newHot = newHot;
            ViewBag.slide = slide;
            ViewBag.lang = lang;
            ViewBag.conf = conf;

            return PartialView(sukien);
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
        public ActionResult PageProLeft(string typePage)
        {
            ViewBag.typePage = typePage;

            var sologan = db.WhyChooseUsses.Where(u => u.active == true && u.type.Equals(typePage)).OrderBy(u => u.numberOder).ToList();//slogan cam kết
            var supports = db.Supports.OrderBy(u => u.phone).OrderBy(u => u.numberOder).ToList();
            var pro = db.Products.Where(u => u.pLang.Equals(typePage) && u.active == true).OrderByDescending(u => u.createDate).Take(5).ToList();
            ViewBag.pro = pro;
            ViewBag.sologan = sologan;

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