using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Web.Script.Serialization;
using System.IO;
using System.Net.Mail;
using System.Net;
using NTSPRODUCT.Models;
using Newtonsoft.Json;

public class ClassExten
{

    static NTSWEBEntities db = new NTSWEBEntities();
    public static DataTable langData;
    public class Bill_Status
    {
        public const int MoiTao = 1;
        public const int DangXuLy = 2;
        public const int DaMuaHang = 3;
        public const int Huy = 4;
    }
    #region[cấu hình loại nhóm ]
    public const int typeProduct = 1;//ndanh mục
    public const int typeNew = 2;//tin tức
    public const string cateParent = "-1";
     public const int timeCacheChild = 3600000;
   // public const int timeCacheChild = 30;
    #endregion
    #region[tao ngon ngu trong admin]
    public static string GetLang()
    {
        string lang = "vi";
        if (HttpContext.Current.Request.Cookies["langcookis"] == null)
        {
            lang = Createlang();

        }
        else
        {
            HttpCookie htc = (HttpContext.Current.Request.Cookies["langcookis"]);
            lang = htc.Values["langcode"];
        }
        return lang;
    }
    public static string Createlang()
    {
        string lang = "vi";
        HttpCookie cook = new HttpCookie("langcookis");
        cook.Values["langcode"] = lang;
        cook.Expires = DateTime.Now.AddDays(10);
        HttpContext.Current.Response.Cookies.Add(cook);
        return lang;
    }

    public static void Changelang(string code)
    {
        HttpCookie cook = new HttpCookie("langcookis");
        cook.Values["langcode"] = code;
        cook.Expires = DateTime.Now.AddDays(10);
        HttpContext.Current.Response.Cookies.Add(cook);
    }
    #endregion

    #region[tao cooki ngon ngu ngoai site]
    public static string GetLangSite()
    {
        string lang = "";
        if (HttpContext.Current.Request.Cookies["langcookisSites"] == null)
        {
            lang = CreatelangSite();
        }
        else
        {
            HttpCookie htc = (HttpContext.Current.Request.Cookies["langcookisSites"]);
            lang = htc.Values["langcodeSites"];
        }
        if (langData == null)
        {
            GetLangData(lang);
        }
        return lang;
    }
    public static string CreatelangSite()
    {
        string lang = "vi";
        HttpCookie cook = new HttpCookie("langcookisSites");
        cook.Values["langcodeSites"] = lang;
        cook.Expires = DateTime.Now.AddDays(10);
        HttpContext.Current.Response.Cookies.Add(cook);
        GetLangData(lang);
        return lang;
    }

    public static void ChangelangSite(string code)
    {
        HttpCookie cook = new HttpCookie("langcookisSites");
        cook.Values["langcodeSites"] = code;
        cook.Expires = DateTime.Now.AddDays(10);
        HttpContext.Current.Response.Cookies.Add(cook);
        GetLangData(code);
    }
    #endregion


