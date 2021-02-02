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
    public class TeacherController : Controller
    {

        //
        // GET: /Teacher/

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
            var all = (from a in db.Teachers.AsNoTracking()
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
                var data = db.Teachers.First(u => u.id.Equals(id));
                data.avata = f["img"];
                data.name = f["teacherName"];
                data.note = f["note"];
                data.descript = f["descript"];
                data.createDate = DateTime.Now;
                data.tOrder = Convert.ToInt32(f["tOrder"]);
                data.titleSeo = f["titleSeo"];
                data.keySeo = f["keySeo"];
                data.desSeo = f["desSeo"];

                var tHot = f["tHot"];
                var teacherStatus = f["teacherStatus"];
                data.tkey = f["teacher_key"];
                data.status = teacherStatus != null ?true: false;
                data.thot = tHot != null ?true: false;
                db.SaveChanges();
                Session["ok"] = "Cập nhật lĩnh vực hoạt động thành công!";
                return Redirect("/Teacher/Index");

            }
            catch (Exception)
            {
                Session["erro"] = "Đã xảy ra lỗi vui lòng thử lại!";
                return Redirect("/Teacher/Update/" + id);
            }
        }
        #endregion

        #region[ham sua]
        [MyAuthorize]
        public ActionResult Update(string id)
        {
            try
            {
                var data = db.Teachers.First(u => u.id == id);
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
                Teacher data = new Teacher();
                data.id = Guid.NewGuid().ToString();
                data.avata = f["img"];
                data.name = f["teacherName"];
                data.note = f["note"];
                data.descript = f["descript"];
                data.createDate = DateTime.Now;
                data.tOrder = Convert.ToInt32(f["tOrder"]);

                data.titleSeo = f["titleSeo"];
                data.keySeo = f["keySeo"];
                data.desSeo = f["desSeo"];

                var teacherStatus = f["teacherStatus"];
                var tHot = f["tHot"];
                data.tkey = f["teacher_key"];// checkTrung(key);

                data.status = teacherStatus != null ? true: false;
                data.thot = tHot != null ? true: false;
                data.lang = lang;
                db.Teachers.Add(data);
                db.SaveChanges();
                Session["ok"] = "Thêm mới lĩnh vực hoạt động thành công!";
                if (addType.Equals("0"))
                {
                    return Redirect("/Teacher/Index");
                }
                else
                {
                    return Redirect("/Teacher/Add");
                }

            }
            catch (Exception)
            {
                Session["erro"] = "Đã xảy ra lỗi vui lòng thử lại!";
                return Redirect("/Teacher/Add");
            }
        }
        #endregion

        #region[ham them]
        [MyAuthorize]
        public ActionResult Add()
        {
            var data = new Teacher();
            data.tOrder = db.Teachers.Select(u => u.id).Count() + 1;
            return View(data);
        }
        #endregion

        #region[ham xoa]
        [MyAuthorize]
        public ActionResult Delete(string id)
        {
            var data = db.Teachers.First(u => u.id.Equals(id));
            {
                try
                {
                    db.Teachers.Remove(data);
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
            var data = db.Teachers.FirstOrDefault(u => u.tkey.Equals(key) && u.lang.Equals(lang));
            if (data != null)
            {
                rs = key + "-" + DateTime.Now.ToString("yyMMdd-HHmmss");
            }
            return rs;
        }
        public string checkTrungUpdate(string key, string id)
        {
            string rs = key;
            var data = db.Teachers.FirstOrDefault(u => !id.Equals(u.id) && u.tkey.Equals(key) && u.lang.Equals(lang));
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
            var data = db.Teachers.First(u => u.id.Equals(id));
            {
                try
                {
                    if (data.status ==true)
                    {
                        data.status = false;
                    }
                    else
                    {
                        data.status = true;
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
        [MyAuthorize]
        public ActionResult ChangeHot(string id)
        {
            var data = db.Teachers.First(u => u.id.Equals(id));
            {
                try
                {
                    if (data.thot == true)
                    {
                        data.thot = false;
                    }
                    else
                    {
                        data.thot = true;
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

        public ActionResult CheckKeyCreate(Teacher model)
        {
            try
            {
                var newCheck = db.Teachers.FirstOrDefault(u => u.tkey.ToLower().Equals(model.tkey) && u.lang.Equals(lang));
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
        public ActionResult CheckKeyUpdate(Teacher model)
        {
            try
            {
                var newCheck = db.Teachers.FirstOrDefault(u => !u.id.Equals(model.id) && u.tkey.ToLower().Equals(model.tkey) && u.lang.Equals(lang));
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

        public ActionResult GenTeacherKey(string name)
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
