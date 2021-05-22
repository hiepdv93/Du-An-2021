using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NTSPRODUCT.Models;
namespace NTSPRODUCT.Controllers.Site
{
    public class NewSiteController : Controller
    {
        NTSWEBEntities db = new NTSWEBEntities();
        string lang = ClassExten.GetLangSite();
        // GET: NewSite
        public ActionResult Index(string id)
        {
            ViewBag.lang = lang;
            Config conf;
            if (ConfigModel.listConfig == null)
            {
                ConfigModel.listConfig = db.Configs.ToList();
            }
            conf = ConfigModel.listConfig.FirstOrDefault(u => u.conLang.Equals(lang));
            ViewBag.conf = conf;
            string page = "1";//so phan trang hien tai
            var pagesize = 6;//so ban ghi tren 1 trang
            var numOfNews = 0;//tong so ban ghi co duoc truoc khi phan trang
            int curpage = 0; // trang hien tai dung cho phan trang
            if (Request["page"] != null)
            {
                page = Request["page"];
                curpage = Convert.ToInt32(page) - 1;
            }
            pagesize = conf.viewNewPageList.Value;
            var all = (from a in db.News.AsNoTracking()
                       where a.newLang.Equals(lang)
                       && a.status == Constants.Active
                       orderby a.newOrder
                       select a).AsQueryable();

            List<string> cateid = new List<string>();
            if (!string.IsNullOrEmpty(id))
            {
                var cateP = db.Categorys.FirstOrDefault(u => u.cateLang.Equals(lang) && u.cateType == ClassExten.typeNew && u.cateKey.Equals(id));
                if (cateP != null)
                {
                    ViewBag.cateP = cateP;
                    #region[load seo]
                    ViewBag.title = cateP.titleSeo;
                    ViewBag.description = cateP.desSeo;
                    ViewBag.keywords = cateP.keySeo;
                    ViewBag.url = HttpContext.Request.Url.AbsoluteUri;
                    ViewBag.img = ClassExten.GetUrlHost() + cateP.cateImage;
                    ViewBag.favicon = ClassExten.GetUrlHost() + conf.favicon;
                    #endregion
                    if (cateP.cate_cap != 3)
                    {
                        cateid = GetListId(cateP.id, cateP.cate_cap.Value);
                    }
                    else
                    {
                        cateid.Add(cateP.id);
                    }
                }
                all = all.Where(u => cateid.Contains(u.groupId));
            }
            else
            {
                ViewBag.key_tintuc = ClassExten.GetLangKey("news");
                #region[load seo]
                ViewBag.title = conf.titleSeo;
                ViewBag.description = conf.desSeo;
                ViewBag.keywords = conf.keySeo;
                ViewBag.url = HttpContext.Request.Url.AbsoluteUri;
                ViewBag.img = ClassExten.GetUrlHost() + conf.logoTop;
                ViewBag.favicon = ClassExten.GetUrlHost() + conf.favicon;
                #endregion
            }


            numOfNews = all.Select(u => u.id).Count();
            var data = all.Skip(curpage * pagesize).Take(pagesize).ToList();
            var url = Request.Path;
            if (numOfNews > pagesize)
            {
                ViewBag.pages = NTSPRODUCT.Models.Phantrang.PhanTrangSite(pagesize, curpage, numOfNews, url);
            }
            return View(data);
        }

        public ActionResult Detail(string id)
        {
            List<News> newOther = new List<News>();

            ViewBag.lang = lang;
            Config conf;
            if (ConfigModel.listConfig == null)
            {
                ConfigModel.listConfig = db.Configs.ToList();
            }
            conf = ConfigModel.listConfig.FirstOrDefault(u => u.conLang.Equals(lang));
            ViewBag.conf = conf;

            var newData = db.News.FirstOrDefault(u => u.new_key.Equals(id) && u.newLang.Equals(lang));
            if (newData != null)
            {
                ViewBag.cateP = db.Categorys.FirstOrDefault(u => u.id.Equals(newData.groupId));
                #region[load seo]
                ViewBag.title = newData.titleSeo;
                ViewBag.description = newData.desSeo;
                ViewBag.keywords = newData.keySeo;
                ViewBag.url = HttpContext.Request.Url.AbsoluteUri;
                ViewBag.img = ClassExten.GetUrlHost() + newData.newImage;
                ViewBag.favicon = ClassExten.GetUrlHost() + conf.favicon;
                #endregion
                ViewBag.newData = newData;
                #region[lay cac bai lien quan]
                newOther = db.News.Where(u => u.status == Constants.Active && u.groupId.Equals(newData.groupId) && !u.id.Equals(newData.id)).OrderByDescending(u => u.createDate).Take(conf.viewNewPageDetail.Value).ToList();
                #endregion
            }
            return View(newOther);
        }

