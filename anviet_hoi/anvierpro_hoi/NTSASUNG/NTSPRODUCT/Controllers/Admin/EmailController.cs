using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NTSPRODUCT.Models;

namespace NTSPRODUCT.Controllers
{
    public class EmailController : Controller
    {
        //
        // GET: /Email/

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
            var all = (from a in db.emails.AsNoTracking()
                       where string.IsNullOrEmpty(model.Name) || a.email1.ToLower().Contains(model.Name.ToLower())
                       orderby a.createDate descending
                       select a).AsQueryable();

            var numOfEmails = all.Select(u => u.id).Count();
            var currPage = model.PageNumber - 1;
            var data = all.Skip((model.PageNumber - 1) * model.PageSize).Take(model.PageSize).ToList();

            ViewBag.Index = (currPage * model.PageSize);
            ViewBag.TotalItem = numOfEmails;
            ViewBag.PageSize = model.PageSize;
            if (numOfEmails > model.PageSize)
            {
                ViewBag.pages = NTSPRODUCT.Models.Phantrang.PhanTrang(model.PageSize, currPage, numOfEmails, "");
            }
            return PartialView(data);
        }

        #endregion

        #region[ham delete]
        [MyAuthorize]
        public ActionResult Delete(int id)
        {
            var data = db.emails.First(u => u.id.Equals(id));
            {
                try
                {
                    db.emails.Remove(data);
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

        #region[ham them]
        public ActionResult AddEmail(string emails)
        {
            var data = db.emails.Where(u => u.email1 == emails).ToList().Count;
            if (data > 0)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            else
            {
                email e = new email();
                e.email1 = emails;

                db.emails.Add(e);
                db.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
    }
}
