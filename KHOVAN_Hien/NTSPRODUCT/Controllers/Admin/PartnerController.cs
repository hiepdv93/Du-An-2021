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
    public class PartnerController : Controller
    {
        //
        // GET: /Partner/
        NTSWEBEntities db = new NTSWEBEntities();
        string lang = ClassExten.GetLang();
        #region[ham view]
        [MyAuthorize]
        public ActionResult Index()
        {
            return View();
        }
        [MyAuthorize]
        public ActionResult GetList(SearchModel model)
        {
            var all = (from a in db.Partners.AsNoTracking()
                       where a.type.Equals(Constants.Partner) &&
                       (string.IsNullOrEmpty(model.Name) || a.pName.ToLower().Contains(model.Name.ToLower()))
                       orderby a.numberOder
                       select a).AsQueryable();

            var numOfSlides = all.Select(u => u.id).Count();
            var currPage = model.PageNumber - 1;
            var data = all.Skip((model.PageNumber - 1) * model.PageSize).Take(model.PageSize).ToList();

            ViewBag.Index = (currPage * model.PageSize);
            ViewBag.TotalItem = numOfSlides;
            ViewBag.PageSize = model.PageSize;
            if (numOfSlides > model.PageSize)
            {
                ViewBag.pages = NTSPRODUCT.Models.Phantrang.PhanTrang(model.PageSize, currPage, numOfSlides, "");
            }
            return PartialView(data);
        }

        #endregion

        #region[ham lay thong tin]
        [MyAuthorize]
        public ActionResult GetbyId(string id)
        {
            var data = db.Partners.First(u => u.id.Equals(id));
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region[ham them, sua]
        [MyAuthorize]
        public ActionResult Add(Partner model)
        {
            try
            {
                Partner data = new Partner();
                data.id = Guid.NewGuid().ToString();
                data.pName = model.pName;
                data.pImage = model.pImage;
                data.pNote = model.pNote;
                data.pLink = model.pLink;
                data.numberOder = model.numberOder;
                data.type = Constants.Partner;
                data.lang = lang;
                db.Partners.Add(data);
                db.SaveChanges();
                return Json(new { ok = true, mess = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { ok = false, mess = "" }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Update(Partner model)
        {
            try
            {
                var data = db.Partners.First(u => u.id.Equals(model.id));
                data.pName = model.pName;
                data.pImage = model.pImage;
                data.pNote = model.pNote;
                data.pLink = model.pLink;
                data.type = Constants.Partner;
                data.lang = lang;
                data.numberOder = model.numberOder;
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
            var data = db.Partners.First(u => u.id.Equals(id));
            {
                try
                {
                    db.Partners.Remove(data);
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

    }
}