    #region[lay ngon ngu]
    private static void GetLangData(string lang)
    {
        lang = lang == "" ? "vi" : lang;
        string langPath = "/Content/language/" + lang + ".xml";
        DataSet ds = new DataSet();
        try
        {
            ds.ReadXml(System.Web.Hosting.HostingEnvironment.MapPath(langPath));
        }
        catch { }
        langData = ds.Tables["Items"];
    }
    public static string GetLangKey(string key)
    {
        try
        {
            return langData.AsEnumerable().FirstOrDefault(row => string.Equals(row["Id"].ToString(), key, StringComparison.OrdinalIgnoreCase))["Name"].ToString();
        }
        catch { return ""; }
    }
    #endregion
    public static LoginModel GetCokies()
    {
        LoginModel model = null;
        if (HttpContext.Current.Request.Cookies["adminNTS"] != null)
        {
            var c = HttpContext.Current.Request.Cookies["adminNTS"];
            if (!string.IsNullOrEmpty(c.Value))
            {
                model = JsonConvert.DeserializeObject<LoginModel>(c.Value); ;
            }
        }
        return model;
    }
    public static bool CheckLogin()
    {
        bool rs = false;
        if (HttpContext.Current.Session["adminloginNTS"] != null)
        {
            rs = true;
        }
        return rs;
    }
    public static string GetUrlHost()
    {
        string url = HttpContext.Current.Request.Url.AbsoluteUri;
        string path = HttpContext.Current.Request.Url.AbsolutePath;
        if (!path.Equals("/"))
        {
            return url.Replace(path, "");
        }
        else
        {
            return url;
        }

    }
    public static string GetChildLevel2(string idcha, string idUpdate, List<Category> list)
    {
        string rs = "";
        try
        {
            var cate = list.Where(u => u.catepar_id.Equals(idcha) && !u.id.Equals(idUpdate)).ToList();
            foreach (var item in cate)
            {
                rs += "<option value='" + item.id + "'>==> " + item.cateName + "</option>";
                rs += GetChildLevel3(item.id, idUpdate, list);
            }
        }
        catch (Exception)
        { }
        return rs;
    }
    public static string GetChildLevel3(string idcha, string idUpdate, List<Category> list)
    {
        string rs = "";
        try
        {
            var cate = list.Where(u => u.catepar_id.Equals(idcha) && !u.id.Equals(idUpdate)).ToList();
            foreach (var item in cate)
            {
                rs += "<option value='" + item.id + "'>====> " + item.cateName + "</option>";
            }
        }
        catch (Exception)
        { }
        return rs;
    }
    public static bool SendMail(string emailInbox, string emailSend, string emailPass, int port, string title, string note)
    {
        #region[code gui mail]
        try
        {
            var configmail = db.Configs.FirstOrDefault();
            MailMessage mailsend = new MailMessage();
            mailsend.To.Add(emailInbox);
            mailsend.From = new MailAddress(emailSend);
            mailsend.Subject = title;
            mailsend.Body = note;
            mailsend.IsBodyHtml = true;
            SmtpClient client = new SmtpClient("smtp.gmail.com", port);
            client.EnableSsl = true;
            NetworkCredential credentials = new NetworkCredential(emailSend, emailPass);
            client.Credentials = credentials;
            client.Send(mailsend);
            return true;
        }
        catch (Exception ex)
        { return false; }
        #endregion
    }

    public static string GetMenuLevel2(string idcha, string idUpdate, List<Menu> list)
    {
        string rs = "";
        try
        {
            var cate = list.Where(u => u.par_id.Equals(idcha) && !u.id.Equals(idUpdate)).ToList();
            foreach (var item in cate)
            {
                rs += "<option value='" + item.id + "'>==> " + item.name + "</option>";
                rs += GetMenuLevel3(item.id, idUpdate, list);
            }
        }
        catch (Exception)
        { }
        return rs;
    }
    public static string GetMenuLevel3(string idcha, string idUpdate, List<Menu> list)
    {
        string rs = "";
        try
        {
            var cate = list.Where(u => u.par_id.Equals(idcha) && !u.id.Equals(idUpdate)).ToList();
            foreach (var item in cate)
            {
                rs += "<option value='" + item.id + "'>====> " + item.name + "</option>";
            }
        }
        catch (Exception)
        { }
        return rs;
    }

    public static string GetMenuCha(string id)
    {
        if (id.Equals(ClassExten.cateParent))
        {
            return string.Empty;
        }
        string rs = "";
        try
        {
            rs = db.Menus.FirstOrDefault(u => u.id.Equals(id)).name;
        }
        catch (Exception)
        { }
        return rs;
    }

    public static string GetProduct(string id)
    {
        string rs = "";
        try
        {
            rs = db.Products.FirstOrDefault(u => u.id.Equals(id)).pro_name;
        }
        catch (Exception)
        { }
        return rs;
    }

