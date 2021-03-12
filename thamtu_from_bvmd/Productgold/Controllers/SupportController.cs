using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Productgold.Models;

namespace Productgold.Controllers
{
    public class SupportController : BaseCookisController
    {
        //
        // GET: /Support/
        WEBTPAEntities db = new WEBTPAEntities();
        string lang = ClassExten.GetLang();
        string siteTPA = ClassExten.siteOption;
        #region[ham view]
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
            var all = db.Supports.Where(u=>u.sLang==lang && u.siteTPA==siteTPA).OrderBy(u=>u.nick).ToList();
            var data = all.Skip(curpage * pagesize).Take(pagesize).ToList();
            var url = Request.Path;
            numOfNews = all.Count;
            ViewBag.pages = Productgold.Models.Phantrang.PhanTrang(pagesize, curpage, numOfNews, url);
            return View(data);
        }
        #endregion

        #region[ham sua]
        public ActionResult Getbyid(int id)
        {
            var data = db.Supports.First(u=>u.id==id);
            return Json(data,JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region[ham delete]
        public ActionResult Delete(int id)
        {
            try
            {
                var data = db.Supports.First(u=>u.id==id);
                db.Supports.Remove(data);
                db.SaveChanges();
                Session["ok"] = "Xóa thành công!";
                 return Redirect("/Support");
            }
            catch (Exception)
            {
                
                return Redirect("/MNGAdmin/P404");
            }
        }
        #endregion

        #region[ham them, sua]
        [HttpPost]
        public ActionResult Update(FormCollection f)
        {
            try
            {
                var idupdate = Convert.ToInt32(f["idupdate"]);
                var name = f["name"];
                var phone = f["phone"];
                var nick = f["nick"];
                var active = f["active"];
                var ac = false;
                if (active=="1")
                {
                    ac = true;
                }
                if (idupdate == -1)
                {
                    Support data = new Support();
                    data.nick = nick;
                    data.fullname = name;
                    data.phone = phone;
                    data.stype = ac;
                    data.sLang = lang;
                    data.siteTPA = siteTPA;
                    db.Supports.Add(data);
                    db.SaveChanges();
                    Session["ok"] = "Thêm mới thành công!";
                }
                else
                {
                    var data = db.Supports.First(u=>u.id==idupdate);
                    data.nick = nick;
                    data.fullname = name;
                    data.phone = phone;
                    data.stype = ac;
                    db.SaveChanges();
                    Session["ok"] = "Cập nhật thành công!";
                }

                return Redirect("/Support");
            }
            catch (Exception)
            {

                return Redirect("/MNGAdmin/P404");
            }
        }
        #endregion

    }
}
