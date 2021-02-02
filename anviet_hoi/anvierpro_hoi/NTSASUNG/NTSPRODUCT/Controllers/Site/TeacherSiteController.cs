using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NTSPRODUCT.Models;
namespace NTSPRODUCT.Controllers.Site
{
    public class TeacherSiteController : Controller
    {
        NTSWEBEntities db = new NTSWEBEntities();
        string lang = ClassExten.GetLangSite();
        // GET: TeacherSite
        public ActionResult Index()
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
            var pagesize = 9;//so ban ghi tren 1 trang
            var numOfNews = 0;//tong so ban ghi co duoc truoc khi phan trang
            int curpage = 0; // trang hien tai dung cho phan trang
            if (Request["page"] != null)
            {
                page = Request["page"];
                curpage = Convert.ToInt32(page) - 1;
            }
           // pagesize = conf.viewNewPageList.Value;
            var all = (from a in db.Teachers.AsNoTracking()
                       where a.lang.Equals(lang)
                       && a.status == true
                       orderby a.tOrder
                       select a).AsQueryable();

            List<string> cateid = new List<string>();

            ViewBag.giangvien = ClassExten.GetLangKey("key_ttgiangvien");
            #region[load seo]
            ViewBag.title = conf.titleSeo;
            ViewBag.description = conf.desSeo;
            ViewBag.keywords = conf.keySeo;
            ViewBag.url = HttpContext.Request.Url.AbsoluteUri;
            ViewBag.img = ClassExten.GetUrlHost() + conf.logoTop;
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

        public ActionResult Detail(string id)
        {
            List<Teacher> newOther = new List<Teacher>();
            ViewBag.lang = lang;
            Config conf;
            if (ConfigModel.listConfig == null)
            {
                ConfigModel.listConfig = db.Configs.ToList();
            }
            conf = ConfigModel.listConfig.FirstOrDefault(u => u.conLang.Equals(lang));
            var newData = db.Teachers.FirstOrDefault(u => u.tkey.Equals(id) && u.lang.Equals(lang));
            if (newData != null)
            {
                #region[load seo]
                ViewBag.title = newData.titleSeo;
                ViewBag.description = newData.desSeo;
                ViewBag.keywords = newData.keySeo;
                ViewBag.url = HttpContext.Request.Url.AbsoluteUri;
                ViewBag.img = ClassExten.GetUrlHost() + newData.avata;
                #endregion
                ViewBag.newData = newData;
                #region[lay cac bai lien quan]
                newOther = db.Teachers.Where(u => u.lang.Equals(lang) && u.status == true ).OrderByDescending(u => u.createDate).Take(5).ToList();
                #endregion
            }
            return View(newOther);
        }

    }
}