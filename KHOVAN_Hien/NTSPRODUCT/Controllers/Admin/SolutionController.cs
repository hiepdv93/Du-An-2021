using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NTSPRODUCT.Models;

namespace NTSPRODUCT.Controllers
{
    public class SolutionController : Controller
    {
        //
        // GET: /Solution/
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
            var all = (from a in db.Solutions.AsNoTracking()
                       where (a.lang.Equals(lang)
                       && (string.IsNullOrEmpty(model.Name) || a.name.ToLower().Contains(model.Name.ToLower()))
                       && (model.Status == null || a.solutionActive == model.Status)
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
                var data = db.Solutions.FirstOrDefault(u => u.id.Equals(id));
                if (data != null)
                {
                    data.lang = lang;
                    data.solutionImage = f["solutionImage"];
                    data.solutionIcon = f["solutionIcon"];
                    data.name_url = f["name_url"];
                    data.name = f["name"];
                    data.solutionDescription = f["solutionDescription"];
                    data.solutionDetail = f["solutionDetail"];
                    data.createDate = DateTime.Now;
                    data.solutionOrder = Convert.ToInt32(f["solutionOrder"]);
                    data.titleSeo = f["titleSeo"];
                    data.keySeo = f["keySeo"];
                    data.desSeo = f["desSeo"];
                    var status = f["status"];
                    data.solutionActive = status != null ? true : false;
                    db.SaveChanges();
                    Session["ok"] = "Cập nhật giải pháp thành công!";
                    return Redirect("/Solution/Index");
                }
                else
                {
                    return Redirect("/Home/Page404");
                }
            }
            catch (Exception)
            {
                Session["erro"] = "Đã xảy ra lỗi vui lòng thử lại!";
                return Redirect("/Solution/Update/" + id);
            }
        }
        #endregion
        #region[ham sua]
        [MyAuthorize]
        public ActionResult Update(string id)
        {
            var data = db.Solutions.First(u => u.id.Equals(id));
            if (data != null)
            {
                return View(data);
            }
            else
            {
                return Redirect("/Home/Page404");
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
                Solution data = new Solution();
                data.id = Guid.NewGuid().ToString();
                data.lang = lang;
                data.solutionImage = f["solutionImage"];
                data.solutionIcon = f["solutionIcon"];
                data.name_url = f["name_url"];
                data.name = f["name"];
                data.solutionDescription = f["solutionDescription"];
                data.solutionDetail = f["solutionDetail"];
                data.createDate = DateTime.Now;
                data.solutionOrder = Convert.ToInt32(f["solutionOrder"]);
                data.titleSeo = f["titleSeo"];
                data.keySeo = f["keySeo"];
                data.desSeo = f["desSeo"];
                var status = f["status"];
                data.solutionActive = status != null ? true : false;
                db.Solutions.Add(data);
                db.SaveChanges();
                Session["ok"] = "Thêm mới giải pháp thành công!";
                if (addType.Equals("0"))
                {
                    return Redirect("/Solution/Index");
                }
                else
                {
                    return Redirect("/Solution/Add");
                }

            }
            catch (Exception)
            {
                Session["erro"] = "Đã xảy ra lỗi vui lòng thử lại!";
                return Redirect("/Solution/Add");
            }
        }
        #endregion
        #region[ham them]
        [MyAuthorize]
        public ActionResult Add()
        {
            var data = new Solution();
            data.solutionOrder = 1;
            return View(data);
        }
        #endregion
        #region[ham xoa]
        [MyAuthorize]
        public ActionResult Delete(string id)
        {
            var data = db.Solutions.FirstOrDefault(u => u.id.Equals(id));
            if (data != null)
            {
                db.Solutions.Remove(data);
                db.SaveChanges();
                return Json(new { ok = true, mess = "" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { ok = false, mess = "Tin đã bị xóa bởi người dùng khác" }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
        [MyAuthorize]
        public ActionResult ChangeStatus(string id)
        {
            var data = db.Solutions.First(u => u.id.Equals(id));
            {
                try
                {
                    if (data.solutionActive == true)
                    {
                        data.solutionActive = false;
                    }
                    else
                    {
                        data.solutionActive = true;
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
        public ActionResult CheckKeyCreate(Solution model)
        {
            try
            {
                var newCheck = db.Solutions.FirstOrDefault(u => u.name_url.ToLower().Equals(model.name_url.ToLower()) && u.lang.Equals(lang));
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
        public ActionResult CheckKeyUpdate(Solution model)
        {
            try
            {
                var newCheck = db.Solutions.FirstOrDefault(u => !u.id.Equals(model.id) && u.name_url.ToLower().Equals(model.name_url.ToLower()) && u.lang.Equals(lang));
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

    }
}
