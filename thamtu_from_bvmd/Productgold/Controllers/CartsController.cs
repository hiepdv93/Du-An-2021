using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Productgold.Models;
using System.Net.Mail;
using System.Net;
namespace Productgold.Controllers
{
    public class CartsController : Controller
    {
        //
        // GET: /Carts/
        WEBTPAEntities db = new WEBTPAEntities();

        double cartTotal;

        #region[index view]
        public ActionResult Index()
        {
            ViewBag.NavTrangchu = ClassExten.GetLangKey("home");
            ViewBag.key_giohang = ClassExten.GetLangKey("key_giohang");
            
            if (Session["ShoppingCart"] != null)
            {
                var cart = (ShoppingCartViewModel)Session["ShoppingCart"];
                return View(cart);
            }
            else
            {
                return Redirect("/San-pham");
            }

        }
        #endregion

        #region[Addcart ngoai site]
        public ActionResult Addcarts(string id)
        {
            int soluong = 1;
            var obj = db.Products.First(u => u.pro_key == id);
            ///float gia = Convert.ToInt32(obj.price_sale);
            float tong = (soluong);
            int flag = -1;
            if (Session["ShoppingCart"] == null)
            {
                InitShoppingCartSession();
            }
            ShoppingCartViewModel shoppCart = (ShoppingCartViewModel)Session["ShoppingCart"];

            if (GetCartItem(shoppCart, obj.id) == flag)
            {
                var cartItem = new Cart
                {
                    productId = obj.id,
                    productName = obj.pro_name,
                    //productImg = obj.images,
                    // price = float.Parse(obj.price_sale.ToString()),
                    count = soluong,
                    total = tong
                };
                shoppCart.CartItems.Add(cartItem);
            }
            else
            {
                flag = GetCartItem(shoppCart, obj.id);
                shoppCart.CartItems[flag].count += soluong;
                shoppCart.CartItems[flag].total = Convert.ToInt32(shoppCart.CartItems[flag].price) * shoppCart.CartItems[flag].count;
            }


            for (int k = 0; k < shoppCart.CartItems.Count; k++)
            {
                cartTotal += shoppCart.CartItems[k].total;
            }
            shoppCart.CartTotal = cartTotal;
            Session["ShoppingCart"] = shoppCart;
            return RedirectToAction("Index", "Carts");
        }
        #endregion

        #region[Addcart chi tiet]
        public ActionResult Addcart(string id)
        {
            string prokey = id;
            int soluong = 1;
            var obj = db.Products.First(u => u.pro_key == prokey);
            int flag = -1;
            double gia = double.Parse(obj.proPrice_sale.ToString());
            if (Session["ShoppingCart"] == null)
            {
                InitShoppingCartSession();
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
        [HttpPost]
        public void RemoveFromCart(int id)
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
        }
        #endregion

        #region[update count item]
        [HttpPost]
        public void UpdateCartCountItem(int id, int cartCount)
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

        }
        #endregion

        #region[get cart]
        private int GetCartItem(ShoppingCartViewModel cart, int key)
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

        public void Guimail(Config configmail, string mailnhan, string tieude,string noidung)
        {
            try
            {
                #region[code gui mail]
                MailMessage mailsend = new MailMessage();
                mailsend.To.Add(mailnhan);
                mailsend.From = new MailAddress(configmail.shopemail);

                mailsend.Subject = tieude;
                mailsend.Body = noidung;
                mailsend.IsBodyHtml = true;

                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                client.EnableSsl = true;
                NetworkCredential credentials = new NetworkCredential(configmail.shopemail, configmail.emailpass);

                client.Credentials = credentials;
                client.Send(mailsend);
                #endregion
            }
            catch (Exception)
            { }
        }

        #region[dat hang]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Dathang(FormCollection fr)
        {
            string host = Request.Url.Host;
            var xx = HttpContext.Request.Url.AbsoluteUri;
            Oder or = new Oder();
            Oderdt dt = new Oderdt();

            var list = (ShoppingCartViewModel)Session["ShoppingCart"];
            if (list.CartItems.Count > 0)
            {
                or.cusid = null;
                or.statust = 0;
                or.siteTPA = ClassExten.siteOption;
                or.createdate = DateTime.Now;
                or.total = list.CartTotal;
                or.fullName = fr["name"];
                or.email = fr["email"];
                or.phone = fr["phone"];
                or.Address = fr["adds"];
                or.oLang = fr["note"];
                db.Oders.Add(or);
                db.SaveChanges();

                var dhnew = (from n in db.Oders orderby n.id descending select n).Take(1).ToList();
                int iddhnew = dhnew[0].id;
                string spname = "<p>Danh sách sản phẩm đặt hàng:</p>";
                for (int i = 0; i < list.CartItems.Count; i++)
                {
                    spname += "<a href=''";
                    spname += list.CartItems[i].productName + ", ";
                    dt.oderid = iddhnew;
                    dt.proid = list.CartItems[i].productId;
                    dt.quantity = list.CartItems[i].count;
                    db.Oderdts.Add(dt);
                    db.SaveChanges();
                 
                }
                var configmail = db.Configs.FirstOrDefault();
                Guimail(configmail,or.email,"Bạn đã đặt hàng thành công sản phẩm trên trang "+ host, spname);
                Guimail(configmail,configmail.Email_Inbox,"Bạn "+or.fullName+ " đã đặt hàng thành công sản phẩm trên trang "+ host, spname);
                Session["ShoppingCart"] = null;
                return Redirect("/");
            }
            else
            {
                return Redirect("/San-pham");
            }
        }
        #endregion

    }
}
