using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Productgold.Models;
using System.Net.Mail;
using System.Net;
namespace Productgold.Controllers
{
    public class ConfigController : BaseCookisController
    {
        //
        // GET: /Config/
        WEBTPAEntities db = new WEBTPAEntities();
        string lang = ClassExten.GetLang();
        string siteTPA = ClassExten.siteOption;
        #region[view]
        public ActionResult Index()
        {
            var data = db.Configs.FirstOrDefault(u => u.conLang == lang && u.siteTPA==siteTPA);
            ConfigModel.conf = data;
            return View(data);
        }
        #endregion

        #region[cap nhat]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Index(FormCollection f)
        {
            try
            {
                ConfigModel.conf = null;
                var idupdate = Convert.ToInt32(f["idupdate"]);
                var data = db.Configs.First(u => u.id == idupdate);

                data.shopname = f["shopname"];
                data.phone = f["phone"];
                data.active = true;
                data.fax = f["fax"];
                data.Email_Inbox = f["emailn"];
                data.shopemail = f["emailg"];
                data.emailpass = f["pass"];
                data.conMail_Port = f["cong"];
                data.logotop = f["logo"];
                data.link = f["link"];
                data.addres = f["addres"];
                data.map = f["map"];
                data.liveChat = f["liveChat"];
                data.conTact = f["conTact"];
                data.yeuCauBaoGia = f["yeuCauBaoGia"];
                string idsanpham0 = f["idsanpham0"];
                string idsanpham1 = f["idsanpham1"];
                string idsanpham2 = f["idsanpham2"];

                string idtintuc0 = f["idtintuc0"];
                string idtintuc1 = f["idtintuc1"];
                string idtintuc2 = f["idtintuc2"];

                string idvideo0 = f["idvideo0"];
                string idvideo1 = f["idvideo1"];
                string idvideo2 = f["idvideo2"];

                string idbinhluan0 = f["idbinhluan0"];
                string idbinhluan1 = f["idbinhluan1"];

                string viewpro = idsanpham0 + ";" + idsanpham1 + ";" + idsanpham2;
                string viewnew = idtintuc0 + ";" + idtintuc1 + ";" + idtintuc2;
                string viewvideo = idvideo0 + ";" + idvideo1 + ";" + idvideo2;
                string viewbl = idbinhluan0 + ";" + idbinhluan1;

                data.viewPro = viewpro;
                data.viewNews = viewnew;
                data.viewVideo = viewvideo;
                data.viewYkien = viewbl;

                data.viewCountFrom = Convert.ToInt32(f["idview0"]);
                data.viewCountTo = Convert.ToInt32(f["idview1"]);
                data.viewNowFrom = Convert.ToInt32(f["idaccess0"]);
                data.viewNowTo = Convert.ToInt32(f["idaccess1"]);

                data.faceBook = f["facebook"];
                data.google = f["google"];
                data.youTube = f["youtube"];
                data.twiter = f["twiter"];

                data.ex2 = f["ex2"];
                data.desSeo = f["desseo"];
                data.keySeo = f["keyseo"];
                data.titleSeo = f["titleseo"];
                data.googleAnalytics = f["googleAnalytics"];
                db.SaveChanges();
                ConfigModel.conf = null;
                Session["ok"] = "Cập nhật thành công!";
                return Redirect("/config");
            }
            catch (Exception)
            {

                return Redirect("/MNGAdmin/p404");
            }
        }
        #endregion

