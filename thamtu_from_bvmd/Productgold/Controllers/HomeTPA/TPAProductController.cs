using Productgold.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace Productgold.Controllers.HomeTPA
{
    public class TPAProductController : Controller
    {
        WEBTPAEntities db = new WEBTPAEntities();
        string lang = ClassExten.GetLangSite();
        string siteOption = ClassExten.siteOption;
        // GET: TPAProduct
        public ActionResult Index(string id)
        {
            //   ViewBag.Sanphamtanphat = ClassExten.GetLangKey("key_sanphamtanphat");
            ViewBag.key_mota = ClassExten.GetLangKey("key_mota");
            ViewBag.key_thongsokt = ClassExten.GetLangKey("key_thongsokt");
            ViewBag.key_video = ClassExten.GetLangKey("key_video");

            ViewBag.key_thuonghieu = ClassExten.GetLangKey("key_thuonghieu");
            ViewBag.key_chatlieu = ClassExten.GetLangKey("key_chatlieu");
            ViewBag.key_baohanh = ClassExten.GetLangKey("key_baohanh");
            ViewBag.key_modelsp = ClassExten.GetLangKey("key_modelsp");

            ViewBag.key_muangay = ClassExten.GetLangKey("key_muangay");
            ViewBag.key_motathem = ClassExten.GetLangKey("key_motathem");
            ViewBag.key_gia = ClassExten.GetLangKey("key_gia");
            ViewBag.key_lienhe = ClassExten.GetLangKey("key_lienhe");
            ViewBag.NavTrangchu = ClassExten.GetLangKey("home");
            ViewBag.key_sanpham = ClassExten.GetLangKey("key_sanpham");
            var number = db.sp_Config_SelectViewNumber(lang, siteOption).ToList();
            List<Product> list = new List<Product>();
            string page = "1";//so phan trang hien tai
            var pagesize = 6;//so ban ghi tren 1 trang
            var numOfNews = 0;//tong so ban ghi co duoc truoc khi phan trang
            int curpage = 0; // trang hien tai dung cho phan trang
            if (Request["page"] != null)
            {
                page = Request["page"];
                curpage = Convert.ToInt32(page) - 1;
            }
            try
            {
                pagesize = int.Parse(number[0].viewPro.Split(';')[0]);
            }
            catch (Exception)
            { }
            if (string.IsNullOrEmpty(id))
            {
                var config = db.sp_Config_SelectSeoOk(lang, siteOption).ToList();
                list = db.Products.Where(u => u.pLang == lang && u.siteTPA == siteOption && u.active == true).OrderBy(u => u.proOrder).ToList();
                #region[load seo]
                ViewBag.title = config[0].titleSeo;
                ViewBag.description = config[0].desSeo;
                ViewBag.keywords = config[0].keySeo;
                ViewBag.url = HttpContext.Request.Url.AbsoluteUri;
                ViewBag.img = ClassExten.GetUrlHost() + config[0].logoTop;
                #endregion

            }
            else
            {
                var cateP = db.Categorys.FirstOrDefault(u => u.cateLang == lang && u.ctype == 1 && u.active == true && u.siteTPA == siteOption && u.keys.Equals(id));
                if (cateP != null)
                {
                    List<int> listId = new List<int>();
                    if (cateP.cate_cap != 3)
                    {
                        listId = GetListId(cateP.id, cateP.cate_cap.Value);
                    }
                    else
                    {
                        listId.Add(cateP.id);
                    }
                    // ViewBag.Sanphamtanphat = cateP.name;
                    ViewBag.TieudeDanhmuc = "<li><a href='#' title='" + cateP.name + "'>" + cateP.name + "</a></li>";
                    list = db.Products.Where(u => u.pLang == lang && u.siteTPA == siteOption && u.active == true && listId.Contains(u.cateid.Value)).OrderBy(u => u.proOrder).ToList();
                }

            }
            var data = list.Skip(curpage * pagesize).Take(pagesize).ToList();
            var url = Request.Path;
            numOfNews = list.Count;
            if (numOfNews > pagesize)
            {
                ViewBag.pages = Productgold.Models.Phantrang.PhanTrang(pagesize, curpage, numOfNews, url);
            }
            return View(data);
        }
        public ActionResult Timkiem(string id)
        {
            ViewBag.key_timkiem = ClassExten.GetLangKey("key_timkiem");
            ViewBag.keytimkiem = id;
            ViewBag.key_mota = ClassExten.GetLangKey("key_mota");
            ViewBag.key_thongsokt = ClassExten.GetLangKey("key_thongsokt");
            ViewBag.key_video = ClassExten.GetLangKey("key_video");

            ViewBag.key_thuonghieu = ClassExten.GetLangKey("key_thuonghieu");
            ViewBag.key_chatlieu = ClassExten.GetLangKey("key_chatlieu");
            ViewBag.key_baohanh = ClassExten.GetLangKey("key_baohanh");
            ViewBag.key_modelsp = ClassExten.GetLangKey("key_modelsp");

            ViewBag.key_muangay = ClassExten.GetLangKey("key_muangay");
            ViewBag.key_motathem = ClassExten.GetLangKey("key_motathem");
            ViewBag.key_gia = ClassExten.GetLangKey("key_gia");
            ViewBag.key_lienhe = ClassExten.GetLangKey("key_lienhe");
            ViewBag.NavTrangchu = ClassExten.GetLangKey("home");
            ViewBag.key_sanpham = ClassExten.GetLangKey("key_sanpham");
            var number = db.sp_Config_SelectViewNumber(lang, siteOption).ToList();
            List<Product> list = new List<Product>();
            string page = "1";//so phan trang hien tai
            var pagesize = 6;//so ban ghi tren 1 trang
            var numOfNews = 0;//tong so ban ghi co duoc truoc khi phan trang
            int curpage = 0; // trang hien tai dung cho phan trang
            if (Request["page"] != null)
            {
                page = Request["page"];
                curpage = Convert.ToInt32(page) - 1;
            }
            try
            {
                pagesize = int.Parse(number[0].viewPro.Split(';')[0]);
            }
            catch (Exception)
            { }
            var config = db.sp_Config_SelectSeoOk(lang, siteOption).ToList();
            list = db.Products.Where(u => u.pLang == lang && u.active == true
            && (u.pro_name.ToLower().Contains(id.ToLower())
            || u.proCode.ToLower().Contains(id.ToLower()))
            ).OrderBy(u => u.proOrder).ToList();
            #region[load seo]
            ViewBag.title = config[0].titleSeo;
            ViewBag.description = config[0].desSeo;
            ViewBag.keywords = config[0].keySeo;
            ViewBag.url = HttpContext.Request.Url.AbsoluteUri;
            ViewBag.img = ClassExten.GetUrlHost() + config[0].logoTop;
            #endregion

            var data = list.Skip(curpage * pagesize).Take(pagesize).ToList();
            var url = Request.Path;
            numOfNews = list.Count;
            if (numOfNews > pagesize)
            {
                ViewBag.pages = Productgold.Models.Phantrang.PhanTrang(pagesize, curpage, numOfNews, url);
            }
            return View(data);
        }
        public List<int> GetListId(int idp, int cap)
        {
            List<int> list = new List<int>();
            if (cap == 1)
            {
                list = db.Categorys.Where(u => u.cateLang == lang && u.siteTPA == siteOption && u.catepar_id == idp && u.cate_cap == 2 && u.active == true && u.ctype == 1).Select(u => u.id).ToList();
                var listCap3 = db.Categorys.Where(u => u.cateLang == lang && u.siteTPA == siteOption && u.cate_cap == 3 && u.active == true && list.Contains(u.catepar_id.Value) && u.ctype == 1).Select(u => u.id).ToList();
                foreach (var item in listCap3)
                {
                    list.Add(item);
                }
            }
            else
            {
                list = db.Categorys.Where(u => u.cateLang == lang && u.siteTPA == siteOption && u.catepar_id == idp && u.cate_cap == 3 && u.active == true && u.ctype == 1).Select(u => u.id).ToList();
            }
            list.Add(idp);
            return list;
        }
        public ActionResult Linhvuc()
        {
            ViewBag.Sanphamtanphat = ClassExten.GetLangKey("key_sanphamtanphat");
            ViewBag.NavTrangchu = ClassExten.GetLangKey("home");
            ViewBag.NavLinhvuc = ClassExten.GetLangKey("key_linhvuc");
            var config = db.sp_Config_SelectSeoOk(lang, siteOption).ToList();
            #region[load seo]
            ViewBag.title = config[0].titleSeo;
            ViewBag.description = config[0].desSeo;
            ViewBag.keywords = config[0].keySeo;
            ViewBag.url = HttpContext.Request.Url.AbsoluteUri;
            ViewBag.img = ClassExten.GetUrlHost() + config[0].logoTop;
            #endregion
            var microsite = db.Advs.Where(u => u.advLang == lang && u.siteTPA == siteOption && u.active == true && u.atype == 5).OrderBy(u => u.advOrder).ToList();
            return View(microsite);
        }

        public ActionResult Khachhang()
        {
            ViewBag.NavTrangchu = ClassExten.GetLangKey("home");
            ViewBag.Navkhachhang = ClassExten.GetLangKey("key_khachhang");
            var config = db.sp_Config_SelectSeoOk(lang, siteOption).ToList();
            #region[load seo]
            ViewBag.title = config[0].titleSeo;
            ViewBag.description = config[0].desSeo;
            ViewBag.keywords = config[0].keySeo;
            ViewBag.url = HttpContext.Request.Url.AbsoluteUri;
            ViewBag.img = ClassExten.GetUrlHost() + config[0].logoTop;
            #endregion
            var cus = db.Advs.Where(u => u.advLang == lang && u.siteTPA == siteOption && u.active == true && u.atype == 4).OrderBy(u => u.advOrder).ToList();
            return View(cus);
        }

        public ActionResult Lienhe()
        {
            ViewBag.home = ClassExten.GetLangKey("home");
            ViewBag.key_lienhe = ClassExten.GetLangKey("key_lienhe");
            ViewBag.key_sdt = ClassExten.GetLangKey("key_sdt");
            ViewBag.key_hoten = ClassExten.GetLangKey("key_hoten");
            ViewBag.lienhevoichungtoi = ClassExten.GetLangKey("key_lienhevoichungtoi");
            ViewBag.key_diachilh = ClassExten.GetLangKey("key_diachilh");
            ViewBag.key_ndlienhe = ClassExten.GetLangKey("key_ndlienhe");
            ViewBag.key_ndlienhe = ClassExten.GetLangKey("key_ndlienhe");
            ViewBag.key_guitinnhan = ClassExten.GetLangKey("key_guitinnhan");

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
            ViewBag.bglienhe = db.Advs.FirstOrDefault(u=>u.advLang==lang && u.active==true && u.atype==4);
            return View(config);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Lienhe(FormCollection fr)
        {
            Contact cont = new Contact();
            cont.fullname = fr["hotenlh"];
            cont.email = fr["emaillh"];
            cont.phone = fr["phone"];
            cont.contents = fr["messagelh"];
            cont.address = fr["addr"];
            cont.conLang = lang;
            cont.siteTPA = siteOption;
            cont.type = 0;
            cont.createdate = DateTime.Now;
            cont.active = false;
            db.Contacts.Add(cont);
            db.SaveChanges();
            Session["guilienhe"] = "ok";
            try
            {
                #region[code gui mail]
                try
                {
                    var configmail = db.Configs.FirstOrDefault();
                    MailMessage mailsend = new MailMessage();
                    mailsend.To.Add(configmail.Email_Inbox);
                    mailsend.From = new MailAddress(configmail.shopemail);

                    mailsend.Subject = "Thông báo một liên hệ được gửi từ trang " + Request.Url.Host + " của khách hàng: " + cont.fullname;
                    string nd = "";
                    nd += "<p>Tên khách hàng: " + cont.fullname + "</p>";
                    nd += "<p>Email: " + cont.email + "</p>";
                    nd += "<p>Số điện thoại liên hệ: " + cont.phone + "</p>";
                    nd += "<p>Nội dung gửi: " + cont.contents + "</p>";
                    mailsend.Body = nd;
                    mailsend.IsBodyHtml = true;
                    int cong = Convert.ToInt32(465);
                    SmtpClient client = new SmtpClient("smtp.gmail.com", cong);
                    client.EnableSsl = true;
                    NetworkCredential credentials = new NetworkCredential(configmail.shopemail, configmail.emailpass);
                    client.Credentials = credentials;
                    client.Send(mailsend);
                }
                catch (Exception)
                {
                }

                #endregion
            }
            catch (Exception)
            { }
            return Redirect("/Lien-he");
        }

        public ActionResult Chitiet(string id)
        {
            ViewBag.contact = ClassExten.GetLangKey("contact");
            ViewBag.key_thoat = ClassExten.GetLangKey("key_thoat");
            ViewBag.key_sdt = ClassExten.GetLangKey("key_sdt");
            ViewBag.key_diachiemail = ClassExten.GetLangKey("key_diachiemail");
            ViewBag.key_hoten = ClassExten.GetLangKey("key_hoten");
            ViewBag.key_tinnhan = ClassExten.GetLangKey("key_tinnhan");
            ViewBag.key_guitinnhan = ClassExten.GetLangKey("key_guitinnhan");
            ViewBag.key_lienhevoichungtoi = ClassExten.GetLangKey("key_lienhevoichungtoi");
            ViewBag.key_modelsp = ClassExten.GetLangKey("key_modelsp");

            ViewBag.key_mota = ClassExten.GetLangKey("key_mota");
            ViewBag.key_thongsokt = ClassExten.GetLangKey("key_thongsokt");
            ViewBag.key_video = ClassExten.GetLangKey("key_video");

            ViewBag.key_gia = ClassExten.GetLangKey("key_gia");
            ViewBag.key_muangay = ClassExten.GetLangKey("key_muangay");
            ViewBag.key_video = ClassExten.GetLangKey("key_video");
            ViewBag.key_thuonghieu = ClassExten.GetLangKey("key_thuonghieu");
            ViewBag.key_chatlieu = ClassExten.GetLangKey("key_chatlieu");
            ViewBag.key_baohanh = ClassExten.GetLangKey("key_baohanh");
            ViewBag.key_modelsp = ClassExten.GetLangKey("key_modelsp");
            ViewBag.key_spcungchuyenmuc = ClassExten.GetLangKey("key_spcungchuyenmuc");

            ViewBag.key_lienhe = ClassExten.GetLangKey("key_lienhe");
            ViewBag.NavTrangchu = ClassExten.GetLangKey("home");
            ViewBag.key_sanpham = ClassExten.GetLangKey("key_sanpham");
            int numberOther = 4;
            var number = db.sp_Config_SelectViewNumber(lang, siteOption).ToList();
            try
            {
                numberOther = int.Parse(number[0].viewPro.Split(';')[1]);
            }
            catch (Exception)
            { }
            var data = db.Products.FirstOrDefault(u => u.pro_key.Equals(id) && u.pLang == lang);
            var listProduct = db.Products.Where(u => u.cateid == data.cateid).OrderBy(u => u.proOrder).Take(numberOther).ToList();
            var catep = db.Categorys.FirstOrDefault(u => u.id == data.cateid);
            string chuoiNav = "";
            chuoiNav += "<li><a href='/San-pham/" + catep.keys + "' title='" + catep.name + "'>" + catep.name + "</a></li>";
            chuoiNav += "<li><a href='#' title='" + data.pro_name + "'>" + data.pro_name + "</a></li>";
            #region[load seo]
            ViewBag.title = data.titleSeo;
            ViewBag.description = data.desSeo;
            ViewBag.keywords = data.keySeo;
            ViewBag.url = HttpContext.Request.Url.AbsoluteUri;
            ViewBag.img = ClassExten.GetUrlHost() + data.proAvata;
            #endregion
            ViewBag.TieudeDanhmuc = chuoiNav;
            ViewBag.listProduct = listProduct;

            return View(data);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Chitiet(string id, FormCollection fr)
        {
            Contact cont = new Contact();
            cont.fullname = fr["hotenlh"];
            cont.email = fr["emaillh"];
            cont.phone = fr["phone"];
            cont.contents = fr["messagelh"];
            cont.conLang = lang;
            cont.siteTPA = siteOption;
            cont.type = 0;
            cont.createdate = DateTime.Now;
            cont.active = false;
            db.Contacts.Add(cont);
            db.SaveChanges();
            Session["guilienhe"] = "ok";
            try
            {
                #region[code gui mail]
                try
                {
                    var configmail = db.Configs.FirstOrDefault();
                    MailMessage mailsend = new MailMessage();
                    mailsend.To.Add(configmail.Email_Inbox);
                    mailsend.From = new MailAddress(configmail.shopemail);

                    mailsend.Subject = "Thông báo một liên hệ được gửi từ trang " + Request.Url.Host + " của khách hàng: " + cont.fullname;
                    string nd = "";
                    nd += "<p>Tên khách hàng: " + cont.fullname + "</p>";
                    nd += "<p>Email: " + cont.email + "</p>";
                    nd += "<p>Số điện thoại liên hệ: " + cont.phone + "</p>";
                    nd += "<p>Nội dung gửi: " + cont.contents + "</p>";
                    mailsend.Body = nd;
                    mailsend.IsBodyHtml = true;
                    int cong = Convert.ToInt32(465);
                    SmtpClient client = new SmtpClient("smtp.gmail.com", cong);
                    client.EnableSsl = true;
                    NetworkCredential credentials = new NetworkCredential(configmail.shopemail, configmail.emailpass);
                    client.Credentials = credentials;
                    client.Send(mailsend);
                }
                catch (Exception)
                {
                }

                #endregion
            }
            catch (Exception)
            { }
            return Redirect("/Chi-tiet/" + id);
        }
        [HttpPost]
        public ActionResult Timkiem(FormCollection fr)
        {
            string txtTimkiem = fr["txtTimkiem"];
            return Redirect("/Tim-kiem/" + txtTimkiem);
        }

        public ActionResult HomePage()
        {
            ViewBag.key_xemchitiet = ClassExten.GetLangKey("key_xemchitiet");
            ViewBag.key_guiyeucau = ClassExten.GetLangKey("key_guiyeucau");
            ViewBag.key_ndyeucau = ClassExten.GetLangKey("key_ndyeucau");
            ViewBag.key_tencongty = ClassExten.GetLangKey("key_tencongty");
            ViewBag.key_emailsdt = ClassExten.GetLangKey("key_emailsdt");
            ViewBag.key_yeucaubaogia = ClassExten.GetLangKey("key_yeucaubaogia");
            ViewBag.key_hoten = ClassExten.GetLangKey("key_hoten");
            ViewBag.key_linhvuchd = ClassExten.GetLangKey("key_linhvuchd");

            ViewBag.key_chonchungtoi = ClassExten.GetLangKey("key_chonchungtoi");
            ViewBag.Sanphamtanphat = ClassExten.GetLangKey("key_sanphamtanphat");
            var listSlide = db.Advs.Where(u => u.advLang == lang && u.siteTPA == siteOption && u.atype == 2 && u.active == true).OrderBy(u => u.advOrder).ToList();
            var cateHome = db.Categorys.Where(u => u.cateLang == lang && u.siteTPA == siteOption && u.ctype == 1 && u.active == true && u.activeHome == true && u.cateLevel.Equals("0")).OrderBy(u => u.cateOrder).ToList();
            var cateHome2 = db.Categorys.Where(u => u.cateLang == lang && u.siteTPA == siteOption && u.ctype == 1 && u.active == true && u.activeHome == true && u.cateLevel.Equals("1")).OrderBy(u => u.cateOrder).ToList();
            var listKhachhang = db.Advs.Where(u => u.advLang == lang && u.siteTPA == siteOption && u.atype == 4 && u.active == true).OrderBy(u => u.advOrder).ToList();
            var listVisao = db.Advs.Where(u => u.advLang == lang && u.siteTPA == siteOption && u.atype == 5 && u.active == true).OrderBy(u => u.advOrder).ToList();
            ViewBag.listSlide = listSlide;
            ViewBag.listKhachhang = listKhachhang;
            ViewBag.listVisao = listVisao;
            ViewBag.cateHome2 = cateHome2;
            return View(cateHome);
        }

        public ActionResult Guiyeucau(string hoten, string email, string congty, string noidung)
        {
            Contact cont = new Contact();
            cont.fullname = hoten;
            cont.email = email;
            cont.phone = email;
            cont.contents = noidung;
            cont.conLang = lang;
            cont.siteTPA = siteOption;
            cont.type = 1;
            cont.address = congty;
            cont.createdate = DateTime.Now;
            cont.active = false;
            db.Contacts.Add(cont);
            db.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}