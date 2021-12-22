﻿using NTSPRODUCT.Models;
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

                var pro = (from a in db.Products.Where(u => u.pLang.Equals(lang)) select new { Id = a.id, Ptype = a.pType }).ToList();
                ViewBag.proCount = pro.Where(u => u.Ptype.Equals(Constants.Product)).Count();
                ViewBag.deviceCount = pro.Where(u => u.Ptype.Equals(Constants.Device)).Count();
                ViewBag.newsCount = db.News.Where(u => u.newLang.Equals(lang)).Select(u => u.id).Count();
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
        public ActionResult GenKey(string name)
        {
            try
            {
                var rs = StringClass.NameToTag(name.ToLower().Trim()).Replace(System.Environment.NewLine, string.Empty);
                rs = rs.Replace("---", "-");
                rs = rs.Replace("--", "-");
                return Json(new { ok = true, rs = rs }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { ok = false, rs = "" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}