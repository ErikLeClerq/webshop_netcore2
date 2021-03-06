﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebShop.Models;
using System.Web;
using WebShop.Helpers;
using Microsoft.AspNetCore.Http;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebShop.Controllers
{
    public class ShoppingCart : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
          
        var cart = HttpContext.Session.GetObjectFromJson<List<ShoppingCartItem>>("cart");
        ViewBag.cart = cart;
        if (ViewBag.cart != null)
        {
         ViewBag.total = cart.Sum(ShoppingCartItem => ShoppingCartItem.Product.Price * ShoppingCartItem.Quantity);
        }
        else
        {
         ViewBag.total = 0;
        }
            return View();
          
           
        }
        
        //[Route("buy/{id}")]
        public IActionResult Buy(string id)
        {
            WebShopContext context = HttpContext.RequestServices.GetService(typeof(WebShopContext)) as WebShopContext;
          
            if (SessionHelper.GetObjectFromJson<List<ShoppingCartItem>>(HttpContext.Session, "cart") == null)
            {
                List<ShoppingCartItem> cart = new List<ShoppingCartItem>();
                cart.Add(new ShoppingCartItem { Product =  context.GetProductFromId(id), Quantity = 1, Price = context.GetProductFromId(id).Price });
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            else
            {
                List<ShoppingCartItem> cart = SessionHelper.GetObjectFromJson<List<ShoppingCartItem>>(HttpContext.Session, "cart");
                int index = isExist(id);
                if (index != -1)
                {
                    cart[index].Quantity++;
                    cart[index].Price += cart[index].Price;
                }
                else
                {
                    cart.Add(new ShoppingCartItem { Product = context.GetProductFromId(id), Quantity = 1 });
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            return RedirectToAction("Index");
        }

        public IActionResult Sell(string id)
        {
            WebShopContext context = HttpContext.RequestServices.GetService(typeof(WebShopContext)) as WebShopContext;

            if (SessionHelper.GetObjectFromJson<List<ShoppingCartItem>>(HttpContext.Session, "cart") != null)
            {
                List<ShoppingCartItem> cart = SessionHelper.GetObjectFromJson<List<ShoppingCartItem>>(HttpContext.Session, "cart");
                int index = isExist(id);
                if (index != -1)
                {
                    if (cart[index].Quantity == 1 )
                    {
                        cart.Remove(cart[index]);
                    }
                    else { 
                    cart[index].Quantity--;
                    cart[index].Price -= cart[index].Price;
                    }
                }

                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
       
            return RedirectToAction("Index");
        }


        private int isExist(string id)
        {
            List<ShoppingCartItem> cart = SessionHelper.GetObjectFromJson<List<ShoppingCartItem>>(HttpContext.Session, "cart");
            foreach(ShoppingCartItem shoppingCart in cart)
            {
                    if(shoppingCart.Product.Id.ToString() == id)
                    {
                    return 0;
                    }

            }
            return -1;
        }
           
   }

    
}
