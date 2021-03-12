using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Productgold.Models;

namespace Productgold.Controllers
{
    public class VideoController : BaseCookisController
    {
        //
        // GET: /Video/
        WEBTPAEntities db = new WEBTPAEntities();
        string lang = ClassExten.GetLang();
        string siteTPA = ClassExten.siteOption;
        #region[ham view]
        public ActionResult Index()
        {
            //var id = ClassExten.CreateFoder("Imglib");
            string titleSearch = "";
            int groupSearch = -1;
            string group = "";
            if (Request["titleSearch"] != null)
            {
                titleSearch = Request["titleSearch"];
            }
            if (Request["groupSearch"] != null)
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
            var all = db.Videos.Where(u => u.vLang == lang
            && u.siteTPA == siteTPA
            && (string.IsNullOrEmpty(titleSearch) || u.vName.ToLower().Contains(titleSearch.ToLower()))
            && (groupSearch == -1 || u.cateId == groupSearch)).OrderBy(u => u.vOrder).ToList();
            var data = all.Skip(curpage * pagesize).Take(pagesize).ToList();
            var url = Request.Path;
            numOfNews = all.Count;
            ViewBag.pages = Productgold.Models.Phantrang.PhanTrang(pagesize, curpage, numOfNews, url);
            var nhomtin = db.Categorys.Where(u => u.ctype == 1 && u.cateLang == lang && u.active == true && u.siteTPA == ClassExten.siteOption).OrderBy(u => u.cateOrder).ToList();
            SelectList sl = new SelectList(nhomtin, "id", "name");
            ViewBag.nhomtin = sl;
            return View(data);
        }

        public ActionResult IndexSearch(FormCollection fr)
        {
            var titleSearch = fr["titleSearch"];
            var groupSearch = fr["groupSearch"];
            return Redirect("/Video/Index?titleSearch=" + titleSearch + "&groupSearch=" + groupSearch);
        }
        #endregion

        #region[ham xoa]
        public ActionResult Delete(int id)
        {
            try
            {
                var data = db.Videos.First(u => u.vId == id);
                db.Videos.Remove(data);
                db.SaveChanges();
                Session["ok"] = "Xóa thành công!";
                return Redirect("/Video");

            }
            catch (Exception)
            {
                return Redirect("/MNGAdmin/P404");
            }
        }
        #endregion

        [HttpPost]
        public ActionResult GetCategory()
        {
            var data = db.Categorys.Where(u => u.ctype == 1  && u.cateLang == lang && u.active == true && u.siteTPA == ClassExten.siteOption).OrderBy(u => u.cateOrder).Select(m => new { id = m.id, name = m.name });
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GetVideoById(int id)
        {
            var data = db.Videos.Where(m => m.vId == id).FirstOrDefault();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(FormCollection f)
        {
            try
            {
                int idEdit = Convert.ToInt32(f["idEdit"]);
                var urlReturn = f["url"];

                var catId = f["cate"];
                var name = f["name"];
                var key = f["key"];
                var keySeo = f["kseo"];
                var desSeo = f["dseo"];
                var tseo = f["tseo"];
                var link = f["link"];
                var order = f["order"];
                var note = f["note"];
                var img = f["img"];
                var fileimg = f["fileimg"];

                bool isEdit = false;
                if (idEdit != -1)
                    isEdit = true;
                Video data;
                if (isEdit)
                {
                    Session["ok"] = "Sửa thành công!";
                    data = db.Videos.Where(m => m.vId == idEdit).FirstOrDefault();
                    if (!string.IsNullOrEmpty(img))
                    {
                        if (data.vimg != img)
                        {
                            data.vimg = img;
                         
                        }
                    }
                    else
                    {
                        data.vimg = "";
                        data.titleSeo = "";
                    }
                    if (!string.IsNullOrEmpty(fileimg))
                    {
                        if (data.vLink != fileimg)
                        {
                            data.vLink = fileimg;
                          
                        }
                    }
                    else
                    {
                        data.keySeo = "";
                        data.vLink = "";
                    }
                }
                else
                {
                    Session["ok"] = "Thêm thành công!";
                    data = new Video();
                    if (!string.IsNullOrEmpty(img))
                    {
                        data.vimg = img;
                      
                    }
                    if (!string.IsNullOrEmpty(fileimg))
                    {
                        data.vLink = fileimg;
                       
                    }

                }
                if (catId != null && !"".Equals(catId))
                    data.cateId = Convert.ToInt32(catId);
                data.vName = name;
                data.vKey = key;
                data.keySeo = keySeo;
                data.desSeo = desSeo;
                data.titleSeo = tseo;
                data.vLang = lang;
                data.siteTPA = siteTPA;
                if (order != null && !"".Equals(order))
                    data.vOrder = Convert.ToInt32(order);
                data.vnote = note;
                if (!isEdit)
                    db.Videos.Add(data);
                db.SaveChanges();

                return Redirect(urlReturn);
            }
            catch (Exception)
            {
                throw;
                Session["ok"] = "Thêm không thành công!";
                return Redirect("/P404");
            }

        }
    }
}
