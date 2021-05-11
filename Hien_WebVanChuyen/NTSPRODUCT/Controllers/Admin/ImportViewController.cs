using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NTSPRODUCT.Models;

namespace NTSPRODUCT.Controllers
{
    public class ImportViewController : Controller
    {
        //
        // GET: /ImportView/
        NTSWEBEntities db = new NTSWEBEntities();
        string lang = ClassExten.GetLang();
        #region[ham gen cart]
        public ActionResult Gencart()
        {
            var cart = (ShoppingCartViewModel)Session["ShoppingCart"];
            return PartialView(cart);
        }
        #endregion
        #region[get cate]
        //combobox danh muc cho san pham
        public ActionResult GetCateForCreate(int type)
        {
            var cateAll = db.Categorys.Where(u => u.cateLang.Equals(lang) && u.cateType == type).OrderBy(u => u.cateOrder).ToList();
            return PartialView(cateAll);
        }
        public ActionResult GetCateForUpdate(int type, string idUpdate)
        {
            ViewBag.idUpdate = idUpdate;
            var cateAll = db.Categorys.Where(u => !u.id.Equals(idUpdate) && u.cateLang.Equals(lang) && u.cateType == type).OrderBy(u => u.cateOrder).ToList();
            return PartialView(cateAll);
        }
        //combobox danh muc cho san pham
        public ActionResult GetCateForProduct(int type)
        {
            ViewBag.type = type;
            var cateAll = db.Categorys.Where(u => u.cateLang.Equals(lang) && u.cateType == type).OrderBy(u => u.cateOrder).ToList();
            return PartialView(cateAll);
        }
        public ActionResult GetBrandsForProduct()
        {
            var cateAll = db.Brands.OrderBy(u => u.numberOder).ToList();
            return PartialView(cateAll);
        }
        //combobox danh muc tim kiem
        public ActionResult GetCateSearch(int type)
        {
            var cateAll = db.Categorys.Where(u => u.cateLang.Equals(lang) && u.cateType == type).OrderBy(u => u.cateOrder).ToList();
            return PartialView(cateAll);
        }

        #endregion



    }
}
