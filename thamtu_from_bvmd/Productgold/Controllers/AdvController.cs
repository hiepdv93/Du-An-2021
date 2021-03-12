using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Productgold.Models;

namespace Productgold.Controllers
{
    public class AdvController : BaseCookisController
    {
        //
        // GET: /Adv/
        WEBTPAEntities db = new WEBTPAEntities();
        string lang = ClassExten.GetLang();
        string siteTPA = ClassExten.siteOption;
        #region[ham view]
        public ActionResult Index()
        {
            string titleSearch = "";
            int sltypeSearch = -1;
            string sltype = "";
            if (Request["titleSearch"] != null)
            {
                titleSearch = Request["titleSearch"];
            }
            if (Request["sltype"] != null)
            {
                sltype = Request["sltype"];
                if (!string.IsNullOrEmpty(sltype))
                {
                    sltypeSearch = Convert.ToInt32(sltype);
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
            var all = db.Advs.Where(u => u.advLang == lang && u.siteTPA== siteTPA
             && u.atype!=5
               && (string.IsNullOrEmpty(titleSearch) || u.advName.ToLower().Contains(titleSearch.ToLower()))
            && (sltypeSearch == -1 || u.atype == sltypeSearch)
            ).OrderBy(u=>u.advOrder).ToList();
            var data = all.Skip(curpage * pagesize).Take(pagesize).ToList();
            var url = Request.Path;
            numOfNews = all.Count;
            ViewBag.pages = Productgold.Models.Phantrang.PhanTrang(pagesize, curpage, numOfNews, url);
            return View(data);
        }
        #endregion

        #region[ham them sua]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Update(FormCollection f)
        {
            try
            {
                var idupdate = f["idupdate"];
                var advName = f["advName"];
                var advW = f["advW"];
                var advH = f["advH"];
                var img = f["img"];//
                var link = f["link"];//
                var advThutu = f["advThutu"];
                var advNote = f["advNote1"];
                var active = f["active"];//
                var atype = Convert.ToInt32(f["atype"]);//

                if (idupdate == "-1")
                {
                    Adv data = new Adv();
                    data.images = img;
                    if (!string.IsNullOrEmpty(data.images))
                    {
                       // var idImg = ClassExten.UploadGoogleDriver(data.images, ClassExten.ForderAdv);
                       // data.imagesId = idImg;
                    }
                    data.link = link;
                    data.atype = atype;
                    data.active = false;
                    if (active == "1")
                    {
                        data.active = true;
                    }
                    data.advLang = lang;
                    data.siteTPA = siteTPA;
                    data.advName = advName;
                    data.width = Convert.ToInt32(advW);
                    data.height = Convert.ToInt32(advH);
                    data.advOrder = Convert.ToInt32(advThutu);
                    data.note = advNote;
                    db.Advs.Add(data);
                    db.SaveChanges();
                    Session["ok"] = "Thêm mới thành công!";
                }
                else
                {
                    var id = Convert.ToInt32(idupdate);
                    var data = db.Advs.First(u => u.id == id);
                
                    data.images = img;
                    data.link = link;

                    data.atype = atype;
                    data.active = false;
                    if (active == "1")
                    {
                        data.active = true;
                    }
                    data.advLang = lang;
                    data.advName = advName;
                    data.width = Convert.ToInt32(advW);
                    data.height = Convert.ToInt32(advH);
                    data.advOrder = Convert.ToInt32(advThutu);
                    data.note = advNote;
                    db.SaveChanges();
                    Session["ok"] = "Cập nhật thành công!";
                }
                if (atype>=4)
                {
                    return Redirect("/Adv/index2");

                }
                else { 
                return Redirect("/Adv");
                }

            }
            catch (Exception)
            {

                return Redirect("/MNGAdmin/P404");
            }
        }
        #endregion

        #region[ham sua]
        public ActionResult Getbyid(int id)
        {
            var data = db.Advs.First(u => u.id == id);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region[ham xoa]
        public ActionResult Delete(int id)
        {
            try
            {
                var data = db.Advs.First(u => u.id == id);
                var type = data.atype;
                db.Advs.Remove(data);
                db.SaveChanges();
                Session["ok"] = "Xóa thành công!";
                if (type==5)
                {
                    return Redirect("/Adv/Index2");
                }
                else { 
                return Redirect("/Adv");
                }

            }
            catch (Exception)
            {

                return Redirect("/MNGAdmin/P404");
            }
        }
        #endregion

        public ActionResult Index2()
        {
            string titleSearch = "";
            int sltypeSearch = -1;
            string sltype = "";
            if (Request["titleSearch"] != null)
            {
                titleSearch = Request["titleSearch"];
            }
            if (Request["sltype"] != null)
            {
                sltype = Request["sltype"];
                if (!string.IsNullOrEmpty(sltype))
                {
                    sltypeSearch = Convert.ToInt32(sltype);
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
            var all = db.Advs.Where(u => u.advLang == lang && u.siteTPA == siteTPA
            && (u.atype == 5 || u.atype == 6 || u.atype == 7)
               && (string.IsNullOrEmpty(titleSearch) || u.advName.ToLower().Contains(titleSearch.ToLower()))
            && (sltypeSearch == -1 || u.atype == sltypeSearch)
            ).OrderBy(u => u.advOrder).ToList();
            var data = all.Skip(curpage * pagesize).Take(pagesize).ToList();
            var url = Request.Path;
            numOfNews = all.Count;
            ViewBag.pages = Productgold.Models.Phantrang.PhanTrang(pagesize, curpage, numOfNews, url);
            return View(data);
        }

        public ActionResult IndexSearch(FormCollection fr)
        {
            var titleSearch = fr["titleSearch"];
            var sltype = fr["sltype"];
            return Redirect("/Adv/Index?titleSearch=" + titleSearch + "&sltype=" + sltype );
        }
        public ActionResult IndexSearch2(FormCollection fr)
        {
            var titleSearch = fr["titleSearch"];
            var sltype = fr["sltype"];
            return Redirect("/Adv/Index2?titleSearch=" + titleSearch + "&sltype=" + sltype);
        }
    }
}
