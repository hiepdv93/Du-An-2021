using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Productgold.Models;

using Google.Apis.Drive.v3;
using Google.Apis.Auth.OAuth2;
using System.Threading;
using Google.Apis.Util.Store;
using Google.Apis.Services;
using Google.Apis.Drive.v3.Data;
using System.Web.Script.Serialization;
using System.IO;

namespace Productgold.Controllers
{
    public class NewsController : BaseCookisController
    {

        //
        // GET: /News/

        WEBTPAEntities db = new WEBTPAEntities();
        string lang = ClassExten.GetLang();
        string siteTPA = ClassExten.siteOption;
        #region[ham view]
        public ActionResult Index()
        {
            string titleSearch = "";
            int groupSearch = -1;
            string group = "";
            if (Request["titleSearch"] != null)
            {
                titleSearch = Request["titleSearch"];
            }
            if (Request["groupSearch"]!=null)
            {
                group = Request["groupSearch"];
                if (!string.IsNullOrEmpty(group))
                {
                    groupSearch = Convert.ToInt32(group);
                }
            }
            string page = "1";//so phan trang hien tai
            var pagesize = 20;//so ban ghi tren 1 trang
            var numOfNews = 0;//tong so ban ghi co duoc truoc khi phan trang
            int curpage = 0; // trang hien tai dung cho phan trang
            if (Request["page"] != null)
            {
                page = Request["page"];
                curpage = Convert.ToInt32(page) - 1;
            }
            var all = db.News.Where(u => u.newLang == lang 
            && u.siteTPA == siteTPA
            && (string.IsNullOrEmpty(titleSearch) || u.title.ToLower().Contains(titleSearch.ToLower()))
            && (groupSearch==-1 || u.groupid==groupSearch)).OrderBy(u => u.newOrder).ToList();
            var data = all.Skip(curpage * pagesize).Take(pagesize).ToList();
            var url = Request.Path;
            numOfNews = all.Count;
            ViewBag.pages = Productgold.Models.Phantrang.PhanTrang(pagesize, curpage, numOfNews, url);
            var nhomtin = db.Categorys.Where(u => u.ctype == 2 && u.cateLang==lang && u.active == true && u.siteTPA == ClassExten.siteOption).OrderBy(u=>u.cateOrder).ToList();
            SelectList sl = new SelectList(nhomtin, "id", "name");
            ViewBag.nhomtin = sl;
            return View(data);
        }

        public ActionResult IndexSearch(FormCollection fr)
        {
            var titleSearch = fr["titleSearch"];
            var groupSearch = fr["groupSearch"];
            return Redirect("/News/Index?titleSearch="+ titleSearch+ "&groupSearch="+ groupSearch);
        }
        #endregion

        #region[ham sua form]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Update(FormCollection f, int id)
        {
            try
            {
                var data = db.News.First(u => u.id == id);
                string active = f["active"];
                string noibat = f["noibat"];
                
                var nhom = Convert.ToInt32(f["catep"]);
                data.groupid = nhom;
                string imgNew = f["img"];

                if (!string.IsNullOrEmpty(imgNew))
                {
                    if (data.images!= imgNew)
                    {
                      // var idImg= ClassExten.UploadGoogleDriver(imgNew, ClassExten.ForderNew);
                       // data.imagesId = idImg;
                    }
                }
                data.images = imgNew;

                data.title = f["title"];
                string key = StringClass.NameToTag(data.title);
                if (data.new_key != key)
                {
                    data.new_key = checkTrungUpdate(key, data.id);
                }
                data.descript = f["mota"];
                data.contents = f["contents"];
                data.createdate = DateTime.Now;
                data.newOrder = Convert.ToInt32(f["thutu"]);
                data.ntype = Convert.ToInt32(f["loai"]);
                data.active = true;
                if (active == null || active == "false")
                {
                    data.active = false;
                }
                data.ex1 = "1";
                if (noibat == null || noibat == "false")
                {
                    data.ex1 = "0";
                }
                data.titleSeo = f["titleseo"];
                data.keySeo = f["keyseo"];
                data.desSeo = f["desseo"];
                db.SaveChanges();
                Session["ok"] = "Cập nhật thành công!";

                return Redirect("/News");

            }
            catch (Exception)
            {

                return Redirect("/MNGAdmin/P404");
            }
        }
        #endregion

