using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebShop.Models;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;

namespace WebShop.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            System.Security.Claims.ClaimsPrincipal currentUser = this.User;

            //check admin
            bool IsAdmin = currentUser.IsInRole("Admin");
            ViewBag.IsAdmin = IsAdmin;
            WebShopContext context = HttpContext.RequestServices.GetService(typeof(WebShopContext)) as WebShopContext;
            ViewBag.Products = (context.GetAllProducts());
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
