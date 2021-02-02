using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NTSPRODUCT.Models;

namespace NTSPRODUCT.Controllers
{
    public class MenusController : Controller
    {
        //
        // GET: /Menus/

        NTSWEBEntities db = new NTSWEBEntities();
        string lang = ClassExten.GetLang();
        List<Menu> all;
        #region[ham view]
        [MyAuthorize]
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
            var all = db.Menus.Where(u => u.mLang == lang
             && (string.IsNullOrEmpty(titleSearch) || u.name.ToLower().Contains(titleSearch.ToLower()))
            && (sltypeSearch == -1 || u.mtype == sltypeSearch)
            && (slpositionSearch == -1 || u.mPosition == slpositionSearch)
            ).OrderBy(u => u.mOrder).ToList();
            var data = all.Skip(curpage * pagesize).Take(pagesize).ToList();
            var url = Request.Path;
            numOfNews = all.Count;
            ViewBag.pages = NTSPRODUCT.Models.Phantrang.PhanTrang(pagesize, curpage, numOfNews, url);

            return View(data);
        }
        [MyAuthorize]
        public ActionResult GetList(SearchModel model)
        {
            all = (from a in db.Menus.AsNoTracking()
                   where (a.mLang.Equals(lang) &&
                   (model.Type == -1 || model.Type == a.mtype) &&
                   (model.Position == -1 || model.Position == a.mPosition)
                   && (string.IsNullOrEmpty(model.Name) || a.name.ToLower().Contains(model.Name.ToLower())))
                   select a).ToList();
            var listP = all.Where(u => u.par_id.Equals(ClassExten.cateParent)).OrderBy(u => u.mPosition).ThenBy(u => u.mOrder).ToList();
            List<Menu> list = new List<Menu>();
            foreach (var item in listP)
            {
                list.Add(item);
                list.AddRange(GetMenuChil(item.id));
            }

            var numOfNews = all.Select(u => u.id).Count();
            var currPage = model.PageNumber - 1;
            var data = list.Skip((model.PageNumber - 1) * model.PageSize).Take(model.PageSize).ToList();

            ViewBag.Index = (currPage * model.PageSize);
            ViewBag.TotalItem = numOfNews;
            ViewBag.PageSize = model.PageSize;
            if (numOfNews > model.PageSize)
            {
                ViewBag.pages = NTSPRODUCT.Models.Phantrang.PhanTrang(model.PageSize, currPage, numOfNews, "");
            }
            return PartialView(data);
        }

        public List<Menu> GetMenuChil(string idP)
        {
            List<Menu> list = new List<Menu>();
            var listP = all.Where(u => u.par_id.Equals(idP)).OrderBy(u => u.mPosition).ThenBy(u => u.mOrder).ToList();
            foreach (var item in listP)
            {
                list.Add(item);
                list.AddRange(GetMenuChil(item.id));
            }
            return list;
        }
        #endregion