        #region[ham sua]
        public ActionResult Update(int id)
        {
            try
            {
                var data = db.News.First(u => u.id == id);
                var nhom = db.Categorys.Where(u => u.active == true && u.cateLang==lang && u.siteTPA == ClassExten.siteOption && u.ctype == 2).OrderBy(u => u.cateOrder).ToList();

                SelectList sl = new SelectList(nhom, "id", "name", data.groupid);
                ViewBag.nhomtin = sl;
                return View(data);
            }
            catch (Exception)
            {

                return Redirect("/MNGAdmin/P404");
            }

        }
        #endregion

        #region[ham them form]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(FormCollection f)
        {
            //var hfc = HttpContext.Request.Files;// HttpContext.Current.Request.Files;
            //var file = hfc[0];
            string active = f["active"];
            string noibat = f["noibat"];
            News data = new News();
            var nhom = Convert.ToInt32(f["catep"]);
            data.groupid = nhom;
            data.images = f["img"];
            if (!string.IsNullOrEmpty(data.images))
            {
              // var idImg= ClassExten.UploadGoogleDriver(data.images, ClassExten.ForderNew);
               // data.imagesId = idImg;
            }
            data.title = f["title"];
            string key = StringClass.NameToTag(data.title);
            data.new_key = checkTrung(key);
            data.descript = f["mota"];
            data.contents = f["contents"];
            data.createdate = DateTime.Now;
            data.newOrder = Convert.ToInt32(f["thutu"]);
            data.ntype = Convert.ToInt32(f["loai"]);
            data.active = true;
            if (active == null || active == "false")
            {
                data.active = false;
            }
            data.ex1 = "1";
            if (noibat == null || noibat == "false")
            {
                data.ex1 = "0";
            }
            data.titleSeo = f["titleseo"];
            data.keySeo = f["keyseo"];
            data.desSeo = f["desseo"];
            data.newLang = lang;
            data.siteTPA = siteTPA;
            data.viewCount = 0;
            db.News.Add(data);
            db.SaveChanges();
            Session["ok"] = "Thêm mới thành công!";


            return Redirect("/News");
        }
        #endregion

        #region[ham them]
        public ActionResult Add()
        {
            var nhom = db.Categorys.Where(u=>u.active==true && u.cateLang==lang && u.siteTPA==ClassExten.siteOption && u.ctype==2).OrderBy(u=>u.cateOrder).ToList();
            SelectList sl = new SelectList(nhom, "id", "name");
            ViewBag.nhomtin = sl;
            return View();
        }
        #endregion

        #region[ham xoa]
        public ActionResult Delete(int id)
        {
            try
            {
                var data = db.News.First(u => u.id == id);
                db.News.Remove(data);
                db.SaveChanges();
                Session["ok"] = "Xóa thành công!";
                return Redirect("/News");

            }
            catch (Exception)
            {

                return Redirect("/MNGAdmin/P404");
            }
        }
        #endregion


        #region[gen key add]
        public ActionResult Genkey(string name)
        {
            string key = Productgold.Models.StringClass.RemoveSign4VietnameseString(name);
            var data = db.News.Where(u => u.new_key == key && u.newLang == lang && u.siteTPA == ClassExten.siteOption).ToList().Count;
            if (data > 0)
            {
                var pro = (from n in db.News orderby n.id descending select n.id).Take(1).FirstOrDefault();
                string keynew = key + "-" + pro;
                return Json(keynew, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(key, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region[gen key update]
        public ActionResult Genkeyup(string name, string id)
        {
            string key = Productgold.Models.StringClass.RemoveSign4VietnameseString(name);
            var data = db.News.Where(u => u.new_key == key && u.newLang == lang && u.siteTPA == ClassExten.siteOption).ToList().Count;
            if (data > 0)
            {
                int idpro = Convert.ToInt32(id);
                var pro = db.News.FirstOrDefault(u => u.id == idpro).id;
                string keynew = key + "-" + pro;
                return Json(keynew, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(key, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region[check trung khi them va cap nhat]
        public string checkTrung(string key)
        {
            string rs = key;
            var cate = db.News.FirstOrDefault(u => u.new_key == key && u.newLang==lang && u.siteTPA==ClassExten.siteOption);
            if (cate != null)
            {
                var idMax = db.News.OrderByDescending(u => u.id).FirstOrDefault();
                rs = key + "-" + idMax.id;
            }
            return rs;
        }
        public string checkTrungUpdate(string key, int id)
        {
            string rs = key;
            var cate = db.News.FirstOrDefault(u => u.new_key == key && u.newLang == lang && u.siteTPA == ClassExten.siteOption);
            if (cate != null)
            {
                var idMax = id;
                rs = key + "-" + idMax;
            }
            return rs;
        }
        #endregion


    }
}
