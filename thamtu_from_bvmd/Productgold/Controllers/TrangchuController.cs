using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Productgold.Models;

namespace Productgold.Controllers
{
    public class TrangchuController : Controller
    {
        //
        // GET: /Trangchu/
        WEBTPAEntities db = new WEBTPAEntities();

        #region[trang chu]
        public ActionResult Index()
        {
            List<Product> pronew = db.Products.Where(u => u.active == true && u.pro_new == true).ToList();
            List<Product> prohot = db.Products.Where(u => u.active == true && u.pro_hot == true).ToList();
            List<Product> prosale = db.Products.Where(u => u.active == true && u.pro_sale == true).ToList();
            ViewBag.pronew = Genpro(pronew, "New");
            ViewBag.prohot = Genpro(prohot, "Hot");
            ViewBag.prosale = Genpro(prosale, "Sale");
            return View();
        }
        #endregion

        #region[gen pro]
        public string Genpro(List<Product> pro, string type)
        {
            string chuoi = "";
            if (pro.Count > 5)
            {
                chuoi += " <li>";
                chuoi += " <div class=\"row\">";
                for (int i = 0; i < 5; i++)
                {
                    chuoi += " <div class=\"col-md-3 col-sm-6\">";
                    chuoi += "<div class=\"products\">";
                    //chuoi += " <div class=\"offer\">" + type + "</div>";
                   // chuoi += " <div class=\"thumbnail\"><a href=\"/Sanpham/Chitiet/" + pro[i].pro_key + "\" title=\"" + pro[i].pro_name + "\"><img src=\"" + pro[i].images + "\" alt=\"" + pro[i].pro_name + "\"></a></div>";
                    chuoi += "<div class=\"productname\">" + pro[i].pro_name + "</div>";
                   // chuoi += " <h4 class=\"price\">" + pro[i].price_sale + "</h4>";
                    chuoi += " <div class=\"button_group\">";
                    chuoi += "  <a href=\"/Carts/Addcarts/" + pro[i].pro_key + "\" title=\"" + pro[i].pro_name + "\"><button class=\"button add-cart\" type=\"button\">Mua ngay</button></a>";
                    chuoi += "  <a href=\"/Sanpham/Chitiet/" + pro[i].pro_key + "\" title=\"" + pro[i].pro_name + "\"><button class=\"button add-cart\" type=\"button\">Chi tiết</button></a>";
                    chuoi += "  </div>";
                    chuoi += " </div>";
                    chuoi += "</div>";
                }

                chuoi += "</div>";
                chuoi += "</li>";

                chuoi += " <li>";
                chuoi += " <div class=\"row\">";
                for (int i = 5; i < pro.Count; i++)
                {
                    chuoi += " <div class=\"col-md-3 col-sm-6\">";
                    chuoi += "<div class=\"products\">";
                    //chuoi += " <div class=\"offer\">" + type + "</div>";
                  //  chuoi += " <div class=\"thumbnail\"><a href=\"/Sanpham/Chitiet/" + pro[i].pro_key + "\" title=\"" + pro[i].pro_name + "\"><img src=\"" + pro[i].images + "\" alt=\"" + pro[i].pro_name + "\"></a></div>";
                    chuoi += "<div class=\"productname\">" + pro[i].pro_name + "</div>";
                    //chuoi += " <h4 class=\"price\">" + pro[i].price_sale + "</h4>";
                    chuoi += " <div class=\"button_group\">";
                    chuoi += "  <a href=\"/Carts/Addcarts/" + pro[i].pro_key + "\" title=\"" + pro[i].pro_name + "\"><button class=\"button add-cart\" type=\"button\">Mua ngay</button></a>";
                    chuoi += "  <a href=\"/Sanpham/Chitiet/" + pro[i].pro_key + "\" title=\"" + pro[i].pro_name + "\"><button class=\"button add-cart\" type=\"button\">Chi tiết</button></a>";
                    chuoi += "  </div>";
                    chuoi += " </div>";
                    chuoi += "</div>";
                }
                chuoi += "</div>";
                chuoi += "</li>";
            }
            else
            {
                chuoi += " <li>";
                chuoi += " <div class=\"row\">";
                for (int i = 0; i < pro.Count; i++)
                {
                    chuoi += " <div class=\"col-md-3 col-sm-6\">";
                    chuoi += "<div class=\"products\">";
                    //chuoi += " <div class=\"offer\">" + type + "</div>";
                  //  chuoi += " <div class=\"thumbnail\"><a href=\"/Sanpham/Chitiet/" + pro[i].pro_key + "\" title=\"" + pro[i].pro_name + "\"><img src=\"" + pro[i].images + "\" alt=\"" + pro[i].pro_name + "\"></a></div>";
                    chuoi += "<div class=\"productname\">" + pro[i].pro_name + "</div>";
                   // chuoi += " <h4 class=\"price\">" + pro[i].price_sale + "</h4>";
                    chuoi += " <div class=\"button_group\">";
                    chuoi += "  <a href=\"/Carts/Addcarts/" + pro[i].pro_key + "\" title=\"" + pro[i].pro_name + "\"><button class=\"button add-cart\" type=\"button\">Mua ngay</button></a>";
                    chuoi += "  <a href=\"/Sanpham/Chitiet/" + pro[i].pro_key + "\" title=\"" + pro[i].pro_name + "\"><button class=\"button add-cart\" type=\"button\">Chi tiết</button></a>";
                    chuoi+="  </div>";
                    chuoi += " </div>";
                    chuoi += "</div>";
                }

                chuoi += "</div>";
                chuoi += "</li>";
            }
            return chuoi;
        }
        #endregion

        #region[ham tim kiem]
        [HttpPost]
        public ActionResult Search(FormCollection f)
        { 
            var search= f["search"];
            string urltk = "/Sanpham/Timkiem/"+search;
            return Redirect(urltk);
        }
        #endregion

        #region[ham them mail km]
        public ActionResult Addmail(string email)
        {
            int count = db.emails.Where(u => u.email1 == email).ToList().Count;
            if (count > 0)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            else
            {
                email e = new email();
                e.email1 = email;
                db.emails.Add(e);
                db.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            //return Redirect("/Trangchu");
        }
        #endregion

        #region[ham lien he]
        public ActionResult Lienhe()
        {
            var config = db.Configs.FirstOrDefault();
            ViewBag.map = config.map;
            @ViewBag.email = config.Email_Inbox;
            @ViewBag.phone = config.phone;
            @ViewBag.add = config.addres;
            if (Session["user"] != null)
            {
                string email = Session["user"].ToString();
                var data = db.Customers.First(u => u.email == email);
                return View(email);
            }
            else
            {
                Customer c = new Customer();
                return View(c);
            }
            
        }
        #endregion

        #region[ham lien he]
        [HttpPost]
        public ActionResult Lienhe(FormCollection f)
        {
            Contact con = new Contact();
            con.fullname = f["name"];
            con.email = f["email"];
            con.phone = f["phone"];
            con.title = f["title"];
           // con.content = f["nd"];
            con.createdate = DateTime.Now;
            con.cusid = 0;
            if (Session["user"]!=null)
            {
                string email = Session["user"].ToString();
                var data = db.Customers.First(u => u.email == email).id;
                con.cusid = data;
            }
            db.Contacts.Add(con);
            db.SaveChanges();
            Session["guiok"]="Gửi liên hệ thành công!";
            return Redirect("/Trangchu/Lienhe");
        }
        #endregion
    }
}
