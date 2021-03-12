using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Productgold.Models;
using FlexCel.XlsAdapter;
using FlexCel.Core;
namespace Productgold.Controllers
{
    public class ProdcutsController : BaseCookisController
    {
        //
        // GET: /Prodcuts/

        WEBTPAEntities db = new WEBTPAEntities();
        string lang = ClassExten.GetLang();
        string siteTPA = ClassExten.siteOption;
        #region[ham view]
        public ActionResult Index()
        {
            string titleSearch = "";
            int groupSearch = -1;
            string group = "";
            if (Request["titleSearch"] != null)
            {
                titleSearch = Request["titleSearch"];
            }
            if (Request["groupSearch"] != null)
            {
                group = Request["groupSearch"];
                if (!string.IsNullOrEmpty(group))
                {
                    groupSearch = Convert.ToInt32(group);
                }
            }

            string page = "1";//so phan trang hien tai
            var pagesize = 20;//so ban ghi tren 1 trang
            var numOfNews = 0;//tong so ban ghi co duoc truoc khi phan trang
            int curpage = 0; // trang hien tai dung cho phan trang
            if (Request["page"] != null)
            {
                page = Request["page"];
                curpage = Convert.ToInt32(page) - 1;
            }
            var all = db.Products.Where(u => u.pLang == lang && u.siteTPA == siteTPA
            && (string.IsNullOrEmpty(titleSearch) || u.pro_name.ToLower().Contains(titleSearch.ToLower()))
            && (groupSearch == -1 || u.cateid == groupSearch)
            ).OrderBy(u => u.proOrder).ToList();
            var data = all.Skip(curpage * pagesize).Take(pagesize).ToList();
            var url = Request.Path;
            numOfNews = all.Count;
            ViewBag.pages = Productgold.Models.Phantrang.PhanTrang(pagesize, curpage, numOfNews, url);
            var nhomsp = db.Categorys.Where(u => u.ctype == 1 && u.cateLang == lang && u.active == true && u.siteTPA == ClassExten.siteOption).OrderBy(u => u.cateOrder).ToList();
            SelectList sl = new SelectList(nhomsp, "id", "name");
            ViewBag.nhomsp = sl;
            return View(data);
        }
        public ActionResult IndexSearch(FormCollection fr)
        {
            var titleSearch = fr["titleSearch"];
            var groupSearch = fr["groupSearch"];
            return Redirect("/Prodcuts/Index?titleSearch=" + titleSearch + "&groupSearch=" + groupSearch);
        }
        #endregion

        #region[ham sua form]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Update(FormCollection f, int id)
        {
            try
            {
                var data = db.Products.First(u => u.id == id);
                string[] manhAnh = data.proImages.Split(';');
                var nhom = Convert.ToInt32(f["catep"]);
                var th = Convert.ToInt32(f["nhomth"]);
                var price = float.Parse(f["price"]);
                var pricesale = float.Parse(f["pricesale"]);
                var thutu = int.Parse(f["thutu"]);
                var name = f["name"];
                var count = f["count"];
                string keyTag = StringClass.NameToTag(name);
                var spconhang = f["spconhang"];
                var spactive = f["spactive"];
                var sphot = f["sphot"];
                var spmoi = f["spmoi"];
                var spgiamgia = f["spgiamgia"];
                string img = f["proAvata1"] + ";" + f["proAvata2"] + ";" + f["proAvata3"] + ";" + f["proAvata4"] + ";" + f["proAvata5"] + ";" + f["proAvata6"];
                string imgId = "";
                string[] mangId = data.proImagesId.Split(';');
                if (data.pro_key != keyTag)
                {
                    string key = checkTrungUpdate(keyTag, data.id);
                    data.pro_key = key;
                }
               
                data.proImagesId = imgId;
                data.brandid = th;
                data.cateid = nhom;
                data.proCode = f["model"];
                data.pro_name = name;
                data.proWant = f["baohanh"];
                data.prodescription = f["mota"];
                data.proContents = f["proContents"];
                data.proContents2 = f["proContents2"];
                data.proContents3 = f["proContents3"];
                data.pro_count = Convert.ToInt32(count);
                // data.pro_view = 0;
                data.proPrice = price;
                data.proPrice_sale = pricesale;
                data.proOrder = thutu;
                data.proAvata = f["proAvata"];
                data.proImages = img;
                data.desSeo = f["desseo"];
                data.titleSeo = f["titleseo"];
                data.keySeo = f["keyseo"];
                data.ex1 = f["chatlieu"];

                #region[the tag]
                string tagId = "";
                string keytag = "";
                string chuoitag = "";
                bool dau = true;
                var tagnumber = Convert.ToInt32(f["tagnumber"]);
                if (tagnumber > 0)
                {
                    for (int i = 0; i < tagnumber; i++)
                    {
                        tagId = f["cktag_" + i];
                        if (tagId != null)
                        {
                            keytag = f["valuetag_" + i];
                            if (dau == true)
                            {
                                chuoitag = keytag;
                                dau = false;
                            }
                            else
                            {
                                chuoitag = ";" + keytag;
                            }
                        }
                    }
                }
                #endregion
                data.pTag = chuoitag;
                data.pro_hot = true;
                data.pro_new = true;
                data.pro_sale = true;
                data.active = true;
                data.statust = true;

                if (spconhang == null || spconhang == "false")
                {
                    data.statust = false;
                }
                if (spactive == null || spactive == "false")
                {
                    data.active = false;
                }
                if (sphot == null || sphot == "false")
                {
                    data.pro_hot = false;
                }
                if (spgiamgia == null || spgiamgia == "false")
                {
                    data.pro_sale = false;
                }
                if (spmoi == null || spmoi == "false")
                {
                    data.pro_new = false;
                }

                db.SaveChanges();
                Session["ok"] = "Cập nhật thành công!";

                return Redirect("/Prodcuts");

            }
            catch (Exception)
            {

                return Redirect("/MNGAdmin/P404");
            }
        }
        #endregion

