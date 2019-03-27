using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebShop.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebShop.Controllers
{
    public class CatagoryController : Controller
    {
        [HttpGet]
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Catagory(int Id)
        {
            WebShopContext context = HttpContext.RequestServices.GetService(typeof(WebShopContext)) as WebShopContext;
            ViewBag.catagory = context.getCatagoryFromId(Id);
            ViewBag.Products = context.getProductsInCatagory(Id);
            return View();
        }
    }
        
}
