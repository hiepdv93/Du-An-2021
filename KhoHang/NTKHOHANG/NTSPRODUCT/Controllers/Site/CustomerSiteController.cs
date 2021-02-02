using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NTSPRODUCT.Models;
namespace NTSPRODUCT.Controllers.Site
{
    public class CustomerSiteController : Controller
    {
        NTSWEBEntities db = new NTSWEBEntities();
        string lang = ClassExten.GetLangSite();
        // GET: CustomerSite
        [HttpGet]
        public ActionResult Login()
        {
            ViewBag.lang = lang;
            Config conf;
            if (ConfigModel.listConfig == null)
            {
                ConfigModel.listConfig = db.Configs.ToList();
            }
            conf = ConfigModel.listConfig.FirstOrDefault(u => u.conLang.Equals(lang));
            #region[load seo]
            ViewBag.title = conf.titleSeo;
            ViewBag.description = conf.desSeo;
            ViewBag.keywords = conf.keySeo;
            ViewBag.url = HttpContext.Request.Url.AbsoluteUri;
            ViewBag.img = ClassExten.GetUrlHost() + conf.logoTop;
            #endregion

            return View(conf);
        }

        [HttpGet]
        public ActionResult Register()
        {
            ViewBag.lang = lang;
            Config conf;
            if (ConfigModel.listConfig == null)
            {
                ConfigModel.listConfig = db.Configs.ToList();
            }
            conf = ConfigModel.listConfig.FirstOrDefault(u => u.conLang.Equals(lang));
            #region[load seo]
            ViewBag.title = conf.titleSeo;
            ViewBag.description = conf.desSeo;
            ViewBag.keywords = conf.keySeo;
            ViewBag.url = HttpContext.Request.Url.AbsoluteUri;
            ViewBag.img = ClassExten.GetUrlHost() + conf.logoTop;
            #endregion


            return View(conf);
        }


        [HttpPost]
        public ActionResult LoginUser(CustomerExten cus)
        {
            try
            {
                if (Session["Loginkey"] == null)
                {
                    throw new Exception("Mã bảo mật đã hết hạn, vui lòng tải lại trang");
                }

                string passLevel2 = ClassExten.ProcessPass(cus.pass);

                string Loginkey = Session["Loginkey"].ToString();
                if (!Loginkey.Equals(cus.CodeLogin.ToLower()))
                {
                    throw new Exception("Mã bảo mật không đúng xin thử lại");
                }
                var cusCheck = db.Customers.FirstOrDefault(u => u.email.ToLower().Equals(cus.email.ToLower()) && u.pass.Equals(passLevel2));
                if (cusCheck == null)
                {
                    throw new Exception("Tài khoản và mật khẩu không hợp lệ");
                }
                if (cusCheck.activeAcount == false)
                {
                    throw new Exception("Tài khoản đã bị khóa vui lòng liên hệ quản trị");
                }

                Session["KhohangUser"] = cus.email;
                Session["Loginkey"] = null;
                Session.Timeout = 60;
                return Json(new { ok = true, mess = "" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { ok = false, mess = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        private void ValidateUser(CustomerExten cus)
        {
            cus.parentId = null;

            if (Session["Registerkey"] == null)
            {
                throw new Exception("Mã bảo mật đã hết hạn, vui lòng tải lại trang");
            }

            string Registerkey = Session["Registerkey"].ToString();
            if (!Registerkey.Equals(cus.CodeLogin.ToLower()))
            {
                throw new Exception("Mã bảo mật không đúng xin thử lại");
            }

            if (string.IsNullOrEmpty(cus.CodeIntro))
            {
                throw new Exception("Mã giới thiệu không được trống");
            }
            Config conf;
            if (ConfigModel.listConfig == null)
            {
                ConfigModel.listConfig = db.Configs.ToList();
            }
            conf = ConfigModel.listConfig.FirstOrDefault(u => u.conLang.Equals(lang));

            string gui = Guid.NewGuid().ToString();

            if (conf.referralCode.ToLower().Equals(cus.CodeIntro.ToLower()))
            {
                cus.parentId = ClassExten.cateParent;
                cus.levelAcount = 1;
                cus.referralCode = "P" + cus.levelAcount + DateTime.Now.ToString("yyMMddHHmmss") + gui.Substring(0, 3);
            }
            if (string.IsNullOrEmpty(cus.parentId))
            {
                var cusCheck = db.Customers.FirstOrDefault(u => u.activeCode == true && u.activeAcount == true
                                                               && u.referralCode.ToLower().Equals(cus.CodeIntro.ToLower()));
                if (cusCheck != null)
                {
                    int? level = cusCheck.levelAcount + 1;
                    cus.levelAcount = level;
                    cus.parentId = cusCheck.id;
                    cus.referralCode = "C" + level + DateTime.Now.ToString("yyMMddHHmmss") + gui.Substring(0, 3);
                }
            }
            if (string.IsNullOrEmpty(cus.parentId))
            {
                throw new Exception("Mã giới thiệu không hợp lệ");
            }

            var cusCheckEmail = db.Customers.FirstOrDefault(u => u.email.ToLower().Equals(cus.email.ToLower()));
            if (cusCheckEmail != null)
            {
                throw new Exception("Email đã tồn tại trong hệ thống");
            }
        }

        [HttpPost]
        public ActionResult RegisterUser(CustomerExten cus)
        {
            try
            {
                ValidateUser(cus);

                string passLevel2 = ClassExten.ProcessPass(cus.pass);

                var data = new Customer();
                data.id = Guid.NewGuid().ToString();
                data.fullName = cus.fullName;
                data.email = cus.email.ToLower();
                data.phone = cus.phone;
                data.addresss = cus.addresss;
                data.parentId = cus.parentId;
                data.levelAcount = cus.levelAcount;
                data.pass = passLevel2;
                data.amount = 0;
                data.activeCode = false;
                data.activeAcount = true;
                data.createDate = DateTime.Now;
                data.referralCode = cus.referralCode.ToUpper();
                data.type = 1;
                db.Customers.Add(data);
                db.SaveChanges();


                Session["KhohangUser"] = cus.email;
                Session["Registerkey"] = null;
                Session.Timeout = 60;

                return Json(new { ok = true, mess = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { ok = false, mess = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult RenderKeyReg()
        {
            //sinh mã
            Random rd = new Random();
            string Registerkey = rd.Next(100, 999) + StringClass.RandomString(3);
            Session["Registerkey"] = Registerkey.ToLower();
            Session.Timeout = 20;
            ViewBag.Registerkey = Registerkey.ToLower();
            return PartialView();
        }

        public ActionResult RenderKeyLogin()
        {
            //sinh mã
            //sinh mã
            Random rd = new Random();
            string Loginkey = rd.Next(100, 999) + StringClass.RandomString(3);
            Session["Loginkey"] = Loginkey.ToLower();
            ViewBag.Loginkey = Loginkey.ToLower();
            Session.Timeout = 20;
            ViewBag.Loginkey = Loginkey.ToLower();
            return PartialView();
        }
    }
}