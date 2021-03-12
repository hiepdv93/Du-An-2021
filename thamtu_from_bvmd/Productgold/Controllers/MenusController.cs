using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Productgold.Models;

namespace Productgold.Controllers
{
    public class MenusController : BaseCookisController
    {
        //
        // GET: /Menus/

        WEBTPAEntities db = new WEBTPAEntities();
        string lang = ClassExten.GetLang();
        string siteTPA = ClassExten.siteOption;

        #region[ham view]
        public ActionResult Index()
        {
            string titleSearch = "";
            int sltypeSearch = -1;
            string sltype = "";
            int slpositionSearch = -1;
            string slposition = "";
            if (Request["titleSearch"] != null)
            {
                titleSearch = Request["titleSearch"];
            }
            if (Request["sltype"] != null)
            {
                sltype = Request["sltype"];
                if (!string.IsNullOrEmpty(sltype))
                {
                    sltypeSearch = Convert.ToInt32(sltype);
                }
            }

            if (Request["slposition"] != null)
            {
                sltype = Request["slposition"];
                if (!string.IsNullOrEmpty(slposition))
                {
                    slpositionSearch = Convert.ToInt32(slposition);
                }
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
            var all = db.Menus.Where(u => u.mLang == lang && u.siteTPA == siteTPA 
             && (string.IsNullOrEmpty(titleSearch) || u.name.ToLower().Contains(titleSearch.ToLower()))
            && (sltypeSearch == -1 || u.mtype == sltypeSearch)
            && (slpositionSearch == -1 || u.mPosition == slpositionSearch)
            ).OrderBy(u => u.mOrder).ToList();
            var data = all.Skip(curpage * pagesize).Take(pagesize).ToList();
            var url = Request.Path;
            numOfNews = all.Count;
            ViewBag.pages = Productgold.Models.Phantrang.PhanTrang(pagesize, curpage, numOfNews, url);

            return View(data);
        }
        public ActionResult IndexSearch(FormCollection fr)
        {
            var titleSearch = fr["titleSearch"];
            var sltype = fr["sltype"];
            var slposition = fr["slposition"];
            return Redirect("/Menus/Index?titleSearch=" + titleSearch + "&sltype=" + sltype + "&slposition=" + slposition);
        }
        #endregion

        #region[ham sua]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Update(FormCollection f, int id)
        {
            try
            {
                var catep = Convert.ToInt32(f["catep"]);
                var name = f["title"];
                var icon = f["icon"];
                var linkmenu = f["linkmenu"];
                var active = f["active"];
                var thutu = f["thutu"];

                var loaimenu = f["loaimenu"];
                var typecate = f["typecate"];
                var cateid = f["cateid"];

                var contents = f["note"];
                var titleseo = f["titleseo"];
                var keyseo = f["keyseo"];
                var desseo = f["desseo"];

                var ac = true;
                if (active == null || active == "false")
                {
                    ac = false;
                }

                Menu data = db.Menus.FirstOrDefault(u => u.id == id);
                data.mPosition = Convert.ToInt32(f["vitri"]);
                data.ex1 = icon;
                data.name = name;
                data.active = ac;
                data.par_id = catep;
                data.mtype = Convert.ToInt32(loaimenu);
                data.mOrder = Convert.ToInt32(thutu);
                data.mLang = lang;
                data.mcap = GetCap(catep);
                if (loaimenu == "1")
                {
                    data.link = linkmenu;
                    data.mtypeOf = "-1";

                    data.titleSeo = "";
                    data.desSeo = "";
                    data.keySeo = "";
                    data.note = "";
                }
                else if (loaimenu == "2")
                {
                    string key = "/Sites/" + StringClass.NameToTag(name);
                    if (data.link != key)
                    {
                        data.link = checkTrungUpdate(key, 2, id);
                    }
                    data.mtypeOf = "-1";
                    data.titleSeo = titleseo;
                    data.desSeo = desseo;
                    data.keySeo = keyseo;
                    data.note = contents;
                }
                else
                {
                    var cateidCV = Convert.ToInt32(cateid);
                    data.mtypeOf = typecate;
                    if (typecate == "8" && cateid == "0")
                    {
                        data.link = "/Linh-vuc";
                    }
                    else if (typecate == "8" && cateid != "0")
                    {
                        var sp = db.Categorys.FirstOrDefault(u => u.id == cateidCV).keys;
                        data.link = "/Linh-vuc/" + sp;
                    }
                    if (typecate == "1" && cateid == "0")
                    {
                        data.link = "/San-pham";
                    }
                    else if (typecate == "2" && cateid == "0")
                    {
                        data.link = "/Tin-tuc";
                    }
                    else if (typecate == "3" && cateid == "0")
                    {
                        data.link = "/Thu-vien";
                    }
                    else if (typecate == "1" && cateid != "0")
                    {
                        var sp = db.Categorys.FirstOrDefault(u => u.id == cateidCV).keys;
                        data.link = "/San-pham/" + sp;
                    }
                    else if (typecate == "2" && cateid != "0")
                    {
                        var sp = db.Categorys.FirstOrDefault(u => u.id == cateidCV).keys;
                        data.link = "/Tin-tuc/" + sp;
                    }
                    else if (typecate == "3" && cateid != "0")
                    {
                        var sp = db.Categorys.FirstOrDefault(u => u.id == cateidCV).keys;
                        data.link = "/Thu-vien/" + sp;
                    }
                    else if (typecate == "4")
                    {
                        data.link = "/";
                    }
                    else if (typecate == "5")
                    {
                        data.link = "/Hoi-dap";
                    }
                    //else if (typecate == "6")
                    //{
                    //    data.link = "/Linh-vuc";
                    //}
                    else if (typecate == "7")
                    {
                        data.link = "/Lien-he";
                    }
                    data.titleSeo = "";
                    data.desSeo = "";
                    data.keySeo = "";
                    data.note = "";

                }
                db.SaveChanges();
                Session["ok"] = "Cập nhật thành công!";

                return Redirect("/Menus");

            }
            catch (Exception)
            {

                return Redirect("/MNGAdmin/P404");
            }
        }
        #endregion

        #region[ham them]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(FormCollection f)
        {
            try
            {
                var catep = Convert.ToInt32(f["catep"]);
                var name = f["title"];
                var icon = f["icon"];
                var linkmenu = f["linkmenu"];
                var active = f["active"];
                var thutu = f["thutu"];

                var loaimenu = f["loaimenu"];
                var typecate = f["typecate"];
                var cateid = f["cateid"];

                var contents = f["contents"];
                var titleseo = f["titleseo"];
                var keyseo = f["keyseo"];
                var desseo = f["desseo"];

                var ac = true;
                if (active == null || active == "false")
                {
                    ac = false;
                }

                Menu data = new Menu();
                data.mPosition = Convert.ToInt32(f["vitri"]);
                data.name = name;
                data.ex1 = icon;
                data.active = ac;
                data.par_id = catep;
                data.mtype = Convert.ToInt32(loaimenu);
                data.mOrder = Convert.ToInt32(thutu);
                data.mLang = lang;
                data.siteTPA = siteTPA;
                data.mcap = GetCap(catep);
                if (loaimenu == "1")
                {
                    data.link = linkmenu;
                    data.mtypeOf = "-1";

                    data.titleSeo = "";
                    data.desSeo = "";
                    data.keySeo = "";
                    data.note = "";
                }
                else if (loaimenu == "2")
                {
                    string key = "/Sites/" + StringClass.NameToTag(name);
                    data.link = checkTrung(key, 2);
                    data.mtypeOf = "-1";
                    data.titleSeo = titleseo;
                    data.desSeo = desseo;
                    data.keySeo = keyseo;
                    data.note = contents;
                }
                else
                {
                    var cateidCV = Convert.ToInt32(cateid);
                    data.mtypeOf = typecate;
                    if (typecate == "8" && cateid == "0")
                    {
                        data.link = "/Linh-vuc";
                    }
                    else if (typecate == "8" && cateid != "0")
                    {
                        var sp = db.Categorys.FirstOrDefault(u => u.id == cateidCV).keys;
                        data.link = "/Linh-vuc/" + sp;
                    }
                    if (typecate == "1" && cateid == "0")
                    {
                        data.link = "/San-pham";
                    }
                    else if (typecate == "2" && cateid == "0")
                    {
                        data.link = "/Tin-tuc";
                    }
                    else if (typecate == "3" && cateid == "0")
                    {
                        data.link = "/Thu-vien";
                    }
                    else if (typecate == "1" && cateid != "0")
                    {
                        var sp = db.Categorys.FirstOrDefault(u => u.id == cateidCV).keys;
                        data.link = "/San-pham/" + sp;
                    }
                    else if (typecate == "2" && cateid != "0")
                    {
                        var sp = db.Categorys.FirstOrDefault(u => u.id == cateidCV).keys;
                        data.link = "/Tin-tuc/" + sp;
                    }
                    else if (typecate == "3" && cateid != "0")
                    {
                        var sp = db.Categorys.FirstOrDefault(u => u.id == cateidCV).keys;
                        data.link = "/Thu-vien/" + sp;
                    }
                    else if (typecate == "4")
                    {
                        data.link = "/";
                    }
                    else if (typecate == "5")
                    {
                        data.link = "/Hoi-dap";
                    }
                    //else if (typecate == "6")
                    //{
                    //    data.link = "/Linh-vuc";
                    //}
                    else if (typecate == "7")
                    {
                        data.link = "/Lien-he";
                    }
                    data.titleSeo = "";
                    data.desSeo = "";
                    data.keySeo = "";
                    data.note = "";

                }
                db.Menus.Add(data);
                db.SaveChanges();
                Session["ok"] = "Thêm mới thành công!";

                return Redirect("/Menus");

            }
            catch (Exception)
            {

                return Redirect("/MNGAdmin/P404");
            }
        }
        #endregion

        #region[ham xoa]
        public ActionResult Delete(int id)
        {
            try
            {
                var data = db.Menus.First(u => u.id == id);
                db.Menus.Remove(data);
                db.SaveChanges();
                Session["ok"] = "Xóa thành công!";
                return Redirect("/Menus");

            }
            catch (Exception)
            {

                return Redirect("/MNGAdmin/P404");
            }
        }
        #endregion

        public ActionResult Add()
        {
            return View();
        }

        public ActionResult Update(int id)
        {
            string idcateLK = "";
            var menu = db.Menus.FirstOrDefault(u => u.id == id);
            if (menu.mtype == 3)
            {
                //la menu lien kêt
                string[] mang = menu.link.Split('/');
                //la menu co lien ket den nhom con
                if (mang.Length == 3)
                {
                    string keycate = mang[2];
                    if (menu.mtypeOf == "1" || menu.mtypeOf == "8")
                    {
                        //la san phâm
                        var cate = db.Categorys.FirstOrDefault(u => u.ctype == 1 && u.keys == keycate);
                        if (cate != null)
                        {
                            idcateLK = cate.id.ToString();
                        }
                    }
                    else if (menu.mtypeOf == "2")
                    {
                        //la tin tuc
                        var cate = db.Categorys.FirstOrDefault(u => u.ctype == 2 && u.keys == keycate);
                        if (cate != null)
                        {
                            idcateLK = cate.id.ToString();
                        }
                    }
                    else
                    {
                        //la video
                        var cate = db.Categorys.FirstOrDefault(u => u.ctype == 3 && u.keys == keycate);
                        if (cate != null)
                        {
                            idcateLK = cate.id.ToString();
                        }
                    }
                }
            }
            ViewBag.idcateLK = idcateLK;
            return View(menu);
        }

        #region[cac ham xu ly]
        public int GetCap(int number)
        {
            int rs = 1;
            if (number == -1)
            {
                rs = 1;
            }
            else
            {
                var cate = db.Menus.FirstOrDefault(u => u.id == number).par_id;
                if (cate == -1)
                {
                    rs = 2;
                }
                else
                {
                    rs = 3;
                }
            }
            return rs;
        }
        public string checkTrung(string key, int type)
        {
            string rs = key;
            var cate = db.Menus.FirstOrDefault(u => u.mtype == type && u.link == key && u.mLang == lang && u.siteTPA == siteTPA);
            if (cate != null)
            {
                var idMax = db.Menus.OrderByDescending(u => u.id).FirstOrDefault();
                rs = key + "-" + idMax.id;
            }
            return rs;
        }
        public string checkTrungUpdate(string key, int type, int id)
        {
            string rs = key;
            var cate = db.Menus.FirstOrDefault(u => u.mtype == type && u.link == key && u.mLang == lang && u.siteTPA == siteTPA);
            if (cate != null)
            {
                var idMax = id;
                rs = key + "-" + idMax;
            }
            return rs;
        }
        public ActionResult GetCha(int id)
        {
            ViewBag.idthis = id;
            var cate = db.Menus.Where(u => u.active == true && u.mLang == lang && u.siteTPA == siteTPA && u.par_id == -1 && (id == -1 || u.id != id)).OrderBy(u => u.mOrder).ToList();
            return PartialView(cate);
        }
        #endregion

        public ActionResult Nhomdanhmuc(string id)
        {
            int type = Convert.ToInt32(id);
            string nhomcha = "";
            if (type == 1)
            {
                nhomcha = "<option value='0'>Sản phẩm</option>";
            }
            else if (type == 2)
            {
                nhomcha = "<option value='0'>Tin tức</option>";
            }
            else
            {
                nhomcha = "<option value='0'>Thư viện</option>";
            }
            ViewBag.nhomcha = nhomcha;
            var cate = db.Categorys.Where(u => u.ctype == type && u.siteTPA == siteTPA && u.cateLang == lang && u.active == true && u.catepar_id == -1).OrderBy(u => u.cateOrder).ToList();
            return PartialView(cate);
        }
    }
}
