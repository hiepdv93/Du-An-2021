using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NTSPRODUCT.Models;

namespace NTSPRODUCT.Controllers
{
    public class OdersController : Controller
    {
        //
        // GET: /Oders/

        NTSWEBEntities db = new NTSWEBEntities();
        #region[ham view]
        [MyAuthorize]
        public ActionResult Index()
        {
            return View();
        }
        [MyAuthorize]
        public ActionResult GetListOrder(SearchModel model)
        {
            var all = (from a in db.Oders.AsNoTracking()
                       join b in db.Customers.AsNoTracking() on a.cusId equals b.id
                       select new OderExten()
                       {
                           id = a.id,
                           cusId = a.cusId,
                           realMoney = a.realMoney,
                           totalMoney = a.totalMoney,
                           status = a.status,
                           createDate = a.createDate,
                           priceShip = a.priceShip,
                           noteSite = a.noteSite,
                           noteSiteAdmin = a.noteSiteAdmin,

                           fullName = b.fullName,
                           phone = b.phone,
                           email = b.email,
                           addresss = b.addresss,
                           // amount=b.amount,
                       }
                       ).AsQueryable();
            if (!string.IsNullOrEmpty(model.Name))
            {
                all = all.Where(u => u.fullName.ToLower().Contains(model.Name.ToLower()));
            }
            if (!string.IsNullOrEmpty(model.Email))
            {
                all = all.Where(u => u.email.ToLower().Contains(model.Email.ToLower()));
            }
            if (!string.IsNullOrEmpty(model.Phone))
            {
                all = all.Where(u => u.phone.ToLower().Contains(model.Phone.ToLower()));
            }
            if (!string.IsNullOrEmpty(model.Status))
            {
                int stt = int.Parse(model.Status);
                all = all.Where(u => u.status == stt);
            }
            if (!string.IsNullOrEmpty(model.DateFrom))
            {
                DateTime dFrom = ClassExten.ConvertDateFromStr(model.DateFrom);
                all = all.Where(u => u.createDate >= dFrom);
            }

            if (!string.IsNullOrEmpty(model.DateTo))
            {
                DateTime dTo = ClassExten.ConvertDateFromStr(model.DateTo);
                all = all.Where(u => u.createDate <= dTo);
            }
            all = all.OrderByDescending(u => u.createDate);
            var numOfOrders = all.Select(u => u.id).Count();
            var currPage = model.PageNumber - 1;
            var data = all.Skip((model.PageNumber - 1) * model.PageSize).Take(model.PageSize).ToList();

            ViewBag.Index = (currPage * model.PageSize);
            ViewBag.TotalItem = numOfOrders;
            ViewBag.PageSize = model.PageSize;
            if (numOfOrders > model.PageSize)
            {
                ViewBag.pages = NTSPRODUCT.Models.Phantrang.PhanTrang(model.PageSize, currPage, numOfOrders, "");
            }
            return PartialView(data);
        }
        #endregion

        #region[ham  sua]
        [HttpPost]
        [MyAuthorize]
        public ActionResult Update(Oder model)
        {
            try
            {
                var data = db.Oders.First(u => u.id.Equals(model.id));
                data.status = model.status;
                data.noteSiteAdmin = model.noteSiteAdmin;
                db.SaveChanges();
                return Json(new { ok = true, mess = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { ok = false, mess = "" }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region[ham sua]
        [MyAuthorize]
        public ActionResult Getbyid(string id)
        {
            var data = db.Oders.First(u => u.id == id);
            Oder oder = new Oder();
            oder.id = data.id;
            oder.totalMoney = data.totalMoney;
            oder.priceShip = data.priceShip;
            oder.status = data.status;
            oder.noteSiteAdmin = data.noteSiteAdmin;
            return Json(oder, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region[ham xoa]
        [MyAuthorize]
        public ActionResult Delete(string id)
        {
            var data = db.Oders.First(u => u.id == id);
            if (data == null)
            {
                return Json(new { ok = false, mess = "Đơn hàng đã bị xóa bởi người dùng khác" }, JsonRequestBehavior.AllowGet);
            }
            try
            {
                var oChild = db.Oderdts.Where(u => u.oderId.Equals(id));
                if (oChild.Count() > 0)
                {
                    db.Oderdts.RemoveRange(oChild);
                }
                db.Oders.Remove(data);
                db.SaveChanges();
                return Json(new { ok = true, mess = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { ok = false, mess = "" }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region[chi tiet]
        [MyAuthorize]
        public ActionResult ViewDetail(string id)
        {
            var order = db.Oders.FirstOrDefault(u => u.id == id);
            ViewBag.cus = db.Customers.FirstOrDefault(u => u.id == order.cusId);
            ViewBag.Oders = order;
            var data = db.Oderdts.Where(u => u.oderId == id).ToList();
            return View(data);
        }
        #endregion

        public ActionResult ViewCustomer(string id)
        {
            return PartialView();
        }

        #region[ham  sua]
        [HttpPost]
        [MyAuthorize]
        public ActionResult xacNhanDonHang(Oder model)
        {
            try
            {
                string lang = ClassExten.GetLangSite();
                Config conf;
                if (ConfigModel.listConfig == null)
                {
                    ConfigModel.listConfig = db.Configs.ToList();
                }
                conf = ConfigModel.listConfig.FirstOrDefault(u => u.conLang.Equals(lang));

                var data = db.Oders.First(u => u.id.Equals(model.id));
                data.status = ClassExten.Bill_Status.DaMuaHang;
                var lstCus = db.Customers.ToList();
                var lstDetail = db.Oderdts.Where(u => u.oderId.Equals(data.id)).ToList();
                int countSp = 0;
                foreach (var item in lstDetail)
                {
                    countSp += item.quantity.Value;
                }
                var cusMuaHang = lstCus.FirstOrDefault(u => u.id.Equals(data.cusId));

                int countLevel = 6;
                int itemMoneyTip = 10000;
                if (conf.countLevel.HasValue)
                {
                    countLevel = conf.countLevel.Value;
                }
                if (conf.tipMoney.HasValue)
                {
                    itemMoneyTip = conf.tipMoney.Value;
                }
                int tienTipChoUser = countSp * itemMoneyTip;
                List<Customer> lstUpdate = new List<Customer>();
                int tongTienHH = 0;

                CalculatorCustomerUpdateMoney(cusMuaHang.parentId, lstCus, lstUpdate, countLevel);

                foreach (var cus in lstUpdate)
                {
                    tongTienHH += tienTipChoUser;
                    cus.amount = cus.amount.HasValue ? cus.amount + tienTipChoUser : tienTipChoUser;
                }
                data.realMoney = data.totalMoney - tongTienHH;

                db.SaveChanges();
                return Json(new { ok = true, mess = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { ok = false, mess = "" }, JsonRequestBehavior.AllowGet);
            }
        }
        private void CalculatorCustomerUpdateMoney(string id, List<Customer> lstAll, List<Customer> lstUpdate, int countList)
        {
            //chỉ lấy tổng số cus cha = tổng cấp chia HH
            var cus = lstAll.FirstOrDefault(u => u.id.Equals(id));
            if (cus != null && lstUpdate.Count < countList)
            {
                lstUpdate.Add(cus);
                CalculatorCustomerUpdateMoney(cus.parentId, lstAll, lstUpdate, countList);
            }
        }
        #endregion
    }
}
