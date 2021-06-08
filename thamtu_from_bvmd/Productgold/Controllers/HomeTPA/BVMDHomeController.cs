using Productgold.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Productgold.Controllers.HomeTPA
{
    public class BVMDHomeController : Controller
    {
        WEBTPAEntities db = new WEBTPAEntities();
        string lang = ClassExten.GetLangSite();
        string siteOption = ClassExten.siteOption;
        // GET: BVMDHome
        public ActionResult Index()
        {
            ViewBag.key_tinxemnhieu = ClassExten.GetLangKey("key_tinxemnhieu");
            ViewBag.key_vechungtoi = ClassExten.GetLangKey("key_vechungtoi");
            ViewBag.key_hinhanhhoatdong = ClassExten.GetLangKey("key_hinhanhhoatdong");
            ViewBag.key_luotxem = ClassExten.GetLangKey("key_luotxem");

            Config config;
            List<NewListView> listTren = new List<NewListView>();
            List<NewListView> listGiua = new List<NewListView>();
            List<NewListView> listDuoi = new List<NewListView>();
            NewListView newView;
            List<Category> lstCate;
            List<News> lstNew;
            #region[load tin theo nhom]
            List<int> listId = new List<int>();
            //khoi tren
            lstCate = db.Categorys.Where(u => u.active == true && u.cateLang == lang && u.cateLevel.Equals("1") && u.ctype == 2).OrderBy(u => u.cateOrder).Take(2).ToList();
            foreach (var item in lstCate)
            {
                listId = new List<int>();
                if (item.cate_cap != 3)
                {
                    listId = GetListId(item.id, item.cate_cap.Value);
                }
                else
                {
                    listId.Add(item.id);
                }
                lstNew = db.News.Where(u => u.newLang == lang && u.active == true && listId.Contains(u.groupid.Value)).OrderBy(u => u.newOrder).Take(5).ToList();
                newView = new NewListView();
                newView.cateNew = item;
                newView.newList = lstNew;
                listTren.Add(newView);
            }
            //khoi giua
            lstCate = db.Categorys.Where(u => u.active == true && u.cateLang == lang && u.cateLevel.Equals("2") && u.ctype == 2).OrderBy(u => u.cateOrder).Take(2).ToList();
            foreach (var item in lstCate)
            {
                listId = new List<int>();
                if (item.cate_cap != 3)
                {
                    listId = GetListId(item.id, item.cate_cap.Value);
                }
                else
                {
                    listId.Add(item.id);
                }
                lstNew = db.News.Where(u => u.newLang == lang && u.active == true && listId.Contains(u.groupid.Value)).OrderBy(u => u.newOrder).Take(5).ToList();
                newView = new NewListView();
                newView.cateNew = item;
                newView.newList = lstNew;
                listGiua.Add(newView);
            }
            //khoi duoi
            lstCate = db.Categorys.Where(u => u.active == true && u.cateLang == lang && u.cateLevel.Equals("3") && u.ctype == 2).OrderBy(u => u.cateOrder).Take(2).ToList();
            foreach (var item in lstCate)
            {
                listId = new List<int>();
                if (item.cate_cap != 3)
                {
                    listId = GetListId(item.id, item.cate_cap.Value);
                }
                else
                {
                    listId.Add(item.id);
                }
                lstNew = db.News.Where(u => u.newLang == lang && u.active == true && listId.Contains(u.groupid.Value)).OrderBy(u => u.newOrder).Take(4).ToList();
                newView = new NewListView();
                newView.cateNew = item;
                newView.newList = lstNew;
                listDuoi.Add(newView);
            }
            #endregion
            #region[kiem tra cache]
            if (ConfigModel.conf == null)
            {
                config = db.Configs.FirstOrDefault(u => u.conLang == lang && u.siteTPA == siteOption);
                ConfigModel.conf = config;
                ConfigModel.date = DateTime.Now;
            }
            else
            {
                config = ConfigModel.conf;
            }
            #endregion
            #region[load seo]
            ViewBag.title = config.titleSeo;
            ViewBag.description = config.desSeo;
            ViewBag.keywords = config.keySeo;
            ViewBag.url = HttpContext.Request.Url.AbsoluteUri;
            ViewBag.img = ClassExten.GetUrlHost() + config.logotop;
            #endregion
            ViewBag.fbRight = config.faceBook;
            ViewBag.mapRight = config.yeuCauBaoGia;
            int numBer = 8;
            try
            {
                //var spNumber = db.sp_Config_SelectViewNumber(lang, siteOption).ToList();
                string[] mang = config.viewPro.Split(';');
                numBer = int.Parse(mang[1]);
            }
            catch (Exception)
            {

            }
            var List_View = db.News.Where(u => u.active == true && u.newLang == lang).OrderByDescending(u => u.viewCount).Take(numBer).ToList();
            ViewBag.List_View = List_View;
            var newHome = db.News.Where(u => u.active == true && u.newLang == lang && u.ntype == 1).OrderBy(u => u.newOrder).Take(4).ToList();
            ViewBag.listTren = listTren;
            ViewBag.listDuoi = listDuoi;
            ViewBag.listGiua = listGiua;

            #region[lay anh giua trang]
            var bonAnh = db.Advs.Where(u => u.advLang == lang && u.active == true && u.atype == 6).OrderBy(u => u.advOrder).Take(4).ToList();
            ViewBag.bonAnh = bonAnh;
            var SlideAnh = db.Advs.Where(u => u.advLang == lang && u.active == true && u.atype == 5).OrderBy(u => u.advOrder).Take(9).ToList();
            ViewBag.SlideAnh = SlideAnh;
            var AnhGiua = db.Advs.OrderBy(u => u.advOrder).FirstOrDefault(u => u.advLang == lang && u.active == true && u.atype == 7);
            ViewBag.AnhGiua = AnhGiua;
            var AnhPhai = db.Advs.Where(u => u.advLang == lang && u.active == true && u.atype == 3).OrderBy(u => u.advOrder).Take(3).ToList();
            ViewBag.AnhPhai = AnhPhai;

            #endregion
            return View(newHome);
        }

        public ActionResult NewList(string id)
        {
            string nav = "";
            ViewBag.home = ClassExten.GetLangKey("home");
            var strNews = ClassExten.GetLangKey("news");
            string page = "1";//so phan trang hien tai
            var pagesize = 6;//so ban ghi tren 1 trang
            var numOfNews = 0;//tong so ban ghi co duoc truoc khi phan trang
            int curpage = 0; // trang hien tai dung cho phan trang
            if (Request["page"] != null)
            {
                page = Request["page"];
                curpage = Convert.ToInt32(page) - 1;
            }
            Config config;
            List<int> listId = new List<int>();
            #region[kiem tra cache]
            if (ConfigModel.conf == null)
            {
                config = db.Configs.FirstOrDefault(u => u.conLang == lang && u.siteTPA == siteOption);
                ConfigModel.conf = config;
                ConfigModel.date = DateTime.Now;
            }
            else
            {
                config = ConfigModel.conf;
            }
            #endregion

            try
            {
                string[] mang = config.viewNews.Split(';');
                pagesize = int.Parse(mang[1]);
                if (!string.IsNullOrEmpty(id))
                {
                    var Cate = db.Categorys.FirstOrDefault(u => u.keys.Equals(id) && u.ctype == 2);
                    if (Cate.cate_cap != 3)
                    {
                        listId = GetListId(Cate.id, Cate.cate_cap.Value);
                    }
                    else
                    {
                        listId.Add(Cate.id);
                    }
                    nav += " <li>>></li> <li><a href = '/Tin-tuc'> " + strNews + " </a></li> ";
                    var cateCha = db.Categorys.FirstOrDefault(u => u.id == Cate.catepar_id);
                    if (cateCha != null)
                    {
                        nav += " <li>>></li> <li><a href = '/Tin-tuc/" + cateCha.keys + "' title='" + cateCha.name + "'> " + cateCha.name + " </a></li> ";
                    }
                    nav += " <li>>></li> <li><a href = '#' title='" + Cate.name + "'> " + Cate.name + " </a></li> ";

                    #region[load seo]
                    ViewBag.title = Cate.titleSeo;
                    ViewBag.description = Cate.desSeo;
                    ViewBag.keywords = Cate.keySeo;
                    ViewBag.url = HttpContext.Request.Url.AbsoluteUri;
                    ViewBag.img = ClassExten.GetUrlHost() + config.logotop;
                    #endregion
                }
                else
                {
                    nav += " <li>>></li> <li><a href = '/Tin-tuc' title='' title='" + strNews + "'> " + strNews + " </a></li> ";
                    #region[load seo]
                    ViewBag.title = config.titleSeo;
                    ViewBag.description = config.desSeo;
                    ViewBag.keywords = config.keySeo;
                    ViewBag.url = HttpContext.Request.Url.AbsoluteUri;
                    ViewBag.img = ClassExten.GetUrlHost() + config.logotop;
                    #endregion
                }

                ViewBag.navTin = nav;
            }
            catch (Exception)
            { }

            var list = db.News.Where(u => u.active == true && u.newLang == lang && (listId.Count == 0 || listId.Contains(u.groupid.Value))).OrderBy(u => u.newOrder).ToList();
            var data = list.Skip(curpage * pagesize).Take(pagesize).ToList();
            var url = Request.Path;
            numOfNews = list.Count;
            if (numOfNews > pagesize)
            {
                ViewBag.pages = Productgold.Models.Phantrang.PhanTrang(pagesize, curpage, numOfNews, url);
            }
            return View(data);

        }

        public ActionResult NewSearch(string id)
        {
            ViewBag.home = ClassExten.GetLangKey("home");
            ViewBag.news = ClassExten.GetLangKey("news");
            ViewBag.key_timkiem = ClassExten.GetLangKey("key_timkiem");
            ViewBag.keytimkiem = id;
            string page = "1";//so phan trang hien tai
            var pagesize = 6;//so ban ghi tren 1 trang
            var numOfNews = 0;//tong so ban ghi co duoc truoc khi phan trang
            int curpage = 0; // trang hien tai dung cho phan trang
            if (Request["page"] != null)
            {
                page = Request["page"];
                curpage = Convert.ToInt32(page) - 1;
            }
            Config config;
            #region[kiem tra cache]
            if (ConfigModel.conf == null)
            {
                config = db.Configs.FirstOrDefault(u => u.conLang == lang && u.siteTPA == siteOption);
                ConfigModel.conf = config;
                ConfigModel.date = DateTime.Now;
            }
            else
            {
                config = ConfigModel.conf;
            }
            #endregion

            try
            {
                string[] mang = config.viewNews.Split(';');
                pagesize = int.Parse(mang[1]);

                #region[load seo]
                ViewBag.title = config.titleSeo;
                ViewBag.description = config.desSeo;
                ViewBag.keywords = config.keySeo;
                ViewBag.url = HttpContext.Request.Url.AbsoluteUri;
                ViewBag.img = ClassExten.GetUrlHost() + config.logotop;
                #endregion

            }
            catch (Exception)
            { }

            var list = db.News.Where(u => u.active == true && u.newLang == lang && u.title.ToLower().Contains(id.ToLower())).OrderBy(u => u.newOrder).ToList();
            var data = list.Skip(curpage * pagesize).Take(pagesize).ToList();
            var url = Request.Path;
            numOfNews = list.Count;
            if (numOfNews > pagesize)
            {
                ViewBag.pages = Productgold.Models.Phantrang.PhanTrang(pagesize, curpage, numOfNews, url);
            }
            return View(data);

        }

        public ActionResult Detail(string id)
        {
            string nav = "";
            ViewBag.home = ClassExten.GetLangKey("home");
            ViewBag.news = ClassExten.GetLangKey("news");
            ViewBag.key_baivietcungchuyenmuc = ClassExten.GetLangKey("key_baivietcungchuyenmuc");
            int number = 3;
            Config config;
            #region[kiem tra cache]
            if (ConfigModel.conf == null)
            {
                config = db.Configs.FirstOrDefault(u => u.conLang == lang && u.siteTPA == siteOption);
                ConfigModel.conf = config;
                ConfigModel.date = DateTime.Now;
            }
            else
            {
                config = ConfigModel.conf;
            }
            #endregion
            try
            {
                string[] mang = config.viewNews.Split(';');
                number = int.Parse(mang[2]);
            }
            catch (Exception)
            { }
            int numView = 1;
            var data = db.News.FirstOrDefault(u => u.new_key.Equals(id));
            numView = data.viewCount.Value + 1;
            data.viewCount = numView;
            db.SaveChanges();
            var Cate = db.Categorys.FirstOrDefault(u => u.id == data.groupid);
            var cateCha = db.Categorys.FirstOrDefault(u => u.id == Cate.catepar_id);
            if (cateCha != null)
            {
                nav += " <li>>></li> <li><a href = '/Tin-tuc/" + cateCha.keys + "' title='" + cateCha.name + "'> " + cateCha.name + " </a></li> ";
            }
            nav += " <li>>></li> <li><a href = '/Tin-tuc/" + Cate.keys + "' title='" + Cate.name + "'> " + Cate.name + " </a></li> ";
            ViewBag.navTin = nav;
            ViewBag.dataNews = data;
            var lstNew = db.News.Where(u => u.newLang == lang && u.groupid == data.groupid && u.id != data.id).OrderBy(u => u.newOrder).Take(number).ToList();


            #region[load seo]
            ViewBag.title = data.titleSeo;
            ViewBag.description = data.desSeo;
            ViewBag.keywords = data.keySeo;
            ViewBag.url = HttpContext.Request.Url.AbsoluteUri;
            ViewBag.img = ClassExten.GetUrlHost() + data.images;
            #endregion
            return View(lstNew);
        }

        [HttpPost]
        public ActionResult Timkiem(FormCollection fr)
        {
            string txtTimkiem = fr["txtTimkiem"];
            return Redirect("/Tim-tin/" + txtTimkiem);
        }

        public ActionResult ListQuestion()
        {

            ViewBag.key_datcauhoi = ClassExten.GetLangKey("key_datcauhoi");
            ViewBag.key_traloihoidap = ClassExten.GetLangKey("key_traloihoidap");
            ViewBag.home = ClassExten.GetLangKey("home");
            ViewBag.key_hoidap = ClassExten.GetLangKey("key_hoidap");

            ViewBag.key_datcauhoi_tvhd = ClassExten.GetLangKey("key_datcauhoi_tvhd");
            ViewBag.key_hoten = ClassExten.GetLangKey("key_hoten");
            ViewBag.key_email_diachi = ClassExten.GetLangKey("key_email_diachi");
            ViewBag.key_email_ndch = ClassExten.GetLangKey("key_email_ndch");
            ViewBag.key_gui_cauhoi = ClassExten.GetLangKey("key_gui_cauhoi");
            ViewBag.key_thoat = ClassExten.GetLangKey("key_thoat");


            string page = "1";//so phan trang hien tai
            var pagesize = 6;//so ban ghi tren 1 trang
            var numOfNews = 0;//tong so ban ghi co duoc truoc khi phan trang
            int curpage = 0; // trang hien tai dung cho phan trang
            if (Request["page"] != null)
            {
                page = Request["page"];
                curpage = Convert.ToInt32(page) - 1;
            }
            Config config;
            #region[kiem tra cache]
            if (ConfigModel.conf == null)
            {
                config = db.Configs.FirstOrDefault(u => u.conLang == lang && u.siteTPA == siteOption);
                ConfigModel.conf = config;
                ConfigModel.date = DateTime.Now;
            }
            else
            {
                config = ConfigModel.conf;
            }
            #endregion
            #region[load seo]
            ViewBag.title = config.titleSeo;
            ViewBag.description = config.desSeo;
            ViewBag.keywords = config.keySeo;
            ViewBag.url = HttpContext.Request.Url.AbsoluteUri;
            ViewBag.img = ClassExten.GetUrlHost() + config.logotop;
            #endregion

            var list = db.Ykiens.Where(u => u.pId == -1).OrderByDescending(u => u.yCreateDate).ToList();
            var data = list.Skip(curpage * pagesize).Take(pagesize).ToList();
            var url = Request.Path;
            numOfNews = list.Count;
            if (numOfNews > pagesize)
            {
                ViewBag.pages = Productgold.Models.Phantrang.PhanTrang(pagesize, curpage, numOfNews, url);
            }
            return View(data);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ListQuestion(FormCollection fr)
        {
            try
            {
                Ykien yk = new Ykien();
                yk.yActive = true;
                yk.pId = -1;
                yk.AdminId = ClassExten.NotAdmin;
                yk.yPhone = "-1";
                yk.yCreateDate = DateTime.Now;
                yk.yName = fr["hoten_ch"];
                yk.yEmailAdd = fr["email_diachi_ch"];
                yk.ynote = fr["noidung_ch"];
                db.Ykiens.Add(yk);
                db.SaveChanges();
                Session["hoidap"] = "ok";
            }
            catch (Exception)
            {
                Session["hoidap"] = null;
            }
            return Redirect("/Hoi-dap");

        }

        public ActionResult DetailQuestion(int id)
        {
            ViewBag.home = ClassExten.GetLangKey("home");
            ViewBag.key_hoidap = ClassExten.GetLangKey("key_hoidap");
            ViewBag.key_xemchitiet = ClassExten.GetLangKey("key_xemchitiet");

            ViewBag.idp = id;
            return View();
        }

        public ActionResult lstDetail(int id)
        {
            if (Session["admins"] != null)
            {
                string adminEmail = Session["admins"].ToString();
                var admin = db.Admins.FirstOrDefault(u => u.email.Equals(adminEmail));
                ViewBag.admin = admin;
            }

            ViewBag.key_thoat = ClassExten.GetLangKey("key_thoat");
            ViewBag.key_sua = ClassExten.GetLangKey("key_sua");
            ViewBag.key_hoten = ClassExten.GetLangKey("key_hoten");
            ViewBag.key_email_diachi = ClassExten.GetLangKey("key_email_diachi");
            ViewBag.key_email_ndch = ClassExten.GetLangKey("key_email_ndch");
            ViewBag.key_gui_bl = ClassExten.GetLangKey("key_gui_bl");

            var data = db.Ykiens.FirstOrDefault(u => u.yId == id);
            ViewBag.datap = data;
            var lstCon = db.Ykiens.Where(u => u.pId == id).OrderBy(u => u.yCreateDate).ToList();
            return PartialView(lstCon);
        }

        public ActionResult AddComment(int id, string name, string email, string note, string idedit)
        {
            try
            {
                if (string.IsNullOrEmpty(idedit))
                {
                    Ykien yk = new Ykien();
                    yk.yCreateDate = DateTime.Now;
                    yk.pId = id;
                    yk.yName = name;
                    yk.ynote = note;
                    yk.yEmailAdd = email;
                    yk.yPhone = "";
                    yk.AdminId = ClassExten.NotAdmin;
                    if (Session["admins"] != null)
                    {
                        yk.AdminId = Session["admins"].ToString();
                    }
                    yk.yEmailAdd = email;
                    yk.yActive = true;
                    db.Ykiens.Add(yk);
                }
                else
                {
                    int iddata = int.Parse(idedit);
                    var yk = db.Ykiens.FirstOrDefault(u => u.yId == iddata);
                    yk.yName = name;
                    yk.ynote = note;
                    yk.yEmailAdd = email;
                }
                db.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

        }

        public List<int> GetListId(int idp, int cap)
        {
            List<int> list = new List<int>();
            if (cap == 1)
            {
                list = db.Categorys.Where(u => u.cateLang == lang && u.siteTPA == siteOption && u.catepar_id == idp && u.cate_cap == 2 && u.active == true).Select(u => u.id).ToList();
                var listCap3 = db.Categorys.Where(u => u.cateLang == lang && u.siteTPA == siteOption && u.cate_cap == 3 && u.active == true && list.Contains(u.catepar_id.Value)).Select(u => u.id).ToList();
                foreach (var item in listCap3)
                {
                    list.Add(item);
                }
            }
            else
            {
                list = db.Categorys.Where(u => u.cateLang == lang && u.siteTPA == siteOption && u.catepar_id == idp && u.cate_cap == 3 && u.active == true).Select(u => u.id).ToList();
            }
            list.Add(idp);
            return list;
        }
    }
}