        #region[ham lay du lieu]
        public ActionResult Getac()
        {
            var data = db.Configs.FirstOrDefault(u => u.conLang == lang && u.siteTPA == siteTPA);
            if (data.active == true)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region[gui mail]
        public ActionResult Sendmail()
        {
            var emaildk = db.emails;
            var kh = db.Customers;
            SelectList sldk = new SelectList(emaildk, "id", "email1");
            SelectList slkh = new SelectList(kh, "id", "fullname");
            ViewBag.idemaildk = sldk;
            ViewBag.idkh = slkh;
            return View();
        }
        #endregion

        #region[gui mail]
        [ValidateInput(false)]
        public ActionResult Sendmails(string type, string email, string title, string nd)
        {
            var configmail = db.Configs.FirstOrDefault(u => u.conLang == lang && u.siteTPA == siteTPA);
            if (type == "0")
            {
                #region[code gui mail]
                try
                {

                    MailMessage mailsend = new MailMessage();
                    mailsend.To.Add(email);
                    //mailsend.From = new MailAddress("Website.Managers.2015@gmail.com");
                    mailsend.From = new MailAddress(configmail.shopemail);

                    mailsend.Subject = title;
                    mailsend.Body = nd;
                    mailsend.IsBodyHtml = true;
                    int cong = Convert.ToInt32(configmail.ex1);
                    SmtpClient client = new SmtpClient("smtp.gmail.com", cong);
                    client.EnableSsl = true;
                    NetworkCredential credentials = new NetworkCredential(configmail.shopemail, configmail.emailpass);

                    client.Credentials = credentials;
                    client.Send(mailsend);
                }
                catch (Exception)
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
                #endregion
            }
            else if (type == "1")
            {
                #region[code gui mail]
                try
                {
                    MailMessage mailsend = new MailMessage();
                    string[] mang = email.Split(',').ToArray();
                    for (int i = 0; i < mang.Length; i++)
                    {
                        int idemail = Convert.ToInt32(mang[i]);
                        var ad = db.Admins.First(u => u.id == idemail);
                        mailsend.To.Add(ad.email);
                    }
                    mailsend.From = new MailAddress(configmail.shopemail);

                    mailsend.Subject = title;
                    mailsend.Body = nd;
                    mailsend.IsBodyHtml = true;
                    int cong = Convert.ToInt32(configmail.ex1);
                    SmtpClient client = new SmtpClient("smtp.gmail.com", cong);
                    client.EnableSsl = true;
                    NetworkCredential credentials = new NetworkCredential(configmail.shopemail, configmail.emailpass);

                    client.Credentials = credentials;
                    client.Send(mailsend);
                }
                catch (Exception)
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
                #endregion
            }
            else if (type == "2")
            {
                #region[code gui mail]
                try
                {
                    MailMessage mailsend = new MailMessage();
                    string[] mang = email.Split(',').ToArray();
                    for (int i = 0; i < mang.Length; i++)
                    {
                        int idemail = Convert.ToInt32(mang[i]);
                        var mail = db.emails.First(u => u.id == idemail);
                        mailsend.To.Add(mail.email1);
                    }
                    mailsend.From = new MailAddress(configmail.shopemail);

                    mailsend.Subject = title;
                    mailsend.Body = nd;
                    mailsend.IsBodyHtml = true;
                    int cong = Convert.ToInt32(configmail.ex1);
                    SmtpClient client = new SmtpClient("smtp.gmail.com", cong);
                    client.EnableSsl = true;
                    NetworkCredential credentials = new NetworkCredential(configmail.shopemail, configmail.emailpass);

                    client.Credentials = credentials;
                    client.Send(mailsend);
                }
                catch (Exception)
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
                #endregion
            }
            else if (type == "3")
            {
                #region[code gui mail]
                try
                {
                    MailMessage mailsend = new MailMessage();

                    string[] mang = email.Split(',').ToArray();
                    for (int i = 0; i < mang.Length; i++)
                    {
                        int idemail = Convert.ToInt32(mang[i]);
                        var cus = db.Customers.First(u => u.id == idemail);
                        mailsend.To.Add(cus.email);
                    }

                    mailsend.From = new MailAddress(configmail.shopemail);

                    mailsend.Subject = title;
                    mailsend.Body = nd;
                    mailsend.IsBodyHtml = true;
                    int cong = Convert.ToInt32(configmail.ex1);
                    SmtpClient client = new SmtpClient("smtp.gmail.com", cong);
                    client.EnableSsl = true;
                    NetworkCredential credentials = new NetworkCredential(configmail.shopemail, configmail.emailpass);

                    client.Credentials = credentials;
                    client.Send(mailsend);
                }
                catch (Exception)
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
                #endregion
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        #endregion


        #region 
        public string GetLang()
        {
            return Request.Cookies["cookie"].ToString();
        }

        #endregion


        public ActionResult Genac()
        {
            if (Session["subadmins"] != null)
            {
                return View();
            }
            else
            {
                return Redirect("/MNGAdmin/LoginAdmin");
            }
        }
        [HttpPost]
        public ActionResult Genac(FormCollection f)
        {
            string key = f["key"];
            key = StringClass.Encrypt(key);
            ViewBag.key = key;
            return View();
        }
        public ActionResult Acac()
        {
            if (Session["subadmins"] != null)
            {
                return View();
            }
            else
            {
                return Redirect("/MNGAdmin/LoginAdmin");
            }
        }
        [HttpPost]
        public ActionResult Acac(FormCollection f)
        {
            string key = f["key"];
            var ac = db.Configs.FirstOrDefault(u => u.conLang == "vi");
            db.SaveChanges();
            ViewBag.key = "ok";
            return View();
        }

        public ActionResult ChangeLangAdmin(string lang)
        {
            ClassExten.Changelang(lang);
            return Json(JsonRequestBehavior.AllowGet,"ok");
        }

        public ActionResult ChangeLangSite(string lang)
        {
            ClassExten.ChangelangSite(lang);
            return Json(JsonRequestBehavior.AllowGet, "ok");
        }
    }
}
