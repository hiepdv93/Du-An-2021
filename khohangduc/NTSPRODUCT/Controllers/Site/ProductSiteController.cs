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
        public ActionResult Index(string id)
        {
            ViewBag.key = id;
            #region[con fig]
            ViewBag.lang = lang;
            Config conf;
            if (ConfigModel.listConfig == null)
            {
                ConfigModel.listConfig = db.Configs.ToList();
            }
            conf = ConfigModel.listConfig.FirstOrDefault();
            ViewBag.conf = conf;
            #endregion
            List<string> cateid = new List<string>();
            var all = (from a in db.Products.AsNoTracking()
                       where a.pLang.Equals(lang)
                       && a.active == true
                       orderby a.proOrder
                       select a).AsQueryable();
            if (!string.IsNullOrEmpty(id))
            {
                #region[xu ly lay sp]
                var cateP = db.Categorys.FirstOrDefault(u => u.cateKey.Equals(id));
                ViewBag.cateP = cateP;

                if (cateP.cate_cap != 3)
                {
                    cateid = GetListId(cateP.id, cateP.cate_cap.Value);
                }
                else
                {
                    cateid.Add(cateP.id);
                }
                all = all.Where(u => cateid.Contains(u.cateId));

                #region[load seo]
                ViewBag.title = cateP.titleSeo;
                ViewBag.description = cateP.desSeo;
                ViewBag.keywords = cateP.keySeo;
                ViewBag.url = HttpContext.Request.Url.AbsoluteUri;
                ViewBag.img = ClassExten.GetUrlHost() + cateP.cateImage;
                ViewBag.favicon = ClassExten.GetUrlHost() + conf.favicon;
                #endregion

                #endregion

            }
            else
            {
                #region[load seo]
                ViewBag.title = conf.titleSeo;
                ViewBag.description = conf.desSeo;
                ViewBag.keywords = conf.keySeo;
                ViewBag.url = HttpContext.Request.Url.AbsoluteUri;
                ViewBag.img = ClassExten.GetUrlHost() + conf.logoTop;
                ViewBag.favicon = ClassExten.GetUrlHost() + conf.favicon;
                #endregion
            }

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

        public List<string> GetListId(string idp, int cap)
        {
            List<string> list = new List<string>();
            if (cap == 1)
            {
                var allCate = db.Categorys.Where(u => u.cateType == ClassExten.typeProduct && u.cateActive == true).ToList();

                list = allCate.Where(u => u.cateType == ClassExten.typeProduct && u.catepar_id.Equals(idp) && u.cate_cap == 2 && u.cateActive == true).Select(u => u.id).ToList();
                var listCap3 = allCate.Where(u => u.cateType == ClassExten.typeProduct && u.cate_cap == 3 && u.cateActive == true && list.Contains(u.catepar_id)).Select(u => u.id).ToList();
                foreach (var item in listCap3)
                {
                    list.Add(item);
                }
            }
            else
            {
                list = db.Categorys.Where(u => u.cateType == ClassExten.typeProduct && u.catepar_id.Equals(idp) && u.cate_cap == 3 && u.cateActive == true).Select(u => u.id).ToList();
            }
            list.Add(idp);
            return list;
        }

        public ActionResult Detail(string id)
        {
            List<Product> proOther = new List<Product>();
            // Category catePro;
            ViewBag.lang = lang;
            Config conf;
            if (ConfigModel.listConfig == null)
            {
                ConfigModel.listConfig = db.Configs.ToList();
            }
            conf = ConfigModel.listConfig.FirstOrDefault();
            ViewBag.conf = conf;
            int numberPro = 6;
            if (conf.viewProPageDetail != null)
            {
                numberPro = conf.viewProPageDetail.Value;
            }
            var proData = db.Products.FirstOrDefault(u => u.pro_key.Equals(id));
            if (proData != null)
            {
                var protag = (from a in db.ProTags.AsNoTracking()
                              join b in db.Tagproes.AsNoTracking() on a.tagId equals b.tagId
                              where a.proId.Equals(proData.id)
                              select b).OrderBy(u => u.tagOrder).ToList();

                var cateP = db.Categorys.FirstOrDefault(u => u.id.Equals(proData.cateId));
                var catePSub = db.Categorys.FirstOrDefault(u => u.id.Equals(cateP.catepar_id));
                #region[load seo]
                ViewBag.title = proData.titleSeo;
                ViewBag.description = proData.desSeo;
                ViewBag.keywords = proData.keySeo;
                ViewBag.url = HttpContext.Request.Url.AbsoluteUri;
                ViewBag.img = ClassExten.GetUrlHost() + proData.proAvata;
                ViewBag.favicon = ClassExten.GetUrlHost() + conf.favicon;
                #endregion
                ViewBag.pro = proData;
                ViewBag.cateP = cateP;
                ViewBag.catePSub = catePSub;
                ViewBag.protag = protag;
                #region[lay cac bai lien quan]
                proOther = db.Products.Where(u => u.active == true && u.cateId.Equals(proData.cateId) && !u.id.Equals(proData.id)).OrderBy(u => u.proOrder).Take(numberPro).ToList();
                #endregion
            }
            return View(proOther);
        }

        public ActionResult Search(string id)
        {
            id = id.ToLower();
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
            List<string> cateid = new List<string>();
            var all = (from a in db.Products.AsNoTracking()
                       where a.pLang.Equals(lang)
                       && a.active == true && a.pro_name.ToLower().Contains(id)
                       orderby a.proOrder
                       select a).AsQueryable();

            ViewBag.cateName = ClassExten.GetLangKey("key_timkiem") + ": " + id;
            #region[load seo]
            ViewBag.title = conf.titleSeo;
            ViewBag.description = conf.desSeo;
            ViewBag.keywords = conf.keySeo;
            ViewBag.url = HttpContext.Request.Url.AbsoluteUri;
            ViewBag.img = ClassExten.GetUrlHost() + conf.logoTop;
            ViewBag.favicon = ClassExten.GetUrlHost() + conf.favicon;
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

        public ActionResult ThuongHieu(string id)
        {
            ViewBag.key = id;
            #region[con fig]
            ViewBag.lang = lang;
            Config conf;
            if (ConfigModel.listConfig == null)
            {
                ConfigModel.listConfig = db.Configs.ToList();
            }
            conf = ConfigModel.listConfig.FirstOrDefault();
            ViewBag.conf = conf;
            #endregion
            List<string> cateid = new List<string>();
            var all = (from a in db.Products.AsNoTracking()
                       where a.active == true
                       orderby a.proOrder
                       select a).AsQueryable();
            if (!string.IsNullOrEmpty(id))
            {
                #region[xu ly lay sp]
                var cateP = db.Brands.FirstOrDefault(u => u.bkKey.Equals(id));
                if (cateP == null)
                {
                    return Redirect("/danh-muc");
                }
                ViewBag.cateP = cateP;


                all = all.Where(u => u.brandId.Equals(cateP.id));

                #endregion
                #region[load seo]
                ViewBag.title = conf.titleSeo;
                ViewBag.description = conf.desSeo;
                ViewBag.keywords = conf.keySeo;
                ViewBag.url = HttpContext.Request.Url.AbsoluteUri;
                ViewBag.img = ClassExten.GetUrlHost() + conf.logoTop;
                ViewBag.favicon = ClassExten.GetUrlHost() + conf.favicon;
                #endregion
            }
            else
            {
                return Redirect("/danh-muc");
            }

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
            conf = ConfigModel.listConfig.FirstOrDefault();
            ViewBag.conf = conf;
            #endregion
            var tag = db.Tagproes.FirstOrDefault(u => u.tagKey.Equals(id));
            if (tag != null)
            {
                var all = (from a in db.Products.AsNoTracking()
                           where a.active == true
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
                ViewBag.favicon = ClassExten.GetUrlHost() + conf.favicon;

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