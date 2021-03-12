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
    public class KhachhangController : Controller
    {
        //
        // GET: /Khachhang/
        WEBTPAEntities db = new WEBTPAEntities();

        #region[thong tin]
        public ActionResult Index()
        {
            if (Session["user"] != null)
            {
                var user = Session["user"].ToString();
                var data = db.Customers.First(u=>u.email==user);
                return View(data);
            }
            else
            {
                return Redirect("/Khachhang/Dangnhap");
            }
        }
        #endregion

        #region[thong tin]
        [HttpPost]
        public ActionResult Index(FormCollection f)
        {
            if (Session["user"] != null)
            {
                var user = Session["user"].ToString();
                var data = db.Customers.First(u => u.email == user);
                data.addres = f["address"];
                //data.birth = DateTime.Parse(f["birth"]);
                data.fullname=f["name"];
                data.phone=f["phone"];
                db.SaveChanges();
                ViewBag.erro = "Cập nhật thành công!";
                return Redirect("/Khachhang");
            }
            else
            {
                return Redirect("/Khachhang/Dangnhap");
            }
        }
        #endregion

        #region[dang ki]
        public ActionResult Dangky()
        {
            return View();
        }
        #endregion

        #region[dang ki]
        [HttpPost]
        public ActionResult Dangky(FormCollection f)
        {
            var email = f["email"];
            var data = db.Customers.Where(u=>u.email==email).ToList().Count;
            if (data > 0)
            {
                ViewBag.erro = "Email đã tồn tại";
                return View();
            }
            else
            {
                Customer c = new Customer();
                c.email = email;c.fullname=f["name"];
                c.phone=f["phone"];
                //c.birth = DateTime.Parse(f["birth"]);
                c.active = true;
                c.addres = f["address"];
                var pass = "a@"+f["pass1"];
                c.pass = Productgold.Models.StringClass.Encrypt(pass);
                db.Customers.Add(c);
                db.SaveChanges();
                return Redirect("/Khachhang/Dangnhap");
            }
        }
        #endregion

        #region[dang nhap]
        public ActionResult Dangnhap()
        {
            return View();
        }
        #endregion

        #region[dang nhap]
        [HttpPost]
        public ActionResult Dangnhap(FormCollection f)
        {
            var pass = "a@"+f["pass"];
            var passmh = Productgold.Models.StringClass.Encrypt(pass);
            var email = f["email"];
            var data = db.Customers.Where(u=>u.email==email && u.pass==passmh && u.active==true).ToList().Count;
            if (data > 0)
            {
                Session["user"] = email;
                if (Session["ShoppingCart"]!=null)
                {
                    return Redirect("/Carts");
                }
                return Redirect("/Trangchu");
            }
            else
            {
                ViewBag.erro = "Tài khoản không hợp lệ!";
                return View();
            }
        }
        #endregion

        #region[Quên mat khau]
        public ActionResult Quenmatkhau()
        {
            return View();
        }
        #endregion

        #region[Quên mat khau]
        [HttpPost]
        public ActionResult Quenmatkhau(FormCollection f)
        {
            var email = f["email"];
            var cus = db.Customers.Where(u=>u.email==email).ToList().Count;
            if (cus > 0)
            {
                Random rd = new Random();
                var rdtext = rd.Next(1, 100);
                string pass = "pass" + rdtext;
                string passmh = Productgold.Models.StringClass.Encrypt("a@" + pass);
                var kh = db.Customers.First(u=>u.email==email);
                kh.pass = passmh;
                db.SaveChanges();
                #region[code gui mail]
                try
                {
                    var configmail = db.Configs.FirstOrDefault();
                    MailMessage mailsend = new MailMessage();
                    mailsend.To.Add(email);
                    mailsend.From = new MailAddress(configmail.shopemail);

                    mailsend.Subject = "Cập nhật mật khẩu cho tài khoản: " +email;
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

                return Redirect("/Khachhang/Dangnhap");
            }
            else
            {
                ViewBag.erro = "Email không tồn tại!";
                return View();
            }
        }
        #endregion

        #region[doi mat khau]
        public ActionResult Doimatkhau()
        {
            if (Session["user"] != null)
            {
                return View();
            }
            else
            {
                return Redirect("/Khachhang/Dangnhap");
            }
        }
        #endregion

        #region[doi mat khau]
        [HttpPost]
        public ActionResult Doimatkhau(FormCollection f)
        {
            if (Session["user"] != null)
            {
                var passcu = "a@"+f["pass0"];
                var user = Session["user"].ToString();
                var passcumh = Productgold.Models.StringClass.Encrypt(passcu);

                var data = db.Customers.Where(u=>u.email==user && u.pass==passcumh).ToList().Count;
                if (data > 0)
                {
                    var passmoi = "a@" + f["pass1"];
                    var passmoimh = Productgold.Models.StringClass.Encrypt(passmoi);
                    var cus = db.Customers.First(u => u.email == user);
                    cus.pass = passmoimh;
                    db.SaveChanges();
                    return Redirect("/Khachhang");
                }
                else
                {
                    ViewBag.erro = "Mật khẩu cũ không đúng";
                    return View();

                }

            }
            else
            {
                return Redirect("/Khachhang/Dangnhap");
            }
        }
        #endregion

        #region[thoat]
        public ActionResult Thoat()
        {
            Session["user"]=null;
            return Redirect("/Trangchu");
        }
        #endregion

        #region[check mail]
        public ActionResult Checkmail(string email)
        {
            var data = db.Customers.Where(u => u.email == email).ToList().Count;
            if (data > 0)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
    }
}
