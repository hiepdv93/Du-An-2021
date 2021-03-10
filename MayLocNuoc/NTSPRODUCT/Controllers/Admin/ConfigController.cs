﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NTSPRODUCT.Models;
using System.Net.Mail;
using System.Net;
namespace NTSPRODUCT.Controllers
{
    public class ConfigController : Controller
    {
        //
        // GET: /Config/
        NTSWEBEntities db = new NTSWEBEntities();
        string lang = ClassExten.GetLang();
        #region[view]
        [MyAuthorize]
        public ActionResult Index()
        {
            var data = db.Configs.FirstOrDefault(u => u.conLang == lang);
            return View(data);
        }
        #endregion

        #region[cap nhat]
        [HttpPost]
        [ValidateInput(false)]
        [MyAuthorize]
        public ActionResult Index(FormCollection f)
        {
            try
            {
                var idupdate = f["idupdate"];
                var data = db.Configs.First(u => u.id == idupdate);

                data.shopName = f["shopname"];
                data.phone = f["phone"];
                data.hotLine = f["hotLine"];
                data.homeVideo = f["homeVideo"];
                data.email_Inbox = f["emailn"];
                data.email_Send = f["emailg"];
                data.emailPass = f["pass"];
                data.mail_Port = int.Parse(f["cong"]);
                data.logoTop = f["logoTop"];
                data.logoBottom = f["logoBottom"];
                data.linkLogo = f["link"];
                data.addresss = f["addres"];
                data.mapBig = f["mapBig"];
                data.mapSmall = f["mapSmall"];
                data.footer = f["footer"];
                data.liveChat = f["liveChat"];
                data.conTact = f["conTact"];
                data.infoPage = f["infoPage"];
                data.copyright = f["copyright"];
                
                data.viewProPageHome = int.Parse(f["viewProPageHome"]);
                data.viewProPageList = int.Parse(f["viewProPageList"]);
                data.viewProPageDetail = int.Parse(f["viewProPageDetail"]);

                data.viewNewPageHome = int.Parse(f["viewNewPageHome"]);
                data.viewNewPageList = int.Parse(f["viewNewPageList"]);
                data.viewNewPageDetail = int.Parse(f["viewNewPageDetail"]);


                data.faceBook = f["facebook"];
                data.google = f["google"];
                data.youTube = f["youtube"];
                data.twiter = f["twiter"];

                data.desSeo = f["desseo"];
                data.keySeo = f["keyseo"];
                data.titleSeo = f["titleseo"];
                data.googleAnalytics = f["googleAnalytics"];
              
                data.zalo = f["zalo"];
                data.tiki = f["tiki"];
                data.lazada = f["lazada"];
                data.shopee = f["shopee"];
                data.priceShip =int.Parse(f["priceShip"]);

                db.SaveChanges();
                ConfigModel.listConfig = null;
                Session["ok"] = "Cập nhật thành công!";
                return Redirect("/config/Index");

            }
            catch (Exception)
            {
                Session["erro"] = "Đã xảy ra lỗi vui lòng thử lại!";
                return Redirect("/config/Index");
            }
        }
        #endregion



        #region 
        public string GetLang()
        {
            return Request.Cookies["cookie"].ToString();
        }

        #endregion


        public ActionResult ChangeLangAdmin(string lang)
        {
            ClassExten.Changelang(lang);
            return Json(JsonRequestBehavior.AllowGet, "ok");
        }

        public ActionResult ChangeLangSite(string lang)
        {
            ClassExten.ChangelangSite(lang);
            return Json(JsonRequestBehavior.AllowGet, "ok");
        }
    }
}