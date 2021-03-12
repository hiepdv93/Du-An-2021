using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Productgold.Models;
namespace Productgold.Controllers
{
    public class SanphamController : Controller
    {
        //
        // GET: /Sanpham/

        WEBTPAEntities db = new WEBTPAEntities();

        #region[trang san pham]
        public ActionResult Index(string id)
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

            List<Product> all;
            string sapxep = "";
            if (Request["order"] != null)
            {
                sapxep = Request["order"].ToString();
            }
            if (sapxep == "name")
            {
                all = db.Products.Where(u => u.active == true).OrderBy(u => u.pro_name).ToList();

            }
            else if (sapxep == "price")
            {
                all = db.Products.Where(u => u.active == true).ToList();

            }
            else
            {
                all = db.Products.Where(u => u.active == true).OrderByDescending(u => u.id).ToList();

            }
            if (id!=null)
            {
                if (id.Equals("hot"))
                {
                    all = all.Where(u=>u.pro_hot==true).ToList();
                }
                else if (id.Equals("new"))
                {
                    all = all.Where(u => u.pro_new == true).ToList();
                }
                else if (id.Equals("sale"))
                {
                    all = all.Where(u => u.pro_sale == true).ToList();
                }
              
            }
            var data = all.Skip(curpage * pagesize).Take(pagesize).ToList();
            var url = HttpContext.Request.Url.AbsoluteUri;
            numOfNews = all.Count;
            ViewBag.pages = Productgold.Models.Phantrang.PhanTrang(pagesize, curpage, numOfNews, url);
            return View(data);
        }
        #endregion

