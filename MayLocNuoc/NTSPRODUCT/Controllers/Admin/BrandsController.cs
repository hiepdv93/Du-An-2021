using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NTSPRODUCT.Models;

namespace NTSPRODUCT.Controllers
{
    public class BrandsController : Controller
    {
        //
        // GET: /Brands/

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
            var all = (from a in db.Brands.AsNoTracking()
                       where string.IsNullOrEmpty(model.Name) || a.bkName.ToLower().Contains(model.Name.ToLower())
                       orderby a.bkName
                       select a).AsQueryable();

            var numOfBrands = all.Select(u => u.id).Count();
            var currPage = model.PageNumber - 1;
            var data = all.Skip((model.PageNumber - 1) * model.PageSize).Take(model.PageSize).ToList();

            ViewBag.Index = (currPage * model.PageSize);
            ViewBag.TotalItem = numOfBrands;
            ViewBag.PageSize = model.PageSize;
            if (numOfBrands > model.PageSize)
            {
                ViewBag.pages = NTSPRODUCT.Models.Phantrang.PhanTrang(model.PageSize, currPage, numOfBrands, "");
            }
            return PartialView(data);
        }

        #endregion


        #region[ham sua]
        [MyAuthorize]
        public ActionResult Getbyid(string id)
        {
            var data = db.Brands.First(u => u.id.Equals(id));
            Brand brand = new Brand();
            brand.id = data.id;
            brand.bkName = data.bkName;
            brand.bkImage = data.bkImage;
            brand.note = data.note;
            brand.numberOder = data.numberOder;
            return Json(brand, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region[ham xoa]
        [MyAuthorize]
        public ActionResult Delete(string id)
        {
            var data = db.Brands.First(u => u.id.Equals(id));
            {
                try
                {
                    db.Brands.Remove(data);
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
        public ActionResult Add(Brand model)
        {
            var check = db.Brands.FirstOrDefault(u=>u.bkName.ToLower().Equals(model.bkName.ToLower()));
            if (check!=null)
            {
                return Json(new { ok = false, mess = "Tên thương hiệu đã tồn tại" }, JsonRequestBehavior.AllowGet);
            }
            try
            {
                Brand data = new Brand();
                data.id = Guid.NewGuid().ToString();
                data.bkName = model.bkName;
                data.bkKey = StringClass.NameToTag(model.bkName);
                data.bkImage = model.bkImage;
                data.note = model.note;
                data.numberOder = model.numberOder;
                db.Brands.Add(data);
                db.SaveChanges();
                return Json(new { ok = true, mess = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { ok = false, mess = "" }, JsonRequestBehavior.AllowGet);
            }
        }
        [MyAuthorize]
        public ActionResult Update(Brand model)
        {
            var check = db.Brands.FirstOrDefault(u =>!u.id.Equals(model.id) && u.bkName.ToLower().Equals(model.bkName.ToLower()));
            if (check != null)
            {
                return Json(new { ok = false, mess = "Tên thương hiệu đã tồn tại" }, JsonRequestBehavior.AllowGet);
            }
            try
            {
                Brand data = db.Brands.FirstOrDefault(u => u.id.Equals(model.id));
                data.bkName = model.bkName;
                data.bkImage = model.bkImage;
                data.note = model.note;
                data.numberOder = model.numberOder;
                data.bkKey = StringClass.NameToTag(model.bkName);
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

    }
}
