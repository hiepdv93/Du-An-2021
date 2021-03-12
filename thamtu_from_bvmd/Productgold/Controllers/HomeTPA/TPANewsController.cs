using Productgold.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Productgold.Controllers.HomeTPA
{
    public class TPANewsController : Controller
    {
        // GET: TPANews
        WEBTPAEntities db = new WEBTPAEntities();
        string lang = ClassExten.GetLangSite();
        string siteOption = ClassExten.siteOption;
        public ActionResult Index(string id)
        {
            ViewBag.NavTrangchu = ClassExten.GetLangKey("home");
            ViewBag.NavTintuc = ClassExten.GetLangKey("news");
            ViewBag.key_tintucsukien = ClassExten.GetLangKey("key_tintucsukien");
            ViewBag.XemThem = ClassExten.GetLangKey("key_xemthem"); 
             var number = db.sp_Config_SelectViewNumber(lang, siteOption).ToList();
            List<News> list = new List<News>();
            string page = "1";//so phan trang hien tai
            var pagesize = 6;//so ban ghi tren 1 trang
            var numOfNews = 0;//tong so ban ghi co duoc truoc khi phan trang
            int curpage = 0; // trang hien tai dung cho phan trang
            if (Request["page"] != null)
            {
                page = Request["page"];
                curpage = Convert.ToInt32(page) - 1;
            }
            try
            {
                pagesize = int.Parse(number[0].viewNews.Split(';')[1]);
            }
            catch (Exception)
            { }
            if (string.IsNullOrEmpty(id))
            {
                var config = db.sp_Config_SelectSeoOk(lang, siteOption).ToList();
                list = db.News.Where(u => u.newLang == lang && u.siteTPA == siteOption && u.active == true).OrderBy(u => u.newOrder).ToList();
                #region[load seo]
                ViewBag.title = config[0].titleSeo;
                ViewBag.description = config[0].desSeo;
                ViewBag.keywords = config[0].keySeo;
                ViewBag.url = HttpContext.Request.Url.AbsoluteUri;
                ViewBag.img = ClassExten.GetUrlHost() + config[0].logoTop;
                #endregion

            }
            else
            {
                var cateP = db.Categorys.FirstOrDefault(u => u.cateLang == lang && u.ctype == 2 && u.active == true && u.siteTPA == siteOption && u.keys.Equals(id));
                if (cateP != null)
                {
                    List<int> listId = new List<int>();
                    if (cateP.cate_cap != 3)
                    {
                        listId = GetListId(cateP.id, cateP.cate_cap.Value);
                    }
                    else
                    {
                        listId.Add(cateP.id);
                    }
                    ViewBag.key_tintucsukien = cateP.name;
                    ViewBag.TieudeDanhmuc = "<li><a href='#' title='" + cateP.name + "'>" + cateP.name + "</a></li>";
                    list = db.News.Where(u => u.newLang == lang && u.siteTPA == siteOption && u.active == true && listId.Contains(u.groupid.Value)).OrderBy(u => u.newOrder).ToList();
                }

            }
            var data = list.Skip(curpage * pagesize).Take(pagesize).ToList();
            var url = Request.Path;
            numOfNews = list.Count;
            if (numOfNews> pagesize)
            {
                ViewBag.pages = Productgold.Models.Phantrang.PhanTrang(pagesize, curpage, numOfNews, url);
            }
            return View(data);
        }
        public ActionResult Timkiem(string id)
        {
            ViewBag.NavTrangchu = ClassExten.GetLangKey("home");
            ViewBag.NavTintuc = ClassExten.GetLangKey("news");
            ViewBag.XemThem = ClassExten.GetLangKey("key_xemthem");
            ViewBag.key_timkiem = ClassExten.GetLangKey("key_timkiem");
            ViewBag.key_ketquatimkiem = ClassExten.GetLangKey("key_ketquatimkiem");
            ViewBag.keytimkiem = id;
            var number = db.sp_Config_SelectViewNumber(lang, siteOption).ToList();
            List<News> list = new List<News>();
            string page = "1";//so phan trang hien tai
            var pagesize = 6;//so ban ghi tren 1 trang
            var numOfNews = 0;//tong so ban ghi co duoc truoc khi phan trang
            int curpage = 0; // trang hien tai dung cho phan trang
            if (Request["page"] != null)
            {
                page = Request["page"];
                curpage = Convert.ToInt32(page) - 1;
            }
            try
            {
                pagesize = int.Parse(number[0].viewNews.Split(';')[1]);
            }
            catch (Exception)
            { }
            var config = db.sp_Config_SelectSeoOk(lang, siteOption).ToList();
            list = db.News.Where(u => u.newLang == lang && u.siteTPA == siteOption && u.active == true && u.title.ToLower().Contains(id.ToLower())).OrderBy(u => u.newOrder).ToList();
            #region[load seo]
            ViewBag.title = config[0].titleSeo;
            ViewBag.description = config[0].desSeo;
            ViewBag.keywords = config[0].keySeo;
            ViewBag.url = HttpContext.Request.Url.AbsoluteUri;
            ViewBag.img = ClassExten.GetUrlHost() + config[0].logoTop;
            #endregion
            var data = list.Skip(curpage * pagesize).Take(pagesize).ToList();
            var url = Request.Path;
            numOfNews = list.Count;
            if (numOfNews > pagesize)
            {
                ViewBag.pages = Productgold.Models.Phantrang.PhanTrang(pagesize, curpage, numOfNews, url);
            }
                return View(data);
        }
        public List<int> GetListId(int idp, int cap)
        {
            List<int> list = new List<int>();
            if (cap == 1)
            {
                list = db.Categorys.Where(u => u.cateLang == lang && u.siteTPA == siteOption && u.catepar_id == idp && u.cate_cap == 2 && u.active == true).Select(u => u.id).ToList();
                var listCap3 = db.Categorys.Where(u => u.cateLang == lang && u.siteTPA == siteOption && u.cate_cap == 3 && u.active == true && list.Contains(u.catepar_id.Value)).Select(u => u.id).ToList();
                foreach (var item in listCap3)
                {
                    list.Add(item);
                }
            }
            else
            {
                list = db.Categorys.Where(u => u.cateLang == lang && u.siteTPA == siteOption && u.catepar_id == idp && u.cate_cap == 3 && u.active == true).Select(u => u.id).ToList();
            }
            list.Add(idp);
            return list;
        }
        public ActionResult Menunoidung(string id)
        {
            var seo = db.sp_Config_SelectSeoOk(lang, siteOption).ToList();
            ViewBag.NavTrangchu = ClassExten.GetLangKey("home");
            var menu = db.Menus.FirstOrDefault(u => u.link.Equals("/Sites/" + id) && u.active == true && u.mtype == 2 && u.mLang == lang && u.siteTPA == siteOption);
            if (menu != null)
            {
                #region[load seo]
                ViewBag.title = menu.titleSeo;
                ViewBag.description = menu.desSeo;
                ViewBag.keywords = menu.keySeo;
                ViewBag.url = HttpContext.Request.Url.AbsoluteUri;
                if (seo.Count > 0)
                {
                    ViewBag.img = ClassExten.GetUrlHost() + seo[0].logoTop;
                }
                #endregion
                ViewBag.NavCon = menu.name;
                #region[lay cac bai lien quan]
                string MenuOther = "";
                var menuList = db.Menus.Where(u => !u.link.Equals("/Sites/" + id) && u.active == true && u.mtype == 2 && u.mLang == lang).OrderBy(u => u.mOrder).Take(3).ToList();
                if (menuList.Count > 0)
                {
                    MenuOther += "<h4>" + ClassExten.GetLangKey("key_baivietcungchuyenmuc") + "</h4>";
                    MenuOther += "<ul>";
                    foreach (var item in menuList)
                    {
                        MenuOther += "<li><a href='" + item.link + "' title='" + item.name + "'>" + item.name + "</a></li>";
                    }
                    MenuOther += "</ul>";
                }
                ViewBag.MenuOther = MenuOther;
                #endregion
            }
            return View(menu);
        }
        public ActionResult Chitiet(string id)
        {
            var number = db.sp_Config_SelectViewNumber(lang, siteOption).ToList();
            int numberView = 5;
            try
            {
                numberView = int.Parse(number[0].viewNews.Split(';')[2]);
            }
            catch (Exception)
            { }
            ViewBag.NavTintuc = ClassExten.GetLangKey("news");
            ViewBag.NavTrangchu = ClassExten.GetLangKey("home");
            var newData = db.News.FirstOrDefault(u => u.new_key.Equals(id) && u.newLang == lang && u.siteTPA == siteOption);
            if (newData != null)
            {
                #region[load seo]
                ViewBag.title = newData.titleSeo;
                ViewBag.description = newData.desSeo;
                ViewBag.keywords = newData.keySeo;
                ViewBag.url = HttpContext.Request.Url.AbsoluteUri;
                ViewBag.img = ClassExten.GetUrlHost() + newData.images;
                #endregion
                var nhomCha = db.Categorys.FirstOrDefault(u => u.id == newData.groupid);
                ViewBag.NavCha = nhomCha.name;
                ViewBag.NavChakeys = nhomCha.keys;
                ViewBag.NavCon = newData.title;
                int numView = 1;
                try
                {
                    numView = newData.viewCount.Value + 1;
                    newData.viewCount = numView;
                    db.SaveChanges();
                }
                catch (Exception)
                { }
                #region[lay cac bai lien quan]
                string MenuOther = "";
                var menuList = db.News.Where(u => !u.new_key.Equals(id) && u.active == true && u.groupid == newData.groupid).OrderBy(u => u.newOrder).Take(numberView).ToList();
                if (menuList.Count > 0)
                {
                    MenuOther += "<h4>" + ClassExten.GetLangKey("key_baivietcungchuyenmuc") + "</h4>";
                    MenuOther += "<ul>";
                    foreach (var item in menuList)
                    {
                        MenuOther += "<li><a href='/Tin/" + item.new_key + "' title='" + item.title + "'>" + item.title + "<span class='box_news_right_list_news_title_view'>(" + item.viewCount + " " + ClassExten.GetLangKey("key_luotxem") + ")</span></a></li>";
                    }
                    MenuOther += "</ul>";
                }
                ViewBag.MenuOther = MenuOther;
                #endregion
            }
            return View(newData);
        }

        public ActionResult Thuvien(string id)
        {
            ViewBag.NavTrangchu = ClassExten.GetLangKey("home");
            ViewBag.Navthuvien = ClassExten.GetLangKey("key_thuvien");
            ViewBag.Download = ClassExten.GetLangKey("key_Download");
            ViewBag.Downloadtailieu = ClassExten.GetLangKey("key_Downloadtailieu");

            ViewBag.key_mota = ClassExten.GetLangKey("key_mota");
            ViewBag.key_nhomsp = ClassExten.GetLangKey("key_nhomsp");
            //bang
            ViewBag.Anhsp = ClassExten.GetLangKey("key_Anhsp");
            ViewBag.Tensp = ClassExten.GetLangKey("key_Tensp");
            ViewBag.thongsokt = ClassExten.GetLangKey("key_thongsokt");
            ViewBag.Danhmuc = ClassExten.GetLangKey("key_Danhmuc");


            var number = db.sp_Config_SelectViewNumber(lang, siteOption).ToList();
            List<Video> list = new List<Video>();
            string page = "1";//so phan trang hien tai
            var pagesize = 6;//so ban ghi tren 1 trang
            var numOfNews = 0;//tong so ban ghi co duoc truoc khi phan trang
            int curpage = 0; // trang hien tai dung cho phan trang
            if (Request["page"] != null)
            {
                page = Request["page"];
                curpage = Convert.ToInt32(page) - 1;
            }
            try
            {

                pagesize = int.Parse(number[0].viewVideo.Split(';')[1]);
            }
            catch (Exception)
            { }
            if (string.IsNullOrEmpty(id))
            {
                var config = db.sp_Config_SelectSeoOk(lang, siteOption).ToList();
                list = db.Videos.Where(u => u.vLang == lang && u.siteTPA == siteOption).OrderBy(u => u.vOrder).ToList();
                #region[load seo]
                ViewBag.title = config[0].titleSeo;
                ViewBag.description = config[0].desSeo;
                ViewBag.keywords = config[0].keySeo;
                ViewBag.url = HttpContext.Request.Url.AbsoluteUri;
                ViewBag.img = ClassExten.GetUrlHost() + config[0].logoTop;
                #endregion

            }
            else
            {
                var cateP = db.Categorys.FirstOrDefault(u => u.cateLang == lang && u.ctype == 3 && u.active == true && u.siteTPA == siteOption && u.keys.Equals(id));
                if (cateP != null)
                {
                    List<int> listId = new List<int>();
                    if (cateP.cate_cap != 3)
                    {
                        listId = GetListId(cateP.id, cateP.cate_cap.Value);
                    }
                    else
                    {
                        listId.Add(cateP.id);
                    }
                    ViewBag.TieudeDanhmuc = "<li><a href='#' title='" + cateP.name + "'>" + cateP.name + "</a></li>";
                    list = db.Videos.Where(u => u.vLang == lang && u.siteTPA == siteOption && listId.Contains(u.cateId.Value)).OrderBy(u => u.vOrder).ToList();
                }

            }
            var data = list.Skip(curpage * pagesize).Take(pagesize).ToList();
            var url = Request.Path;
            numOfNews = list.Count;
            ViewBag.pages = Productgold.Models.Phantrang.PhanTrang(pagesize, curpage, numOfNews, url);
            return View(data);
        }

        [HttpPost]
        public ActionResult Timkiem(FormCollection fr)
        {
            string txtTimkiem = fr["txtTimkiem"];
            return Redirect("/Tim-tin/"+txtTimkiem);
        }
    }
}