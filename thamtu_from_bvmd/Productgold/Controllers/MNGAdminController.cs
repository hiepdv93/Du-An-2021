using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Productgold.Models;
using System.Net.Mail;
using System.Net;
using System.Data;
using Newtonsoft.Json;

namespace Productgold.Controllers
{
    public class MNGAdminController : Controller
    {
        //
        // GET: /MNGAdmin/

        WEBTPAEntities db = new WEBTPAEntities();

        public ActionResult Index()
        {

            return View();
        }

        #region[ham danh sach admin]
        public ActionResult Indexs()
        {
            var loginInfo = ClassExten.GetCokies();
            if (loginInfo != null && loginInfo.subAdmin == true)
            {
                var admin = db.Admins.ToList();
                return View(admin);
            }
            else
            {
                return Redirect("/MNGAdmin/LoginAdmin");
            }
        }
        #endregion

        #region[ham login]
        public ActionResult LoginAdmin()
        {
            if (Request.Cookies["adminNTS"] != null)
            {
                var c = new HttpCookie("adminNTS");
                c.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(c);
            }
            return View();
        }
        #endregion

        #region[ham login]
        [HttpPost]
        public ActionResult LoginAdmin(FormCollection f)
        {
            string uu = f["uu"];
            string pp = f["pp"];
            string pmh = Productgold.Models.StringClass.Encrypt("a@" + pp);
            var data = db.Admins.Where(u => u.email == uu && u.pass == pmh && u.active == true).ToList();
            if (data.Count > 0)
            {
                LoginModel loginModel = new LoginModel();
                loginModel.Email = uu;
                loginModel.FullName = data[0].fullname;
                if (data[0].depart == "1")
                {
                    loginModel.subAdmin = true;
                }
                else
                {
                    loginModel.subAdmin = false;
                }

                HttpCookie ntsCookies = new HttpCookie("adminNTS");
                ntsCookies.Value = JsonConvert.SerializeObject(loginModel);
                ntsCookies.Expires = DateTime.Now.AddDays(7);
                Response.Cookies.Add(ntsCookies);

                return Redirect("/MNGAdmin");
                //}
                //else
                //{
                //    ViewBag.erro = "Tài khoản này không có quyền truy cập trang!";
                //    return View();
                // }

            }
            else
            {
                ViewBag.erro = "Tài khoản truy cập không hợp lệ!";
                return View();
            }
        }
        #endregion

        #region[ham delete]
        public ActionResult Delete(int id)
        {
            var loginInfo = ClassExten.GetCokies();
            if (loginInfo != null && loginInfo.subAdmin == true)
            {
                try
                {
                    var data = db.Admins.Where(u => u.id == id).FirstOrDefault();
                    db.Admins.Remove(data);
                    db.SaveChanges();
                    Session["ok"] = "Xóa thành công!";
                    return Redirect("/MNGAdmin/Indexs");
                }
                catch (Exception)
                {
                    return Redirect("/Home/P404");
                }
            }
            else
            {
                return Redirect("/MNGAdmin/LoginAdmin");
            }
        }
        #endregion

        #region[ham them moi admin]
        [HttpPost]
        public ActionResult Add(FormCollection f)
        {
            var loginInfo = ClassExten.GetCokies();
            if (loginInfo != null && loginInfo.subAdmin == true)
            {
                var emailf = f["email"].ToLower();
                var checkEmail = db.Admins.FirstOrDefault(u => u.email.ToLower().Equals(emailf));
                if (checkEmail != null)
                {
                    Session["ok"] = "Email đã tồn tại không thể thêm mới!";
                    return Redirect("/MNGAdmin/Indexs");
                }
                Admin ad = new Admin();
                ad.fullname = f["fullname"];
                ad.email = emailf;
                ad.addresss = f["address"];

                var pp = f["password"];
                ad.pass = Productgold.Models.StringClass.Encrypt("a@" + pp);


                var subadmin = f["subadmin"];
                var nameactive = f["nameactive"];
                ad.active = false;

                if (nameactive == "1")
                {
                    ad.active = true;
                }
                var quyen0 = f["quyen0"];
                var quyen1 = f["quyen1"];
                var quyen2 = f["quyen2"];
                var quyen3 = f["quyen3"];
                string chuoiQuyen = "";
                if (quyen0 != null && quyen0 != "false")
                {
                    chuoiQuyen += "0;";
                }
                if (quyen1 != null && quyen1 != "false")
                {
                    chuoiQuyen += "1;";
                }
                if (quyen2 != null && quyen2 != "false")
                {
                    chuoiQuyen += "2;";
                }
                if (quyen3 != null && quyen3 != "false")
                {
                    chuoiQuyen += "3;";
                }
                if (!string.IsNullOrEmpty(chuoiQuyen))
                {
                    chuoiQuyen = chuoiQuyen.Substring(0, chuoiQuyen.Length - 1);
                }
                ad.depart = "0";
                ad.siteModul = chuoiQuyen;
                db.Admins.Add(ad);
                db.SaveChanges();
                Session["ok"] = "Thêm thành công!";
                return Redirect("/MNGAdmin/Indexs");
            }
            else
            {
                return Redirect("/MNGAdmin/LoginAdmin");
            }
        }
        #endregion