    #region[các hàm cho ngoài site]
    public static string GetMenuSiteLevel2(string idcha, List<Menu> list)
    {
        string rs = "";
        try
        {
            var cate = list.Where(u => u.par_id.Equals(idcha)).OrderBy(u => u.mOrder).ToList();
            if (cate.Count > 0)
            {
                rs += "<ul>";
                foreach (var item in cate)
                {
                    rs += " <li><a href='" + item.link + "' title='" + item.name + "' >" + item.name + "</a>";
                    rs += GetMenuSiteLevel3(item.id, list);
                    rs += "</li>";
                }
                rs += "</ul>";
            }

        }
        catch (Exception)
        { }
        return rs;
    }
    public static string GetMenuSiteLevel3(string idcha, List<Menu> list)
    {
        string rs = "";
        try
        {
            var cate = list.Where(u => u.par_id.Equals(idcha)).OrderBy(u => u.mOrder).ToList();
            if (cate.Count > 0)
            {
                rs += "<ul>";
                foreach (var item in cate)
                {
                    rs += " <li><a href='" + item.link + "' title='" + item.name + "'>" + item.name + "</a></li>";
                }
                rs += "</ul>";
            }

        }
        catch (Exception)
        { }
        return rs;
    }
    public static string GetCateNewChild(string idcha, List<Category> list)
    {
        string rs = "";
        try
        {
            var cate = list.Where(u => u.catepar_id.Equals(idcha)).OrderBy(u => u.cateOrder).ToList();
            if (cate.Count > 0)
            {
                rs += "<ul class='nav1'>";
                foreach (var item in cate)
                {
                    rs += "<li><a href='/Tin-tuc/" + item.cateKey + "' title='" + item.cateName + "' class='nav1-child'><span>" + item.cateName + "</span></a></li>";
                }
                rs += "</ul>";
            }
        }
        catch (Exception)
        { }
        return rs;
    }

    public static string GetCateLib(int tyepe, string idCate)
    {
        string rs = "";
        try
        {
            var cate = db.Categorys.FirstOrDefault(u => u.id.Equals(idCate) && u.cateType == tyepe);
            if (cate != null)
            {
                string url = "#";
                switch (tyepe)
                {
                    case 1:
                        url = "/Nganh-nghe/" + cate.cateKey;
                        break;
                    case 3:
                        url = "/Giai-phap/" + cate.cateKey;
                        break;
                    case 4:
                        url = "/Hang/" + cate.cateKey;
                        break;
                    case 5:
                        url = "/Ung-dung/" + cate.cateKey;
                        break;
                    default:
                        break;
                }
                rs += "<a href='" + url + "' title='" + cate.cateName + "'>" + cate.cateName + "</a>";
            }
        }
        catch (Exception)
        { }
        return rs;
    }



    public static string GetCateCha(string id)
    {
        if (id.Equals(ClassExten.cateParent))
        {
            return string.Empty;
        }
        string rs = "";
        try
        {
            rs = db.Categorys.FirstOrDefault(u => u.id.Equals(id)).cateName;
        }
        catch (Exception)
        { }
        return rs;
    }
    #endregion
    public static string GetPageReference(string id)
    {
        //0 la trang home, -1 la trang lien he, khac la trang khoa hoc
        string rs = "";
        try
        {
            if (id.Equals("0"))
            {
                rs = "Trang chủ";
            }
            else if (id.Equals("-1"))
            {
                rs = "Trang liên hệ";
            }
            else
            {
                var prod = db.Products.FirstOrDefault(u => u.id.Equals(id));
                rs = "<a href='/san-pham/" + prod.pro_key + "'>" + prod.pro_name + "</a>";
            }
        }
        catch (Exception)
        { }
        return rs;
    }

