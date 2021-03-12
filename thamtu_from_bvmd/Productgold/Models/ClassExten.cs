using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Productgold.Models;
using System.Web.Mvc;

using System.Data;
using Google.Apis.Drive.v3;
using Google.Apis.Auth.OAuth2;
using System.Threading;
using Google.Apis.Util.Store;
using Google.Apis.Services;
using Google.Apis.Drive.v3.Data;
using System.Web.Script.Serialization;
using System.IO;
using Newtonsoft.Json;

public class ClassExten
{

    static WEBTPAEntities db = new WEBTPAEntities();
    public static DataTable langData;

    #region[tao ngon ngu trong admin]
    public static string GetLang()
    {
        string lang = "";
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
        var langDf = db.Languages.FirstOrDefault(u => u.langDefault == true && u.siteTPA == siteOption);
        HttpCookie cook = new HttpCookie("langcookis");
        cook.Values["langcode"] = langDf.langCode;
        cook.Expires = DateTime.Now.AddDays(10);
        HttpContext.Current.Response.Cookies.Add(cook);
        return langDf.langCode;
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
        var langDf = db.Languages.FirstOrDefault(u => u.langDefault == true && u.siteTPA == siteOption);
        HttpCookie cook = new HttpCookie("langcookisSites");
        cook.Values["langcodeSites"] = langDf.langCode;
        cook.Expires = DateTime.Now.AddDays(10);
        HttpContext.Current.Response.Cookies.Add(cook);
        GetLangData(langDf.langCode);
        return langDf.langCode;
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

    #region[xu ly cap menu va nhom ]
    public static string GetCon(string idcha, int id)
    {
        string rs = "";
        try
        {
            int idp = Convert.ToInt32(idcha);

            var cate = db.Categorys.Where(u => u.active == true && u.catepar_id == idp && (id == -1 || u.id != id)).ToList();
            for (int i = 0; i < cate.Count; i++)
            {
                rs += "<option value='" + cate[i].id + "'>=== " + cate[i].name + "</option>";
            }
        }
        catch (Exception)
        { }
        return rs;
    }
    public static string GetConcon(string idcha)
    {
        string rs = "";
        try
        {
            int idp = Convert.ToInt32(idcha);
            var cate = db.Categorys.Where(u => u.active == true && u.catepar_id == idp).ToList();
            for (int i = 0; i < cate.Count; i++)
            {
                rs += "<option value='" + cate[i].id + "'>=== " + cate[i].name + "</option>";
                rs += GetConconcon(cate[i].id.ToString());
            }
        }
        catch (Exception)
        { }
        return rs;
    }
    public static string GetConconcon(string idcha)
    {
        string rs = "";
        try
        {
            int idp = Convert.ToInt32(idcha);
            var cate = db.Categorys.Where(u => u.active == true && u.catepar_id == idp).ToList();
            for (int i = 0; i < cate.Count; i++)
            {
                rs += "<option value='" + cate[i].id + "'>====== " + cate[i].name + "</option>";
            }
        }
        catch (Exception)
        { }
        return rs;
    }
    public static string GetVT(string vt)
    {
        string rs = "";
        if (vt.Equals("1"))
        {
            rs = "Khối trên";
        }
        else if (vt.Equals("2"))
        {
            rs = "Khối giữa";
        }
        else if (vt.Equals("3"))
        {
            rs = "Khối dưới";
        }
        else
        {
            rs = "Nhóm thường";
        }
        return rs;
    }

    public static string GetNhomcha(int idcha)
    {
        string rs = "";
        try
        {
            if (idcha == -1)
            {
                rs = "Nhóm cha";
            }
            else
            {
                rs = db.Categorys.FirstOrDefault(u => u.id == idcha).name;
            }
        }
        catch (Exception)
        { }
        return rs;
    }
    public static string GetConMenu(string idcha, int id)
    {
        string rs = "";
        try
        {
            int idp = Convert.ToInt32(idcha);
            var cate = db.Menus.Where(u => u.active == true && u.par_id == idp && (id == -1 || u.id != id)).ToList();
            for (int i = 0; i < cate.Count; i++)
            {
                rs += "<option value='" + cate[i].id + "'>=== " + cate[i].name + "</option>";
            }
        }
        catch (Exception)
        { }
        return rs;
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

    #region[cau hinh site]
    public static string siteOption = "2";
    //site chinh la 0, site con 1 là 1, site con 2 là 2, site con 3 là 3
    public static string QLTin = "0";
    public static string QLSanpham = "1";
    public static string QLLienhe = "2";
    public static string QLCauhinh = "3";
    public static string QLAnhQC = "4";
    public static string QLGiohang = "5";
    public static string QLMenu = "6";

    public static string ForderNew = "1cfkmtCPbo3Re7etZbZm0Cj66OXaEjakp";
    public static string ForderProduct = "1scV48gkonmspnOt8874y1FznUNV58Y5h";
    public static string ForderAdv = "1Sb0FSe_NRU01VtsgXnBrVDC3An51Ts5W";
    public static string ForderLib = "1OCJ_r7Fj6BxYji0SdmMcj753OSDme2tc";

    #endregion

    public static bool CheckLogin()
    {
        bool rs = false;
        if (HttpContext.Current.Session["adminlogin" + siteOption] != null)
        {
            rs = true;
        }
        return rs;
    }

    public static void CheckAuthen()
    {
        var user = ClassExten.GetCokies();
        if (user == null)
        {
            HttpContext.Current.Response.Redirect("/MNGAdmin/LoginAdmin");
        }
    }

    public static string GetUrlHost()
    {
        string url = HttpContext.Current.Request.Url.AbsoluteUri;
        string path = HttpContext.Current.Request.Url.AbsolutePath;
        return url.Replace(path, "");
    }

    public static string GetDanhmucThuvien(int idp)
    {
        string lang1 = GetLangSite();
        string rs = "";
        try
        {
            var data = db.Categorys.FirstOrDefault(u => u.cateLang == lang1 && u.siteTPA == siteOption && u.id == idp);
            if (data != null)
            {
                if (data.cateLevel == "0")
                {
                    rs = "<a href='/San-pham/" + data.keys + "' title='" + data.name + "'>" + data.name + "</a>";
                }
                else
                {
                    rs = "<a href='/Linh-vuc/" + data.keys + "' title='" + data.name + "'>" + data.name + "</a>";
                }
            }
        }
        catch (Exception)
        { }
        return rs;
    }

    public static string Site0 = "http://103.237.147.155:8086";
    public static string Site1 = "http://103.237.147.155:8082";
    public static string Site2 = "http://103.237.147.155:8083";
    public static string Site3 = "http://103.237.147.155:8084";
    public static string GetLocalSite(string siteNumber)
    {
        string rs = "";
        switch (siteNumber)
        {
            case "0":
                rs = Site0;
                break;
            case "1":
                rs = Site1;
                break;
            case "2":
                rs = Site2;
                break;
            case "3":
                rs = Site3;
                break;
            default:
                rs = Site3;
                break;
        }
        return rs;
    }
    public static string NotAdmin = "NotAdmin";


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
}
