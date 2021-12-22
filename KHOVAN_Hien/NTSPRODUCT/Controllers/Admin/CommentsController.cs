﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NTSPRODUCT.Models;

namespace NTSPRODUCT.Controllers
{
    public class CommentsController : Controller
    {
        //
        // GET: /Comments/

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
            string type = "";
            if (model.Type != null)
            {
                type = model.Type + "";
            }
            var all = (from a in db.Comments.AsNoTracking()
                       where (string.IsNullOrEmpty(model.Name) || a.fullName.ToLower().Contains(model.Name.ToLower()))
                       && (string.IsNullOrEmpty(model.Email) || a.email.ToLower().Contains(model.Email.ToLower()))
                       //&& (string.IsNullOrEmpty(model.ProName) || b.pro_name.ToLower().Contains(model.ProName.ToLower()))
                       && (string.IsNullOrEmpty(type) || a.cType.Equals(type))
                       orderby a.createDate descending
                       select a).AsQueryable();

            var numOfContacts = all.Select(u => u.id).Count();
            var currPage = model.PageNumber - 1;
            var data = all.Skip((model.PageNumber - 1) * model.PageSize).Take(model.PageSize).ToList();

            ViewBag.Index = (currPage * model.PageSize);
            ViewBag.TotalItem = numOfContacts;
            ViewBag.PageSize = model.PageSize;
            if (numOfContacts > model.PageSize)
            {
                ViewBag.pages = NTSPRODUCT.Models.Phantrang.PhanTrang(model.PageSize, currPage, numOfContacts, "");
            }
            return PartialView(data);
        }
        #endregion

        #region[ham sua]
        [MyAuthorize]
        public ActionResult Getbyid(string id)
        {
            var data = db.Comments.First(u => u.id == id);
            data.active = true;
            db.SaveChanges();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion


        #region[ham delete]
        [MyAuthorize]
        public ActionResult Delete(string id)
        {
            var data = db.Comments.First(u => u.id.Equals(id));
            {
                try
                {
                    db.Comments.Remove(data);
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
