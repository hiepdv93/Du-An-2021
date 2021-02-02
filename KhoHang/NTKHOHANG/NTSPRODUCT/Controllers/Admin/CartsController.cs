using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
using System.Net;
using NTSPRODUCT.Models;

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

            if (Session["ShoppingCart"] != null)
            {
                if (Session["KhohangUser"] != null)
                {
                    string email = Session["KhohangUser"].ToString();
                    var cus = db.Customers.FirstOrDefault(u=>u.email.Equals(email));
                    ViewBag.cusInfo = cus;
                }
                var cart = (ShoppingCartViewModel)Session["ShoppingCart"];
                return View(cart);
            }
            else
            {
                return Redirect("/danh-muc");
            }

        }

        public ActionResult GetList()
        {
            if (Session["ShoppingCart"] != null)
            {
                var cart = (ShoppingCartViewModel)Session["ShoppingCart"];
                return PartialView(cart);
            }
            else
            {
                return PartialView();
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
            if (Session["ShoppingCart"] == null)
            {
                InitShoppingCartSession();
                Session.Timeout = 60;
            }
            ShoppingCartViewModel shoppCart = (ShoppingCartViewModel)Session["ShoppingCart"];

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
            Session["ShoppingCart"] = shoppCart;
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
            if (Session["ShoppingCart"] == null)
            {
                InitShoppingCartSession();
                Session.Timeout = 60;
            }
            ShoppingCartViewModel shoppCart = (ShoppingCartViewModel)Session["ShoppingCart"];

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
            Session["ShoppingCart"] = shoppCart;
            return Redirect("/Gio-hang");
        }
        #endregion

        #region[shop cart]
        public void InitShoppingCartSession()
        {
            var shoppingCart = new ShoppingCartViewModel();
            Session["ShoppingCart"] = shoppingCart;
        }
        #endregion

        #region[remo cart]
        public ActionResult RemoveFromCart(string id)
        {
            try
            {
                ShoppingCartViewModel shoppCart = (ShoppingCartViewModel)Session["ShoppingCart"];
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

                Session["ShoppingCart"] = shoppCart;
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

                ShoppingCartViewModel shoppCart = (ShoppingCartViewModel)Session["ShoppingCart"];
                for (int i = 0; i < shoppCart.CartItems.Count; i++)
                {
                    if (shoppCart.CartItems[i].productId == id)
                    {
                        if (cartCount == 0)
                        {
                            cartCount = 1;
                        }
                        else
                            if (cartCount < 0)
                        {

                            cartCount = Math.Abs(cartCount);
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
                Session["ShoppingCart"] = shoppCart;
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
        public ActionResult Dathang(Oder model)
        {
            Config conf;
            if (ConfigModel.listConfig == null)
            {
                ConfigModel.listConfig = db.Configs.ToList();
            }
            conf = ConfigModel.listConfig.FirstOrDefault(u => u.conLang.Equals(lang));

            string host = Request.Url.Host;
            Oder or = new Oder();
            Oderdt dt;
            //  var content = "<p><b>Danh sách sản phẩm đơn hàng:</b></p>";
            // string cssth = "style='padding-top:12px;padding-bottom:12px;text-align:left; background-color: #4CAF50;color: white; padding: 8px; '";
            // string csstd = "style='border: 1px solid #ddd; padding: 8px; '";
            var list = (ShoppingCartViewModel)Session["ShoppingCart"];
            if (list.CartItems.Count > 0 && Session["KhohangUser"] != null)
            {
                try
                {
                    //content += "<table style='border-collapse: collapse; width: 100%'>";
                    //content += "<tr>";
                    //content += "<th " + cssth + ">Sản phẩm</th>";
                    //content += "<th " + cssth + ">Số lượng</th>";
                    //content += "<th " + cssth + ">Giá</th>";
                    //content += "<th " + cssth + ">Tổng tiền</th>";
                    //content += "</tr>";
                    string email = Session["KhohangUser"].ToString();

                    var cus = db.Customers.FirstOrDefault(u => u.email == email);

                    or.createDate = DateTime.Now;
                    or.id = Guid.NewGuid().ToString();
                    or.cusId = cus.id;
                    or.realMoney = list.CartTotal;
                    or.totalMoney = list.CartTotal;
                    or.noteSite = model.noteSite;
                    or.noteSite = string.Empty;
                    or.status = ClassExten.Bill_Status.MoiTao;
                    or.priceShip = 0;
                    or.createDate = DateTime.Now;
                    db.Oders.Add(or);

                    var hostUrl = Request.Url.Host;
                    // string title = "Thông tin đơn hàng tại " + hostUrl;
                    //  string titleAdmin = "Thông tin đơn hàng từ khách: " + model.fullName + " tại " + hostUrl;
                    for (int i = 0; i < list.CartItems.Count; i++)
                    {


                        dt = new Oderdt();
                        dt.id = Guid.NewGuid().ToString();
                        dt.oderId = or.id;
                        dt.proId = list.CartItems[i].productId;
                        dt.quantity = list.CartItems[i].count;
                        dt.priceNow = list.CartItems[i].price;
                        dt.priceCount = list.CartItems[i].price * list.CartItems[i].count;

                        //content += "<tr>";
                        //content += "<td " + csstd + "><a href='http://" + hostUrl + "/chi-tiet/" + list.CartItems[i].key + "' title='" + list.CartItems[i].productName + "'>" + list.CartItems[i].productName + "</a></td>";
                        //content += "<td " + csstd + ">" + list.CartItems[i].count + "</td>";
                        //content += "<td " + csstd + ">" + list.CartItems[i].price.ToString("N0") + "</td>";
                        //content += "<td " + csstd + ">" + dt.priceCount.Value.ToString("N0") + "</td>";
                        //content += "</tr>";

                        db.Oderdts.Add(dt);
                    }
                    db.SaveChanges();
                    Session["ShoppingCart"] = null;

                    //content += "</table>";
                    //ClassExten.SendMail(model.email, conf.email_Send, conf.emailPass, conf.mail_Port.Value, title, content);
                    //ClassExten.SendMail(conf.email_Inbox, conf.email_Send, conf.emailPass, conf.mail_Port.Value, titleAdmin, content);


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