        public List<string> GetListId(string idp, int cap)
        {
            List<string> list = new List<string>();
            if (cap == 1)
            {
                var allCate = db.Categorys.Where(u => u.cateLang.Equals(lang) && u.cateType == ClassExten.typeNew && u.cateActive == true).ToList();

                list = allCate.Where(u => u.cateType == ClassExten.typeNew && u.cateLang.Equals(lang) && u.catepar_id.Equals(idp) && u.cate_cap == 2 && u.cateActive == true).Select(u => u.id).ToList();
                var listCap3 = allCate.Where(u => u.cateType == ClassExten.typeNew && u.cateLang.Equals(lang) && u.cate_cap == 3 && u.cateActive == true && list.Contains(u.catepar_id)).Select(u => u.id).ToList();
                foreach (var item in listCap3)
                {
                    list.Add(item);
                }
            }
            else
            {
                list = db.Categorys.Where(u => u.cateType == ClassExten.typeNew && u.cateLang.Equals(lang) && u.catepar_id.Equals(idp) && u.cate_cap == 3 && u.cateActive == true).Select(u => u.id).ToList();
            }
            list.Add(idp);
            return list;
        }

        public ActionResult SiteContent(string id)
        {
            ViewBag.lang = lang;
            Config conf;
            if (ConfigModel.listConfig == null)
            {
                ConfigModel.listConfig = db.Configs.ToList();
            }
            conf = ConfigModel.listConfig.FirstOrDefault(u => u.conLang.Equals(lang));

            ViewBag.conf = conf;

            var menu = db.Menus.FirstOrDefault(u => u.link.Equals("/sites/" + id) && u.active == true && u.mtype == 2 && u.mLang.Equals(lang));
            if (menu != null)
            {
                #region[load seo]
                ViewBag.title = menu.titleSeo;
                ViewBag.description = menu.desSeo;
                ViewBag.keywords = menu.keySeo;
                ViewBag.url = HttpContext.Request.Url.AbsoluteUri;
                ViewBag.img = ClassExten.GetUrlHost() + conf.logoTop;
                ViewBag.favicon = ClassExten.GetUrlHost() + conf.favicon;
                #endregion
                #region[lay cac bai lien quan]
                var menus = db.Menus.Where(u => !u.id.Equals(menu.id) && u.active == true && u.mtype == 2 && u.mLang.Equals(lang));
                ViewBag.menu = menu;
                ViewBag.menus = menus;
                #endregion
            }
            return View();
        }
        public ActionResult Search(string id)
        {
            id = id.ToLower();
            ViewBag.id = id;
            ViewBag.lang = lang;
            Config conf;
            if (ConfigModel.listConfig == null)
            {
                ConfigModel.listConfig = db.Configs.ToList();
            }
            conf = ConfigModel.listConfig.FirstOrDefault(u => u.conLang.Equals(lang));

            string page = "1";//so phan trang hien tai
            var pagesize = 6;//so ban ghi tren 1 trang
            var numOfNews = 0;//tong so ban ghi co duoc truoc khi phan trang
            int curpage = 0; // trang hien tai dung cho phan trang
            if (Request["page"] != null)
            {
                page = Request["page"];
                curpage = Convert.ToInt32(page) - 1;
            }
            pagesize = conf.viewNewPageList.Value;
            var all = (from a in db.News.AsNoTracking()
                       where a.newLang.Equals(lang)
                       && a.status == Constants.Active
                       && a.title.ToLower().Contains(id)
                       orderby a.createDate descending
                       select a).AsQueryable();

            List<string> cateid = new List<string>();
            ViewBag.key_tintuc = ClassExten.GetLangKey("key_timkiem") + ": " + id;
            #region[load seo]
            ViewBag.title = conf.titleSeo;
            ViewBag.description = conf.desSeo;
            ViewBag.keywords = conf.keySeo;
            ViewBag.url = HttpContext.Request.Url.AbsoluteUri;
            ViewBag.img = ClassExten.GetUrlHost() + conf.logoTop;
            ViewBag.favicon = ClassExten.GetUrlHost() + conf.favicon;
            #endregion



            numOfNews = all.Select(u => u.id).Count();
            var data = all.Skip(curpage * pagesize).Take(pagesize).ToList();
            var url = Request.Path;
            if (numOfNews > pagesize)
            {
                ViewBag.pages = NTSPRODUCT.Models.Phantrang.PhanTrangSite(pagesize, curpage, numOfNews, url);
            }
            return View(data);
        }

        public ActionResult BangGia()
        {
            Config conf;
            if (ConfigModel.listConfig == null)
            {
                ConfigModel.listConfig = db.Configs.ToList();
            }
            conf = ConfigModel.listConfig.FirstOrDefault(u => u.conLang.Equals(lang));
            ViewBag.conf = conf;

            return View();
        }
    }
}