using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Productgold.Models;
namespace Productgold.Controllers
{
    public class TagproController : Controller
    {
        //
        // GET: /Tagpro/


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
            var all = db.Tagproes.Where(u=>u.tagLang==lang && u.siteTPA==siteTPA).OrderBy(u=>u.tagOrder).ToList();
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
                var thutu = f["thutu"];
                var name = f["name"];
                if (idupdate == "-1")
                {
                    Tagpro data = new Tagpro();
                    data.tagName = name;
                    data.tagLang = lang;
                    data.siteTPA = siteTPA;
                    data.tagKey = StringClass.NameToTag(name);
                    data.tagOrder = Convert.ToInt32(thutu);
                    db.Tagproes.Add(data);
                    db.SaveChanges();
                    Session["ok"] = "Thêm mới thành công!";
                }
                else
                {
                    var id = Convert.ToInt32(idupdate);
                    var data = db.Tagproes.FirstOrDefault(u => u.tagId == id);
                    data.tagName = name;
                    data.tagKey = StringClass.NameToTag(name);
                    data.tagOrder = Convert.ToInt32(thutu);
                    db.SaveChanges();
                    Session["ok"] = "Cập nhật thành công!";
                }
                return Redirect("/Tagpro");

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
            var data = db.Tagproes.First(u => u.tagId == id);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region[ham xoa]
        public ActionResult Delete(int id)
        {
            try
            {
                var data = db.Tagproes.FirstOrDefault(u => u.tagId == id);
                db.Tagproes.Remove(data);
                db.SaveChanges();
                Session["ok"] = "Xóa thành công!";
                return Redirect("/Tagpro");

            }
            catch (Exception)
            {

                return Redirect("/MNGAdmin/P404");
            }
        }
        #endregion

    }
}
