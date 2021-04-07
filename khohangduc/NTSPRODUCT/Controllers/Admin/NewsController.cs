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
    public class NewsController : Controller
    {

        //
        // GET: /News/

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
            var all = (from a in db.News.AsNoTracking()
                       where (a.newLang.Equals(lang) &&
                       (string.IsNullOrEmpty(model.CateId) || model.CateId.Equals(a.groupId))
                       && (string.IsNullOrEmpty(model.Name) || a.title.ToLower().Contains(model.Name.ToLower()))
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
                var data = db.News.First(u => u.id.Equals(id));
                data.groupId = f["cateId"];
                data.newImage = f["img"];
                data.newFile = f["newFile"];
                data.title = f["newTitle"];
                data.descript = f["newDescription"];
                data.contents = f["contents"];
                data.updateDate = DateTime.Now;
                data.newOrder = Convert.ToInt32(f["newOrder"]);

                data.titleSeo = f["titleSeo"];
                data.keySeo = f["keySeo"];
                data.desSeo = f["desSeo"];

                var newHot = f["newHot"];
                var newStatus = f["newStatus"];
                var newNew = f["newNew"];
                ///string key = StringClass.NameToTag(data.title.ToLower()).Replace(" ", "").Replace(System.Environment.NewLine, string.Empty);
                //if (!data.new_key.Equals(key))
                //{
                //    data.new_key = checkTrungUpdate(key, data.id);
                //}
                data.new_key = f["new_key"];
                data.newHot = newHot != null ? true : false;
                data.newNew = newNew != null ? true : false;
                data.status = newStatus != null ? Constants.Active : Constants.NotActive;
                db.SaveChanges();
                Session["ok"] = "Cập nhật tin tức thành công!";
                return Redirect("/News/Index");

            }
            catch (Exception)
            {
                Session["erro"] = "Đã xảy ra lỗi vui lòng thử lại!";
                return Redirect("/News/Update/" + id);
            }
        }
        #endregion

        #region[ham sua]
        [MyAuthorize]
        public ActionResult Update(string id)
        {
            try
            {
                var data = db.News.First(u => u.id == id);
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
                News data = new News();
                data.id = Guid.NewGuid().ToString();
                data.groupId = f["cateId"];
                data.newImage = f["img"];
                data.newFile = f["newFile"];
                data.title = f["newTitle"];
                data.descript = f["newDescription"];
                data.contents = f["contents"];
                data.createDate = DateTime.Now;
                data.updateDate = DateTime.Now;
                data.newOrder = Convert.ToInt32(f["newOrder"]);

                data.titleSeo = f["titleSeo"];
                data.keySeo = f["keySeo"];
                data.desSeo = f["desSeo"];

                var newHot = f["newHot"];
                var newStatus = f["newStatus"];
                var newNew = f["newNew"];
               // string key = StringClass.NameToTag(data.title.ToLower()).Replace(" ", "").Replace(System.Environment.NewLine, string.Empty);
                data.new_key = f["new_key"];// checkTrung(key);

                data.newHot = newHot == null ? false : true;
                data.newNew = newNew == null ? false : true;
                data.status = newStatus != null ? Constants.Active : Constants.NotActive;
                data.newLang = lang;
                data.viewCount = 0;
                db.News.Add(data);
                db.SaveChanges();
                Session["ok"] = "Thêm mới tin tức thành công!";
                if (addType.Equals("0"))
                {
                    return Redirect("/News/Index");
                }
                else
                {
                    return Redirect("/News/Add");
                }

            }
            catch (Exception)
            {
                Session["erro"] = "Đã xảy ra lỗi vui lòng thử lại!";
                return Redirect("/News/Add");
            }
        }
        #endregion

        #region[ham them]
        [MyAuthorize]
        public ActionResult Add()
        {
            var data = new News();
            data.newOrder = db.News.Select(u => u.id).Count() + 1;
            return View(data);
        }
        #endregion

        #region[ham xoa]
        [MyAuthorize]
        public ActionResult Delete(string id)
        {
            var data = db.News.First(u => u.id.Equals(id));
            {
                try
                {
                    db.News.Remove(data);
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
            var data = db.News.FirstOrDefault(u => u.new_key.Equals(key) && u.newLang.Equals(lang));
            if (data != null)
            {
                rs = key + "-" + DateTime.Now.ToString("yyMMdd-HHmmss");
            }
            return rs;
        }
        public string checkTrungUpdate(string key, string id)
        {
            string rs = key;
            var data = db.News.FirstOrDefault(u => !id.Equals(u.id) && u.new_key.Equals(key) && u.newLang.Equals(lang));
            if (data != null)
            {
                rs = key + "-" + DateTime.Now.ToString("yyMMdd-HHmmss");
            }
            return rs;
        }
        #endregion

        #region[cac ham cap nhap nhanh trang thai]
        [MyAuthorize]
        public ActionResult ChangeNewNew(string id)
        {
            var data = db.News.First(u => u.id.Equals(id));
            {
                try
                {
                    if (data.newNew == true)
                    {
                        data.newNew = false;
                    }
                    else
                    {
                        data.newNew = true;
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
        public ActionResult ChangeNewHot(string id)
        {
            var data = db.News.First(u => u.id.Equals(id));
            {
                try
                {
                    if (data.newHot == true)
                    {
                        data.newHot = false;
                    }
                    else
                    {
                        data.newHot = true;
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
        public ActionResult ChangeStatus(string id)
        {
            var data = db.News.First(u => u.id.Equals(id));
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
        public ActionResult ChangeOrder(string id, int newOrder)
        {
            var data = db.News.First(u => u.id.Equals(id));
            {
                try
                {
                    data.newOrder = newOrder;
                    db.SaveChanges();
                    return Json(new { ok = true, mess = "" }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(new { ok = false, mess = ex.Message }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public ActionResult CheckKeyCreate(News model)
        {
            try
            {
                var newCheck = db.News.FirstOrDefault(u => u.new_key.ToLower().Equals(model.new_key) && u.newLang.Equals(lang));
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
        public ActionResult CheckKeyUpdate(News model)
        {
            try
            {
                var newCheck = db.News.FirstOrDefault(u => !u.id.Equals(model.id) && u.new_key.ToLower().Equals(model.new_key) && u.newLang.Equals(lang));
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
