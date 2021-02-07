﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NTSPRODUCT.Models;

namespace NTSPRODUCT.Controllers
{
    public class FAQController : Controller
    {
        //
        // GET: /FAQ/
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
            var all = (from a in db.Faqs.AsNoTracking()
                       where a.lang.Equals(lang)
                       && (string.IsNullOrEmpty(model.Name) || a.name.ToLower().Contains(model.Name.ToLower()))
                       orderby a.createDate descending
                       select a).AsQueryable();

            var numOfSayWes = all.Select(u => u.id).Count();
            var currPage = model.PageNumber - 1;
            var data = all.Skip((model.PageNumber - 1) * model.PageSize).Take(model.PageSize).ToList();

            ViewBag.Index = (currPage * model.PageSize);
            ViewBag.TotalItem = numOfSayWes;
            ViewBag.PageSize = model.PageSize;
            if (numOfSayWes > model.PageSize)
            {
                ViewBag.pages = NTSPRODUCT.Models.Phantrang.PhanTrang(model.PageSize, currPage, numOfSayWes, "");
            }
            return PartialView(data);
        }

        #endregion

        #region[ham lay thong tin]
        [MyAuthorize]
        public ActionResult GetbyId(string id)
        {
            var data = db.Faqs.First(u => u.id.Equals(id));
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region[ham them, sua]
        [MyAuthorize]
        [ValidateInput(false)]
        public ActionResult Add(Faq model)
        {
            try
            {
                Faq data = new Faq();
                data.id = Guid.NewGuid().ToString();
                data.name = model.name;
                data.lang = lang;
                data.contents = model.contents;
                data.number = 1;
                data.createDate = DateTime.Now;
                db.Faqs.Add(data);
                db.SaveChanges();
                return Json(new { ok = true, mess = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { ok = false, mess = "" }, JsonRequestBehavior.AllowGet);
            }
        }
        [MyAuthorize]
        [ValidateInput(false)]
        public ActionResult Update(Faq model)
        {
            try
            {
                var data = db.Faqs.First(u => u.id.Equals(model.id));

                data.name = model.name;
                data.contents = model.contents;
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
            var data = db.Faqs.First(u => u.id.Equals(id));
            {
                try
                {
                    db.Faqs.Remove(data);
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
