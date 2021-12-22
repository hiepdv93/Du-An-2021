using System;
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
                data.map = f["map"];
                data.footer = f["footer"];
                data.liveChat = f["liveChat"];
                data.conTact = f["conTact"];
                data.infoPage = f["infoPage"];

                data.viewProPageHome = int.Parse(f["viewProPageHome"]);
                data.viewProPageList = int.Parse(f["viewProPageList"]);
                data.viewProPageDetail = int.Parse(f["viewProPageDetail"]);

                data.viewNewPageHome = int.Parse(f["viewNewPageHome"]);
                data.viewNewPageList = int.Parse(f["viewNewPageList"]);
                data.viewNewPageDetail = int.Parse(f["viewNewPageDetail"]);

                data.viewLibPageList = int.Parse(f["viewLibPageList"]);

                data.faceBook = f["facebook"];
                data.google = f["google"];
                data.youTube = f["youtube"];
                data.twiter = f["twiter"];

                data.desSeo = f["desseo"];
                data.keySeo = f["keyseo"];
                data.titleSeo = f["titleseo"];
                data.googleAnalytics = f["googleAnalytics"];
                data.googleAdsense = f["googleAdsense"];

                data.introStep1 = f["introStep1"];
                data.introStep2 = f["introStep2"];
                data.introStep3 = f["introStep3"];

                data.introStep1R = f["introStep1R"];
                data.introStep2R = f["introStep2R"];
                data.introStep3R = f["introStep3R"];
                data.fbChat = f["fbChat"];
                data.zaloChat = f["zaloChat"];
                data.copyright = f["copyright"];

                data.lableRow1 = f["lableRow1"];
                data.lableRow2 = f["lableRow2"];
                data.lableRow3 = f["lableRow3"];
                data.lableRow4 = f["lableRow4"];

                var countRow1 = f["countRow1"];
                var countRow2 = f["countRow2"];
                var countRow3 = f["countRow3"];
                var countRow4 = f["countRow4"];

                data.countRow1 = string.IsNullOrEmpty(countRow1) ? 0 : int.Parse(countRow1);
                data.countRow2 = string.IsNullOrEmpty(countRow2) ? 0 : int.Parse(countRow2);
                data.countRow3 = string.IsNullOrEmpty(countRow3) ? 0 : int.Parse(countRow3);
                data.countRow4 = string.IsNullOrEmpty(countRow4) ? 0 : int.Parse(countRow4);

                data.lableIntroName = f["lableIntroName"];
                data.lableIntroDes = f["lableIntroDes"];
                data.lableSolutionName = f["lableSolutionName"];
                data.lableSolutionDes = f["lableSolutionDes"];
                data.lableSayName = f["lableSayName"];
                data.lableSayDes = f["lableSayDes"];
                data.lableWhyName = f["lableWhyName"];
                data.lableWhyDes = f["lableWhyDes"];
                data.lablePartnerName = f["lablePartnerName"];
                data.lablePartnerDes = f["lablePartnerDes"];

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
