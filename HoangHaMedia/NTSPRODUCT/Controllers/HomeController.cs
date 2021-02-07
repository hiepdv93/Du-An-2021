using NTSPRODUCT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NTSPRODUCT.Controllers
{
    public class HomeController : Controller
    {
        NTSWEBEntities db = new NTSWEBEntities();
        string lang = ClassExten.GetLang();
        public ActionResult Index()
        {
            try
            {
                ViewBag.proCount = db.Products.Where(u => u.pLang.Equals(lang) ).Select(u => u.id).Count();
                ViewBag.newsCount = db.News.Where(u => u.newLang.Equals(lang)).Select(u => u.id).Count();
                ViewBag.teacherCount = db.Categorys.Where(u => u.cateLang.Equals(lang) && u.cateType==ClassExten.typeProduct).Select(u => u.id).Count();
                ViewBag.contactCount = db.Contacts.Select(u => u.id).Count();

            }
            catch (Exception)
            { }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}