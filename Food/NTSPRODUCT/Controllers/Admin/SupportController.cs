using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NTSPRODUCT.Models;

namespace NTSPRODUCT.Controllers
{
    public class SupportController : Controller
    {
        //
        // GET: /Support/
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
            var all = (from a in db.Supports.AsNoTracking()
                       where
                        (string.IsNullOrEmpty(model.Name) || a.fullName.ToLower().Contains(model.Name.ToLower()))
                       && (string.IsNullOrEmpty(model.Nick) || a.nick.ToLower().Contains(model.Nick.ToLower()) || a.phone.ToLower().Contains(model.Nick.ToLower()))
                       orderby a.fullName
                       select a).AsQueryable();

            var numOfSupport = all.Select(u => u.id).Count();
            var currPage = model.PageNumber - 1;
            var data = all.Skip((model.PageNumber - 1) * model.PageSize).Take(model.PageSize).ToList();

            ViewBag.Index = (currPage * model.PageSize);
            ViewBag.TotalItem = numOfSupport;
            ViewBag.PageSize = model.PageSize;
            if (numOfSupport > model.PageSize)
            {
                ViewBag.pages = NTSPRODUCT.Models.Phantrang.PhanTrang(model.PageSize, currPage, numOfSupport, "");
            }
            return PartialView(data);
        }

        #endregion

        #region[ham sua]
        [MyAuthorize]
        public ActionResult GetbyId(string id)
        {
            var data = db.Supports.First(u => u.id == id);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region[ham delete]
        [MyAuthorize]
        public ActionResult Delete(string id)
        {
            try
            {
                var data = db.Supports.First(u => u.id == id);
                db.Supports.Remove(data);
                db.SaveChanges();
                return Json(new { ok = true, mess = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                return Redirect("/MNGAdmin/P404");
            }
        }
        #endregion

        #region[ham them, sua]
        [MyAuthorize]
        public ActionResult Add(Support model)
        {
            try
            {
                Support data = new Support();
                data.id = Guid.NewGuid().ToString();
                data.fullName = model.fullName;
                data.nick = model.nick;
                data.phone = model.phone;
                data.addresss = model.addresss;
                data.sType = model.sType;
                data.email = model.email;
                data.numberOder = model.numberOder;
                db.Supports.Add(data);
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
        public ActionResult Update(Support model)
        {
            try
            {
                Support data = db.Supports.FirstOrDefault(u => u.id.Equals(model.id));
                data.fullName = model.fullName;
                data.nick = model.nick;
                data.phone = model.phone;
                data.addresss = model.addresss;
                data.sType = model.sType;
                data.email = model.email;
                data.numberOder = model.numberOder;

                db.SaveChanges();
                return Json(new { ok = true, mess = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { ok = false, mess = "" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult UpdateOrCreate()
        {
            return PartialView();
        }

        public ActionResult ChangeOrder(string id, int numberOder)
        {
            var data = db.Supports.First(u => u.id.Equals(id));
            {
                try
                {
                    data.numberOder = numberOder;
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
