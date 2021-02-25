using Microsoft.Owin.Security;
using Newtonsoft.Json;
using NTSPRODUCT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace NTSPRODUCT.Controllers.Admins
{
    public class AdminsController : Controller
    {
        NTSWEBEntities db = new NTSWEBEntities();
        string lang = ClassExten.GetLang();
        // GET: Admins
        [MyAuthorize]
        public ActionResult Index()
        {
            #region[check admin]
            var userInfo = ClassExten.GetCokies();
            if (userInfo != null)
            {
                if (userInfo.subAdmin == false)
                {
                    return Redirect("/Admins/Login");
                }
            }
            else
            {
                return Redirect("/Admins/Login");
            }
            #endregion
            return View();
        }
        [MyAuthorize]
        public ActionResult GetList(SearchModel model)
        {
            var all = (from a in db.Admins.AsNoTracking()
                       where a.subAdmin == false
                       && (string.IsNullOrEmpty(model.Name) || a.fullName.ToLower().Contains(model.Name) || a.email.ToLower().Contains(model.Name))
                       orderby a.fullName
                       select a).AsQueryable();

            var numOfSlides = all.Select(u => u.id).Count();
            var currPage = model.PageNumber - 1;
            var data = all.Skip((model.PageNumber - 1) * model.PageSize).Take(model.PageSize).ToList();

            ViewBag.Index = (currPage * model.PageSize);
            ViewBag.TotalItem = numOfSlides;
            ViewBag.PageSize = model.PageSize;
            if (numOfSlides > model.PageSize)
            {
                ViewBag.pages = NTSPRODUCT.Models.Phantrang.PhanTrang(model.PageSize, currPage, numOfSlides, "");
            }
            return PartialView(data);
        }

        [MyAuthorize]
        public ActionResult Delete(string id)
        {
            var data = db.Admins.First(u => u.id.Equals(id));
            {
                try
                {
                    db.Admins.Remove(data);
                    db.SaveChanges();
                    return Json(new { ok = true, mess = "" }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(new { ok = false, mess = ex.Message }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        #region[ham lay thong tin]
        [MyAuthorize]
        public ActionResult GetbyId(string id)
        {
            var data = db.Admins.First(u => u.id.Equals(id));
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region[ham them, sua]
        [MyAuthorize]
        public ActionResult UpdatePass(LoginModel model)
        {
            var user = ClassExten.GetCokies();
            if (user == null)
            {
                return Json(new { ok = 2, mess = "Phiên đăng nhập hết hạn, vui lòng đăng nhập lại để sử dụng tính năng" }, JsonRequestBehavior.AllowGet);
            }
            var data = db.Admins.First(u => u.email.Equals(user.Email));
            var passOld = StringClass.Encrypt(model.Password);
            passOld = StringClass.Encrypt(passOld);
            if (!data.pass.Equals(passOld))
            {
                return Json(new { ok = 0, mess = "Mật khẩu cũ không đúng" }, JsonRequestBehavior.AllowGet);
            }
            try
            {
                var passNew = StringClass.Encrypt(model.PasswordNew);
                passNew = StringClass.Encrypt(passNew);
                data.pass = passNew;
                db.SaveChanges();
                return Json(new { ok = 1, mess = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { ok = 0, mess = "" }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Add(Admin model)
        {
            var check = db.Admins.FirstOrDefault(u => u.email.ToLower().Equals(model.email.ToLower()));
            if (check != null)
            {
                return Json(new { ok = false, mess = "Email này đã tồn tại" }, JsonRequestBehavior.AllowGet);
            }
            try
            {
                var passOld = StringClass.Encrypt("123456");
                passOld = StringClass.Encrypt(passOld);
                Admin data = new Admin();
                data.id = Guid.NewGuid().ToString();
                data.fullName = model.fullName;
                data.addresss = model.addresss;
                data.phone = model.phone;
                data.email = model.email;
                data.active = model.active;
                data.pass = passOld;
                data.subAdmin = false;
                data.dateStart = DateTime.Now;
                data.dateEnd = DateTime.Now;
                data.keyFogot = data.id;
                db.Admins.Add(data);
                db.SaveChanges();
                return Json(new { ok = true, mess = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { ok = false, mess = "" }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Update(Admin model)
        {
            var check = db.Admins.FirstOrDefault(u => !u.id.Equals(model.id) && u.email.ToLower().Equals(model.email.ToLower()));
            if (check != null)
            {
                return Json(new { ok = false, mess = "Email này đã tồn tại" }, JsonRequestBehavior.AllowGet);
            }
            try
            {
                var data = db.Admins.First(u => u.id.Equals(model.id));
                data.fullName = model.fullName;
                data.addresss = model.addresss;
                data.phone = model.phone;
                data.email = model.email;
                data.active = model.active;

                db.SaveChanges();
                return Json(new { ok = true, mess = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { ok = false, mess = "" }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
        public ActionResult ChangeStatus(string id)
        {
            var data = db.Admins.First(u => u.id.Equals(id));
            try
            {
                data.active = !data.active;
                db.SaveChanges();
                return Json(new { ok = true, mess = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { ok = false, mess = "" }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult UpdateOrCreate()
        {
            return PartialView();
        }


        public ActionResult Login()
        {
            if (Request.Cookies["adminNTS"] != null)
            {
                var c = new HttpCookie("adminNTS");
                c.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(c);
            }
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection fr)
        {
            try
            {
                LoginModel loginModel = new LoginModel();
                loginModel.Email = fr["Username"];
                loginModel.Password = fr["password"];
                var pasHas = StringClass.Encrypt(loginModel.Password);
                pasHas = StringClass.Encrypt(pasHas);
                var userInfo = db.Admins.FirstOrDefault(u => u.email.ToLower().Equals(loginModel.Email.ToLower()) && u.pass.Equals(pasHas));
                if (userInfo != null)
                {
                    var securityKey = Guid.NewGuid().ToString();
                    loginModel.SecurityKey = securityKey;
                    var identity = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name, userInfo.email),
                    new Claim(ClaimTypes.Actor, userInfo.fullName),
                    new Claim(ClaimTypes.Uri,string.Empty),
                    new Claim(ClaimTypes.Version, securityKey)}, "ApplicationCookie");
                    var ctx = Request.GetOwinContext();
                    var authManager = ctx.Authentication;
                   // authManager.SignIn(identity);
                    authManager.SignIn(new AuthenticationProperties { IsPersistent = true, ExpiresUtc = DateTimeOffset.UtcNow.AddDays(7) }, identity);
                    loginModel.FullName = userInfo.fullName;
                    loginModel.subAdmin = userInfo.subAdmin.Value;
                    HttpCookie ntsCookies = new HttpCookie("adminNTS");
                    ntsCookies.Value = JsonConvert.SerializeObject(loginModel);
                    ntsCookies.Expires = DateTime.Now.AddDays(7);
                    Response.Cookies.Add(ntsCookies);

                    return Redirect("/Home/Index");
                }
                else
                {
                    ViewBag.erro = "Tài khoản không hợp lệ";
                    return View();
                }

            }
            catch (Exception ex)
            {
                ViewBag.erro = "Xảy ra lỗi vui lòng thử lại";
                return View();
            }
        }
        public ActionResult ChangPass()
        {
            return PartialView();
        }

        public ActionResult Fogot()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Fogot(FormCollection fr)
        {
            try
            {
                var Email = fr["Username"];
                var phone = fr["phone"];
                var userInfo = db.Admins.FirstOrDefault(u => u.email.ToLower().Equals(Email.ToLower()) && u.phone.Equals(phone));
                if (userInfo != null)
                {
                    var securityKey = Guid.NewGuid().ToString();
                    userInfo.keyFogot = securityKey;
                    userInfo.dateStart = DateTime.Now;
                    userInfo.dateEnd = userInfo.dateStart.Value.AddHours(2);
                    db.SaveChanges();
                    ViewBag.erro = "Gửi yêu cầu thành công bạn vui lòng check mail để lấy mật khẩu(yêu cầu sẽ hết hạn trong 2h)";
                    var conf = db.Configs.FirstOrDefault(u => u.conLang.Equals(lang));

                    string title = "Yêu cầu lấy mật khẩu quản trị hệ thống website " + Request.Url.Host;
                    string note = "<p>Yêu cầu lấy mật khẩu quản trị hệ thống website " + Request.Url.Host + " sẽ hết hạn trong 2h kể từ khi gửi yêu cầu</p>";
                    note += "<p>Bạn vui lòng click vào <a href='http://" + Request.Url.Host + "/Admins/FogotChange/" + securityKey + "'> đây </a> để cập nhật mật khẩu mới </p>";
                    var rs = ClassExten.SendMail(userInfo.email, conf.email_Send, conf.emailPass, conf.mail_Port.Value, title, note);
                    if (rs == false)
                    {
                        ViewBag.erro = "Hệ thống gửi mail lỗi vui lòng thử lại sau";
                    }
                    return View();
                }
                else
                {
                    ViewBag.erro = "Tài khoản và số điện thoại không hợp lệ";
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.erro = "Xảy ra lỗi vui lòng thử lại";
                return View();
            }
        }


        public ActionResult FogotChange(string id)
        {
            ViewBag.SecurityKey = id;
            return View();
        }
        public ActionResult UpdateFogotChange(LoginModel model)
        {
            var user = db.Admins.FirstOrDefault(u => u.keyFogot.Equals(model.SecurityKey));
            if (user == null)
            {
                return Json(new { ok = false, mess = "Không tìm thấy yêu cầu lấy lại mật khẩu!" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var timeSp = DateTime.Now - user.dateStart.Value;
                if (timeSp.TotalMinutes >= 120)
                {
                    return Json(new { ok = false, mess = "Yêu cầu lấy lại mật khẩu đã hết hạn!" }, JsonRequestBehavior.AllowGet);
                }
            }
            try
            {
                var passOld = StringClass.Encrypt(model.Password);
                passOld = StringClass.Encrypt(passOld);
                user.pass = passOld;
                db.SaveChanges();
                return Json(new { ok = true, mess = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { ok = false, mess = "" }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}
