using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NTSPRODUCT.Models;
using System.Web.Script.Serialization;
using System.IO;

namespace NTSPRODUCT.Controllers
{
    public class DepartmentController : Controller
    {

        //
        // GET: /Department/

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
            var all = (from a in db.Departments.AsNoTracking()
                       where (a.lang.Equals(lang)
                       && (string.IsNullOrEmpty(model.Name) || a.name.ToLower().Contains(model.Name.ToLower()))
                       )
                       orderby a.createDate descending
                       select a).AsQueryable();

            var numOfNews = all.Select(u => u.id).Count();
            var currPage = model.PageNumber - 1;
            var data = all.Skip((model.PageNumber - 1) * model.PageSize).Take(model.PageSize).ToList();

            ViewBag.Index = (currPage * model.PageSize);
            ViewBag.TotalItem = numOfNews;
            ViewBag.PageSize = model.PageSize;
            if (numOfNews > model.PageSize)
            {
                ViewBag.pages = NTSPRODUCT.Models.Phantrang.PhanTrang(model.PageSize, currPage, numOfNews, "");
            }
            return PartialView(data);
        }
        #endregion

        #region[ham sua form]
        [HttpPost]
        [ValidateInput(false)]
        [MyAuthorize]
        public ActionResult Update(FormCollection f, string id)
        {
            try
            {
                var data = db.Departments.First(u => u.id.Equals(id));
                data.lang = lang;
                data.avata = f["avata"];
                data.dkey = f["dkey"];
                data.name = f["name"];
                data.descript = f["descript"];
                data.contents = f["contents"];
                data.createDate = DateTime.Now;
                data.dOrder = Convert.ToInt32(f["dOrder"]);
                data.titleSeo = f["titleSeo"];
                data.keySeo = f["keySeo"];
                data.desSeo = f["desSeo"];
                var newStatus = f["newStatus"];
                data.status = newStatus != null ? Constants.Active : Constants.NotActive;
                db.SaveChanges();
                Session["ok"] = "Cập nhật phòng đào tạo thành công!";
                return Redirect("/Department/Index");

            }
            catch (Exception)
            {
                Session["erro"] = "Đã xảy ra lỗi vui lòng thử lại!";
                return Redirect("/Department/Update/" + id);
            }
        }
        #endregion

        #region[ham sua]
        [MyAuthorize]
        public ActionResult Update(string id)
        {
            try
            {
                var data = db.Departments.First(u => u.id == id);
                return View(data);
            }
            catch (Exception)
            {
                return Redirect("/MNGAdmin/P404");
            }

        }
        #endregion

        #region[ham them form]
        [HttpPost]
        [ValidateInput(false)]
        [MyAuthorize]
        public ActionResult Add(FormCollection f)
        {
            try
            {
                var addType = f["addType"];
                Department data = new Department();
                data.lang = lang;
                data.id = Guid.NewGuid().ToString();
                data.avata = f["avata"];
                data.dkey = f["dkey"];
                data.name = f["name"];
                data.descript = f["descript"];
                data.contents = f["contents"];
                data.createDate = DateTime.Now;
                data.dOrder = Convert.ToInt32(f["dOrder"]);
                data.titleSeo = f["titleSeo"];
                data.keySeo = f["keySeo"];
                data.desSeo = f["desSeo"];
                var newStatus = f["newStatus"];
                data.status = newStatus != null ? Constants.Active : Constants.NotActive;
                db.Departments.Add(data);
                db.SaveChanges();
                Session["ok"] = "Thêm mới phòng đào tạo thành công!";
                if (addType.Equals("0"))
                {
                    return Redirect("/Department/Index");
                }
                else
                {
                    return Redirect("/Department/Add");
                }

            }
            catch (Exception)
            {
                Session["erro"] = "Đã xảy ra lỗi vui lòng thử lại!";
                return Redirect("/Department/Add");
            }
        }
        #endregion

        #region[ham them]
        [MyAuthorize]
        public ActionResult Add()
        {
            var data = new Department();
            data.dOrder = db.Departments.Select(u => u.id).Count() + 1;
            return View(data);
        }
        #endregion

        #region[ham xoa]
        [MyAuthorize]
        public ActionResult Delete(string id)
        {
            var data = db.Departments.First(u => u.id.Equals(id));
            {
                try
                {
                    db.Departments.Remove(data);
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
        public string checkTrung(string key)
        {
            string rs = key;
            var data = db.Departments.FirstOrDefault(u => u.dkey.Equals(key) && u.lang.Equals(lang));
            if (data != null)
            {
                rs = key + "-" + DateTime.Now.ToString("yyMMdd-HHmmss");
            }
            return rs;
        }
        public string checkTrungUpdate(string key, string id)
        {
            string rs = key;
            var data = db.Departments.FirstOrDefault(u => !id.Equals(u.id) && u.dkey.Equals(key) && u.lang.Equals(lang));
            if (data != null)
            {
                rs = key + "-" + DateTime.Now.ToString("yyMMdd-HHmmss");
            }
            return rs;
        }
        #endregion

        #region[cac ham cap nhap nhanh trang thai]

        [MyAuthorize]
        public ActionResult ChangeStatus(string id)
        {
            var data = db.Departments.First(u => u.id.Equals(id));
            {
                try
                {
                    if (data.status == Constants.Active)
                    {
                        data.status = Constants.NotActive;
                    }
                    else
                    {
                        data.status = Constants.Active;
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
        #endregion

        public ActionResult CheckKeyCreate(Department model)
        {
            try
            {
                var newCheck = db.Departments.FirstOrDefault(u => u.dkey.ToLower().Equals(model.dkey) && u.lang.Equals(lang));
                if (newCheck == null)
                {
                    return Json(new { ok = 1, mess = "" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { ok = 2, mess = "" }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception)
            {
                return Json(new { ok = 3, mess = "" }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult CheckKeyUpdate(Department model)
        {
            try
            {
                var newCheck = db.Departments.FirstOrDefault(u => !u.id.Equals(model.id) && u.dkey.ToLower().Equals(model.dkey) && u.lang.Equals(lang));
                if (newCheck == null)
                {
                    return Json(new { ok = 1, mess = "" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { ok = 2, mess = "" }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception)
            {
                return Json(new { ok = 3, mess = "" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GenNewKey(string name)
        {
            try
            {
                string rs = "";
                rs = StringClass.NameToTag(name.ToLower()).Replace(" ", "").Replace(System.Environment.NewLine, string.Empty);
                return Json(new { ok = true, rs = rs }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { ok = false, rs = "" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
