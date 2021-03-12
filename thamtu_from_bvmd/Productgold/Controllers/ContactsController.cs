using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Productgold.Models;

namespace Productgold.Controllers
{
    public class ContactsController : BaseCookisController
    {
        //
        // GET: /Contacts/

        WEBTPAEntities db = new WEBTPAEntities();
        string lang = ClassExten.GetLang();
        string siteTPA = ClassExten.siteOption;
        #region[ham view]
        public ActionResult Index()
        {
            string titleSearch = "";
            string phone_email = "";
            int sltypeSearch = -1;
            string sltype = "";
            if (Request["titleSearch"] != null)
            {
                titleSearch = Request["titleSearch"];
            }
            if (Request["phone_email"] != null)
            {
                phone_email = Request["phone_email"];
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
            var all = db.Contacts.Where(u=>u.conLang==lang && u.siteTPA== siteTPA
             && (string.IsNullOrEmpty(titleSearch) || u.fullname.ToLower().Contains(titleSearch.ToLower()))
             && (string.IsNullOrEmpty(phone_email) || u.phone.ToLower().Contains(phone_email.ToLower()) || u.email.ToLower().Contains(phone_email.ToLower()))
            && (sltypeSearch == -1 || u.type == sltypeSearch)
            ).OrderByDescending(u=>u.createdate).ToList();
            var data = all.Skip(curpage * pagesize).Take(pagesize).ToList();
            var url = Request.Path;
            numOfNews = all.Count;
            ViewBag.pages = Productgold.Models.Phantrang.PhanTrang(pagesize, curpage, numOfNews, url);
            return View(data);
        }
        #endregion

        #region[ham sua]
        public ActionResult Getbyid(int id)
        {
            var data = db.Contacts.First(u=>u.id==id);
            data.active = true;
            db.SaveChanges();
            return Json(data,JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region[ham pdate]
        [HttpPost]
        public ActionResult Update(FormCollection f)
        {
            try
            {
            var idupdate = Convert.ToInt32(f["idupdate"]);
            var active = f["active"];
            var ac = false;
            if (active=="1")
            {
                ac = true;
            }
            var data = db.Contacts.First(u=>u.id==idupdate);
            data.active = ac;
            db.SaveChanges();
            Session["ok"] = "Cập nhật thành công!";
            return Redirect("/Contacts");
            
            }
            catch (Exception)
            {

                return Redirect("/MNGAdmin/P404");
            }
        }
        #endregion

        #region[ham delete]
        public ActionResult Delete(int id)
        {
            try
            {

            var data = db.Contacts.First(u=>u.id==id);
            db.Contacts.Remove(data);
            db.SaveChanges();
            Session["ok"] = "Xóa thành công!";
            return Redirect("/Contacts");

            }
            catch (Exception)
            {

                return Redirect("/MNGAdmin/P404");
            }
        }
        #endregion

        public ActionResult IndexSearch(FormCollection fr)
        {
            var titleSearch = fr["titleSearch"];
            var sltype = fr["sltype"];
            var phone_email = fr["phone_email"];
            return Redirect("/Contacts/Index?titleSearch=" + titleSearch + "&sltype=" + sltype+ "&phone_email="+ phone_email);
        }
    }
}
