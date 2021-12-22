using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NTSPRODUCT.Models;

namespace NTSPRODUCT.Controllers
{
    public class SologanController : Controller
    {
        //
        // GET: /Sologan/
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
            var all = (from a in db.Sologans.AsNoTracking()
                       where a.lang.Equals(lang)
                       && (string.IsNullOrEmpty(model.Name) || a.name.ToLower().Contains(model.Name.ToLower()))
                       && (model.Status == null || a.status == model.Status)
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
            var data = db.Sologans.First(u => u.id.Equals(id));
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region[ham them, sua]
        [MyAuthorize]
        public ActionResult Add(Sologan model)
        {
            try
            {
                Sologan data = new Sologan();
                data.id = Guid.NewGuid().ToString();
                data.name = model.name;
                data.avata = model.avata;
                data.descript = model.descript;
                data.status = model.status;
                data.dOrder = model.dOrder;
                data.lang = lang;
                data.createDate = DateTime.Now;
                db.Sologans.Add(data);
                db.SaveChanges();
                return Json(new { ok = true, mess = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { ok = false, mess = "" }, JsonRequestBehavior.AllowGet);
            }
        }
        [MyAuthorize]
        public ActionResult Update(Sologan model)
        {
            try
            {
                var data = db.Sologans.First(u => u.id.Equals(model.id));

                data.name = model.name;
                data.avata = model.avata;
                data.descript = model.descript;
                data.status = model.status;
                data.dOrder = model.dOrder;
                data.lang = lang;
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
            var data = db.Sologans.First(u => u.id.Equals(id));
            {
                try
                {
                    db.Sologans.Remove(data);
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
            var data = db.Sologans.First(u => u.id.Equals(id));
            {
                try
                {
                    if (data.status == Constants.Checked)
                    {
                        data.status = Constants.NotChecked;
                    }
                    else
                    {
                        data.status = Constants.Checked;
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
