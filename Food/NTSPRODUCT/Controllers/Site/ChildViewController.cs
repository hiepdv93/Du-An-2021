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

        [OutputCache(Duration = ClassExten.timeCacheChild, VaryByParam = "lang")]
        public ActionResult ChildHeader(string lang)
        {

            ViewBag.countSp = CountCart();

            Config conf;
            if (ConfigModel.listConfig == null)
            {
                ConfigModel.listConfig = db.Configs.ToList();
            }
            conf = ConfigModel.listConfig.FirstOrDefault(u => u.conLang.Equals(lang));

            var menu = db.Menus.Where(u => u.mPosition == 1 && u.active == true && u.par_id.Equals(ClassExten.cateParent)).OrderBy(u => u.mOrder).ToList();
            var allCate = db.Categorys.Where(u => u.cateActive == true && u.cateType == ClassExten.typeProduct).ToList();
            ViewBag.conf = conf;
            ViewBag.menu = menu;

            return PartialView(allCate);
        }
        private int CountCart()
        {
            int countSp = 0;
            var cartGet = ClassExten.GetCokiesCart();
            if (cartGet != null)
            {
                if (cartGet.CartItems!=null)
                {
                    countSp = cartGet.CartItems.Sum(u=>u.count);
                }
            }
            return countSp;
        }
        [OutputCache(Duration = ClassExten.timeCacheChild, VaryByParam = "lang")]
        public ActionResult ChildHome(string lang)
        {
            List<Product> lstPro = db.Products.Where(u => u.active == true).ToList();
            List<Category> lstCate = db.Categorys.Where(u => u.cateActive == true && u.cateType == ClassExten.typeProduct).ToList();
            //danh mục hiển thị trang chủ là nhóm cấp 1 dc check  hiển thị trang chủ
            //sản phẩm hiển thị theo nhóm là những nhóm cấp 2 check hiển thị trang chủ
            Config conf;
            if (ConfigModel.listConfig == null)
            {
                ConfigModel.listConfig = db.Configs.ToList();
            }
            conf = ConfigModel.listConfig.FirstOrDefault();
            ViewBag.conf = conf;

            int numPro = conf.viewProPageHome != null ? conf.viewProPageHome.Value : 12;
            int numNew = conf.viewNewPageHome != null ? conf.viewNewPageHome.Value : 5;
            var newsHot = db.News.Where(u => u.status == Constants.Active && u.newHot == true).OrderBy(u => u.newOrder).Take(numNew).ToList();//tin hot home
                                                                                                                                              // var SayWe = db.SayWes.Where(u => u.active == true).OrderBy(u => u.numberOder).Take(4).ToList();

            var proBanChay = lstPro.Where(u => u.pro_hot == true && u.active == true).OrderBy(u => u.proOrder).Take(numPro).ToList();
            var proSale = lstPro.Where(u => u.pro_sale == true && u.active == true).OrderBy(u => u.proOrder).Take(numPro).ToList();

            List<string> cateId = null;
            List<ProductModel> proHome = new List<ProductModel>();
            ProductModel productModel;
            var cateHome = lstCate.Where(u => u.cateActiveHome == true).OrderBy(u => u.cateOrder).ToList();//lấy ra nhóm hiển thị trang chủ
            foreach (var item in cateHome)
            {
                cateId = GetListId(item.id, item.cate_cap.Value, lstCate);
                productModel = new ProductModel();
                productModel.cate = item;
                productModel.pro = lstPro.Where(u => cateId.Contains(u.cateId)).OrderBy(u => u.proOrder).Take(numPro).ToList();
                proHome.Add(productModel);
            }

            ViewBag.newsHot = newsHot;
            // ViewBag.SayWe = SayWe;

            ViewBag.lang = lang;

            ViewBag.proSale = proSale;
            ViewBag.proBanChay = proBanChay;
            if (conf.isShowVideoHome == true)
            {
                var videos = db.Advs.Where(u => u.advActive == true && u.advType == 4).OrderBy(u => u.advOrder).Take(8).ToList(); ;
                ViewBag.videos = videos;
            }

            return PartialView(proHome);
        }

        public List<string> GetListId(string idp, int cap, List<Category> allCate)
        {
            List<string> list = new List<string>();
            if (cap == 1)
            {
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

        public ActionResult NavMenu()
        {
            bool isHome = false;
            Config conf;
            if (ConfigModel.listConfig == null)
            {
                ConfigModel.listConfig = db.Configs.ToList();
            }
            conf = ConfigModel.listConfig.FirstOrDefault();
            ViewBag.conf = conf;

            var menu = db.Menus.Where(u => u.mPosition == 1 && u.active == true && u.par_id.Equals(ClassExten.cateParent)).OrderBy(u => u.mOrder).ToList();
            var advRight = db.Advs.Where(u => u.advActive == true && u.advType == 2).Take(4).ToList();//quảng cáo phải slide
            ViewBag.advRight = advRight;

            var allCate = db.Categorys.Where(u => u.cateActive == true && u.cateType == ClassExten.typeProduct).ToList();
            string path = HttpContext.Request.Url.AbsolutePath;

            var slide = db.Slides.Where(u => u.active == true).OrderBy(u => u.numberOder).ToList();
            var sologan = db.WhyChooseUsses.Where(u => u.active == true).OrderBy(u => u.numberOder).Take(4).ToList();//slogan cam kết

            ViewBag.slide = slide;
            ViewBag.sologan = sologan;
            isHome = true;
            ViewBag.isHome = isHome;
            ViewBag.menu = menu;

            return PartialView(allCate);
        }


    }
}