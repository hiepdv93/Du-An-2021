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
            var dateNow = DateTime.Now;
            var lichsu = db.LichSuTruyCaps.Where(u => u.viewMonth == dateNow.Month && u.viewYear == dateNow.Year).ToList();
            try
            {
                ViewBag.proCount = db.Products.Where(u => u.pLang.Equals(lang)).Select(u => u.id).Count();
                ViewBag.newsCount = db.News.Where(u => u.newLang.Equals(lang)).Select(u => u.id).Count();
                ViewBag.teacherCount = db.Oders.Select(u => u.id).Count();
                ViewBag.contactCount = db.Contacts.Select(u => u.id).Count();

            }
            catch (Exception)
            { }
            return View(lichsu);
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

        public ActionResult Chart()
        {
            return View();
        }
    }
}