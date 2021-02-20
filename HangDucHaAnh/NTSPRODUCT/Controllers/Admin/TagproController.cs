using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NTSPRODUCT.Models;
namespace NTSPRODUCT.Controllers
{
    public class TagproController : Controller
    {
        //
        // GET: /Tagpro/


        NTSWEBEntities db = new NTSWEBEntities();
        string lang = ClassExten.GetLang();
        #region[ham view]
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetList(SearchModel model)
        {
            var all = (from a in db.Tagproes.AsNoTracking()
                       where (string.IsNullOrEmpty(model.Name) || a.tagName.ToLower().Contains(model.Name.ToLower()))
                       && (model.Type == null || a.typeTag == model.Type)
                       && a.lang.Equals(lang)
                       orderby a.tagOrder
                       select a).AsQueryable();

            var numOfTagpros = all.Select(u => u.tagId).Count();
            var currPage = model.PageNumber - 1;
            var data = all.Skip((model.PageNumber - 1) * model.PageSize).Take(model.PageSize).ToList();

            ViewBag.Index = (currPage * model.PageSize);
            ViewBag.TotalItem = numOfTagpros;
            ViewBag.PageSize = model.PageSize;
            if (numOfTagpros > model.PageSize)
            {
                ViewBag.pages = NTSPRODUCT.Models.Phantrang.PhanTrang(model.PageSize, currPage, numOfTagpros, "");
            }
            return PartialView(data);
        }
        #endregion

        #region[ham them sua]
        [HttpPost]
        public ActionResult Update(Tagpro model)
        {
            if (!string.IsNullOrEmpty(model.tagId))
            {
                //check trung update
                var check = db.Tagproes.FirstOrDefault(u => u.lang.Equals(lang) && !u.tagId.Equals(model.tagId) && u.typeTag == model.typeTag && u.tagName.ToLower().Equals(model.tagName.ToLower()));
                if (check != null)
                {
                    return Json(new { ok = false, mess = "Tên thẻ tag đã tồn tại" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                //check trung them moi
                var check = db.Tagproes.FirstOrDefault(u => u.typeTag == model.typeTag && u.tagName.ToLower().Equals(model.tagName.ToLower()));
                if (check != null)
                {
                    return Json(new { ok = false, mess = "Tên thẻ tag đã tồn tại" }, JsonRequestBehavior.AllowGet);
                }
            }
            try
            {
                if (string.IsNullOrEmpty(model.tagId))
                {
                    Tagpro data = new Tagpro();
                    data.tagId = Guid.NewGuid().ToString();
                    data.tagName = model.tagName;
                    data.tagKey = StringClass.NameToTag(model.tagName.Trim().ToLower());
                    data.tagOrder = model.tagOrder;
                    data.typeTag = model.typeTag;
                    data.lang = lang;
                    db.Tagproes.Add(data);
                    db.SaveChanges();
                    return Json(new { ok = true, mess = "" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var data = db.Tagproes.FirstOrDefault(u => u.tagId.Equals(model.tagId));
                    data.tagName = model.tagName;
                    data.tagKey = StringClass.NameToTag(model.tagName.Trim().ToLower());
                    data.tagOrder = model.tagOrder;
                    data.typeTag = model.typeTag;
                    data.lang = lang;
                    db.SaveChanges();
                    return Json(new { ok = true, mess = "" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {
                return Json(new { ok = false, mess = "Có lỗi phát sinh, vui lòng thử lại" }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region[ham sua]
        public ActionResult Getbyid(string id)
        {
            var data = db.Tagproes.First(u => u.tagId == id);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region[ham xoa]
        public ActionResult Delete(string id)
        {
            var data = db.Tagproes.First(u => u.tagId.Equals(id));
            {
                try
                {
                    var proTag = db.ProTags.Where(u => u.tagId.Equals(id));
                    if (proTag.Count() > 0)
                    {
                        db.ProTags.RemoveRange(proTag);
                    }
                    db.Tagproes.Remove(data);
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

        public ActionResult UpdateOrCreate()
        {
            return PartialView();
        }

    }
}
