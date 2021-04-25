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
    public class HinhAnhController : Controller
    {
        //
        // GET: /HinhAnh/
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
            var all = (from a in db.Advs.AsNoTracking()
                       where (a.advLang.Equals(lang)
                       && (string.IsNullOrEmpty(model.Name) || a.advName.ToLower().Contains(model.Name.ToLower())))
                       && (model.Type==null || model.Type==a.advType)
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
            var data = db.Advs.First(u => u.id.Equals(id));
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region[ham them, sua]
        [MyAuthorize]
        public ActionResult Add(Adv model)
        {
            try
            {
                Adv data = new Adv();
                data.id = Guid.NewGuid().ToString();
                data.advName = model.advName;
                data.advOrder = model.advOrder;
                data.advImage = model.advImage;
                data.advNote = model.advNote;
                data.advType = model.advType;
                data.advLink = model.advLink;
                data.advLang = lang;
                data.createDate = DateTime.Now;
                data.advActive = model.advActive;

                db.Advs.Add(data);
                db.SaveChanges();
                return Json(new { ok = true, mess = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { ok = false, mess = "" }, JsonRequestBehavior.AllowGet);
            }
        }
        [MyAuthorize]
        public ActionResult Update(Adv model)
        {
            try
            {
                var data = db.Advs.First(u => u.id.Equals(model.id));

                data.advName = model.advName;
                data.advOrder = model.advOrder;
                data.advImage = model.advImage;
                data.advNote = model.advNote;
                data.advType = model.advType;
                data.advLink = model.advLink;
                data.advLang = lang;
                data.createDate = DateTime.Now;
                data.advActive = model.advActive;

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
            var data = db.Advs.First(u => u.id.Equals(id));
            {
                try
                {
                    db.Advs.Remove(data);
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
            var data = db.Advs.First(u => u.id.Equals(id));
            {
                try
                {
                    if (data.advActive == Constants.Checked)
                    {
                        data.advActive = Constants.NotChecked;
                    }
                    else
                    {
                        data.advActive = Constants.Checked;
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

        public ActionResult ChangeOrder(string id, int numberOder)
        {
            var data = db.Advs.First(u => u.id.Equals(id));
            {
                try
                {
                    data.advOrder = numberOder;
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
