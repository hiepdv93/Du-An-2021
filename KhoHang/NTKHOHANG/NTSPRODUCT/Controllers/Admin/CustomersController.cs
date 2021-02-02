using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using NTSPRODUCT.Models;

namespace NTSPRODUCT.Controllers
{
    public class CustomersController : Controller
    {
        //
        // GET: /Slide/
        NTSWEBEntities db = new NTSWEBEntities();
        string lang = ClassExten.GetLang();
        List<Customer> all;
        #region[ham view]
        [MyAuthorize]
        public ActionResult Index()
        {
            return View();
        }
        [MyAuthorize]
        public ActionResult GetList(SearchModel model)
        {
            all = (from a in db.Customers.AsNoTracking()
                   where
                    (string.IsNullOrEmpty(model.Name) || a.fullName.ToLower().Contains(model.Name.ToLower()))
                   && (string.IsNullOrEmpty(model.Email) || a.email.ToLower().Contains(model.Email.ToLower()))
                   select a).ToList();
            var listP = all.Where(u => u.parentId.Equals(ClassExten.cateParent)).OrderByDescending(u => u.createDate).ToList();
            List<CustomerExten> list = new List<CustomerExten>();
            foreach (var item in listP)
            {

                list.Add(ConvertCustomer(item, 0));
                list.AddRange(GetChild(item.id, 1));
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
        public List<CustomerExten> GetChild(string idP, int level)
        {
            List<CustomerExten> list = new List<CustomerExten>();
            var listP = all.Where(u => u.parentId.Equals(idP)).OrderByDescending(u => u.createDate).ToList();
            foreach (var item in listP)
            {
                list.Add(ConvertCustomer(item, level));
                list.AddRange(GetChild(item.id, level + 1));
            }
            return list;
        }
        #endregion
        private CustomerExten ConvertCustomer(Customer c, int level)
        {
            string output = JsonConvert.SerializeObject(c);
            var rs = JsonConvert.DeserializeObject<CustomerExten>(output);
            rs.Level = level;
            return rs;
        }
        #region[ham lay thong tin]
        [MyAuthorize]
        public ActionResult GetbyId(string id)
        {
            var data = db.Customers.First(u => u.id.Equals(id));
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion


        #region[ham xoa]
        [MyAuthorize]
        public ActionResult Delete(string id)
        {
            var data = db.Customers.First(u => u.id.Equals(id));
            {
                try
                {
                    db.Customers.Remove(data);
                    db.SaveChanges();
                    return Json(new { ok = true, mess = "" }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(new { ok = false, mess = ex.Message }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        #endregion
        [MyAuthorize]
        public ActionResult ChangeStatus(CustomerExten cus)
        {
            var data = db.Customers.First(u => u.id.Equals(cus.id));
            {
                try
                {
                    data.activeAcount = cus.activeAcount;
                    data.activeCode = cus.activeCode;

                    db.SaveChanges();
                    return Json(new { ok = true, mess = "" }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(new { ok = false, mess = ex.Message }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        public ActionResult UpdateOrCreate()
        {
            return PartialView();
        }

        public ActionResult ShowResetPass()
        {
            return PartialView();
        }

        [MyAuthorize]
        public ActionResult ResetPass(CustomerExten cus)
        {
            var data = db.Customers.First(u => u.id.Equals(cus.id));
            {
                try
                {
                    string passLevel2 = ClassExten.ProcessPass(cus.pass);
                    data.pass = passLevel2;
                    db.SaveChanges();
                    return Json(new { ok = true, mess = "" }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(new { ok = false, mess = ex.Message }, JsonRequestBehavior.AllowGet);
                }
            }
        }

    }
}