        #region[ham sua]
        public ActionResult Update(int id)
        {
            try
            {
                var tag = db.Tagproes.Where(u => u.siteTPA == ClassExten.siteOption).OrderBy(u => u.tagOrder).ToList();
                ViewBag.tagpro = tag;
                var data = db.Products.First(u => u.id == id);
                var th = db.Brands.Where(u => u.siteTPA == ClassExten.siteOption);
                SelectList slth = new SelectList(th, "id", "name", data.brandid);
                ViewBag.nhomth = slth;
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
        public ActionResult Add(FormCollection f)
        {
            Product data = new Product();
            var nhom = Convert.ToInt32(f["catep"]);
            var th = Convert.ToInt32(f["nhomth"]);
            var price = float.Parse(f["price"]);
            var pricesale = float.Parse(f["pricesale"]);
            var thutu = int.Parse(f["thutu"]);
            var name = f["name"];
            var count = f["count"];
            string keyTag = StringClass.NameToTag(name);
            var key = checkTrung(keyTag);
            var spconhang = f["spconhang"];
            var spactive = f["spactive"];
            var sphot = f["sphot"];
            var spmoi = f["spmoi"];
            var spgiamgia = f["spgiamgia"];
            string img = f["proAvata1"] + ";" + f["proAvata2"] + ";" + f["proAvata3"] + ";" + f["proAvata4"] + ";" + f["proAvata5"] + ";" + f["proAvata6"];
            string imgId = "";
         
            data.proImagesId = imgId;
            data.pLang = lang;
            data.brandid = th;
            data.cateid = nhom;
            data.proCode = f["model"];
            data.pro_name = name;
            data.proWant = f["baohanh"];
            data.prodescription = f["mota"];
            data.proContents = f["proContents"];
            data.proContents2 = f["proContents2"];
            data.proContents3 = f["proContents3"];
            data.pro_key = key;
            data.pro_count = Convert.ToInt32(count);
            data.pro_view = 0;
            data.proPrice = price;
            data.proPrice_sale = pricesale;
            data.proOrder = thutu;
            data.proAvata = f["proAvata"];
            data.proImages = img;
            data.desSeo = f["desseo"];
            data.titleSeo = f["titleseo"];
            data.keySeo = f["keyseo"];
            data.ex1 = f["chatlieu"];
            #region[the tag]
            string tagId = "";
            string keytag = "";
            string chuoitag = "";
            bool dau = true;
            var tagnumber = Convert.ToInt32(f["tagnumber"]);
            if (tagnumber > 0)
            {
                for (int i = 0; i < tagnumber; i++)
                {
                    tagId = f["cktag_" + i];
                    if (tagId != null)
                    {
                        keytag = f["valuetag_" + i];
                        if (dau == true)
                        {
                            chuoitag = keytag;
                            dau = false;
                        }
                        else
                        {
                            chuoitag = ";" + keytag;
                        }
                    }
                }
            }
            #endregion
            data.pTag = chuoitag;
            data.pro_hot = true;
            data.pro_new = true;
            data.pro_sale = true;
            data.active = true;
            data.statust = true;
            data.siteTPA = siteTPA;

            if (spconhang == null || spconhang == "false")
            {
                data.statust = false;
            }
            if (spactive == null || spactive == "false")
            {
                data.active = false;
            }
            if (sphot == null || sphot == "false")
            {
                data.pro_hot = false;
            }
            if (spgiamgia == null || spgiamgia == "false")
            {
                data.pro_sale = false;
            }
            if (spmoi == null || spmoi == "false")
            {
                data.pro_new = false;
            }
            db.Products.Add(data);
            db.SaveChanges();
            Session["ok"] = "Thêm mới thành công!";

            return Redirect("/Prodcuts");
        }
        #endregion

        #region[ham them]
        public ActionResult Add()
        {
            var tag = db.Tagproes.Where(u => u.siteTPA == ClassExten.siteOption && u.tagLang == lang).OrderBy(u => u.tagOrder).ToList();
            ViewBag.tagpro = tag;
            var th = db.Brands.Where(u => u.siteTPA == ClassExten.siteOption && u.blang == lang);
            SelectList slth = new SelectList(th, "id", "name");
            ViewBag.nhomth = slth;
            return View();
        }
        #endregion

        #region[ham xoa]
        public ActionResult Delete(int id)
        {
            try
            {
                var data = db.Products.First(u => u.id == id);
                db.Products.Remove(data);
                db.SaveChanges();
                Session["ok"] = "Xóa thành công!";
                return Redirect("/Prodcuts");

            }
            catch (Exception)
            {

                return Redirect("/MNGAdmin/P404");
            }
        }
        #endregion

        #region[gen key add]
        public ActionResult Genkey(string name)
        {
            string key = Productgold.Models.StringClass.RemoveSign4VietnameseString(name);
            var data = db.Products.Where(u => u.pro_key == key && u.pLang == lang && u.siteTPA == ClassExten.siteOption).ToList().Count;
            if (data > 0)
            {
                var pro = (from n in db.Products orderby n.id descending select n.id).Take(1).FirstOrDefault();
                string keynew = key + "-" + pro;
                return Json(keynew, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(key, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region[gen key update]
        public ActionResult Genkeyup(string name, string id)
        {
            string key = Productgold.Models.StringClass.RemoveSign4VietnameseString(name);
            var data = db.Products.Where(u => u.pro_key == key && u.pLang == lang && u.siteTPA == ClassExten.siteOption).ToList().Count;
            if (data > 0)
            {
                int idpro = Convert.ToInt32(id);
                var pro = db.Products.FirstOrDefault(u => u.id == idpro).id;
                string keynew = key + "-" + pro;
                return Json(keynew, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(key, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        public ActionResult GetCha(int type)
        {
            var cate = db.Categorys.Where(u => u.ctype == type && u.cateLang == lang && u.active == true && u.catepar_id == -1 && u.siteTPA == ClassExten.siteOption).OrderBy(u => u.cateOrder).ToList();
            return PartialView(cate);
        }


        public string checkTrung(string key)
        {
            string rs = key;
            var cate = db.Products.FirstOrDefault(u => u.pro_key == key && u.pLang == lang);
            if (cate != null)
            {
                var idMax = db.Products.OrderByDescending(u => u.id).FirstOrDefault();
                rs = key + "-" + idMax.id;
            }
            return rs;
        }
        public string checkTrungUpdate(string key, int id)
        {
            string rs = key;
            var cate = db.Products.FirstOrDefault(u => u.pro_key == key && u.pLang == lang );
            if (cate != null)
            {
                var idMax = id;
                rs = key + "-" + idMax;
            }
            return rs;
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ImportExcel(FormCollection fr)
        {
            string fileUpload = fr["file_excel"];
            string proname = "";
            string procode = "";
            string proprice = "";
            string procate = "";
            List<Product> list = new List<Product>();

            if (fileUpload.Split('.')[1] == "xls" || fileUpload.Split('.')[1] == "xlsx")
            {
                Product pro;
                XlsFile xls = new XlsFile(Server.MapPath(fileUpload));
                xls.ActiveSheetByName = xls.GetSheetName(1);
                for (int row = 7; row <= xls.RowCount; row++)
                {
                    pro = new Product();
                    try
                    {
                        proname = xls.GetCellValue(row, 1) != null ? xls.GetCellValue(row, 1).ToString() : "";
                        procode = xls.GetCellValue(row, 2) != null ? xls.GetCellValue(row, 2).ToString() : "";
                    }
                    catch (Exception ex)
                    {
                    }
                    list.Add(pro);
                }

                try
                {
                    db.Products.AddRange(list);
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    Session["ok"] = "Có lỗi xảy ra, kiểm tra lại thông tin file!";
                    return Redirect("/Prodcuts");
                }
            }
            else
            {
                Session["ok"] = "File sai định dạng!";
                return Redirect("/Prodcuts");
            }
            return View();
        }


    }
}
