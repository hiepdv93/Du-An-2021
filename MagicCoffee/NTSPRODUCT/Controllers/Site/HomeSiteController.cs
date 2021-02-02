using NTSPRODUCT.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NTSPRODUCT.Controllers.Site
{
    public class HomeSiteController : Controller
    {
        NTSWEBEntities db = new NTSWEBEntities();
        string lang = ClassExten.GetLangSite();
        // GET: HomeSite
        public ActionResult Index()
        {
            ViewBag.lang = lang;
            Config conf;
            if (ConfigModel.listConfig == null)
            {
                ConfigModel.listConfig = db.Configs.ToList();
            }
            conf = ConfigModel.listConfig.FirstOrDefault();
            #region[load seo]
            ViewBag.title = conf.titleSeo;
            ViewBag.description = conf.desSeo;
            ViewBag.keywords = conf.keySeo;
            ViewBag.url = HttpContext.Request.Url.AbsoluteUri;
            ViewBag.img = ClassExten.GetUrlHost() + conf.logoTop;
            #endregion
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.lang = lang;
            Config conf;
            if (ConfigModel.listConfig == null)
            {
                ConfigModel.listConfig = db.Configs.ToList();
            }
            conf = ConfigModel.listConfig.FirstOrDefault();
            #region[load seo]
            ViewBag.title = conf.titleSeo;
            ViewBag.description = conf.desSeo;
            ViewBag.keywords = conf.keySeo;
            ViewBag.url = HttpContext.Request.Url.AbsoluteUri;
            ViewBag.img = ClassExten.GetUrlHost() + conf.logoTop;
            #endregion

            return View(conf);
        }

        public ActionResult AddContact(Contact model)
        {
            try
            {
                Contact data = new Contact();
                data.id = Guid.NewGuid().ToString();
                data.fullName = model.fullName;
                data.email = model.email;
                data.phone = model.phone;
                data.address = string.Empty;
                data.title = string.Empty;
                data.contents = model.contents;
                data.createDate = DateTime.Now;
                data.active = false;

                db.Contacts.Add(data);
                db.SaveChanges();
                return Json(new { ok = true, mess = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { ok = false, mess = "" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult CheckPage()
        {
            try
            {
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                if (controllerName.ToLower().Equals("newsite"))
                {
                    return Json(new { ok = true, page = "n" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { ok = true, page = "p" }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception)
            {
                return Json(new { ok = true, page = "p" }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Robots()
        {
            string all = "";
            using (StreamReader file = new StreamReader(System.Web.Hosting.HostingEnvironment.MapPath("~/robots.txt")))
            {
                int counter = 0;
                string ln;
                while ((ln = file.ReadLine()) != null)
                {
                    all += ln;
                }
                file.Close();
                Console.WriteLine($"File has {counter} lines.");
            }
            Response.Write(all);
            return View();
        }
        public ActionResult SiteMap()
        {
            string changeFreq = "weekly";
            string pagePriority = "0.80";
            string productPriority = "0.80";
            string categoryPriority = "0.60";
            string newsPriority = "0.70";
            string otherPriority = "0.60";
            var urlHost = Request.Url.Host;
            Response.ContentType = "text/xml";
            string text = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" + Environment.NewLine;
            text += "<urlset xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xsi:schemaLocation=\"http://www.sitemaps.org/schemas/sitemap/0.9 http://www.sitemaps.org/schemas/sitemap/0.9/sitemap.xsd\">" + Environment.NewLine;

            //Home page
            text += "<url>" + Environment.NewLine + "\t<loc>http://" + urlHost + "</loc>" + Environment.NewLine + "\t<changefreq>" + changeFreq + "</changefreq>" + Environment.NewLine + "\t<priority>1.00</priority>" + Environment.NewLine + "</url>" + Environment.NewLine;

            //danh muc moduler
            var cateP = db.Categorys.Where(s =>  s.cateActive == true && s.cateType == ClassExten.typePro).OrderByDescending(s => s.cateOrder).ToList();
            foreach (var item in cateP)
            {
                text += "<url>" + Environment.NewLine + "\t<loc>http://" + urlHost + "/moduler/" + item.cateKey + "</loc>" + Environment.NewLine + "\t<changefreq>" + changeFreq + "</changefreq>" + Environment.NewLine + "\t<priority>" + categoryPriority + "</priority>" + Environment.NewLine + "</url>" + Environment.NewLine;
            }
            //danh muc thiet bi
          
            //khoa hoc
            var product = db.Products.Where(s =>  s.active == true).OrderByDescending(s => s.proOrder).ToList();
            foreach (var item in product)
            {
                text += "<url>" + Environment.NewLine + "\t<loc>http://" + urlHost +( "/san-pham/" )  + item.pro_key + "</loc>" + Environment.NewLine + "\t<changefreq>" + changeFreq + "</changefreq>" + Environment.NewLine + "\t<priority>" + productPriority + "</priority>" + Environment.NewLine + "</url>" + Environment.NewLine;
            }
            //nhom tin
            var cateN = db.Categorys.Where(s =>  s.cateActive == true && s.cateType == ClassExten.typeNew).OrderByDescending(s => s.cateOrder).ToList();
            foreach (var item in cateN)
            {
                text += "<url>" + Environment.NewLine + "\t<loc>http://" + urlHost + "/tin-tuc/" + item.cateKey + "</loc>" + Environment.NewLine + "\t<changefreq>" + changeFreq + "</changefreq>" + Environment.NewLine + "\t<priority>" + categoryPriority + "</priority>" + Environment.NewLine + "</url>" + Environment.NewLine;
            }
            //News
            var news = db.News.Where(s => s.status == Constants.Active).OrderByDescending(s => s.newOrder).ToList();
            foreach (var item in news)
            {
                text += "<url>" + Environment.NewLine + "\t<loc>http://" + urlHost + "/tin/" + item.new_key + "</loc>" + Environment.NewLine + "\t<changefreq>" + changeFreq + "</changefreq>" + Environment.NewLine + "\t<priority>" + newsPriority + "</priority>" + Environment.NewLine + "</url>" + Environment.NewLine;
            }

            var menus = db.Menus.Where(s =>  s.active == true && s.mtype == 2).OrderByDescending(s => s.mOrder).ToList();
            foreach (var item in menus)
            {
                text += "<url>" + Environment.NewLine + "\t<loc>http://" + urlHost + item.link + "</loc>" + Environment.NewLine + "\t<changefreq>" + changeFreq + "</changefreq>" + Environment.NewLine + "\t<priority>" + pagePriority + "</priority>" + Environment.NewLine + "</url>" + Environment.NewLine;
            }

            //lien he
            // text += "<url>" + Environment.NewLine + "\t<loc>http://" + urlHost + "/lien-he</loc>" + Environment.NewLine + "\t<changefreq>" + pagePriority + "</changefreq>" + Environment.NewLine + "\t<priority>1.00</priority>" + Environment.NewLine + "</url>" + Environment.NewLine;

            text += "</urlset>";

            //Create sitemap.xml file
            string pathfile = System.Web.Hosting.HostingEnvironment.MapPath("~/sitemap.xml");
            //Server.MapPath("sitemap.xml");
            try
            {
                FileStream fs = null;
                if (!System.IO.File.Exists(pathfile))
                {
                    using (fs = System.IO.File.Create(pathfile)) { }
                }
                using (StreamWriter sw = new StreamWriter(pathfile))
                {
                    sw.Write(text);
                }
            }
            catch (Exception exvi)
            { }
            Response.Write(text);
            return View();
        }
        public ActionResult AddComment(Comment model)
        {
            try
            {
                Comment data = new Comment();
                data.id = Guid.NewGuid().ToString();
                data.proId = model.proId;
                data.fullName = model.fullName;
                data.email = model.email;
                data.phone = model.phone;
                data.address = model.address;//dùng làm loại bl:1 sản phẩm, 0 là tin
                data.title = string.Empty;
                data.contents = model.contents;
                data.createDate = DateTime.Now;
                data.active = false;

                db.Comments.Add(data);
                db.SaveChanges();
                return Json(new { ok = true, mess = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { ok = false, mess = "" }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}