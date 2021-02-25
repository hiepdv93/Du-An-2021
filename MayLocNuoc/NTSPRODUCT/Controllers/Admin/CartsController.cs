using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
using System.Net;
using NTSPRODUCT.Models;
using Newtonsoft.Json;

namespace NTSPRODUCT.Controllers
{
    public class CartsController : Controller
    {
        //
        // GET: /Carts/
        NTSWEBEntities db = new NTSWEBEntities();
        string lang = ClassExten.GetLangSite();
        int cartTotal;

        #region[index view]
        public ActionResult Index()
        {
            Config conf;
            if (ConfigModel.listConfig == null)
            {
                ConfigModel.listConfig = db.Configs.ToList();
            }
            conf = ConfigModel.listConfig.FirstOrDefault(u => u.conLang.Equals(lang));
            #region[load seo]
            ViewBag.title = ClassExten.GetLangKey("key_giohang");
            ViewBag.description = conf.desSeo;
            ViewBag.keywords = conf.keySeo;
            ViewBag.url = HttpContext.Request.Url.AbsoluteUri;
            ViewBag.img = ClassExten.GetUrlHost() + conf.logoTop;
            #endregion

            ShoppingCartViewModel shoppCart;
            var cartGet = ClassExten.GetCokiesCart();
            if (cartGet == null)
            {
                return Redirect("/danh-muc");
            }
            else
            {
                shoppCart = cartGet;
                return View(shoppCart);
            }

        }

        public ActionResult GetList()
        {
            ShoppingCartViewModel shoppCart;
            var cartGet = ClassExten.GetCokiesCart();
            if (cartGet == null)
            {
                return PartialView();
            }
            else
            {
                shoppCart = cartGet;
                return PartialView(shoppCart);

            }

        }
        #endregion


        #region[Addcart ngoai site]
        public ActionResult Addcarts(string id)
        {
            int soluong = 1;
            var obj = db.Products.First(u => u.pro_key.Equals(id) && u.pLang.Equals(lang));
            int tong = (soluong);
            int flag = -1;

            ShoppingCartViewModel shoppCart;
            var cartGet = ClassExten.GetCokiesCart();
            if (cartGet == null)
            {
                shoppCart = new ShoppingCartViewModel();
                ClassExten.CreateCookiesCart(string.Empty);
            }
            else
            {
                shoppCart = cartGet;
            }

            if (GetCartItem(shoppCart, obj.id) == flag)
            {
                var cartItem = new Cart
                {
                    productId = obj.id,
                    productName = obj.pro_name,
                    productImg = obj.proAvata,
                    price = obj.proPrice_sale != null ? obj.proPrice_sale.Value : 0,
                    count = soluong,
                    total = tong,
                    key = obj.pro_key
                };
                shoppCart.CartItems.Add(cartItem);
            }
            else
            {
                flag = GetCartItem(shoppCart, obj.id);
                shoppCart.CartItems[flag].count += soluong;
                shoppCart.CartItems[flag].total = shoppCart.CartItems[flag].price * shoppCart.CartItems[flag].count;
            }


            for (int k = 0; k < shoppCart.CartItems.Count; k++)
            {
                cartTotal += shoppCart.CartItems[k].total;
            }
            shoppCart.CartTotal = cartTotal;
            ClassExten.UpdateCookiesCart(JsonConvert.SerializeObject(shoppCart));
            return Redirect("/Gio-hang");
        }
        #endregion

