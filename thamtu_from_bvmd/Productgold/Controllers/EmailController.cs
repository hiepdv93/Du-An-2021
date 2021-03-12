using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Productgold.Models;

namespace Productgold.Controllers
{
    public class EmailController : BaseCookisController
    {
        //
        // GET: /Email/

        WEBTPAEntities db = new WEBTPAEntities();
        string siteTPA = ClassExten.siteOption;
        string lang = ClassExten.GetLang();
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
            var all = db.emails.Where(u=>u.eLang==lang && u.siteTPA==siteTPA).OrderBy(u=>u.email1).ToList();
            var data = all.Skip(curpage * pagesize).Take(pagesize).ToList();
            var url = Request.Path;
            numOfNews = all.Count;
            ViewBag.pages = Productgold.Models.Phantrang.PhanTrang(pagesize, curpage, numOfNews, url);
            return View(data);
        }
        #endregion

        #region[ham delete]
        public ActionResult Delete(int id)
        {
            try
            {
                var data = db.emails.First(u => u.id == id);
                db.emails.Remove(data);
                db.SaveChanges();
                Session["ok"] = "Xóa thành công!";
                return Redirect("/Email");
            }
            catch (Exception)
            {

                return Redirect("/MNGAdmin/P404");
            }
        }
        #endregion

        #region[ham them]
        public ActionResult AddEmail(string emails)
        {
            var data = db.emails.Where(u => u.email1 == emails).ToList().Count;
            if (data > 0)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            else
            {
                email e = new email();
                e.email1 = emails;
                e.siteTPA = siteTPA;
                e.eLang = ClassExten.GetLang();
                db.emails.Add(e);
                db.SaveChanges();
                return Json(true,JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
    }
}
