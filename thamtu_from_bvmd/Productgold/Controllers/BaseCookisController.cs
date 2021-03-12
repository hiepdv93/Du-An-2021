using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Productgold.Models;

namespace Productgold.Controllers
{
    public class BaseCookisController : Controller
    {
        WEBTPAEntities db = new WEBTPAEntities();
        public string GetLang()
        {
            string lang = "";
            if (Request.Cookies["langcookis"] == null)
            {
                lang = Createlang();
            }
            else
            {
                HttpCookie htc = Request.Cookies["langcookis"];
                lang = htc.Values["langcode"];
            }
            return lang;
        }
        public string Createlang()
        {
            var langDf = db.Languages.FirstOrDefault(u => u.langDefault == true);
            HttpCookie cook = new HttpCookie("langcookis");
            cook.Values["langcode"] = langDf.langCode;
            cook.Expires = DateTime.Now.AddDays(10);
            Response.Cookies.Add(cook);
            return langDf.langCode;
        }
        public void Changelang(string code)
        {
            var langDf = db.Languages.FirstOrDefault(u => u.langCode == code);
            HttpCookie cook = new HttpCookie("langcookis");
            cook.Values["langcode"] = langDf.langCode;
            cook.Expires = DateTime.Now.AddDays(10);
            Response.Cookies.Add(cook);
        }

        public BaseCookisController()
        {
            ClassExten.CheckAuthen();
        }
     
      

        //protected override void Initialize(RequestContext requestContext)
        //{
        //    var user = ClassExten.GetCokies();
        //    if (user == null)
        //    {
        //        base.Initialize(requestContext);
        //        requestContext.HttpContext.Response.Clear();
        //        requestContext.HttpContext.Response.Redirect(Url.Action("LoginAdmin", "MNGAdmin"));
        //        requestContext.HttpContext.Response.End();
        //    }
        //}

    }

  
}
