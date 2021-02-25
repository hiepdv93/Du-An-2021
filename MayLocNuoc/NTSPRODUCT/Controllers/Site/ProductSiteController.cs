using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NTSPRODUCT.Models;
namespace NTSPRODUCT.Controllers.Site
{
    public class ProductSiteController : Controller
    {
        NTSWEBEntities db = new NTSWEBEntities();
        string lang = ClassExten.GetLangSite();
        // GET: ProductSite

        //sản phẩm(trai la giai phap, ben phai la san pham)
        public ActionResult Index()
        {
            Config conf;
            if (ConfigModel.listConfig == null)
            {
                ConfigModel.listConfig = db.Configs.ToList();
            }
            conf = ConfigModel.listConfig.FirstOrDefault();
            ViewBag.conf = conf;
            #region[load seo]
            ViewBag.title = conf.titleSeo;
            ViewBag.description = conf.desSeo;
            ViewBag.keywords = conf.keySeo;
            ViewBag.url = HttpContext.Request.Url.AbsoluteUri;
            ViewBag.img = ClassExten.GetUrlHost() + conf.logoTop;
            #endregion

            ViewBag.slide = db.Slides.FirstOrDefault(u => u.active == true);
            var menu = db.Menus.Where(u => u.active == true).ToList();
            var news = db.News.Where(u => u.status == Constants.Active && u.newHot == true).OrderBy(u => u.newOrder).Take(5).ToList();
            var sologan = db.Sologans.Where(u => u.status == true).OrderBy(u => u.dOrder).Take(4).ToList();
            var pro = db.Products.Where(u => u.active == true).OrderBy(u => u.proOrder).ToList();
            ViewBag.menu = menu;
            ViewBag.news = news;
            ViewBag.sologan = sologan;
            return View(pro);
        }

        public List<string> GetListId(string idp, int cap)
        {
            List<string> list = new List<string>();
            if (cap == 1)
            {

                list = ConfigModel.listCate.Where(u => u.cateType == ClassExten.typePro && u.catepar_id.Equals(idp) && u.cate_cap == 2 && u.cateActive == true).Select(u => u.id).ToList();
                var listCap3 = ConfigModel.listCate.Where(u => u.cateType == ClassExten.typePro && u.cate_cap == 3 && u.cateActive == true && list.Contains(u.catepar_id)).Select(u => u.id).ToList();
                foreach (var item in listCap3)
                {
                    list.Add(item);
                }
            }
            else
            {
                list = ConfigModel.listCate.Where(u => u.cateType == ClassExten.typePro && u.catepar_id.Equals(idp) && u.cate_cap == 3 && u.cateActive == true).Select(u => u.id).ToList();
            }
            list.Add(idp);
            return list;
        }
        public ActionResult Detail(string id)
        {
            Config conf;
            if (ConfigModel.listConfig == null)
            {
                ConfigModel.listConfig = db.Configs.ToList();
            }
            conf = ConfigModel.listConfig.FirstOrDefault();
            ViewBag.conf = conf;
            #region[load seo]
            ViewBag.title = conf.titleSeo;
            ViewBag.description = conf.desSeo;
            ViewBag.keywords = conf.keySeo;
            ViewBag.url = HttpContext.Request.Url.AbsoluteUri;
            ViewBag.img = ClassExten.GetUrlHost() + conf.logoTop;
            #endregion

            ViewBag.slide = db.Slides.FirstOrDefault(u => u.active == true);
            var menu = db.Menus.Where(u => u.active == true).ToList();
            var news = db.News.Where(u => u.status == Constants.Active && u.newHot == true).OrderBy(u => u.newOrder).Take(5).ToList();
            var sologan = db.Sologans.Where(u => u.status == true).OrderBy(u => u.dOrder).Take(4).ToList();
            var pro = db.Products.Where(u => u.active == true).OrderBy(u => u.proOrder).ToList();
            ViewBag.menu = menu;
            ViewBag.news = news;
            ViewBag.sologan = sologan;

            List<Product> proOther = new List<Product>();
            var proData = pro.FirstOrDefault(u => u.pro_key.Equals(id));
            if (proData != null)
            {
                var cateP = db.Categorys.FirstOrDefault(u => u.id.Equals(proData.groupId));
                #region[load seo]
                ViewBag.title = proData.titleSeo;
                ViewBag.description = proData.desSeo;
                ViewBag.keywords = proData.keySeo;
                ViewBag.url = HttpContext.Request.Url.AbsoluteUri;
                ViewBag.img = ClassExten.GetUrlHost() + proData.proAvata;
                #endregion
                ViewBag.pro = proData;
                ViewBag.cateP = cateP;
                #region[lay cac bai lien quan]
                proOther = pro.Where(u => u.active == true && u.groupId.Equals(proData.groupId) ).OrderBy(u => u.proOrder).ToList();
                #endregion
                proData.pro_view = proData.pro_view != null ? proData.pro_view + 1 : 2;
                db.SaveChanges();
            }

            return View(proOther);
        }

