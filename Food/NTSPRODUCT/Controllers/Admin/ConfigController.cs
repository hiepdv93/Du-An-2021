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
                data.copyRight = f["copyRight"];
                data.hotLine = f["hotLine"];
                data.email_Inbox = f["email_Inbox"];
                data.email_Send = f["email_Send"];
                data.emailPass = f["emailPass"];
                data.logoTop = f["logoTop"];
                data.logoBottom = f["logoBottom"];
                data.linkLogo = f["linkLogo"];
                data.addresss = f["addresss"];

                data.viewProPageHome = int.Parse(f["viewProPageHome"]);
                data.viewProPageList = int.Parse(f["viewProPageList"]);
                data.viewProPageDetail = int.Parse(f["viewProPageDetail"]);

                data.viewNewPageHome = int.Parse(f["viewNewPageHome"]);
                data.viewNewPageList = int.Parse(f["viewNewPageList"]);
                data.viewNewPageDetail = int.Parse(f["viewNewPageDetail"]);

                data.priceShip = int.Parse(f["priceShip"]);
                data.mail_Port = int.Parse(f["mail_Port"]);

                data.faceBook = f["facebook"];
                data.google = f["google"];
                data.youTube = f["youtube"];
                data.twiter = f["twiter"];
                data.tiki = f["tiki"];
                data.shoppe = f["shoppe"];
                data.favicon = f["favicon"];

                data.desSeo = f["desseo"];
                data.keySeo = f["keyseo"];
                data.titleSeo = f["titleseo"];
                data.googleAdsense = f["googleAdsense"];
                data.liveChat = f["liveChat"];
                data.infoPage = f["infoPage"];
                data.homeVideo = f["homeVideo"];
                data.conTact = f["conTact"];

                data.footerExten = f["footerExten"];
                data.footer = f["footer"];
                data.mapBig = f["mapBig"];
                data.mapSmall = f["mapSmall"];
                data.zaloChat = f["zaloChat"];
                data.langDefault = f["langDefault"];
                data.isShowVideoHome = Boolean.Parse(f["isShowVideoHome"]);
                data.typeSendMail = int.Parse(f["typeSendMail"]);

                int viewProBy = 1;
                Int32.TryParse(f["viewProBy"], out viewProBy);
                data.viewProBy = viewProBy;

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
