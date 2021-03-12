using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Productgold.Models
{
    public class ShoppingCartViewModel
    {
        public List<Cart> CartItems { get; set; }
        public double CartTotal { get; set; }

        public ShoppingCartViewModel()
        {
            this.CartItems = new List<Cart>();
        }

    }
}