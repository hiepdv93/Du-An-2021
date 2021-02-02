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
        public ActionResult ChildHeader(string path)
        {
            int countSp = 0;
            var cartGet = ClassExten.GetCokiesCart();
            if (cartGet!=null)
            {
                countSp = cartGet.CartItems.Count;
            }
            ViewBag.countSp = countSp;
            bool isHome = false;
            if (path.Equals("") || path.Equals("/"))
            {
            }
            isHome = true;
            var qcTop = db.Advs.FirstOrDefault(u => u.advActive == true && u.advType == 1);
            ViewBag.qcTop = qcTop;
            Config conf;
            if (ConfigModel.listConfig == null)
            {
                ConfigModel.listConfig = db.Configs.ToList();
            }
            conf = ConfigModel.listConfig.FirstOrDefault();
            ViewBag.conf = conf;
            ViewBag.isHome = isHome;
            return PartialView();
        }

        public ActionResult RightMenu()
        {
            var menu = db.Menus.Where(u => u.mPosition == 1 && u.active == true).ToList();
            return PartialView(menu);
        }

        public ActionResult LeftCate()
        {
            if (ConfigModel.listCate == null || true)
            {
                ConfigModel.listCate = db.Categorys.Where(u => u.cateActive == true && u.cateType == ClassExten.typePro).ToList();
            }
            var cate = ConfigModel.listCate.Where(u => u.cateType == ClassExten.typePro && u.cateActive == true).OrderBy(u => u.cateOrder).ToList();
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
            bool isMobile = false;
            if (Request.Browser.IsMobileDevice)
            {
                isMobile = true;
            }
            ViewBag.isMobile = isMobile;
            List<ProductHome> productHomes = new List<ProductHome>();
            ProductHome productHome;

            Config conf;
            if (ConfigModel.listConfig == null)
            {
                ConfigModel.listConfig = db.Configs.ToList();
            }
            if (ConfigModel.listCate == null || true)
            {
                ConfigModel.listCate = db.Categorys.Where(u => u.cateActive == true && u.cateType == ClassExten.typePro).ToList();
            }
            conf = ConfigModel.listConfig.FirstOrDefault();
            int nProductHome = 8;
            if (conf.viewProPageHome.HasValue)
            {
                nProductHome = conf.viewProPageHome.Value;
            }
            var products = db.Products.Where(u => u.active == true).ToList();
            List<string> listPId;
            List<string> listPId_Cap2;

            var brand = db.Brands.OrderBy(u => u.numberOder).ToList();
            var slogan = db.Sologans.Where(u => u.status == true).OrderBy(u => u.dOrder).ToList();
            var qc = db.Advs.Where(u => u.advActive == true && u.advType==2).OrderBy(u => u.advOrder).Take(3).ToList();
            //var pn = db.Partners.Where(u =>  u.groupType == Constants.Partner).OrderBy(u => u.numberOder).ToList();

            var cateP = ConfigModel.listCate.Where(u => u.catepar_id.Equals(ClassExten.cateParent) && u.cateActiveHome == true).OrderBy(u => u.cateOrder).ToList();
            foreach (var item in cateP)
            {
                listPId = new List<string>();
                productHome = new ProductHome();
                productHome.Item = item;
                productHome.ListItem = ConfigModel.listCate.Where(u => u.catepar_id.Equals(item.id) && u.cateActive == true).OrderBy(u => u.cateOrder).ToList();

                listPId_Cap2 = productHome.ListItem.Select(u => u.id).ToList();
                listPId.Add(item.id);
                listPId.AddRange(listPId_Cap2);
                listPId.AddRange(GetListId_Cap3(listPId_Cap2));

                productHome.ListProduct = products.Where(u => u.active == true && listPId.Contains(u.groupId)).OrderBy(u => u.proOrder).Take(nProductHome).ToList();

                productHomes.Add(productHome);
            }
            // ViewBag.pn = pn;
            ViewBag.qc = qc;
            ViewBag.conf = conf;
            ViewBag.brand = brand;
            ViewBag.slogan = slogan;


            return PartialView(productHomes);
        }
        public List<string> GetListId_Cap3(List<string> pId)
        {
            List<string> list = new List<string>();
            foreach (var item in pId)
            {
                list.AddRange(ConfigModel.listCate.Where(u => u.cateType == ClassExten.typePro && u.catepar_id.Equals(item) && u.cateActive == true).Select(u => u.id).ToList());
            }
            return list;
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
            var catepro = db.Categorys.Where(u =>  u.cateType == ClassExten.typePro && u.cateActive == true && u.catepar_id.Equals("-1")).ToList();
            var slide = db.Slides.Where(u =>  u.active == true).OrderBy(u => u.numberOder).ToList();
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

            var chooseWe = db.WhyChooseUsses.Where(u =>  u.active == true).OrderBy(u => u.numberOder).ToList();
            var partner = (from a in db.Partners.AsNoTracking()
                           orderby a.numberOder
                           select a).ToList();
            ViewBag.partner = partner;
            ViewBag.chooseWe = chooseWe;
            return PartialView();
        }
        // [OutputCache(Duration = ClassExten.timeCacheChild, VaryByParam = "lang")]
        public ActionResult ChildFooter()
        {
            //var isMb = Request.Browser.IsMobileDevice;
            //ViewBag.isMb = isMb;
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
        //[OutputCache(Duration = ClassExten.timeCacheChild, VaryByParam = "lang")]
        public ActionResult PageNewLeft(string lang)
        {
            var cateNew = db.Categorys.Where(u => u.cateActive == true && u.cateType == ClassExten.typeNew).ToList();
            ViewBag.cateNew = cateNew;
            var newsNew = db.News.Where(u =>  u.status == Constants.Active && u.newNew == true).OrderBy(u => u.newOrder).Take(6).ToList();
            return PartialView(newsNew);
        }
        #endregion

        #region[bên trái trang sản phẩm]
        //[OutputCache(Duration = ClassExten.timeCacheChild, VaryByParam = "lang")]
        public ActionResult PageProLeft(Category category)
        {
            if (ConfigModel.listCate == null || true)
            {
                ConfigModel.listCate = db.Categorys.Where(u => u.cateActive == true && u.cateType == ClassExten.typePro).ToList();
            }
            List<Category> listCate;
            if (category != null)
            {
                if (category.cate_cap == 1)
                {
                    ViewBag.categoryP = category;
                    listCate = ConfigModel.listCate.Where(u => u.cateActive == true && u.catepar_id.Equals(category.id)).OrderBy(u => u.cateOrder).ToList();
                }
                else
                {
                    ViewBag.categoryP = ConfigModel.listCate.FirstOrDefault(u => u.id.Equals(category.catepar_id));

                    listCate = ConfigModel.listCate.Where(u => u.cateActive == true && u.catepar_id.Equals(category.catepar_id)).OrderBy(u => u.cateOrder).ToList();
                }
            }
            else
            {

                listCate = ConfigModel.listCate.Where(u => u.cateActive == true && u.catepar_id.Equals(ClassExten.cateParent)).OrderBy(u => u.cateOrder).ToList();
            }
            var brands = db.Brands.OrderBy(u => u.numberOder).ToList();
            ViewBag.brands = brands;
            ViewBag.category = category;

            return PartialView(listCate);
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