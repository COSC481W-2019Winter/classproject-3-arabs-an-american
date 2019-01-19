using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Authentication2.Models;
using Microsoft.AspNetCore.Identity;
using Authentication2.Identity;

namespace Authentication2.Controllers
{
    public class HomeController : Controller
    {
        UserManager<MyIdentityUser> _manager;

        public HomeController(UserManager<MyIdentityUser> manager)
        {
            _manager = manager;
        }

        public IActionResult Index()
        {
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
