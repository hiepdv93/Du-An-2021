using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NTSPRODUCT.Models;

namespace NTSPRODUCT.Controllers
{
    public  class BaseCookisController : Controller
    {
        NTSWEBEntities db = new NTSWEBEntities();
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
            HttpCookie cook = new HttpCookie("langcookis");
            cook.Values["langcode"] = "vi";
            cook.Expires = DateTime.Now.AddDays(10);
            Response.Cookies.Add(cook);
            return "vi";
        }
        public void Changelang(string code)
        {
            HttpCookie cook = new HttpCookie("langcookis");
            cook.Values["langcode"] = "vi";
            cook.Expires = DateTime.Now.AddDays(10);
            Response.Cookies.Add(cook);
        }

       
    }
}
