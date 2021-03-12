using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Productgold.Models;

namespace Productgold.Controllers
{
    public class CategorysController : BaseCookisController
    {
        //
        // GET: /Categorys/
        WEBTPAEntities db = new WEBTPAEntities();
        string lang = ClassExten.GetLang();
        string siteTPA = ClassExten.siteOption;

        public ActionResult IndexSearch(FormCollection fr)
        {
            var titleSearch = fr["titleSearch"];
            var vitrihome = fr["vitrihome"];
            
            return Redirect("/Categorys/Catenew?titleSearch=" + titleSearch+ "&vitrihome="+ vitrihome);
        }
        public ActionResult IndexSearchPro(FormCollection fr)
        {
            var sltype = fr["sltype"];
            var titleSearch = fr["titleSearch"];
            return Redirect("/Categorys/Catepro?titleSearch=" + titleSearch + "&sltype=" + sltype);
        }

        public ActionResult IndexSearchVideo(FormCollection fr)
        {
            var titleSearch = fr["titleSearch"];
            return Redirect("/Categorys/Catevideo?titleSearch=" + titleSearch);
        }
        #region[ham xoa nhom san pham]
        public ActionResult Deletepro(int id)
        {
            try
            {
                var newList = db.Products.Where(u => u.cateid == id).Select(u => u.id).Count();
                if (newList > 0)
                {
                    Session["ok"] = "Nhóm đang được sử dụng không thể xóa!";
                }
                else
                {
                    var data = db.Categorys.First(u => u.id == id);
                    db.Categorys.Remove(data);
                    db.SaveChanges();
                    Session["ok"] = "Xóa thành công!";
                }
                return Redirect("/Categorys/Catepro");

            }
            catch (Exception)
            {

                return Redirect("/MNGAdmin/P404");
            }
        }
        #endregion

        #region[ham xoa nhom tin]
        public ActionResult Deletenew(int id)
        {
            try
            {
                var newList = db.News.Where(u => u.groupid == id).Select(u => u.id).Count();
                if (newList > 0)
                {
                    Session["ok"] = "Nhóm đang được sử dụng không thể xóa!";
                }
                else
                {
                    var data = db.Categorys.First(u => u.id == id);
                    db.Categorys.Remove(data);
                    db.SaveChanges();
                    Session["ok"] = "Xóa thành công!";
                }
                return Redirect("/Categorys/Catenew");

            }
            catch (Exception)
            {

                return Redirect("/MNGAdmin/P404");
            }
        }
        #endregion

        #region[ham xoa nhom video]
        public ActionResult Deletevideo(int id)
        {
            try
            {
                var newList = db.Videos.Where(u => u.cateId == id).Select(u => u.vId).Count();
                if (newList > 0)
                {
                    Session["ok"] = "Nhóm đang được sử dụng không thể xóa!";
                }
                else
                {
                    var data = db.Categorys.First(u => u.id == id);
                    db.Categorys.Remove(data);
                    db.SaveChanges();
                    Session["ok"] = "Xóa thành công!";
                }
                return Redirect("/Categorys/Catevideo");

            }
            catch (Exception)
            {

                return Redirect("/MNGAdmin/P404");
            }
        }

        #endregion

