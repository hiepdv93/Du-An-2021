using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NTSPRODUCT.Models;
namespace NTSPRODUCT.Controllers
{
    public class ProdcutsController : Controller
    {
        //
        // GET: /Prodcuts/

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
            var all = (from a in db.Products.AsNoTracking()
                       where a.pLang.Equals(lang)
                       && (string.IsNullOrEmpty(model.CateId) || a.cateId.Equals(model.CateId))
                      && (string.IsNullOrEmpty(model.Name) || a.pro_name.ToLower().Contains(model.Name.ToLower()))
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
                var data = db.Products.First(u => u.id.Equals(id));
                data.cateId = f["cateId"];
                data.pro_view =1;
                data.proPrice = int.Parse(f["price"]);
                data.proPrice_sale = int.Parse(f["priceSale"]);
                data.pro_name = f["proName"];
                string keyTag = StringClass.NameToTag(data.pro_name.ToLower()).Replace(" ", "");
                var key = checkTrungUpdate(keyTag, data.id);
                data.pro_key = key;
                data.proFile = f["filePro"];
                data.brank = f["brank"];
                data.desPro = f["desPro"];

                data.proContentTab1 = f["proContentTab1"];
                data.proContentTab2 = f["proContentTab2"];
               // data.proContentTab3 = f["proContentTab3"];
                data.introContent = f["introContent"];
                data.proOrder = int.Parse(f["proOrder"]);
                //check
                var pro_home = f["proHome"];
                var pro_hot = f["proHot"];
                var active = f["proStatus"];
                data.pro_home = pro_home == null ? false : true;
                data.pro_hot = pro_hot == null ? false : true;
                data.active = active == null ? false : true;
                //seo
                data.desSeo = f["desSeo"];
                data.titleSeo = f["titleSeo"];
                data.keySeo = f["keySeo"];
                //ảnh
                data.proAvata = f["proAvata"];
                data.proImages1 = f["proAvata1"];
                data.proImages2 = f["proAvata2"];
                data.proImages3 = f["proAvata3"];
                data.proImages4 = f["proAvata4"];
                data.proImages5 = f["proAvata5"];
                //hết ảnh

                data.updateDate = DateTime.Now;
         
                db.SaveChanges();
                Session["ok"] = "Cập nhật thành công!";
                return Redirect("/Prodcuts/Index");

            }
            catch (Exception)
            {
                Session["erro"] = "Đã xảy ra lỗi vui lòng thử lại!";
                return Redirect("/Prodcuts/Update/" + id);
            }
        }
        [MyAuthorize]
        public ActionResult Update(string id)
        {
            try
            {
                var data = db.Products.First(u => u.id.Equals(id));
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
                Product data = new Product();
                data.id = Guid.NewGuid().ToString();
                data.pLang = lang;
                data.pro_view = 1;
                data.proPrice = int.Parse(f["price"]);
                data.proPrice_sale = int.Parse(f["priceSale"]);
                data.cateId = f["cateId"];
                data.pro_name = f["proName"];
                string keyTag = StringClass.NameToTag(data.pro_name.ToLower()).Replace(" ", "");
                var key = checkTrung(keyTag);
                data.pro_key = key;
                data.proFile = f["filePro"];
                data.brank = f["brank"];
                data.desPro = f["desPro"];
                data.proContentTab1 = f["proContentTab1"];
                data.proContentTab2 = f["proContentTab2"];
               // data.proContentTab3 = f["proContentTab3"];
                data.introContent = f["introContent"];
                data.proOrder = int.Parse(f["proOrder"]);
                //check
                var pro_home = f["proHome"];
                var pro_hot = f["proHot"];
                var active = f["proStatus"];
                data.pro_home = pro_home == null ? false : true;
                data.pro_hot = pro_hot == null ? false : true;
                data.active = active == null ? false : true;
                //seo
                data.desSeo = f["desSeo"];
                data.titleSeo = f["titleSeo"];
                data.keySeo = f["keySeo"];
                //ảnh
                data.proAvata = f["proAvata"];
                data.proImages1 = f["proAvata1"];
                data.proImages2 = f["proAvata2"];
                data.proImages3 = f["proAvata3"];
                data.proImages4 = f["proAvata4"];
                data.proImages5 = f["proAvata5"];

                data.createDate = DateTime.Now;
                data.updateDate = DateTime.Now;
           
                db.Products.Add(data);
                db.SaveChanges();
                Session["ok"] = "Thêm mới sản phẩm thành công!";
                if (addType.Equals("0"))
                {
                    return Redirect("/Prodcuts/Index");
                }
                else
                {
                    return Redirect("/Prodcuts/Add");
                }
            }
            catch (Exception)
            {
                Session["erro"] = "Đã xảy ra lỗi vui lòng thử lại!";
                return Redirect("/Prodcuts/Add/");
            }
        }
        [MyAuthorize]
        public ActionResult Add()
        {
            return View();
        }
        #endregion

        #region[ham xoa]
        [MyAuthorize]
        public ActionResult Delete(string id)
        {
            try
            {
                var data = db.Products.First(u => u.id.Equals(id));
                db.Products.Remove(data);
                db.SaveChanges();
                return Json(new { ok = true, mess = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { ok = false, mess = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion


        #region[check trung khi them va cap nhat]
        public string checkTrung(string key)
        {
            string rs = key;
            var data = db.Products.FirstOrDefault(u => u.pro_key.Equals(key) && u.pLang.Equals(lang));
            if (data != null)
            {
                rs = key + "-" + DateTime.Now.ToString("yyMMdd-HHmmss");
            }
            return rs;
        }
        public string checkTrungUpdate(string key, string id)
        {
            string rs = key;
            var data = db.Products.FirstOrDefault(u => !id.Equals(u.id) && u.pro_key.Equals(key) && u.pLang.Equals(lang));
            if (data != null)
            {
                rs = key + "-" + DateTime.Now.ToString("yyMMdd-HHmmss");
            }
            return rs;
        }
        #endregion

        #region[cac ham cap nhap nhanh trang thai]
        public ActionResult ChangeProductHot(string id)
        {
            var data = db.Products.First(u => u.id.Equals(id));
            {
                try
                {
                    if (data.pro_hot == true)
                    {
                        data.pro_hot = false;
                    }
                    else
                    {
                        data.pro_hot = true;
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
        public ActionResult ChangeStatus(string id)
        {
            var data = db.Products.First(u => u.id.Equals(id));
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
        #endregion

        public ActionResult CheckKeyCreate(Product model)
        {
            try
            {
                var newCheck = db.Products.FirstOrDefault(u => u.pro_key.ToLower().Equals(model.pro_key) && u.pLang.Equals(lang));
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
        public ActionResult CheckKeyUpdate(Product model)
        {
            try
            {
                var newCheck = db.Products.FirstOrDefault(u => !u.id.Equals(model.id) && u.pro_key.ToLower().Equals(model.pro_key) && u.pLang.Equals(lang));
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