        #region[ham sua]
        [HttpPost]
        [ValidateInput(false)]
        [MyAuthorize]
        public ActionResult Update(FormCollection f, string id)
        {
            try
            {

                var catep = f["catep"];
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

                Menu data = db.Menus.FirstOrDefault(u => u.id == id);
                data.mPosition = Convert.ToInt32(f["vitri"]);
                data.icon = icon;
                data.name = name;
                data.active = active == null ? false : true;
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
                    string key = "/sites/" + StringClass.NameToTag(name);
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
                    var cateidCV = cateid;
                    data.mtypeOf = typecate;
                    if (typecate == "1" && cateid == "0")
                    {
                        data.link = "";//moduler
                    }
                    else if (typecate == "2" && cateid == "0")
                    {
                        data.link = "/tin-tuc";
                    }
                    else if (typecate == "3" && cateid == "0")
                    {
                        data.link = "";//danh-muc
                    }
                    else if (typecate == "4" && cateid == "0")
                    {
                        data.link = "/giai-phap";//giai-phap
                    }
                    else if (typecate == "1" && cateid != "0")
                    {
                        var sp = db.Categorys.FirstOrDefault(u => u.id == cateidCV).cateKey;
                        data.link = "/moduler/" + sp;
                    }
                    else if (typecate == "2" && cateid != "0")
                    {
                        var sp = db.Categorys.FirstOrDefault(u => u.id == cateidCV).cateKey;
                        data.link = "/tin-tuc/" + sp;
                    }
                    else if (typecate == "3" && cateid != "0")
                    {
                        var sp = db.Categorys.FirstOrDefault(u => u.id == cateidCV).cateKey;
                        data.link = "/danh-muc/" + sp;
                    }
                   
                    else if (typecate == "0")
                    {
                        data.link = "/";
                    }
                    else if (typecate == "6")
                    {
                        data.link = "/ung-dung";
                    }
                    else if (typecate == "7")
                    {
                        data.link = "/lien-he";
                    }

                    data.titleSeo = "";
                    data.desSeo = "";
                    data.keySeo = "";
                    data.note = "";
                }
                data.updateDate = DateTime.Now;
                db.SaveChanges();
                Session["ok"] = "Cập nhật menu thành công!";
                return Redirect("/Menus/Index");

            }
            catch (Exception)
            {
                Session["erro"] = "Đã xảy ra lỗi vui lòng thử lại!";
                return Redirect("/Menus/Update/" + id);
            }
        }
        #endregion

        #region[ham them]
        [HttpPost]
        [ValidateInput(false)]
        [MyAuthorize]
        public ActionResult Add(FormCollection f)
        {
            try
            {
                var addType = f["addType"];
                var catep = f["catep"];
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

                Menu data = new Menu();
                data.id = Guid.NewGuid().ToString();
                data.icon = icon;
                data.mPosition = Convert.ToInt32(f["vitri"]);
                data.name = name;
                data.active = active == null ? false : true;
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
                    string key = "/sites/" + StringClass.NameToTag(name);
                    data.link = checkTrung(key, 2);
                    data.mtypeOf = "-1";
                    data.titleSeo = titleseo;
                    data.desSeo = desseo;
                    data.keySeo = keyseo;
                    data.note = contents;
                }
                else
                {
                    var cateidCV = cateid;
                    data.mtypeOf = typecate;
                    if (typecate == "1" && cateid == "0")
                    {
                        data.link = "";
                    }
                    else if (typecate == "2" && cateid == "0")
                    {
                        data.link = "/tin-tuc";
                    }
                    else if (typecate == "3" && cateid == "0")
                    {
                        data.link = "";
                    }
                    else if (typecate == "4" && cateid == "0")
                    {
                        data.link = "/giai-phap";
                    }
                    else if (typecate == "1" && cateid != "0")
                    {
                        var sp = db.Categorys.FirstOrDefault(u => u.id == cateidCV).cateKey;
                        data.link = "/moduler/" + sp;
                    }
                    else if (typecate == "2" && cateid != "0")
                    {
                        var sp = db.Categorys.FirstOrDefault(u => u.id == cateidCV).cateKey;
                        data.link = "/tin-tuc/" + sp;
                    }
                    else if (typecate == "3" && cateid != "0")
                    {
                        var sp = db.Categorys.FirstOrDefault(u => u.id == cateidCV).cateKey;
                        data.link = "/danh-muc/" + sp;
                    }
                   
                    else if (typecate == "0")
                    {
                        data.link = "/";
                    }
                    else if (typecate == "6")
                    {
                        data.link = "/ung-dung";
                    }
                    else if (typecate == "7")
                    {
                        data.link = "/lien-he";
                    }

                    data.titleSeo = "";
                    data.desSeo = "";
                    data.keySeo = "";
                    data.note = "";
                }
                data.createDate = DateTime.Now;
                data.updateDate = DateTime.Now;
                db.Menus.Add(data);
                db.SaveChanges();
                Session["ok"] = "Thêm mới menu thành công!";
                if (addType.Equals("0"))
                {
                    return Redirect("/Menus/Index");
                }
                else
                {
                    return Redirect("/Menus/Add");
                }
            }
            catch (Exception ex)
            {
                Session["erro"] = "Đã xảy ra lỗi vui lòng thử lại!";
                return Redirect("/Menus/Add");
            }
        }
        #endregion

        #region[ham xoa]
        [MyAuthorize]
        public ActionResult Delete(string id)
        {
            var check = db.Menus.Where(u => u.par_id.Equals(id)).Count();
            if (check > 0)
            {
                return Json(new { ok = false, mess = "Menu chứa menu con không thể xóa" }, JsonRequestBehavior.AllowGet);
            }
            try
            {
                var data = db.Menus.First(u => u.id == id);
                db.Menus.Remove(data);
                db.SaveChanges();
                return Json(new { ok = true, mess = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { ok = false, mess = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
        [MyAuthorize]
        public ActionResult ChangeStatus(string id)
        {
            try
            {
                var data = db.Menus.First(u => u.id == id);
                data.active = !data.active;
                db.SaveChanges();
                return Json(new { ok = true, mess = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { ok = false, mess = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [MyAuthorize]
        public ActionResult Add()
        {
            return View();
        }
        [MyAuthorize]
        public ActionResult Update(string id)
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
                    if (menu.mtypeOf == "1")
                    {
                        //la san phâm
                        var cate = db.Categorys.FirstOrDefault(u => u.cateType == ClassExten.typePro && u.cateKey == keycate);
                        if (cate != null)
                        {
                            idcateLK = cate.id;
                        }
                    }
                    else if (menu.mtypeOf == "2")
                    {
                        //la tin tuc
                        var cate = db.Categorys.FirstOrDefault(u => u.cateType == ClassExten.typeNew && u.cateKey == keycate);
                        if (cate != null)
                        {
                            idcateLK = cate.id;
                        }
                    }
                    //else if (menu.mtypeOf == "3")
                    //{
                    //    //la tin tuc
                    //    var cate = db.Categorys.FirstOrDefault(u => u.cateType == ClassExten.typeSolution && u.cateKey == keycate);
                    //    if (cate != null)
                    //    {
                    //        idcateLK = cate.id;
                    //    }
                    //}
                    //else if (menu.mtypeOf == "4")
                    //{
                    //    //la tin tuc
                    //    var cate = db.Categorys.FirstOrDefault(u => u.cateType == ClassExten.typeManufac && u.cateKey == keycate);
                    //    if (cate != null)
                    //    {
                    //        idcateLK = cate.id;
                    //    }
                    //}
                    //else if (menu.mtypeOf == "5")
                    //{
                    //    la tin tuc
                    //    var cate = db.Categorys.FirstOrDefault(u => u.cateType == ClassExten.typeApp && u.cateKey == keycate);
                    //    if (cate != null)
                    //    {
                    //        idcateLK = cate.id;
                    //    }
                    //}
                }
            }
            ViewBag.idcateLK = idcateLK;
            return View(menu);
        }

        #region[cac ham xu ly]
        public int GetCap(string number)
        {
            int rs = 1;
            if (number == "-1")
            {
                rs = 1;
            }
            else
            {
                var cate = db.Menus.FirstOrDefault(u => u.id == number).par_id;
                if (cate == "-1")
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
            var cate = db.Menus.FirstOrDefault(u => u.mtype == type && u.link == key && u.mLang == lang);
            if (cate != null)
            {
                var idMax = db.Menus.OrderByDescending(u => u.id).FirstOrDefault();
                rs = key + "-" + idMax.id;
            }
            return rs;
        }
        public string checkTrungUpdate(string key, int type, string id)
        {
            string rs = key;
            var cate = db.Menus.FirstOrDefault(u => u.mtype == type && u.link == key && u.mLang == lang);
            if (cate != null)
            {
                var idMax = id;
                rs = key + "-" + idMax;
            }
            return rs;
        }
        public ActionResult GetMenuAdd()
        {
            var cate = db.Menus.Where(u => u.mLang == lang).OrderBy(u => u.mPosition).ThenBy(u => u.mcap).ThenBy(u => u.mOrder).ToList();
            return PartialView(cate);
        }
        public ActionResult GetMenuUpdate(string idUpdate)
        {
            ViewBag.idUpdate = idUpdate;
            var cate = db.Menus.Where(u => u.mLang == lang && !u.id.Equals(idUpdate)).OrderBy(u => u.mPosition).ThenBy(u => u.mcap).ThenBy(u => u.mOrder).ToList();
            return PartialView(cate);
        }
        #endregion

        public ActionResult Nhomdanhmuc(string id)
        {
            int type = Convert.ToInt32(id);
            string nhomcha = "";
            if (type == 1)
            {
                nhomcha = "<option value='0'>==Danh mục==</option>";
            }
            else if (type == 2)
            {
                nhomcha = "<option value='0'>==Tin tức==</option>";
            }
            else if (type == 3)
            {
                nhomcha = "<option value='0'>==Thiết bị==</option>";
            }
            ViewBag.nhomcha = nhomcha;
            var cate = db.Categorys.Where(u => u.cateType == type && u.cateLang == lang).OrderBy(u => u.cateOrder).ToList();
            return PartialView(cate);
        }
     
    }
}
