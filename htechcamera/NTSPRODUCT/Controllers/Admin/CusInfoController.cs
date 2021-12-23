using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NTSPRODUCT.Models;

namespace NTSPRODUCT.Controllers
{
    public class CusInfoController : Controller
    {
        //
        // GET: /CusInfo/
        NTSWEBEntities db = new NTSWEBEntities();
        string lang = ClassExten.GetLang();
        #region[ham view]
        public ActionResult Index(string name, string phone)
        {
            var all = (from a in db.CusInfoes.AsNoTracking()
                       where (string.IsNullOrEmpty(name) || a.name.ToLower().Contains(name.ToLower()))
                       && (string.IsNullOrEmpty(phone) || a.phone.ToLower().Contains(phone.ToLower()))
                       orderby a.createDate descending
                       select a).ToList();
            return View(all);
        }

        #endregion


        public ActionResult Add(CusInfo model)
        {
            try
            {
                CusInfo data = new CusInfo();
                data.id = Guid.NewGuid().ToString();
                data.name = model.name;
                data.phone = model.phone;
                data.ex = model.ex;
                data.active = false;
                data.createDate = DateTime.Now;
                db.CusInfoes.Add(data);
                db.SaveChanges();
                return Json(new { ok = true, mess = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { ok = false, mess = "" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ChangeStatus(string id)
        {
            var data = db.CusInfoes.First(u => u.id.Equals(id));
            {
                try
                {
                    data.active = !data.active;
                    db.SaveChanges();
                    return Json(new { ok = true, mess = "" }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(new { ok = false, mess = ex.Message }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public ActionResult Delete(string id)
        {
            var data = db.CusInfoes.First(u => u.id.Equals(id));
            {
                try
                {
                    db.CusInfoes.Remove(data);
                    db.SaveChanges();
                    return Redirect("/cusinfo/index");
                }
                catch (Exception ex)
                {
                    return Json(new { ok = false, mess = ex.Message }, JsonRequestBehavior.AllowGet);
                }
            }
        }

    }
}
