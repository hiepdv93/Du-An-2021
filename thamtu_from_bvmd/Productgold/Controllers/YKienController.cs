using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Productgold.Models;

namespace Productgold.Controllers
{

    public class YKienController : Controller
    {
        //
        // GET: /YKien/

        WEBTPAEntities db = new WEBTPAEntities();
        string lang = ClassExten.GetLang();
        string siteTPA = ClassExten.siteOption;
        public ActionResult Index()
        {
            string titleSearch = "";
            string searchemail = "";
            if (Request["titleSearch"] != null)
            {
                titleSearch = Request["titleSearch"];
            }
            if (Request["searchemail"] != null)
            {
                searchemail = Request["searchemail"];

            }

            string page = "1";//so phan trang hien tai
            var pagesize = 20;//so ban ghi tren 1 trang
            var numOfNews = 0;//tong so ban ghi co duoc truoc khi phan trang
            int curpage = 0; // trang hien tai dung cho phan trang
            if (Request["page"] != null)
            {
                page = Request["page"];
                curpage = Convert.ToInt32(page) - 1;
            }
            var all = db.Ykiens.Where(u =>
               (string.IsNullOrEmpty(titleSearch) || u.yName.ToLower().Contains(titleSearch.ToLower()))
              && (string.IsNullOrEmpty(searchemail) || u.yPhone.ToLower().Contains(titleSearch.ToLower()))
              && u.pId == -1
            ).OrderByDescending(u => u.yCreateDate).ToList();
            var data = all.Skip(curpage * pagesize).Take(pagesize).ToList();
            var url = Request.Path;
            numOfNews = all.Count;
            ViewBag.pages = Productgold.Models.Phantrang.PhanTrang(pagesize, curpage, numOfNews, url);
            return View(data);

        }