    public static string GetCateSiteLevel2(string idcha, List<Category> list)
    {
        string rs = "";
        try
        {
            var cate = list.Where(u => u.catepar_id.Equals(idcha)).OrderBy(u => u.cateOrder).ToList();
            if (cate.Count > 0)
            {
                rs += "<i class=''fa fa-angle-down></i> <ul class='dropdown-menu'>";
                foreach (var item in cate)
                {
                    rs += " <li class='dropdown-submenu nav-item'><a class='nav-link' href='/danh-muc/" + item.cateKey + "' title='" + item.cateName + "'>" + item.cateName + "</a>";
                    rs += "</li>";
                }
                rs += "</ul>";
            }

        }
        catch (Exception)
        { }
        return rs;
    }
    public static string GetNewSiteLevel2(string idcha, List<Category> list)
    {
        string rs = "";
        try
        {
            var cate = list.Where(u => u.catepar_id.Equals(idcha)).OrderBy(u => u.cateOrder).ToList();
            if (cate.Count > 0)
            {
                rs += "<i class=''fa fa-angle-down></i><ul class='dropdown-menu'>";
                foreach (var item in cate)
                {
                    rs += " <li class='dropdown-submenu nav-item'><a class='nav-link' href='/tin-tuc/" + item.cateKey + "' title='" + item.cateName + "'>" + item.cateName + "</a>";
                    rs += "</li>";
                }
                rs += "</ul>";
            }

        }
        catch (Exception)
        { }
        return rs;
    }
    /// <summary>
    /// gen menu sản phẩm
    /// </summary>
    /// <param name="idcha"></param>
    /// <param name="list"></param>
    /// <returns></returns>
    public static string GetMenuSiteProductLevel2(string idcha, List<Menu> list)
    {
        string lang = GetLang();
        var allCate = db.Categorys.Where(u => u.cateLang.Equals(lang) && u.cateActive == true && u.cateType == ClassExten.typeProduct).ToList();
        var catepro = allCate.Where(u => u.cateLang.Equals(lang) && u.cateType == ClassExten.typeProduct && u.cateActive == true && u.catepar_id.Equals(ClassExten.cateParent)).ToList();

        string rs = "";
        try
        {
            // var cate = list.Where(u => u.par_id.Equals(idcha)).OrderBy(u => u.mOrder).ToList();
            if (catepro.Count > 0)
            {
                foreach (var item in catepro)
                {
                    rs += " <li class='level1 parent item'><h2 class='h4'><a href='/danh-muc/" + item.cateKey + "' title='" + item.cateName + "' ><span>" + item.cateName + "</span></a></h2>";
                    rs += GetMenuSiteProductLevel3(item.id, allCate);
                    rs += "</li>";
                }
            }

        }
        catch (Exception)
        { }
        return rs;
    }
    public static string GetMenuSiteProductLevel3(string idcha, List<Category> list)
    {
        string rs = "";
        try
        {
            var cate = list.Where(u => u.catepar_id.Equals(idcha)).OrderBy(u => u.cateOrder).ToList();
            if (cate.Count > 0)
            {
                rs += "<ul class='level1'>";
                foreach (var item in cate)
                {
                    rs += " <li><a href='/danh-muc/" + item.cateKey + "' title='" + item.cateName + "'><span>" + item.cateName + "</span></a></li>";
                }
                rs += "</ul>";
            }

        }
        catch (Exception)
        { }
        return rs;
    }
    public static DateTime ConvertDateFromStr(string date)
    {
        if (date == null)
        {
            return new DateTime();
        }

        DateTime returnValue = DateTime.ParseExact(date + " 00:00:00", "dd/MM/yyyy HH:mm:ss", null);

        return returnValue;
    }
    public static DateTime ConvertDateTo(string date)
    {
        if (date == null)
        {
            return new DateTime();
        }

        DateTime returnValue = DateTime.ParseExact(date + " 23:59:59", "dd/MM/yyyy HH:mm:ss", null);

        return returnValue;
    }
    public static ShoppingCartViewModel GetCokiesCart()
    {
        ShoppingCartViewModel model = null;
        if (HttpContext.Current.Request.Cookies["cartNTS"] != null)
        {
            var c = HttpContext.Current.Request.Cookies["cartNTS"];
            if (!string.IsNullOrEmpty(c.Value))
            {
                model = JsonConvert.DeserializeObject<ShoppingCartViewModel>(c.Value); ;
            }
            else
            {
                return new ShoppingCartViewModel();
            }
        }
        return model;
    }

    public static void CreateCookiesCart(string obj)
    {
        HttpCookie ntsCookies = new HttpCookie("cartNTS");
        ntsCookies.Value = obj;
        ntsCookies.Expires = DateTime.Now.AddHours(1);
        HttpContext.Current.Response.Cookies.Add(ntsCookies);
    }
    public static void UpdateCookiesCart(string obj)
    {
        HttpCookie cookie = (HttpContext.Current.Request.Cookies["cartNTS"]);
        cookie.Value = obj;
        cookie.Expires = DateTime.Now.AddHours(1);
        HttpContext.Current.Response.Cookies.Add(cookie);
    }



}
