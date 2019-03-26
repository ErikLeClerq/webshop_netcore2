﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebShop.Models
{
    public class ShoppingCartItem
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal Price  { get; set; }
}

}