        #region[Chitiet]
        public ActionResult Chitiet(string id)
        {
            string chuoianh = ""; string tinhnang = ""; string chuoi = "";
            try
            {
                var data = db.Products.Where(u => u.pro_key == id).FirstOrDefault();
                Session["Proid"] = data;

                #region[san pham lien quan]
                int idnhom = Convert.ToInt32(data.cateid);
                var pro = db.Products.Where(u => u.cateid == idnhom && u.active == true).Take(6).ToList();
                if (pro.Count > 3)
                {
                    chuoi += "<li>";
                    chuoi += " <div class=\"row\">";
                    for (int i = 0; i < 3; i++)
                    {
                        chuoi += "<div class=\"col-md-4 col-sm-4\">";
                        chuoi += "<div class=\"products\">";
                        //chuoi += " <div class=\"offer\">" + type + "</div>";
                       // chuoi += " <div class=\"thumbnail\"><a href=\"/Sanpham/Chitiet/" + pro[i].pro_key + "\" title=\"" + pro[i].pro_name + "\"><img src=\"" + pro[i].images + "\" alt=\"" + pro[i].pro_name + "\"></a></div>";
                        chuoi += "<div class=\"productname\">" + pro[i].pro_name + "</div>";
                       // chuoi += " <h4 class=\"price\">" + pro[i].price_sale + "</h4>";
                        chuoi += " <div class=\"button_group\">";
                        chuoi += "  <a href=\"/Sanpham/Chitiet/" + pro[i].pro_key + "\" title=\"" + pro[i].pro_name + "\"><button class=\"button add-cart\" type=\"button\">Mua ngay</button></a>";
                        chuoi += "  <a href=\"/Sanpham/Chitiet/" + pro[i].pro_key + "\" title=\"" + pro[i].pro_name + "\"><button class=\"button add-cart\" type=\"button\">Chi tiết</button></a>";
                        chuoi += "  </div>";
                        chuoi += " </div>";
                        chuoi += "</div>";
                    }
                    chuoi += "</div>";
                    chuoi += "</li>";

                    chuoi += "<li>";
                    chuoi += " <div class=\"row\">";
                    for (int i = 3; i < pro.Count; i++)
                    {
                        chuoi += "<div class=\"col-md-4 col-sm-4\">";
                        chuoi += "<div class=\"products\">";
                        //chuoi += " <div class=\"offer\">" + type + "</div>";
                      //  chuoi += " <div class=\"thumbnail\"><a href=\"/Sanpham/Chitiet/" + pro[i].pro_key + "\" title=\"" + pro[i].pro_name + "\"><img src=\"" + pro[i].images + "\" alt=\"" + pro[i].pro_name + "\"></a></div>";
                        chuoi += "<div class=\"productname\">" + pro[i].pro_name + "</div>";
                       // chuoi += " <h4 class=\"price\">" + pro[i].price_sale + "</h4>";
                        chuoi += " <div class=\"button_group\">";
                        chuoi += "  <a href=\"/Sanpham/Chitiet/" + pro[i].pro_key + "\" title=\"" + pro[i].pro_name + "\"><button class=\"button add-cart\" type=\"button\">Mua ngay</button></a>";
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
                    chuoi += "<li>";
                    chuoi += " <div class=\"row\">";
                    for (int i = 0; i < pro.Count; i++)
                    {
                        chuoi += "<div class=\"col-md-4 col-sm-4\">";
                        chuoi += "<div class=\"products\">";
                        //chuoi += " <div class=\"offer\">" + type + "</div>";
                        //chuoi += " <div class=\"thumbnail\"><a href=\"/Sanpham/Chitiet/" + pro[i].pro_key + "\" title=\"" + pro[i].pro_name + "\"><img src=\"" + pro[i].images + "\" alt=\"" + pro[i].pro_name + "\"></a></div>";
                        chuoi += "<div class=\"productname\">" + pro[i].pro_name + "</div>";
                       // chuoi += " <h4 class=\"price\">" + pro[i].price_sale + "</h4>";
                        chuoi += " <div class=\"button_group\">";
                        chuoi += "  <a href=\"/Sanpham/Chitiet/" + pro[i].pro_key + "\" title=\"" + pro[i].pro_name + "\"><button class=\"button add-cart\" type=\"button\">Mua ngay</button></a>";
                        chuoi += "  <a href=\"/Sanpham/Chitiet/" + pro[i].pro_key + "\" title=\"" + pro[i].pro_name + "\"><button class=\"button add-cart\" type=\"button\">Chi tiết</button></a>";
                        chuoi += "  </div>";
                        chuoi += " </div>";
                        chuoi += "</div>";
                    }
                    chuoi += "</div>";
                    chuoi += "</li>";
                }
                #endregion
                ViewBag.prolq = chuoi;
                #region[lay thong tin lien quan]
                var idsp = data.id;
               // var anh = db.Images.Where(u => u.pro_id == idsp).ToList();
        
              
                #endregion

                #region[top san pham hot]
                List<Product> pronew = db.Products.Where(u => u.active == true && u.pro_new == true).Take(4).ToList();
                List<Product> prohot = db.Products.Where(u => u.active == true && u.pro_hot == true).Take(4).ToList();
                List<Product> prosale = db.Products.Where(u => u.active == true && u.pro_sale == true).Take(4).ToList();
               ViewBag.pnew = GenproTop(pronew);
               ViewBag.phot = GenproTop(prohot);
               ViewBag.psale = GenproTop(prosale);

                #endregion

                return View(data);
            }
            catch (Exception)
            {


                return Redirect("/MNGAdmin/P404");
            }

        }
        #endregion

        #region[san pham theo nhom]
        public ActionResult Nhom(string id)
        {
            ViewBag.tieude = db.Categorys.First(u => u.keys == id).name;

            string page = "1";//so phan trang hien tai
            var pagesize = 20;//so ban ghi tren 1 trang
            var numOfNews = 0;//tong so ban ghi co duoc truoc khi phan trang
            int curpage = 0; // trang hien tai dung cho phan trang
            if (Request["page"] != null)
            {
                page = Request["page"];
                curpage = Convert.ToInt32(page) - 1;
            }

            List<Product> all;
            string sapxep = "";
            if (Request["order"] != null)
            {
                sapxep = Request["order"].ToString();
            }
            if (sapxep == "name")
            {
                all = db.Products.Where(u => u.active == true).OrderByDescending(u => u.pro_name).OrderBy(u => u.pro_name).ToList();

            }
            else if (sapxep == "price")
            {
                all = (from p in db.Products join c in db.Categorys on p.cateid equals c.id where (c.keys == id && p.active == true) select p).ToList();

            }
            else
            {
                all = (from p in db.Products join c in db.Categorys on p.cateid equals c.id where (c.keys == id && p.active == true) select p).OrderByDescending(u => u.id).ToList();

            }
            var data = all.Skip(curpage * pagesize).Take(pagesize).ToList();
            var url = Request.Path;
            numOfNews = all.Count;
            url = Request.RawUrl; ;
            numOfNews = 50;
            ViewBag.pages = Productgold.Models.Phantrang.PhanTrang(pagesize, curpage, numOfNews, url);
            return View(data);
        }
        #endregion

        #region[san pham theo thương hieu]
        public ActionResult Thuonghieu(int id)
        {
            ViewBag.tieude = db.Brands.First(u => u.id == id).name;
            string page = "1";//so phan trang hien tai
            var pagesize = 20;//so ban ghi tren 1 trang
            var numOfNews = 0;//tong so ban ghi co duoc truoc khi phan trang
            int curpage = 0; // trang hien tai dung cho phan trang
            if (Request["page"] != null)
            {
                page = Request["page"];
                curpage = Convert.ToInt32(page) - 1;
            }

            List<Product> all;
            string sapxep = "";
            if (Request["order"] != null)
            {
                sapxep = Request["order"].ToString();
            }
            if (sapxep == "name")
            {
                all = (from p in db.Products join t in db.Brands on p.brandid equals t.id where (t.id == id && p.active == true) select p).OrderBy(u => u.pro_name).ToList();

            }
            else if (sapxep == "price")
            {
                all = (from p in db.Products join t in db.Brands on p.brandid equals t.id where (t.id == id && p.active == true) select p).ToList();

            }
            else
            {
                all = (from p in db.Products join t in db.Brands on p.brandid equals t.id where (t.id == id && p.active == true) select p).OrderByDescending(u => u.id).ToList();

            }
            var data = all.Skip(curpage * pagesize).Take(pagesize).ToList();
            var url = Request.Path;
            numOfNews = all.Count;
            ViewBag.pages = Productgold.Models.Phantrang.PhanTrang(pagesize, curpage, numOfNews, url);
            return View(data);
        }
        #endregion

        #region[trang tim kiem]
        public ActionResult Timkiem(string id)
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
            List<Product> all ;
           
            string sapxep = "";
            if (Request["order"] != null)
            {
                sapxep = Request["order"].ToString();
            }
             if (Request["type"]!=null)
            {
                string[] mang = id.Split('-').ToArray();
                float nho = float.Parse(mang[0]);
                float to = float.Parse(mang[1]);
                if (sapxep == "name")
                {
                    all = db.Products.Where(u => u.active == true ).OrderBy(u => u.pro_name).ToList();

                }
                else if (sapxep == "price")
                {
                    all = db.Products.Where(u => u.active == true).ToList();

                }
                else
                {
                    all = db.Products.Where(u => u.active == true ).OrderByDescending(u => u.id).ToList();

                }
            }else
                 {
                     if (sapxep == "name")
                     {
                         all = db.Products.Where(u => u.active == true && u.pro_name.ToLower().Contains(id.ToLower())).OrderBy(u => u.pro_name).ToList();

                     }
                     else if (sapxep == "price")
                     {
                         all = db.Products.Where(u => u.active == true && u.pro_name.ToLower().Contains(id.ToLower())).ToList();

                     }
                     else
                     {
                         all = db.Products.Where(u => u.active == true && u.pro_name.ToLower().Contains(id.ToLower())).OrderByDescending(u => u.id).ToList();

                     }
                 }
        
            var data = all.Skip(curpage * pagesize).Take(pagesize).ToList();
            var url = HttpContext.Request.Url.AbsoluteUri;
            numOfNews = all.Count;
            ViewBag.pages = Productgold.Models.Phantrang.PhanTrang(pagesize, curpage, numOfNews, url);
            return View(data);
            
        }
        #endregion

