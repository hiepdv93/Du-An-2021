using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Productgold.Models;

namespace Productgold.Controllers
{
    public class CustomersController : BaseCookisController
    {
        //
        // GET: /Customers/
        WEBTPAEntities db = new WEBTPAEntities();
        string lang = ClassExten.GetLang();
        string siteTPA = ClassExten.siteOption;
        #region[ham view danh sach]
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
            var all = db.Customers.Where(u=>u.cusLang==lang && u.siteTPA==siteTPA).OrderBy(u=>u.fullname).ToList();
            var data = all.Skip(curpage * pagesize).Take(pagesize).ToList();
            var url = Request.Path;
            numOfNews = all.Count;
            ViewBag.pages = Productgold.Models.Phantrang.PhanTrang(pagesize, curpage, numOfNews, url);
            return View(data);
        }
        #endregion

        #region[update trang thai]
        [HttpPost]
        public ActionResult Update(FormCollection f)
        {
            int id = Convert.ToInt32(f["idupdate"]);
            var active = f["active"];
            var data = db.Customers.First(u=>u.id==id);
            if (active == "1")
            {
                data.active = true;
            }
            else
            {
                data.active = false;
            }
            db.SaveChanges();
            Session["ok"] = "Cập nhật thành công!";
            return Redirect("/Customers");
        }
        #endregion

        #region[ham sua]
        public ActionResult Getbyid(int id)
        {
            var dataGet = db.Customers.First(u => u.id == id);
            Customer data = new Customer();
            data.id = dataGet.id;
            data.email = dataGet.email;
            data.phone = dataGet.phone;
            data.fullname = dataGet.fullname;
            data.active = dataGet.active;
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion

    }
}
