using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NTSPRODUCT.Models;

namespace NTSPRODUCT.Controllers
{
    public class CategorysController : Controller
    {
        //
        // GET: /Categorys/
        NTSWEBEntities db = new NTSWEBEntities();
        string lang = ClassExten.GetLang();

        #region[ham xoa nhom san pham]
        [MyAuthorize]
        public ActionResult Deletepro(string id)
        {
        
            var cateChild = db.Categorys.FirstOrDefault(u => u.catepar_id.Equals(id));
            if (cateChild != null)
            {
                return Json(new { ok = false, mess = "Nhóm đang được sử dụng không thể xóa!" }, JsonRequestBehavior.AllowGet);
            }
            try
            {
                var data = db.Categorys.First(u => u.id == id);
                db.Categorys.Remove(data);
                db.SaveChanges();
                return Json(new { ok = true, }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {
                return Json(new { ok = false, mess = "Phát sinh lỗi vui lòn thử lại!" }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region[ham xoa nhom tin]
        [MyAuthorize]
        public ActionResult Deletenew(string id)
        {
            var newList = db.News.Where(u => u.groupId == id).Select(u => u.id).Count();
            if (newList > 0)
            {
                return Json(new { ok = false, mess = "Nhóm đang được sử dụng không thể xóa!" }, JsonRequestBehavior.AllowGet);
            }
            var cateChild = db.Categorys.FirstOrDefault(u => u.catepar_id.Equals(id));
            if (cateChild != null)
            {
                return Json(new { ok = false, mess = "Nhóm đang được sử dụng không thể xóa!" }, JsonRequestBehavior.AllowGet);
            }
            try
            {
                var data = db.Categorys.First(u => u.id == id);
                db.Categorys.Remove(data);
                db.SaveChanges();
                return Json(new { ok = true, }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { ok = false, mess = "Phát sinh lỗi vui lòn thử lại!" }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region[ham view nhom pro]
        [MyAuthorize]
        public ActionResult Catepro()
        {
            return View();
        }

        [MyAuthorize]
        public ActionResult GetListCatePro(SearchModel model)
        {
            var all = (from a in db.Categorys.AsNoTracking()
                       where a.cateLang.Equals(lang) && ClassExten.typeCareer == a.cateType
                             && (string.IsNullOrEmpty(model.Name) || a.cateName.ToLower().Contains(model.Name.ToLower()))
                       orderby a.cate_cap, a.cateName
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

        #region[ham view nhom new]
        [MyAuthorize]
        public ActionResult Catenew()
        {
            return View();
        }
        [MyAuthorize]
        public ActionResult GetListCateNews(SearchModel model)
        {
            var all = (from a in db.Categorys.AsNoTracking()
                       where a.cateLang.Equals(lang) && ClassExten.typeNew == a.cateType
                             && (string.IsNullOrEmpty(model.Name) || a.cateName.ToLower().Contains(model.Name.ToLower()))
                       orderby a.cate_cap, a.cateName
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

        #region[nhom san  pham]
        [MyAuthorize]
        public ActionResult AddCatepro()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        [MyAuthorize]
        public ActionResult AddCatepro(FormCollection fr)
        {
            try
            {
                var addType = fr["addType"];
                string catename = fr["catename"];
                string catep = fr["cateId"];
                string order = fr["order"];
                string catedes = fr["catedes"];
                string note = fr["catenote"];
                string titleseo = fr["titleseo"];
                string keyseo = fr["keyseo"];
                string desseo = fr["desseo"];
                string ckHome = fr["ckHome"];
                string ckActive = fr["ckActive"];
                string cateAvata = fr["cateImage"];
                string key = StringClass.NameToTag(catename);
                Category cate = new Category();
                cate.cateName = catename;
                cate.cateicon = fr["cateicon"];
                cate.cateImage = fr["cateImage"];
                cate.cateDescription = fr["cateDescription"];
                cate.cateKey = checkTrung(key, ClassExten.typeCareer);
                cate.cateType = ClassExten.typeCareer;

                cate.cateActiveHome = ckHome == null ? false : true;
                cate.cateActive = ckActive == null ? false : true;

                cate.id = Guid.NewGuid().ToString();
                cate.cateLang = lang;
                cate.cateLevel = "";
                cate.titleSeo = titleseo;
                cate.desSeo = desseo;
                cate.keySeo = keyseo;
                cate.cateImage = cateAvata;
                cate.createDate = DateTime.Now;
                cate.updateDate = DateTime.Now;
                cate.cateOrder = Convert.ToInt32(order);
                cate.catepar_id = catep;
                cate.cate_cap = GetCap(catep);
                db.Categorys.Add(cate);
                db.SaveChanges();
                Session["ok"] = "Thêm mới thành công!";
                if (addType.Equals("0"))
                {
                    return Redirect("/Categorys/Catepro");
                }
                else
                {
                    return Redirect("/Categorys/AddCatepro");
                }
            }
            catch (Exception ex)
            {
                Session["erro"] = "Đã xảy ra lỗi vui lòng thử lại!";
                return Redirect("/Categorys/AddCatepro");
            }
        }
        [MyAuthorize]
        public ActionResult UpdateCatepro(string id)
        {
            var cate = db.Categorys.FirstOrDefault(u => u.id == id);
            return View(cate);
        }

        [HttpPost]
        [ValidateInput(false)]
        [MyAuthorize]
        public ActionResult UpdateCatepro(FormCollection fr, string id)
        {
            try
            {
                //int catetype = Convert.ToInt32(fr["catetype"]);
                string catename = fr["catename"];
                string catep = fr["cateId"];
                string order = fr["order"];
                string catedes = fr["catedes"];
                string note = fr["catenote"];
                string titleseo = fr["titleseo"];
                string keyseo = fr["keyseo"];
                string desseo = fr["desseo"];
                string ckHome = fr["ckHome"];
                string ckActive = fr["ckActive"];
                string cateAvata = fr["cateImage"];

                string key = StringClass.NameToTag(catename);

                Category cate = db.Categorys.FirstOrDefault(u => u.id == id);
                cate.cateName = catename;
                cate.cateicon = fr["cateicon"];
                cate.cateImage = fr["cateImage"];
                cate.cateDescription = fr["cateDescription"];
                if (cate.cateKey != key)
                {
                    cate.cateKey = checkTrungUpdate(key, ClassExten.typeCareer, id);
                }
                cate.cateType = ClassExten.typeCareer;
                cate.cateActiveHome = true;
                cate.cateActive = true;
                cate.cateActiveHome = ckHome == null ? false : true;
                cate.cateActive = ckActive == null ? false : true;
                cate.cateLang = lang;
                cate.cateLevel = "";
                cate.titleSeo = titleseo;
                cate.desSeo = desseo;
                cate.keySeo = keyseo;
                cate.cateImage = cateAvata;
                cate.updateDate = DateTime.Now;
                cate.cateOrder = Convert.ToInt32(order);
                cate.catepar_id = catep;
                cate.cate_cap = GetCap(catep);
                db.SaveChanges();
                Session["ok"] = "Cập nhật thành công!";
            }
            catch (Exception)
            {
                Session["erro"] = "Đã xảy ra lỗi vui lòng thử lại!";
                return Redirect("/Categorys/UpdateCatepro" + id);
            }
            return Redirect("/Categorys/Catepro");
        }
        #endregion

        #region[cac ham xu ly]
        public int GetCap(string number)
        {
            int rs = 1;
            if (number.Equals(ClassExten.cateParent))
            {
                rs = 1;
            }
            else
            {
                var cate = db.Categorys.FirstOrDefault(u => u.id == number).catepar_id;
                if (cate.Equals(ClassExten.cateParent))
                {
                    rs = 2;
                }
                else
                {
                    rs = 3;
                }
            }
            return rs;
        }
        public string checkTrung(string key, int type)
        {

            string rs = key;
            var cate = db.Categorys.FirstOrDefault(u => u.cateType == type && u.cateLang.Equals(lang) && u.cateKey.Equals(key));
            if (cate != null)
            {
                rs = key + "-" + DateTime.Now.ToString("yyMMdd-HHmmss");
            }
            return rs;
        }
        public string checkTrungUpdate(string key, int type, string id)
        {
            string rs = key;
            var cate = db.Categorys.FirstOrDefault(u => !u.id.Equals(id) && u.cateType == type && u.cateKey.Equals(key) && u.cateLang.Equals(lang));
            if (cate != null)
            {
                rs = key + "-" + DateTime.Now.ToString("yyMMdd-HHmmss");
            }
            return rs;
        }
        #endregion

        #region[nhom tin tuc]
        [MyAuthorize]
        public ActionResult AddCatenews()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        [MyAuthorize]
        public ActionResult AddCatenews(FormCollection fr)
        {
            try
            {

                var addType = fr["addType"];
                string catename = fr["catename"];
                string catep = fr["cateId"];
                string order = fr["order"];
                string catedes = fr["catedes"];
                string note = fr["catenote"];
                string titleseo = fr["titleseo"];
                string keyseo = fr["keyseo"];
                string desseo = fr["desseo"];
                string ckHome = fr["ckHome"];
                string ckActive = fr["ckActive"];
                string cateAvata = fr["cateAvata"];
                string key = StringClass.NameToTag(catename);
                Category cate = new Category();
                cate.id = Guid.NewGuid().ToString();
                cate.cateName = catename;
                cate.cateicon = fr["cateicon"];
                cate.cateImage = fr["cateImage"];
                cate.cateDescription = fr["cateDescription"];
                cate.cateKey = checkTrung(key, ClassExten.typeNew);
                cate.cateType = ClassExten.typeNew;
        
                cate.cateActiveHome = ckHome == null ? false : true;
                cate.cateActive = ckActive == null ? false : true;
                cate.cateLang = lang;
                cate.cateLevel = "";

                cate.titleSeo = titleseo;
                cate.desSeo = desseo;
                cate.keySeo = keyseo;
                cate.cateImage = cateAvata;
                cate.createDate = DateTime.Now;
                cate.updateDate = DateTime.Now;
                cate.cateOrder = Convert.ToInt32(order);
                cate.catepar_id = catep;
                cate.cate_cap = GetCap(catep);
                db.Categorys.Add(cate);

                db.SaveChanges();
                Session["ok"] = "Thêm mới thành công!";
                if (addType.Equals("0"))
                {
                    return Redirect("/Categorys/Catenew");
                }
                else
                {
                    return Redirect("/Categorys/AddCatenews");
                }
            }
            catch (Exception)
            {
                Session["erro"] = "Đã xảy ra lỗi vui lòng thử lại!";
                return Redirect("/Categorys/AddCatenews");
            }
        }

        [MyAuthorize]
        public ActionResult UpdateCatenews(string id)
        {
            var cate = db.Categorys.FirstOrDefault(u => u.id == id);
            return View(cate);
        }

        [HttpPost]
        [ValidateInput(false)]
        [MyAuthorize]
        public ActionResult UpdateCatenews(FormCollection fr, string id)
        {
            try
            {
                // int catetype = Convert.ToInt32(fr["catetype"]);
                string catename = fr["catename"];
                string catep = fr["cateId"];
                string order = fr["order"];
                string catedes = fr["catedes"];
                string note = fr["catenote"];
                string titleseo = fr["titleseo"];
                string keyseo = fr["keyseo"];
                string desseo = fr["desseo"];
                string ckHome = fr["ckHome"];
                string ckActive = fr["ckActive"];
                string cateAvata = fr["cateAvata"];
                string key = StringClass.NameToTag(catename);

                Category cate = db.Categorys.FirstOrDefault(u => u.id == id);
                cate.cateName = catename;
                cate.cateicon = fr["cateicon"];
                cate.cateImage = fr["cateImage"];
                cate.cateDescription = fr["cateDescription"];
                if (cate.cateKey != key)
                {
                    cate.cateKey = checkTrungUpdate(key, ClassExten.typeNew, id);
                }
                cate.cateType = ClassExten.typeNew;
                cate.cateActiveHome = true;
                cate.cateActive = true;
                cate.cateActiveHome = ckHome == null ? false : true;
                cate.cateActive = ckActive == null ? false : true;
                cate.cateLang = lang;
                cate.cateLevel = "";

                cate.titleSeo = titleseo;
                cate.desSeo = desseo;
                cate.keySeo = keyseo;
                cate.cateImage = cateAvata;
                cate.updateDate = DateTime.Now;
                cate.cateOrder = Convert.ToInt32(order);
                cate.catepar_id = catep;
                cate.cate_cap = GetCap(catep);
                db.SaveChanges();
                Session["ok"] = "Cập nhật thành công!";
            }
            catch (Exception)
            {
                Session["erro"] = "Đã xảy ra lỗi vui lòng thử lại!";
                return Redirect("/Categorys/UpdateCatenews/" + id);
            }
            return Redirect("/Categorys/Catenew");
        }
        #endregion
        [MyAuthorize]
        public ActionResult ChangeStatus(string id)
        {
            var data = db.Categorys.First(u => u.id == id);
            if (data==null)
            {
                return Json(new { ok = false, mess = "Bản ghi này đã bị xóa bởi người dùng khác!" }, JsonRequestBehavior.AllowGet);
            }
            try
            {
                data.cateActive = !data.cateActive;
                db.SaveChanges();
                return Json(new { ok = true, }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { ok = false, mess = "Phát sinh lỗi vui lòn thử lại!" }, JsonRequestBehavior.AllowGet);
            }
        }
        [MyAuthorize]
        public ActionResult ChangeActiveHome(string id)
        {
            var data = db.Categorys.First(u => u.id == id);
            if (data == null)
            {
                return Json(new { ok = false, mess = "Bản ghi này đã bị xóa bởi người dùng khác!" }, JsonRequestBehavior.AllowGet);
            }
            try
            {
                data.cateActiveHome = !data.cateActiveHome;
                db.SaveChanges();
                return Json(new { ok = true, }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { ok = false, mess = "Phát sinh lỗi vui lòn thử lại!" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
