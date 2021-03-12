using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Productgold.Models;

namespace Productgold.Controllers
{
    public class BrandsController : BaseCookisController
    {
        //
        // GET: /Brands/

        WEBTPAEntities db = new WEBTPAEntities();
        string lang = ClassExten.GetLang();
        string siteTPA = ClassExten.siteOption;

        #region[ham view]
        public ActionResult Index()
        {
            string page = "1";//so phan trang hien tai
            var pagesize = 20;//so ban ghi tren 1 trang
            var numOfNews = 0;//tong so ban ghi co duoc truoc khi phan trang
            int curpage = 0; // trang hien tai dung cho phan trang
            if (Request["page"] != null)
            {
                page = Request["page"];
                curpage = Convert.ToInt32(page) - 1;
            }
            var all = db.Brands.Where(u=>u.blang==lang && u.siteTPA== siteTPA).OrderBy(u=>u.name).ToList();
            var data = all.Skip(curpage * pagesize).Take(pagesize).ToList();
            var url = Request.Path;
            numOfNews = all.Count;
            ViewBag.pages = Productgold.Models.Phantrang.PhanTrang(pagesize, curpage, numOfNews, url);
            return View(data);
        }
        #endregion

        #region[ham them sua]
        [HttpPost]
        public ActionResult Update(FormCollection f)
        {
            try
            {
                var idupdate = f["idupdate"];
                var img = f["img"];
                var name = f["name"];
                if (idupdate == "-1")
                {
                    Brand data = new Brand();
                    data.images=img ;
                    data.name = name;
                    data.bkey = StringClass.NameToTag(name);
                    data.blang = lang;
                    data.siteTPA = siteTPA;
                    data.btype = 1;
                    db.Brands.Add(data);
                    db.SaveChanges();
                    Session["ok"] = "Thêm mới thành công!";
                }
                else
                {
                    var id = Convert.ToInt32(idupdate);
                    var data = db.Brands.First(u => u.id == id);
                    data.images = img;
                    data.name = name;
                    data.btype = 1;
                    data.bkey = StringClass.NameToTag(name);
                    data.blang = lang;
                    db.SaveChanges();
                    Session["ok"] = "Cập nhật thành công!";
                }
                return Redirect("/Brands");

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
            var data = db.Brands.First(u => u.id == id);
            Brand bnew = new Brand();
            bnew.id = data.id;
            bnew.name = data.name;
            bnew.images = data.images;
            return Json(bnew, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region[ham xoa]
        public ActionResult Delete(int id)
        {
            try
            {
                var data = db.Brands.First(u => u.id == id);
                db.Brands.Remove(data);
                db.SaveChanges();
                Session["ok"] = "Xóa thành công!";
                return Redirect("/Brands");

            }
            catch (Exception)
            {

                return Redirect("/MNGAdmin/P404");
            }
        }
        #endregion

    }
}