        #region[ham update thong tin]
        [HttpPost]
        public ActionResult Update(FormCollection f)
        {
            var loginInfo = ClassExten.GetCokies();
            if (loginInfo != null && loginInfo.subAdmin == true)
            {
                int idadmin = Convert.ToInt32(f["idupdate"]);
                var data = db.Admins.Where(u => u.id == idadmin).FirstOrDefault();

                string active = f["active"];
                string admin = f["admin"];
                if ("0".Equals(active))
                {
                    data.active = false;
                }
                else
                {
                    data.active = true;
                }

                var quyen0 = f["quyen0"];
                var quyen1 = f["quyen1"];
                var quyen2 = f["quyen2"];
                var quyen3 = f["quyen3"];
                string chuoiQuyen = "";
                if (quyen0 != null && quyen0 != "false")
                {
                    chuoiQuyen += "0;";
                }
                if (quyen1 != null && quyen1 != "false")
                {
                    chuoiQuyen += "1;";
                }
                if (quyen2 != null && quyen2 != "false")
                {
                    chuoiQuyen += "2;";
                }
                if (quyen3 != null && quyen3 != "false")
                {
                    chuoiQuyen += "3;";
                }
                if (!string.IsNullOrEmpty(chuoiQuyen))
                {
                    chuoiQuyen = chuoiQuyen.Substring(0, chuoiQuyen.Length - 1);
                }
                data.fullname = f["fullname"];
                data.siteModul = chuoiQuyen;
                db.SaveChanges();
                Session["ok"] = "Cập nhật thành công!";
                return Redirect("/MNGAdmin/Indexs");
            }
            else
            {
                return Redirect("/MNGAdmin/LoginAdmin");
            }
        }
        #endregion

        #region[ham doi pass]
        [HttpPost]
        public ActionResult ChagePass(FormCollection f)
        {
            var loginInfo = ClassExten.GetCokies();
            if (loginInfo != null )
            {
                string uu = loginInfo.Email;// Session["admins"].ToString();
                string p0 = f["p0"];
                string p1 = f["p1"];
                string p0mh = Productgold.Models.StringClass.Encrypt("a@" + p0);
                var data = db.Admins.Where(u => u.email == uu && u.pass == p0mh).ToList().Count;
                if (data > 0)
                {
                    string p1mh = Productgold.Models.StringClass.Encrypt("a@" + p1);
                    var data0 = db.Admins.Where(u => u.email == uu).FirstOrDefault();
                    data0.pass = p1mh;
                    db.SaveChanges();
                    Session["ok"] = "Cập nhật thành công!";
                    return Redirect("/MNGAdmin/");

                }
                else
                {
                    Session["ok"] = "Mật khẩu cũ ko đúng!";
                    return Redirect("/MNGAdmin/");
                }


            }

            else
            {
                return Redirect("/MNGAdmin/LoginAdmin");
            }
        }
        #endregion

        #region[ham quen pass]
        public ActionResult ForgotPass()
        {
            return View();
        }
        #endregion

        #region[ham quen pass]
        [HttpPost]
        public ActionResult ForgotPass(FormCollection f)
        {
            string email = f["pp"];
            var data = db.Admins.Where(u => u.email == email).ToList();
            if (data.Count > 0)
            {
                Random rd = new Random();
                var rdtext = rd.Next(1, 100);
                string pass = "adminpass" + rdtext;
                string passmh = Productgold.Models.StringClass.Encrypt("a@" + pass);
                var admin = db.Admins.First(u => u.email == email);
                admin.pass = passmh;
                db.SaveChanges();
                #region[code gui mail]
                try
                {
                    var configmail = db.Configs.FirstOrDefault();
                    MailMessage mailsend = new MailMessage();
                    mailsend.To.Add(email);
                    mailsend.From = new MailAddress(configmail.shopemail);

                    mailsend.Subject = "Cập nhật mật khẩu cho tài khoản: " + data[0].email;
                    mailsend.Body = "Mật khẩu mới của bạn là:" + pass + ", để bảo mật thông tin cá nhân bạn vui lòng đăng nhập và đổi mật khẩu.";
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
                }

                #endregion


                return Redirect("/MNGAdmin/LoginAdmin");
            }
            else
            {
                ViewBag.erro = "Tài khoản không hợp lệ!";
                return View();

            }
        }
        #endregion

