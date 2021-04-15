using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
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
            Config conf;
            if (ConfigModel.listConfig == null)
            {
                ConfigModel.listConfig = db.Configs.ToList();
            }
            conf = ConfigModel.listConfig.FirstOrDefault();

            var all = (from a in db.Products.AsNoTracking()
                       where a.pLang.Equals(lang)
                       && (string.IsNullOrEmpty(model.CateId) || a.cateId.Equals(model.CateId))
                      && (string.IsNullOrEmpty(model.Name) || a.pro_name.ToLower().Contains(model.Name.ToLower()))
                       //orderby a.createDate descending
                       select a).AsQueryable();
            if (conf.viewProBy == ClassExten.ViewPro.ThuTu)
            {
                all = all.OrderBy(u => u.proOrder);
            }
            else
            {
                all = all.OrderByDescending(u => u.createDate);
            }
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
                data.brandId = f["brandId"];
                data.pro_view = 1;
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
                var pro_sale = f["pro_sale"];

                data.pro_sale = pro_sale == null ? false : true;
                data.pro_home = pro_home == null ? false : true;
                data.pro_hot = pro_hot == null ? false : true;
                data.active = active == null ? false : true;
                //seo
                data.desSeo = f["desSeo"];
                data.titleSeo = f["titleSeo"];
                data.keySeo = f["keySeo"];

                if (string.IsNullOrEmpty(data.desSeo))
                {
                    data.desSeo = data.pro_name;
                }
                if (string.IsNullOrEmpty(data.titleSeo))
                {
                    data.titleSeo = data.pro_name;
                }
                if (string.IsNullOrEmpty(data.keySeo))
                {
                    data.keySeo = data.pro_name;
                }
                //ảnh
                data.proAvata = f["proAvata"];
                data.proImages1 = f["proAvata1"];
                data.proImages2 = f["proAvata2"];
                data.proImages3 = f["proAvata3"];
                data.proImages4 = f["proAvata4"];
                data.proImages5 = f["proAvata5"];
                //hết ảnh

                data.updateDate = DateTime.Now;

                #region[giai phap]
                var protag = db.ProTags.Where(u => u.proId.Equals(data.id));
                if (protag.Count() > 0)
                {
                    db.ProTags.RemoveRange(protag);
                }
                string tagId = "";
                var Faqnumber = Convert.ToInt32(f["Tagnumber"]);
                if (Faqnumber > 0)
                {
                    ProTag pros;
                    for (int i = 0; i < Faqnumber; i++)
                    {

                        tagId = f["ckTag_" + i];
                        if (tagId != null)
                        {
                            pros = new ProTag();
                            pros.idPT = Guid.NewGuid().ToString();
                            pros.proId = data.id;
                            pros.tagId = f["valueTag_" + i];
                            db.ProTags.Add(pros);
                        }
                    }
                }
                #endregion
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
                var tags = db.Tagproes.Where(u => u.lang.Equals(lang)).OrderBy(u => u.tagOrder).ToList();
                ViewBag.tags = tags;
                var tagSelect = db.ProTags.Where(u => u.proId.Equals(id)).Select(u => u.tagId).ToList();
                ViewBag.tagSelect = tagSelect;
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
                data.brandId = f["brandId"];
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
                var pro_sale = f["pro_sale"];

                data.pro_sale = pro_sale == null ? false : true;
                data.pro_home = pro_home == null ? false : true;
                data.pro_hot = pro_hot == null ? false : true;
                data.active = active == null ? false : true;
                //seo
                data.desSeo = f["desSeo"];
                data.titleSeo = f["titleSeo"];
                data.keySeo = f["keySeo"];

                if (string.IsNullOrEmpty(data.desSeo))
                {
                    data.desSeo = data.pro_name;
                }
                if (string.IsNullOrEmpty(data.titleSeo))
                {
                    data.titleSeo = data.pro_name;
                }
                if (string.IsNullOrEmpty(data.keySeo))
                {
                    data.keySeo = data.pro_name;
                }
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
                #region[giai phap]
                string tagId = "";
                var Faqnumber = Convert.ToInt32(f["Tagnumber"]);
                if (Faqnumber > 0)
                {
                    ProTag pros;
                    for (int i = 0; i < Faqnumber; i++)
                    {

                        tagId = f["ckTag_" + i];
                        if (tagId != null)
                        {
                            pros = new ProTag();
                            pros.idPT = Guid.NewGuid().ToString();
                            pros.proId = data.id;
                            pros.tagId = f["valueTag_" + i];
                            db.ProTags.Add(pros);
                        }
                    }
                }
                #endregion


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
            var tags = db.Tagproes.Where(u => u.lang.Equals(lang)).OrderBy(u => u.tagOrder).ToList();
            ViewBag.tags = tags;
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
        public ActionResult ChangeProductSale(string id)
        {
            var data = db.Products.First(u => u.id.Equals(id));
            {
                try
                {
                    if (data.pro_sale == true)
                    {
                        data.pro_sale = false;
                    }
                    else
                    {
                        data.pro_sale = true;
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
        public ActionResult ChangeOrder(string id, int proOrder)
        {
            var data = db.Products.First(u => u.id.Equals(id));
            {
                try
                {
                    data.proOrder = proOrder;
                    db.SaveChanges();
                    return Json(new { ok = true, mess = "" }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(new { ok = false, mess = ex.Message }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        public ActionResult ChangePrice(string id, string price, int type)
        {
            var data = db.Products.First(u => u.id.Equals(id));
            {
                try
                {
                    if (type == 1)
                    {
                        data.proPrice = int.Parse(price.Trim().Replace(",", string.Empty));
                    }
                    else
                    {
                        data.proPrice_sale = int.Parse(price.Trim().Replace(",", string.Empty));
                    }
                    db.SaveChanges();
                    return Json(new { ok = true, mess = "" }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(new { ok = false, mess = "Đơn giá phải là số" }, JsonRequestBehavior.AllowGet);
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

        [MyAuthorize]
        public ActionResult AutoPro(string id, string pid, int? number)
        {
            int sl = 10;
            if (number != null)
            {
                sl = number.Value;
            }

            var product = db.Products.FirstOrDefault(u => u.id.Equals(id));
            if (product != null)
            {
                for (int i = 0; i < sl; i++)
                {
                    var pro = new Product()
                    {
                        createDate = product.createDate,
                        cateId = product.cateId,
                        brank = product.brank,
                        desPro = product.desPro,
                        active = product.active,
                        desSeo = product.desSeo,
                        brandId = product.brandId,
                        introContent = product.introContent,
                        keySeo = product.keySeo,
                        pLang = product.pLang,

                        proAvata = product.proAvata,
                        proContentTab1 = product.proContentTab1,
                        proContentTab2 = product.proContentTab2,
                        proFile = product.proAvata,
                        proImages1 = product.proImages1,
                        proImages2 = product.proImages2,
                        proImages3 = product.proImages3,
                        proImages4 = product.proImages4,
                        proImages5 = product.proImages5,
                        proOrder = product.proOrder,

                        proPrice = product.proPrice,
                        proPrice_sale = product.proPrice_sale,
                        pro_home = product.pro_home,
                        pro_hot = product.pro_hot,
                        pro_name = product.pro_name,
                        pro_view = product.pro_view,
                        titleSeo = product.titleSeo,
                        updateDate = product.updateDate,
                        id = Guid.NewGuid().ToString()

                    };
                    pro.pro_key = product.pro_key + "-" + DateTime.Now.ToString("yyMMdd-HHmmss") + i;
                    if (!string.IsNullOrEmpty(pid))
                    {
                        pro.cateId = pid;
                    }
                    db.Products.Add(pro);
                }
                db.SaveChanges();
            }
            ViewBag.countAll = db.Products.Select(u => u.id).Count();
            return View();
        }


        [MyAuthorize]
        public ActionResult AutoNews(string id, string pid, int? number)
        {
            int sl = 10;
            if (number != null)
            {
                sl = number.Value;
            }

            var product = db.News.FirstOrDefault(u => u.id.Equals(id));
            if (product != null)
            {
                for (int i = 0; i < sl; i++)
                {
                    var pro = new News()
                    {
                        createDate = product.createDate,
                        keySeo = product.keySeo,
                        titleSeo = product.titleSeo,
                        descript = product.descript,
                        desSeo = product.desSeo,
                        contents = product.contents,
                        newFile = product.newFile,
                        groupId = product.groupId,
                        newLang = product.newLang,
                        newHot = product.newHot,
                        newImage = product.newImage,
                        newNew = product.newNew,
                        newOrder = product.newOrder,
                        status = product.status,
                        viewCount = product.viewCount,
                        title = product.title,
                        updateDate = product.updateDate,
                        id = Guid.NewGuid().ToString()

                    };
                    pro.new_key = product.new_key + "-" + DateTime.Now.ToString("yyMMdd-HHmmss") + i;
                    if (!string.IsNullOrEmpty(pid))
                    {
                        pro.groupId = pid;
                    }
                    db.News.Add(pro);
                }
                db.SaveChanges();
            }
            ViewBag.countAll = db.Products.Select(u => u.id).Count();
            return View();
        }
    }
}