        #region[ham view nhom pro]
        public ActionResult Catepro()
        {
            string titleSearch = "";
            string sltypeSearch = "-1";
            string sltype = "";
            if (Request["titleSearch"] != null)
            {
                titleSearch = Request["titleSearch"];
            }
            if (Request["sltype"] != null)
            {
                sltype = Request["sltype"];
                if (!string.IsNullOrEmpty(sltype))
                {
                    sltypeSearch = sltype;
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
            List<Category> all = db.Categorys.Where(u => u.cateLang == lang && u.ctype == 1 && u.siteTPA == siteTPA
             && (string.IsNullOrEmpty(titleSearch) || u.name.ToLower().Contains(titleSearch.ToLower()))
              && (sltypeSearch == "-1" || u.cateLevel == sltypeSearch)
            ).OrderBy(u => u.cateOrder).ToList();
            var data = all.Skip(curpage * pagesize).Take(pagesize).ToList();
            var url = Request.Path;
            numOfNews = all.Count;
            ViewBag.pages = Productgold.Models.Phantrang.PhanTrang(pagesize, curpage, numOfNews, url);
            return View(data);
        }
        #endregion

        #region[ham view nhom new]
        public ActionResult Catenew()
        {
            string titleSearch = "";
            if (Request["titleSearch"] != null)
            {
                titleSearch = Request["titleSearch"];
            }

            string vitrihome = "0";
            if (Request["vitrihome"] != null)
            {
                vitrihome = Request["vitrihome"];
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
            List<Category> all = db.Categorys.Where(u => u.cateLang == lang && u.ctype == 2 && u.siteTPA == siteTPA
             && (string.IsNullOrEmpty(titleSearch) || u.name.ToLower().Contains(titleSearch.ToLower()))
             && (vitrihome.Equals("0") || u.cateLevel.Equals(vitrihome))
            ).OrderBy(u => u.cateOrder).ToList();
            var data = all.Skip(curpage * pagesize).Take(pagesize).ToList();
            var url = Request.Path;
            numOfNews = all.Count;
            ViewBag.pages = Productgold.Models.Phantrang.PhanTrang(pagesize, curpage, numOfNews, url);
            return View(data);
        }
        #endregion

        #region[ham view nhom video]
        public ActionResult Catevideo()
        {
            string titleSearch = "";
            if (Request["titleSearch"] != null)
            {
                titleSearch = Request["titleSearch"];
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
            List<Category> all = db.Categorys.Where(u => u.cateLang == lang && u.ctype == 3 && u.siteTPA == siteTPA
                && (string.IsNullOrEmpty(titleSearch) || u.name.ToLower().Contains(titleSearch.ToLower()))
                ).OrderBy(u => u.cateOrder).ToList();
            var data = all.Skip(curpage * pagesize).Take(pagesize).ToList();
            var url = Request.Path;
            numOfNews = all.Count;
            ViewBag.pages = Productgold.Models.Phantrang.PhanTrang(pagesize, curpage, numOfNews, url);
            return View(data);
        }
        #endregion

        #region[nhom san  pham]
        public ActionResult AddCatepro()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddCatepro(FormCollection fr)
        {
            try
            {

                int catetype = Convert.ToInt32(fr["catetype"]);
                string catename = fr["catename"];
                int catep = Convert.ToInt32(fr["catep"]);
                string order = fr["order"];
                string catedes = fr["catedes"];
                string note = fr["catenote"];
                string titleseo = fr["titleseo"];
                string keyseo = fr["keyseo"];
                string desseo = fr["desseo"];
                string ckHome = fr["ckHome"];
                string ckActive = fr["ckActive"];
                string cateAvata = fr["cateAvata"];

                string listimg1 = fr["listimg1"];
                string listimg2 = fr["listimg2"];
                string listimg3 = fr["listimg3"];
                string listimg4 = fr["listimg4"];
                string listimg5 = fr["listimg5"];
                string listimg6 = fr["listimg6"];
                string listimg7 = fr["listimg7"];
                string listimg8 = fr["listimg8"];
                string key = StringClass.NameToTag(catename);
                string img = listimg1 + ";" + listimg2 + ";" + listimg3 + ";" + listimg4 + ";" + listimg5 + ";" + listimg6 + ";" + listimg7 + ";" + listimg8;

                Category cate = new Category();
                cate.name = catename;
                cate.keys = checkTrung(key, catetype);
                cate.ctype = catetype;
                cate.activeHome = true;
                cate.active = true;
                if (ckHome == null || ckHome == "false")
                {
                    cate.activeHome = false;
                }
                if (ckActive == null || ckActive == "false")
                {
                    cate.active = false;
                }
                cate.cateLang = lang;
                cate.siteTPA = siteTPA;
                cate.cateLevel =fr["loai"];
                cate.catenote = note;
                cate.titleSeo = titleseo;
                cate.desSeo = desseo;
                cate.keySeo = keyseo;
                cate.cateAvata = cateAvata;
                cate.cateImg = img;
             
                cate.cateImg_des = catedes;
                cate.cateOrder = Convert.ToInt32(order);
                cate.catepar_id = catep;
                cate.cate_cap = GetCap(catep);
                db.Categorys.Add(cate);

                db.SaveChanges();
                Session["ok"] = "Thêm mới thành công!";
            }
            catch (Exception)
            {
                return View();
            }
            return Redirect("/Categorys/Catepro");
        }

        public ActionResult UpdateCatepro(int id)
        {
            var cate = db.Categorys.FirstOrDefault(u => u.id == id);
            return View(cate);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult UpdateCatepro(FormCollection fr, int id)
        {
            try
            {
                int catetype = Convert.ToInt32(fr["catetype"]);
                string catename = fr["catename"];
                int catep = Convert.ToInt32(fr["catep"]);
                string order = fr["order"];
                string catedes = fr["catedes"];
                string note = fr["catenote"];
                string titleseo = fr["titleseo"];
                string keyseo = fr["keyseo"];
                string desseo = fr["desseo"];
                string ckHome = fr["ckHome"];
                string ckActive = fr["ckActive"];
                string cateAvata = fr["cateAvata"];

                string listimg1 = fr["listimg1"];
                string listimg2 = fr["listimg2"];
                string listimg3 = fr["listimg3"];
                string listimg4 = fr["listimg4"];
                string listimg5 = fr["listimg5"];
                string listimg6 = fr["listimg6"];
                string listimg7 = fr["listimg7"];
                string listimg8 = fr["listimg8"];
                string key = StringClass.NameToTag(catename);
                string img = listimg1 + ";" + listimg2 + ";" + listimg3 + ";" + listimg4 + ";" + listimg5 + ";" + listimg6 + ";" + listimg7 + ";" + listimg8;

                Category cate = db.Categorys.FirstOrDefault(u => u.id == id);
                cate.name = catename;
                if (cate.keys != key)
                {
                    cate.keys = checkTrungUpdate(key, catetype, id);
                }
                cate.ctype = catetype;
                cate.activeHome = true;
                cate.active = true;
                if (ckHome == null || ckHome == "false")
                {
                    cate.activeHome = false;
                }
                if (ckActive == null || ckActive == "false")
                {
                    cate.active = false;
                }
                cate.cateLang = lang;
                cate.cateLevel =fr["loai"];
                cate.catenote = note;
                cate.titleSeo = titleseo;
                cate.desSeo = desseo;
                cate.keySeo = keyseo;
                cate.cateAvata = cateAvata;
               
                cate.cateImg = img;
                cate.cateImg_des = catedes;
                cate.cateOrder = Convert.ToInt32(order);
                cate.catepar_id = catep;
                cate.cate_cap = GetCap(catep);
                db.SaveChanges();
                Session["ok"] = "Cập nhật thành công!";
            }
            catch (Exception)
            {
                return View();
            }
            return Redirect("/Categorys/Catepro");
        }
        #endregion

        #region[cac ham xu ly]
        public int GetCap(int number)
        {
            int rs = 1;
            if (number == -1)
            {
                rs = 1;
            }
            else
            {
                var cate = db.Categorys.FirstOrDefault(u => u.id == number).catepar_id;
                if (cate == -1)
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
            var cate = db.Categorys.FirstOrDefault(u => u.ctype == type && u.cateLang == lang && u.keys == key && u.siteTPA == ClassExten.siteOption);
            if (cate != null)
            {
                var idMax = db.Categorys.OrderByDescending(u => u.id).FirstOrDefault();
                rs = key + "-" + idMax.id;
            }
            return rs;
        }
        public string checkTrungUpdate(string key, int type, int id)
        {
            string rs = key;
            var cate = db.Categorys.FirstOrDefault(u => u.ctype == type && u.keys == key && u.cateLang == lang && u.siteTPA == ClassExten.siteOption);
            if (cate != null)
            {
                var idMax = id;
                rs = key + "-" + idMax;
            }
            return rs;
        }
        public ActionResult GetCha(int type, int id)
        {
            ViewBag.idthis = id;
            var cate = db.Categorys.Where(u => u.ctype == type && u.cateLang == lang && u.siteTPA == ClassExten.siteOption && u.active == true && u.catepar_id == -1 && (id == -1 || u.id != id)).OrderBy(u => u.cateOrder).ToList();
            return PartialView(cate);
        }
        #endregion

        #region[nhom tin tuc]
        public ActionResult AddCatenews()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddCatenews(FormCollection fr)
        {
            try
            {

                int catetype = Convert.ToInt32(fr["catetype"]);
                string catename = fr["catename"];
                int catep = Convert.ToInt32(fr["catep"]);
                string order = fr["order"];
                string catedes = fr["catedes"];
                string note = fr["catenote"];
                string titleseo = fr["titleseo"];
                string keyseo = fr["keyseo"];
                string desseo = fr["desseo"];
                string ckHome = fr["ckHome"];
                string ckActive = fr["ckActive"];
                string cateAvata = fr["cateAvata"];

                string listimg1 = fr["listimg1"];
                string listimg2 = fr["listimg2"];
                string listimg3 = fr["listimg3"];
                string listimg4 = fr["listimg4"];
                string listimg5 = fr["listimg5"];
                string listimg6 = fr["listimg6"];
                string key = StringClass.NameToTag(catename);
                string img = listimg1 + ";" + listimg2 + ";" + listimg3 + ";" + listimg4 + ";" + listimg5 + ";" + listimg6;

                Category cate = new Category();
                cate.name = catename;
                cate.keys = checkTrung(key, catetype);
                cate.ctype = catetype;
                cate.activeHome = true;
                cate.active = true;
                if (ckHome == null || ckHome == "false")
                {
                    cate.activeHome = false;
                }
                if (ckActive == null || ckActive == "false")
                {
                    cate.active = false;
                }
                cate.cateLang = lang;
                cate.siteTPA = siteTPA;
                cate.cateLevel = fr["vitrihome"];
                cate.catenote = note;
                cate.titleSeo = titleseo;
                cate.desSeo = desseo;
                cate.keySeo = keyseo;
                cate.cateAvata = cateAvata;
                cate.cateImg = img;
                if (!string.IsNullOrEmpty(img))
                {
                  //  var cateImg = ClassExten.UploadGoogleDriver(img, ClassExten.ForderNew);
                  //  cate.cateAvataId = cateImg;
                }
                cate.cateImg_des = catedes;
                cate.cateOrder = Convert.ToInt32(order);
                cate.catepar_id = catep;
                cate.cate_cap = GetCap(catep);
                db.Categorys.Add(cate);

                db.SaveChanges();
                Session["ok"] = "Thêm mới thành công!";
            }
            catch (Exception)
            {
                return View();
            }
            return Redirect("/Categorys/Catenew");
        }

        public ActionResult UpdateCatenews(int id)
        {
            var cate = db.Categorys.FirstOrDefault(u => u.id == id);
            return View(cate);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult UpdateCatenews(FormCollection fr, int id)
        {
            try
            {
                int catetype = Convert.ToInt32(fr["catetype"]);
                string catename = fr["catename"];
                int catep = Convert.ToInt32(fr["catep"]);
                string order = fr["order"];
                string catedes = fr["catedes"];
                string note = fr["catenote"];
                string titleseo = fr["titleseo"];
                string keyseo = fr["keyseo"];
                string desseo = fr["desseo"];
                string ckHome = fr["ckHome"];
                string ckActive = fr["ckActive"];
                string cateAvata = fr["cateAvata"];

                string listimg1 = fr["listimg1"];
                string listimg2 = fr["listimg2"];
                string listimg3 = fr["listimg3"];
                string listimg4 = fr["listimg4"];
                string listimg5 = fr["listimg5"];
                string listimg6 = fr["listimg6"];
                string key = StringClass.NameToTag(catename);
                string img = listimg1 + ";" + listimg2 + ";" + listimg3 + ";" + listimg4 + ";" + listimg5 + ";" + listimg6;

                Category cate = db.Categorys.FirstOrDefault(u => u.id == id);
                cate.name = catename;
                if (cate.keys != key)
                {
                    cate.keys = checkTrungUpdate(key, catetype, id);
                }
                cate.ctype = catetype;
                cate.activeHome = true;
                cate.active = true;
                if (ckHome == null || ckHome == "false")
                {
                    cate.activeHome = false;
                }
                if (ckActive == null || ckActive == "false")
                {
                    cate.active = false;
                }
                cate.cateLang = lang;
                cate.cateLevel = fr["vitrihome"];
                cate.catenote = note;
                cate.titleSeo = titleseo;
                cate.desSeo = desseo;
                cate.keySeo = keyseo;
                cate.cateAvata = cateAvata;
                if (!string.IsNullOrEmpty(img))
                {
                    if (img != cate.cateImg)
                    {
                      //  var cateImg = ClassExten.UploadGoogleDriver(img, ClassExten.ForderNew);
                       // cate.cateAvataId = cateImg;
                    }
                }
                cate.cateImg = img;
                cate.cateImg_des = catedes;
                cate.cateOrder = Convert.ToInt32(order);
                cate.catepar_id = catep;
                cate.cate_cap = GetCap(catep);
                db.SaveChanges();
                Session["ok"] = "Cập nhật thành công!";
            }
            catch (Exception)
            {
                return View();
            }
            return Redirect("/Categorys/Catenew");
        }
        #endregion

        #region[nhom video]
        public ActionResult AddCatevideo()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddCatevideo(FormCollection fr)
        {
            try
            {

                int catetype = Convert.ToInt32(fr["catetype"]);
                string catename = fr["catename"];
                int catep = Convert.ToInt32(fr["catep"]);
                string order = fr["order"];
                string catedes = fr["catedes"];
                string note = fr["catenote"];
                string titleseo = fr["titleseo"];
                string keyseo = fr["keyseo"];
                string desseo = fr["desseo"];
                string ckHome = fr["ckHome"];
                string ckActive = fr["ckActive"];
                string cateAvata = fr["cateAvata"];

                string listimg1 = fr["listimg1"];
                string listimg2 = fr["listimg2"];
                string listimg3 = fr["listimg3"];
                string listimg4 = fr["listimg4"];
                string listimg5 = fr["listimg5"];
                string listimg6 = fr["listimg6"];
                string key = StringClass.NameToTag(catename);
                string img = listimg1 + ";" + listimg2 + ";" + listimg3 + ";" + listimg4 + ";" + listimg5 + ";" + listimg6;

                Category cate = new Category();
                cate.name = catename;
                cate.keys = checkTrung(key, catetype);
                cate.ctype = catetype;
                cate.activeHome = true;
                cate.active = true;
                if (ckHome == null || ckHome == "false")
                {
                    cate.activeHome = false;
                }
                if (ckActive == null || ckActive == "false")
                {
                    cate.active = false;
                }
                cate.siteTPA = siteTPA;
                cate.cateLang = lang;
                cate.cateLevel = "";
                cate.catenote = note;
                cate.titleSeo = titleseo;
                cate.desSeo = desseo;
                cate.keySeo = keyseo;
                cate.cateAvata = cateAvata;
                cate.cateImg = img;
                cate.cateImg_des = catedes;
                cate.cateOrder = Convert.ToInt32(order);
                cate.catepar_id = catep;
                cate.cate_cap = GetCap(catep);
                db.Categorys.Add(cate);

                db.SaveChanges();
                Session["ok"] = "Thêm mới thành công!";
            }
            catch (Exception)
            {
                return View();
            }
            return Redirect("/Categorys/Catevideo");
        }

        public ActionResult UpdateCatevideo(int id)
        {
            var cate = db.Categorys.FirstOrDefault(u => u.id == id);
            return View(cate);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult UpdateCatevideo(FormCollection fr, int id)
        {
            try
            {
                int catetype = Convert.ToInt32(fr["catetype"]);
                string catename = fr["catename"];
                int catep = Convert.ToInt32(fr["catep"]);
                string order = fr["order"];
                string catedes = fr["catedes"];
                string note = fr["catenote"];
                string titleseo = fr["titleseo"];
                string keyseo = fr["keyseo"];
                string desseo = fr["desseo"];
                string ckHome = fr["ckHome"];
                string ckActive = fr["ckActive"];
                string cateAvata = fr["cateAvata"];

                string listimg1 = fr["listimg1"];
                string listimg2 = fr["listimg2"];
                string listimg3 = fr["listimg3"];
                string listimg4 = fr["listimg4"];
                string listimg5 = fr["listimg5"];
                string listimg6 = fr["listimg6"];
                string key = StringClass.NameToTag(catename);
                string img = listimg1 + ";" + listimg2 + ";" + listimg3 + ";" + listimg4 + ";" + listimg5 + ";" + listimg6;

                Category cate = db.Categorys.FirstOrDefault(u => u.id == id);
                cate.name = catename;
                if (cate.keys != key)
                {
                    cate.keys = checkTrungUpdate(key, catetype, id);
                }
                cate.ctype = catetype;
                cate.activeHome = true;
                cate.active = true;
                if (ckHome == null || ckHome == "false")
                {
                    cate.activeHome = false;
                }
                if (ckActive == null || ckActive == "false")
                {
                    cate.active = false;
                }
                cate.cateLang = lang;
                cate.cateLevel = "";
                cate.catenote = note;
                cate.titleSeo = titleseo;
                cate.desSeo = desseo;
                cate.keySeo = keyseo;
                cate.cateAvata = cateAvata;
                cate.cateImg = img;
                cate.cateImg_des = catedes;
                cate.cateOrder = Convert.ToInt32(order);
                cate.catepar_id = catep;
                cate.cate_cap = GetCap(catep);
                db.SaveChanges();
                Session["ok"] = "Cập nhật thành công!";
            }
            catch (Exception)
            {
                return View();
            }
            return Redirect("/Categorys/Catevideo");
        }
        #endregion
    }
}
