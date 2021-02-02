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
    public class LibController : Controller
    {
        //
        // GET: /Slide/
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
            var all = (from a in db.Libs.AsNoTracking()
                       where a.libLang.Equals(lang)
                       && (string.IsNullOrEmpty(model.Name) || a.linName.ToLower().Contains(model.Name.ToLower()))
                       orderby a.createDate descending
                       select a).AsQueryable();

            var numOfLibs = all.Select(u => u.libId).Count();
            var currPage = model.PageNumber - 1;
            var data = all.Skip((model.PageNumber - 1) * model.PageSize).Take(model.PageSize).ToList();

            ViewBag.Index = (currPage * model.PageSize);
            ViewBag.TotalItem = numOfLibs;
            ViewBag.PageSize = model.PageSize;
            if (numOfLibs > model.PageSize)
            {
                ViewBag.pages = NTSPRODUCT.Models.Phantrang.PhanTrang(model.PageSize, currPage, numOfLibs, "");
            }
            return PartialView(data);
        }

        #endregion

        #region[ham lay thong tin]
        [MyAuthorize]
        public ActionResult GetbyId(string id)
        {
            var data = db.Libs.First(u => u.libId.Equals(id));
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region[ham them, sua]
        [MyAuthorize]
        public ActionResult Add(Lib model)
        {
            try
            {
                Lib data = new Lib();
                data.libId = Guid.NewGuid().ToString();
                data.linName = model.linName;
                data.libLink = model.libLink;
                data.libNote = model.libNote;
                data.libImage = model.libImage;
                data.libOrder = model.libOrder;
                data.cateId = model.cateId;
                data.libType = model.libType;
                data.libLang = lang;
                data.createDate = DateTime.Now;
                data.updateDate = DateTime.Now;

                db.Libs.Add(data);
                db.SaveChanges();
                return Json(new { ok = true, mess = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { ok = false, mess = "" }, JsonRequestBehavior.AllowGet);
            }
        }
        [MyAuthorize]
        public ActionResult Update(Lib model)
        {
            try
            {
                var data = db.Libs.First(u => u.libId.Equals(model.libId));

                data.linName = model.linName;
                data.libLink = model.libLink;
                data.libNote = model.libNote;
                data.libImage = model.libImage;
                data.libOrder = model.libOrder;
                data.cateId = model.cateId;
                data.libType = model.libType;
                data.libLang = lang;
                data.updateDate = DateTime.Now;
                db.SaveChanges();
                return Json(new { ok = true, mess = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { ok = false, mess = "" }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
        [MyAuthorize]
        public ActionResult UpdateOrCreate()
        {
            return PartialView();
        }

        #region[ham xoa]
        [MyAuthorize]
        public ActionResult Delete(string id)
        {
            var data = db.Libs.First(u => u.libId.Equals(id));
            {
                try
                {
                    db.Libs.Remove(data);
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
