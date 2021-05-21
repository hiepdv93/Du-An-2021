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
        public ActionResult ChildHeader(string lang)
        {
            Config conf;
            if (ConfigModel.listConfig == null)
            {
                ConfigModel.listConfig = db.Configs.ToList();
            }
            conf = ConfigModel.listConfig.FirstOrDefault(u => u.conLang.Equals(lang));

            var menu = db.Menus.Where(u => u.mPosition == 1 && u.active == true && u.par_id.Equals(ClassExten.cateParent)).OrderBy(u => u.mOrder).ToList();
            ViewBag.conf = conf;

            return PartialView(menu);
        }
        private ShoppingCartViewModel CountCart()
        {
            ShoppingCartViewModel cartGet = ClassExten.GetCokiesCart();

            return cartGet;
        }
        [OutputCache(Duration = ClassExten.timeCacheChild, VaryByParam = "lang")]
        public ActionResult ChildHome(string lang)
        {
            Config conf;
            if (ConfigModel.listConfig == null)
            {
                ConfigModel.listConfig = db.Configs.ToList();
            }
            conf = ConfigModel.listConfig.FirstOrDefault();

            ViewBag.conf = conf;
            int numberNewsHome = 6;
            if (conf.viewNewPageHome.HasValue)
            {
                numberNewsHome = conf.viewNewPageHome.Value;
            }
            var news = db.News.Where(u => u.status == Constants.Active).OrderBy(u => u.newOrder).Take(numberNewsHome).ToList(); ;
            ViewBag.news = news;

            var par = db.Partners.OrderBy(u => u.numberOder).Take(6).ToList(); ;
            ViewBag.par = par;

            var why = db.WhyChooseUsses.Where(u=>u.active==true).OrderBy(u => u.numberOder).Take(6).ToList(); ;
            ViewBag.why = why;

            var sup = db.Supports.OrderBy(u => u.numberOder).Take(4).ToList(); ;
            ViewBag.sup = sup;

            var slogan = db.Advs.Where(u => u.advActive == true && u.advType == ClassExten.TableType.AdvSlogan).OrderBy(u => u.advOrder).Take(5).ToList(); ;
            ViewBag.slogan = slogan;

            var slide = db.Slides.OrderBy(u => u.numberOder).FirstOrDefault(u => u.active == true);
            ViewBag.slide = slide;

            var SayWe = db.SayWes.OrderBy(u => u.numberOder).FirstOrDefault(u => u.active == true);
            ViewBag.SayWe = SayWe;

            var cate = db.Categorys.Where(u => u.cateActive == true && u.cateType == ClassExten.typeProduct && u.cateActiveHome == true).OrderBy(u => u.cateOrder).Take(6).ToList();
            ViewBag.cate = cate;

            var faq = db.Faqs.OrderBy(u => u.number).ToList();

            return PartialView(faq);
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