        public ActionResult IndexSearch(FormCollection fr)
        {
            var titleSearch = fr["titleSearch"];
            var searchemail = fr["searchemail"];
            return Redirect("/Ykien/Index?titleSearch=" + titleSearch + "&searchemail=" + searchemail);
        }
        public ActionResult Delete(int id)
        {
            var ykien = db.Ykiens.Where(m => m.yId == id).FirstOrDefault();
            if (ykien != null)
            {
                var list = db.Ykiens.Where(u => u.pId == ykien.yId);
                if (list.Count() > 0)
                {
                    db.Ykiens.RemoveRange(list);
                }
                db.Ykiens.Remove(ykien);
                db.SaveChanges();
                Session["ok"] = "Xóa thành công!";
                return Redirect("/YKien/");
            }
            else
            {
                return Redirect("/MNGAdmin/P404");
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(FormCollection f)
        {
            try
            {
                int id = Convert.ToInt32(f["idEdit"]);
                int order = Convert.ToInt32(f["order"]);
                var urlCallback = f["url"];
                var customer = f["customer"];
                var name = f["name"];
                var email = f["email"];
                var img = f["img"];
                var phone = f["phone"];
                var product = f["product"];
                var active = f["active"];
                var note = f["note"];

                Ykien ykien;
                if (id == -1)
                {
                    ykien = new Ykien();
                }
                else
                {
                    ykien = db.Ykiens.First(m => m.yId == id);
                }
                if (customer != null)
                    //  ykien.cusId = Convert.ToInt32(customer);
                    ykien.yName = name;
                //ykien.yEmail = email;
                // ykien.yImg = img;
                ykien.yPhone = phone;
                // ykien.yLang = lang;
                // ykien.siteTPA = siteTPA;
                // ykien.yOrder = order;
                // if (product != null)
                //    ykien.yPro = Convert.ToInt32(product);
                ykien.yActive = active.Equals("1");
                ykien.ynote = note;
                if (id == -1)
                {
                    db.Ykiens.Add(ykien);
                    Session["ok"] = "Thêm thành công!";
                }
                else
                {
                    Session["ok"] = "Sửa thành công!";
                }

                db.SaveChanges();

                return Redirect(urlCallback);
            }
            catch (Exception)
            {
                Session["ok"] = "Thêm không thành công!";
                return Redirect("/P404");
            }
        }

        [HttpPost]
        public ActionResult GetCustomer()
        {
            var customer = db.Customers.Where(m => m.type == -1).Select(m => new { id = m.id, name = m.email });
            return Json(customer, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GetCategory()
        {
            var data = db.Categorys.Select(m => new { id = m.id, name = m.name });
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GetProductByCatId(int catId)
        {
            var data = db.Products.Where(m => m.cateid == catId).Select(m => new { id = m.id, name = m.pro_name });
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetEditById(int id)
        {
            Ykien ykien = db.Ykiens.Where(m => m.yId == id).FirstOrDefault();
            //cusId
            int catId = -1;

            return Json(new { catid = catId, y = ykien }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Chitiet(int id)
        {
            List<Ykien> list = new List<Ykien>();
            var data = db.Ykiens.FirstOrDefault(u => u.yId == id);
            if (data != null)
            {
                list = db.Ykiens.Where(u => u.pId == id).OrderBy(u => u.yCreateDate).ToList();
            }
            else
            {
                data = new Ykien();
            }

            ViewBag.data = data;

            return View(list);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Traloi(FormCollection fr)
        {
            string dataObj_Id = fr["dataObj_Id"];

            try
            {
                string dataObj_Traloi_Id = fr["dataObj_Traloi_Id"];
                string hoten_ch = fr["hoten_ch"];
                string email_diachi_ch = fr["email_diachi_ch"];
                string noidung_ch = fr["noidung_ch"];

                if (string.IsNullOrEmpty(dataObj_Traloi_Id))
                {
                    Ykien yk = new Ykien();
                    yk.pId = int.Parse(dataObj_Id);
                    yk.yCreateDate = DateTime.Now;
                    yk.yEmailAdd = email_diachi_ch;
                    yk.ynote = noidung_ch;
                    yk.yName = hoten_ch;
                    yk.AdminId = Session["admins"].ToString();
                    db.Ykiens.Add(yk);
                }
                else
                {
                    int id = int.Parse(dataObj_Traloi_Id);
                    var yk = db.Ykiens.FirstOrDefault(u => u.yId == id);
                    if (yk != null)
                    {
                        yk.yEmailAdd = email_diachi_ch;
                        yk.ynote = noidung_ch;
                        yk.yName = hoten_ch;
                    }
                }
                db.SaveChanges();

            }
            catch (Exception)
            { }
            return Redirect("/Ykien/Chitiet/" + dataObj_Id);
        }


        public ActionResult GetAdmin()
        {
            string adminEmail = Session["admins"].ToString();
            var admin = db.Admins.FirstOrDefault(u => u.email.Equals(adminEmail));
            return Json(admin, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetbyId(int id)
        {
            var data = db.Ykiens.FirstOrDefault(u => u.yId == id);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult TraloiSite(FormCollection fr)
        {
            string dataObj_Id = fr["dataObj_Id"];
            try
            {
                string hoten_ch = fr["hoten_ch"];
                string email_diachi_ch = fr["email_diachi_ch"];
                string noidung_ch = fr["noidung_ch"];

                Ykien yk = new Ykien();
                yk.pId = int.Parse(dataObj_Id);
                yk.yCreateDate = DateTime.Now;
                yk.yEmailAdd = email_diachi_ch;
                yk.ynote = noidung_ch;
                yk.yName = hoten_ch;

                if (Session["admins"] != null)
                {
                    yk.AdminId = Session["admins"].ToString();
                }
                else
                {
                    yk.AdminId = ClassExten.NotAdmin;
                }
                db.Ykiens.Add(yk);

                db.SaveChanges();

            }
            catch (Exception)
            { }
            return Redirect("/Ykien/Chitiet/" + dataObj_Id);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult HoidapSite(FormCollection fr)
        {
            try
            {
                string hoten_ch = fr["hoten_ch"];
                string email_diachi_ch = fr["email_diachi_ch"];
                string noidung_ch = fr["noidung_ch"];

                Ykien yk = new Ykien();
                yk.pId = -1;
                yk.yCreateDate = DateTime.Now;
                yk.yEmailAdd = email_diachi_ch;
                yk.ynote = noidung_ch;
                yk.yName = hoten_ch;
                yk.AdminId = ClassExten.NotAdmin;
                db.Ykiens.Add(yk);
                db.SaveChanges();
            }
            catch (Exception)
            { }
            return Redirect("/hoi-dap");
        }

    }
}
