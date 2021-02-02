using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NTSPRODUCT.Models;

namespace NTSPRODUCT.Controllers
{
    public class SayWeController : Controller
    {
        //
        // GET: /Saywe/
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
            var all = (from a in db.SayWes.AsNoTracking()
                       where a.slang.Equals(lang)
                       && (string.IsNullOrEmpty(model.Name) || a.fullName.ToLower().Contains(model.Name.ToLower()))
                       && (string.IsNullOrEmpty(model.Email) || a.email.ToLower().Contains(model.Email.ToLower()))
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
            var data = db.SayWes.First(u => u.id.Equals(id));
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region[ham them, sua]
        [MyAuthorize]
        public ActionResult Add(SayWe model)
        {
            try
            {
                SayWe data = new SayWe();
                data.id = Guid.NewGuid().ToString();
                data.fullName = model.fullName;
                data.email = model.email;
                data.contents = model.contents;
                data.phone = model.phone;
                data.addresss = model.addresss;
                data.avata = model.avata;
                data.active = model.active;
                data.slang = lang;
                data.numberOder = model.numberOder;
                data.createDate = DateTime.Now;
                db.SayWes.Add(data);
                db.SaveChanges();
                return Json(new { ok = true, mess = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { ok = false, mess = "" }, JsonRequestBehavior.AllowGet);
            }
        }
        [MyAuthorize]
        public ActionResult Update(SayWe model)
        {
            try
            {
                var data = db.SayWes.First(u => u.id.Equals(model.id));

                data.fullName = model.fullName;
                data.email = model.email;
                data.contents = model.contents;
                data.phone = model.phone;
                data.addresss = model.addresss;
                data.avata = model.avata;
                data.active = model.active;
                data.slang = lang;
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
            var data = db.SayWes.First(u => u.id.Equals(id));
            {
                try
                {
                    db.SayWes.Remove(data);
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
            var data = db.SayWes.First(u => u.id.Equals(id));
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