        public ActionResult Search(string id)
        {
            id = id.ToLower();
            #region[con fig]
            Config conf;
            if (ConfigModel.listConfig == null)
            {
                ConfigModel.listConfig = db.Configs.ToList();
            }
            conf = ConfigModel.listConfig.FirstOrDefault(u => u.conLang.Equals(lang));
            ViewBag.conf = conf;
            #endregion
            var all = (from a in db.Products.AsNoTracking()
                       where a.active == true && a.pro_name.ToLower().Contains(id)
                       orderby a.proOrder
                       select a).AsQueryable();

            ViewBag.cateName = ClassExten.GetLangKey("key_timkiem") + ": " + id;
            #region[load seo]
            ViewBag.title = conf.titleSeo;
            ViewBag.description = conf.desSeo;
            ViewBag.keywords = conf.keySeo;
            ViewBag.url = HttpContext.Request.Url.AbsoluteUri;
            ViewBag.img = ClassExten.GetUrlHost() + conf.logoTop;
            #endregion
            string page = "1";//so phan trang hien tai
            var pagesize = 6;//so ban ghi tren 1 trang
            var numOfNews = 0;//tong so ban ghi co duoc truoc khi phan trang
            int curpage = 0; // trang hien tai dung cho phan trang
            if (Request["page"] != null)
            {
                page = Request["page"];
                curpage = Convert.ToInt32(page) - 1;
            }
            pagesize = conf.viewProPageList.Value;

            numOfNews = all.Select(u => u.id).Count();
            var data = all.Skip(curpage * pagesize).Take(pagesize).ToList();
            var url = Request.Path;
            if (numOfNews > pagesize)
            {
                ViewBag.pages = NTSPRODUCT.Models.Phantrang.PhanTrangSite(pagesize, curpage, numOfNews, url);
            }
            return View(data);
        }

        public ActionResult Tags(string id)
        {
            #region[con fig]
            ViewBag.lang = lang;
            Config conf;
            if (ConfigModel.listConfig == null)
            {
                ConfigModel.listConfig = db.Configs.ToList();
            }
            conf = ConfigModel.listConfig.FirstOrDefault(u => u.conLang.Equals(lang));
            ViewBag.conf = conf;
            #endregion
            var tag = db.Tagproes.FirstOrDefault(u => u.tagKey.Equals(id));
            if (tag != null)
            {
                var all = (from a in db.Products.AsNoTracking()
                           where a.pLang.Equals(lang) && a.active == true
                           join b in db.ProTags.AsNoTracking() on a.id equals b.proId
                           where b.tagId.Equals(tag.tagId)
                           orderby a.proOrder
                           select a).AsQueryable();
                ViewBag.cateName = "Tags: " + tag.tagName;
                #region[load seo]
                ViewBag.title = conf.titleSeo;
                ViewBag.description = conf.desSeo;
                ViewBag.keywords = conf.keySeo;
                ViewBag.url = HttpContext.Request.Url.AbsoluteUri;
                ViewBag.img = ClassExten.GetUrlHost() + conf.logoTop;
                #endregion
                string page = "1";//so phan trang hien tai
                var pagesize = 6;//so ban ghi tren 1 trang
                var numOfNews = 0;//tong so ban ghi co duoc truoc khi phan trang
                int curpage = 0; // trang hien tai dung cho phan trang
                if (Request["page"] != null)
                {
                    page = Request["page"];
                    curpage = Convert.ToInt32(page) - 1;
                }
                pagesize = conf.viewProPageList.Value;

                numOfNews = all.Select(u => u.id).Count();
                var data = all.Skip(curpage * pagesize).Take(pagesize).ToList();
                var url = Request.Path;
                if (numOfNews > pagesize)
                {
                    ViewBag.pages = NTSPRODUCT.Models.Phantrang.PhanTrangSite(pagesize, curpage, numOfNews, url);
                }
                return View(data);
            }
            else
            {
                ViewBag.cateName = "Tags";
                return View();
            }
        }

    }
}