        #region[Addcart chi tiet]
        public ActionResult Addcart(string id)
        {
            string prokey = id;
            int soluong = 1;
            if (Request["number"] != null)
            {
                soluong = int.Parse(Request["number"].ToString());
            }
            var obj = db.Products.First(u => u.id.Equals(id));
            int flag = -1;
            int gia = obj.proPrice_sale.Value;

            ShoppingCartViewModel shoppCart;
            var cartGet = ClassExten.GetCokiesCart();
            if (cartGet == null)
            {
                shoppCart = new ShoppingCartViewModel();
                ClassExten.CreateCookiesCart(string.Empty);
            }
            else
            {
                shoppCart = cartGet;
            }
            if (GetCartItem(shoppCart, obj.id) == flag)
            {
                var cartItem = new Cart
                {
                    productId = obj.id,
                    productName = obj.pro_name,
                    productImg = obj.proAvata,
                    price = gia,
                    count = soluong,
                    total = soluong * gia,
                    key = obj.pro_key
                };
                shoppCart.CartItems.Add(cartItem);
            }
            else
            {
                flag = GetCartItem(shoppCart, obj.id);
                shoppCart.CartItems[flag].count += soluong;
                shoppCart.CartItems[flag].total = shoppCart.CartItems[flag].price * shoppCart.CartItems[flag].count;
            }


            for (int k = 0; k < shoppCart.CartItems.Count; k++)
            {
                cartTotal += shoppCart.CartItems[k].total;
            }
            shoppCart.CartTotal = cartTotal;
            ClassExten.UpdateCookiesCart(JsonConvert.SerializeObject(shoppCart));
            return Redirect("/Gio-hang");
        }
        #endregion


