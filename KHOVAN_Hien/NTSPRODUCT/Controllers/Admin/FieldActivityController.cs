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
    public class FieldActivityController : Controller
    {
        //
        // GET: /FieldActivity/
        NTSWEBEntities db = new NTSWEBEntities();
        string lang = ClassExten.GetLang();
        #region[ham view]
        [MyAuthorize]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetList(SearchModel model)
        {
            var all = (from a in db.FieldActivities.AsNoTracking()
                       where (a.slang.Equals(lang)
                       && (string.IsNullOrEmpty(model.Name) || a.name.ToLower().Contains(model.Name.ToLower())))
                       orderby a.createDate descending
                       select a).AsQueryable();

            var numOfAdvs = all.Select(u => u.id).Count();
            var currPage = model.PageNumber - 1;
            var data = all.Skip((model.PageNumber - 1) * model.PageSize).Take(model.PageSize).ToList();

            ViewBag.Index = (currPage * model.PageSize);
            ViewBag.TotalItem = numOfAdvs;
            ViewBag.PageSize = model.PageSize;
            if (numOfAdvs > model.PageSize)
            {
                ViewBag.pages = NTSPRODUCT.Models.Phantrang.PhanTrang(model.PageSize, currPage, numOfAdvs, "");
            }
            return PartialView(data);
        }
        #endregion

        #region[ham lay thong tin]
        [MyAuthorize]
        public ActionResult GetbyId(string id)
        {
            var data = db.FieldActivities.First(u => u.id.Equals(id));
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region[ham them, sua]
        [MyAuthorize]
        public ActionResult Add(FieldActivity model)
        {
            try
            {
                FieldActivity data = new FieldActivity();
                data.id = Guid.NewGuid().ToString();
                data.name = model.name;
                data.numberOder = model.numberOder;
                data.image = model.image;
                data.contents = model.contents;
                data.slang = lang;
                data.createDate = DateTime.Now;
                data.active = model.active;
                db.FieldActivities.Add(data);
                db.SaveChanges();
                return Json(new { ok = true, mess = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { ok = false, mess = "" }, JsonRequestBehavior.AllowGet);
            }
        }
        [MyAuthorize]
        public ActionResult Update(FieldActivity model)
        {
            try
            {
                var data = db.FieldActivities.First(u => u.id.Equals(model.id));
                data.name = model.name;
                data.numberOder = model.numberOder;
                data.image = model.image;
                data.contents = model.contents;
                data.slang = lang;
                data.createDate = DateTime.Now;
                data.active = model.active;
                db.SaveChanges();
                return Json(new { ok = true, mess = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { ok = false, mess = "" }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        public ActionResult UpdateOrCreate()
        {
            return PartialView();
        }

        #region[ham xoa]
        [MyAuthorize]
        public ActionResult Delete(string id)
        {
            var data = db.FieldActivities.First(u => u.id.Equals(id));
            {
                try
                {
                    db.FieldActivities.Remove(data);
                    db.SaveChanges();
                    return Json(new { ok = true, mess = "" }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(new { ok = false, mess = ex.Message }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        #endregion
        [MyAuthorize]
        public ActionResult ChangeStatus(string id)
        {
            var data = db.FieldActivities.First(u => u.id.Equals(id));
            {
                try
                {
                    if (data.active == Constants.Checked)
                    {
                        data.active = Constants.NotChecked;
                    }
                    else
                    {
                        data.active = Constants.Checked;
                    }
                    db.SaveChanges();
                    return Json(new { ok = true, mess = "" }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(new { ok = false, mess = ex.Message }, JsonRequestBehavior.AllowGet);
                }
            }
        }
    }
}
