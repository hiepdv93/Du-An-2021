using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Productgold.Models;

namespace Productgold.Controllers
{
    public class TintucController : Controller
    {
        //
        // GET: /Tintuc/

        WEBTPAEntities db = new WEBTPAEntities();

        #region[danh sach tin tuc]
        public ActionResult Index()
        {
            string page = "1";//so phan trang hien tai
            var pagesize = 20;//so ban ghi tren 1 trang
            var numOfNews = 0;//tong so ban ghi co duoc truoc khi phan trang
            int curpage = 0; // trang hien tai dung cho phan trang
            if (Request["page"] != null)
            {
                page = Request["page"];
                curpage = Convert.ToInt32(page) - 1;
            }

            var all = db.News.Where(u => u.active == true).ToList();
            var data = all.Skip(curpage * pagesize).Take(pagesize).ToList();
            var url = Request.Path;
            numOfNews = all.Count;
            ViewBag.pages = Productgold.Models.Phantrang.PhanTrang(pagesize, curpage, numOfNews, url);
            return View(data);
        }
        #endregion

        #region[chi tiet tin tuc]
        public ActionResult Chitiet(string id)
        {
            var data = db.News.First(u=>u.new_key==id);
            return View(data);
        }
        #endregion


    }
}