        #region[remo cart]
        public ActionResult RemoveFromCart(string id)
        {
            try
            {

                ShoppingCartViewModel shoppCart;
                var cartGet = ClassExten.GetCokiesCart();
                if (cartGet == null)
                {
                    shoppCart = new ShoppingCartViewModel();
                    ClassExten.CreateCookiesCart(string.Empty);
                }
                else
                {
                    shoppCart = cartGet;
                }


                for (int i = 0; i < shoppCart.CartItems.Count; i++)
                {
                    if (shoppCart.CartItems[i].productId == id)
                    {
                        shoppCart.CartItems.RemoveAt(i);
                        break;
                    }
                }
                if (shoppCart.CartItems.Count > 0)
                {
                    for (int j = 0; j < shoppCart.CartItems.Count; j++)
                    {
                        cartTotal += shoppCart.CartItems[j].total;
                    }
                    shoppCart.CartTotal = cartTotal;
                }
                else
                {
                    shoppCart.CartTotal = 0;
                }

                ClassExten.UpdateCookiesCart(JsonConvert.SerializeObject(shoppCart));
                return Json(new { ok = true, mess = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { ok = false, mess = "" }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region[update count item]
        public ActionResult UpdateCartCountItem(string id, int cartCount)
        {
            try
            {

                ShoppingCartViewModel shoppCart;
                var cartGet = ClassExten.GetCokiesCart();
                if (cartGet == null)
                {
                    shoppCart = new ShoppingCartViewModel();
                    ClassExten.CreateCookiesCart(string.Empty);
                }
                else
                {
                    shoppCart = cartGet;
                }
                for (int i = 0; i < shoppCart.CartItems.Count; i++)
                {
                    if (shoppCart.CartItems[i].productId == id)
                    {
                        if (cartCount == 0 || cartCount < 0)
                        {
                            cartCount = 1;
                        }
                        shoppCart.CartItems[i].count = cartCount;

                        shoppCart.CartItems[i].total = Convert.ToInt32(shoppCart.CartItems[i].price) * cartCount;
                        break;
                    }
                }
                for (int j = 0; j < shoppCart.CartItems.Count; j++)
                {
                    cartTotal += shoppCart.CartItems[j].total;
                }
                shoppCart.CartTotal = cartTotal;
                ClassExten.UpdateCookiesCart(JsonConvert.SerializeObject(shoppCart));
                return Json(new { ok = true, mess = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { ok = false, mess = "" }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region[get cart]
        private int GetCartItem(ShoppingCartViewModel cart, string key)
        {
            int found = -1;
            if (cart.CartItems.Count > 0)
            {
                for (int i = 0; i < cart.CartItems.Count; i++)
                {
                    if (cart.CartItems[i].productId == key)
                    {
                        found = i;
                    }
                }
            }
            return found;
        }
        #endregion

        public ActionResult Dathang(Customer model)
        {
            ShoppingCartViewModel shoppCart;
            var cartGet = ClassExten.GetCokiesCart();
            if (cartGet == null)
            {
                return Json(new { ok = 2, mess = "Lỗi không tìm thấy giỏ hàng" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                shoppCart = cartGet;
            }

            model.id = Guid.NewGuid().ToString();
            model.active = true;
            model.type = 1;
            Config conf;
            if (ConfigModel.listConfig == null)
            {
                ConfigModel.listConfig = db.Configs.ToList();
            }
            conf = ConfigModel.listConfig.FirstOrDefault();

            string host = Request.Url.Host;
            Oder or = new Oder();
            Oderdt dt;
            var content = "<p><b>Danh sách sản phẩm đơn hàng:</b></p>";
            string cssth = "style='padding-top:12px;padding-bottom:12px;text-align:left; background-color: #4CAF50;color: white; padding: 8px; '";
            string csstd = "style='border: 1px solid #ddd; padding: 8px; '";
            var list = shoppCart;
            if (list.CartItems.Count > 0)
            {
                try
                {
                    content += "<table style='border-collapse: collapse; width: 100%'>";
                    content += "<tr>";
                    content += "<th " + cssth + ">Sản phẩm</th>";
                    content += "<th " + cssth + ">Số lượng</th>";
                    content += "<th " + cssth + ">Giá</th>";
                    content += "<th " + cssth + ">Tổng tiền</th>";
                    content += "</tr>";
                    string email = model.email;

                    var cus = db.Customers.FirstOrDefault(u => u.email == email);

                    or.createDate = DateTime.Now;
                    or.id = Guid.NewGuid().ToString();
                    or.cusId = cus.id;
                    or.total = list.CartTotal;
                    // or.noteSite = model.noteSite;
                    or.noteSite = string.Empty;
                    or.status = ClassExten.Bill_Status.MoiTao;
                    or.priceShip = 0;
                    or.createDate = DateTime.Now;

                    db.Customers.Add(model);
                    or.cusId = model.id;
                    db.Oders.Add(or);

                    var hostUrl = Request.Url.Host;
                    string title = "Thông tin đơn hàng tại " + hostUrl;
                    string titleAdmin = "Thông tin đơn hàng từ khách: " + model.fullName + " tại " + hostUrl;
                    for (int i = 0; i < list.CartItems.Count; i++)
                    {


                        dt = new Oderdt();
                        dt.id = Guid.NewGuid().ToString();
                        dt.oderId = or.id;
                        dt.proId = list.CartItems[i].productId;
                        dt.quantity = list.CartItems[i].count;
                        dt.priceNow = list.CartItems[i].price;
                        dt.priceCount = list.CartItems[i].price * list.CartItems[i].count;

                        content += "<tr>";
                        content += "<td " + csstd + "><a href='http://" + hostUrl + "/chi-tiet/" + list.CartItems[i].key + "' title='" + list.CartItems[i].productName + "'>" + list.CartItems[i].productName + "</a></td>";
                        content += "<td " + csstd + ">" + list.CartItems[i].count + "</td>";
                        content += "<td " + csstd + ">" + list.CartItems[i].price.ToString("N0") + "</td>";
                        content += "<td " + csstd + ">" + dt.priceCount.Value.ToString("N0") + "</td>";
                        content += "</tr>";
                        db.Oderdts.Add(dt);
                    }
                    db.SaveChanges();

                    if (Request.Cookies["cartNTS"] != null)
                    {
                        var c = new HttpCookie("cartNTS");
                        c.Expires = DateTime.Now.AddDays(-1);
                        Response.Cookies.Add(c);
                    }

                    content += "</table>";
                    ClassExten.SendMail(model.email, conf.email_Send, conf.emailPass, conf.mail_Port.Value, title, content);
                    ClassExten.SendMail(conf.email_Inbox, conf.email_Send, conf.emailPass, conf.mail_Port.Value, titleAdmin, content);


                    return Json(new { ok = 1, mess = "" }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception)
                {
                    return Json(new { ok = 0, mess = "" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { ok = 2, mess = "" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