        #region[ham them admin]
        [HttpPost]
        public ActionResult AdminAdd(FormCollection f, Admin a)
        {
            var loginInfo = ClassExten.GetCokies();
            if (loginInfo != null && loginInfo.subAdmin==true)
            {
                var birth = Convert.ToDateTime(f["birth"]);


                a.fullname = f["fullname"];
                a.email = f["email"];
                a.email = f["uu"];
                var pp = "a@" + f["pass1"];
                var ppmh = Productgold.Models.StringClass.Encrypt(pp);
                a.pass = ppmh;
                a.fullname = f["fullname"];
                db.Admins.Add(a);
                db.SaveChanges();
                return Redirect("/MNGAdmin");
            }

            else
            {
                return Redirect("/MNGAdmin/LoginAdmin");
            }
        }
        #endregion

        #region[check email json]
        public ActionResult CheckEmail(string email)
        {
            var data = db.Admins.Where(u => u.email == email).ToList();
            if (data.Count > 0)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);

            }
        }
        #endregion

        #region[ham Profile]
        [HttpPost]
        public ActionResult Profile(FormCollection f)
        {
            var loginInfo = ClassExten.GetCokies();
            if (loginInfo != null )
            {
                return Redirect("/MNGAdmin/LoginAdmin");
            }
            else
            {
                try
                {

                    var user = loginInfo.Email;
                    var data = db.Admins.Where(u => u.email == user).FirstOrDefault();
                    data.fullname = f["fullname"];
                    data.addresss = f["address"];

                    db.SaveChanges();
                    Session["ok"] = "Cập nhật thành công!";
                    return Redirect("/MNGAdmin/Admin?modam=profile");
                }
                catch (Exception)
                {

                    return Redirect("/MNGAdmin/Admin?modam=profile");
                }
            }
        }
        #endregion

        #region[ham doi pass va thong tin]
        public ActionResult Admin()
        {
            var loginInfo = ClassExten.GetCokies();
            if (loginInfo != null)
            {
                return Redirect("/MNGAdmin/LoginAdmin");
            }
            else
            {
                var user = loginInfo.Email;// Session["admins"].ToString();
                var data = db.Admins.Where(u => u.email == user).FirstOrDefault();
                return View(data);
            }
        }
        #endregion

        #region[ham get by id]
        public ActionResult Getbyid(int id)
        {
            var data = db.Admins.First(u => u.id == id);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion

        public ActionResult CheckPassUser(string pass)
        {
            string passMH = StringClass.Encrypt("a@" + pass);

            var loginInfo = ClassExten.GetCokies();
            if (loginInfo != null )
            {

                string email = loginInfo.Email;// Session["admins"].ToString();
                var data = db.Admins.FirstOrDefault(u => u.email == email && u.pass == passMH);
                if (data != null)
                {
                    return Json(JsonRequestBehavior.AllowGet, "1");
                }
                else
                {
                    return Json(JsonRequestBehavior.AllowGet, "0");
                }

            }
            else
            {
                return Redirect("/MNGAdmin/");
            }

        }
        public ActionResult CheckUser(string email)
        {
            var loginInfo = ClassExten.GetCokies();
            if (loginInfo != null)
            {
                var data = db.Admins.FirstOrDefault(u => u.email.ToLower() == email.ToLower());
                if (data != null)
                {
                    return Json(JsonRequestBehavior.AllowGet, "1");
                }
                else
                {
                    return Json(JsonRequestBehavior.AllowGet, "0");
                }

            }
            else
            {
                return Redirect("/MNGAdmin/");
            }

        }
        public ActionResult CheckUserUpdate(string email, int id)
        {
            var loginInfo = ClassExten.GetCokies();
            if (loginInfo != null)
            {
                var data = db.Admins.FirstOrDefault(u => u.email.ToLower() == email.ToLower() && u.id != id);
                if (data != null)
                {
                    return Json(JsonRequestBehavior.AllowGet, "1");
                }
                else
                {
                    return Json(JsonRequestBehavior.AllowGet, "0");
                }

            }
            else
            {
                return Redirect("/MNGAdmin/");
            }

        }
    }
}
