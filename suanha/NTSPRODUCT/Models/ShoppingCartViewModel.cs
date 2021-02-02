using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NTSPRODUCT.Models
{
    public class ShoppingCartViewModel
    {
        public List<Cart> CartItems { get; set; }
        public int CartTotal { get; set; }

        public ShoppingCartViewModel()
        {
            this.CartItems = new List<Cart>();
        }

    }
}