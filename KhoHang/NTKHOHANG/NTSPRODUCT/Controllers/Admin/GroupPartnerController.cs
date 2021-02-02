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
    public class GroupPartnerController : Controller
    {
        //
        // GET: /GroupPartner/
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
            var all = (from a in db.GroupPartners.AsNoTracking()
                       where (string.IsNullOrEmpty(model.Name) || a.name.ToLower().Contains(model.Name.ToLower()))
                       && (string.IsNullOrEmpty(model.Status) || a.gtype.Equals(model.Status))
                       && a.lang.Equals(lang)
                       orderby a.number
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
            var data = db.GroupPartners.First(u => u.id.Equals(id));
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region[ham them, sua]
        [MyAuthorize]
        public ActionResult Add(GroupPartner model)
        {
            try
            {
                GroupPartner data = new GroupPartner();
                data.id = Guid.NewGuid().ToString();
                data.number = model.number;
                data.name = model.name;
                data.lang = lang;
                data.gtype = model.gtype;
                string key = StringClass.NameToTag(data.name.ToLower()).Replace(" ", "").Replace(System.Environment.NewLine, string.Empty);
                data.keys = checkTrung(key, model.gtype);
                db.GroupPartners.Add(data);
                db.SaveChanges();
                return Json(new { ok = true, mess = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { ok = false, mess = "" }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Update(GroupPartner model)
        {
            try
            {
                var data = db.GroupPartners.First(u => u.id.Equals(model.id));
                data.name = model.name;
                data.number = model.number;
                data.lang = lang;
                data.gtype = model.gtype;
                string key = StringClass.NameToTag(data.name.ToLower()).Replace(" ", "").Replace(System.Environment.NewLine, string.Empty);
                data.keys = checkTrungUpdate(key, model.id, model.gtype);
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
            var data = db.GroupPartners.First(u => u.id.Equals(id));
            var checkChild = db.Partners.FirstOrDefault(u => u.groupId.Equals(id));
            if (checkChild != null)
            {
                return Json(new { ok = false, mess = "Nhóm đang dược sử dụng không thể xóa" }, JsonRequestBehavior.AllowGet);
            }
            {
                try
                {
                    db.GroupPartners.Remove(data);
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

        #region[check trung khi them va cap nhat]
        public string checkTrung(string key, string type)
        {
            string rs = key;
            var data = db.GroupPartners.FirstOrDefault(u => u.keys.Equals(key) && u.gtype.Equals(type) && u.lang.Equals(lang));
            if (data != null)
            {
                rs = key + "-" + DateTime.Now.ToString("yyMMdd-HHmmss");
            }
            return rs;
        }
        public string checkTrungUpdate(string key, string id, string type)
        {
            string rs = key;
            var data = db.GroupPartners.FirstOrDefault(u => !id.Equals(u.id) && u.keys.Equals(key) && u.gtype.Equals(type) && u.lang.Equals(lang));
            if (data != null)
            {
                rs = key + "-" + DateTime.Now.ToString("yyMMdd-HHmmss");
            }
            return rs;
        }
        #endregion

    }
}
