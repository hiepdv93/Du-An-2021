using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Productgold.Models;

namespace Productgold.Controllers
{
    public class OdersController : BaseCookisController
    {
        //
        // GET: /Oders/

        WEBTPAEntities db = new WEBTPAEntities();
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

            var data = db.Oders.Where(u=>u.siteTPA==siteTPA
             && (string.IsNullOrEmpty(titleSearch) || u.fullName.ToLower().Contains(titleSearch.ToLower()))
            && (sltypeSearch == -1 || u.statust == sltypeSearch)
            ).OrderByDescending(u=>u.createdate).ToList();
            return View(data);
        }
        #endregion

        #region[ham them sua]
        [HttpPost]
        public ActionResult Update(FormCollection f)
        {
            try
            {
                var id = Convert.ToInt32(f["idupdate"]);
                var data = db.Oders.First(u => u.id == id);
                var comb = Convert.ToInt32(f["trangthai"]);
                data.statust = comb;
                db.SaveChanges();
                Session["ok"] = "Cập nhật thành công!";
                return Redirect("/Oders");

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
            var data = db.Oders.First(u => u.id == id);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region[ham xoa]
        public ActionResult Delete(int id)
        {
            try
            {
                var data = db.Oders.First(u => u.id == id);
                db.Oders.Remove(data);
                db.SaveChanges();
                Session["ok"] = "Xóa thành công!";
                return Redirect("/Oders");

            }
            catch (Exception)
            {

                return Redirect("/MNGAdmin/P404");
            }
        }
        #endregion

        #region[chi tiet]
        public ActionResult ViewDetail(int id)
        {
           // var customer = (from o in db.Oders join cus in db.Customers on o.cusid equals cus.id where(o.id==id) select cus.fullname).ToList();
            ViewBag.cus = db.Oders.FirstOrDefault(u=>u.id==id).fullName;
            var data = db.Oderdts.Where(u=>u.oderid==id).ToList();
            return View(data);
        }
        #endregion

        public ActionResult IndexSearch(FormCollection fr)
        {
            var titleSearch = fr["titleSearch"];
            var sltype = fr["sltype"];
            return Redirect("/Oders/Index?titleSearch=" + titleSearch + "&sltype=" + sltype);
        }
    }
}
