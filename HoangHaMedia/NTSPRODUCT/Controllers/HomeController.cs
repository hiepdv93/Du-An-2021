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
            int countAll = db.LichSuTruyCaps.Sum(u => u.countTotal);
            ViewBag.viewAll = countAll;

            var lichsu = db.LichSuTruyCaps.Where(u => u.viewYear == dateNow.Year).ToList();
            ViewBag.viewYear = lichsu.Sum(u => u.countTotal);
            ViewBag.viewMonth = lichsu.Where(u => u.viewMonth == dateNow.Month).Sum(u => u.countTotal);
            //biểu đồ theo tháng
            List<int> lstThang = new List<int>();
            for (int i = 1; i < 13; i++)
            {
                lstThang.Add(lichsu.Where(u => u.viewMonth == i).Sum(u => u.countTotal));
            }
            ViewBag.dataChart = string.Join(",", lstThang);

            //biểu đồ theo ngày
            DateTime dateTemp;
            List<int> lstTuan = new List<int>();
            List<string> lstTitleTuan = new List<string>();
            for (int i = 6; i >= 0; i--)
            {
                dateTemp = dateNow.AddDays(i * -1);
                lstTuan.Add(lichsu.Where(u => u.viewMonth == dateTemp.Month && u.viewDay == dateTemp.Day).Sum(u => u.countTotal));
                lstTitleTuan.Add(dateTemp.ToString("dd/MM"));
            }
            ViewBag.dataChartTuan = string.Join(",", lstTuan);
            ViewBag.dataTitleTuan = string.Join(",", lstTitleTuan);
            try
            {
                ViewBag.htCount = db.Products.Where(u => u.active == true && u.pro_home == true && u.pLang.Equals(ClassExten.HoiTruong)).Count();
                ViewBag.sukienCount = db.Products.Where(u => u.active == true && u.pro_home == true && u.pLang.Equals(ClassExten.SuKien)).Count();
                ViewBag.newsCount = db.News.Where(u => u.newLang.Equals(lang)).Select(u => u.id).Count();
                ViewBag.contactCount = db.Contacts.Select(u => u.id).Count();

                var news = db.News.Where(u => u.newLang.Equals(lang)).OrderByDescending(u => u.viewCount).Take(10).ToList();
                var sukien = db.Products.Where(u => u.active == true && u.pro_home == true && u.pLang.Equals(ClassExten.SuKien)).OrderBy(u => u.proOrder).Take(10).ToList();
                ViewBag.news = news;
                ViewBag.sukien = sukien;
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
    }
}