        #region[ham gen pro]
        public string GenproTop(List<Product> p)
        {
            string chuoi = "";

            for (int i = 0; i < p.Count; i++)
            {
                chuoi += " <div class=\"special-item\">";
                chuoi += "<div class=\"product-image\">";
                chuoi += " <a href=\"/Sanpham/Chitiet/" + p[i].pro_key + "\" title=\"" + p[i].pro_name + "\">";
               // chuoi += "  <img src=\"" + p[i].images + "\" alt=\"" + p[i].pro_name + "\"></a>";
                chuoi += "</div>";
                chuoi += " <div class=\"product-info\">";
                chuoi += " <p>" + p[i].pro_name + "</p>";
              //  chuoi += " <h5 class=\"price\">" + p[i].price_sale + "</h5>";
                chuoi += "  </div>";
                chuoi += " </div>";
            }
            return chuoi;
        }
        #endregion

        #region[tim ten]
        [HttpPost]
        public ActionResult Timten(FormCollection f)
        {

            return Redirect("/Sanpham/Timkiem/" + f["search"]);
        }
        #endregion

        #region[tim ten]
        [HttpPost]
        public ActionResult Timgia(FormCollection f)
        {
            string nho = f["nho"];
            string to = f["to"];
            string noi = nho + "-" + to;
            return Redirect("/Sanpham/Timkiem/"+noi);
        }
        #endregion
    }
}
