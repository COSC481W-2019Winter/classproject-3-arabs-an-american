using Authentication2.DataAccessLayer;
using Authentication2.Models;
using Authentication2.VIewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Authentication2.Mail;
using Authentication2.Identity;
using Microsoft.AspNetCore.Identity;

namespace Authentication2.Areas.Driver.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AccountsController : Controller
    {
    
        private readonly MyProductionDbContext _context;
        private readonly UserManager<MyIdentityUser> _userManager;

        public AccountsController(MyProductionDbContext context, UserManager<MyIdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult RequestDriver()
        {
            List<MyIdentityUser> users = _context.GetRequestedDrivers();

            List<UserViewModel> accountsView = new List<UserViewModel> { };
            foreach (MyIdentityUser user in users)
            {
                accountsView.Add(new UserViewModel(user));
            }
            return View(accountsView);
        }

        public IActionResult Approve(string id)
        {
            MyIdentityUser user = _context.GetUser(id);
            user.DriverStatus = "Accepted";
            IdentityResult roleResult = _userManager.AddToRoleAsync(user, "Driver").Result;
            _context.Update(user);
            _context.SaveChanges();

            var subject = "Request to Become Driver Accepted";
            var message = "Congratulations, your request to become a driver has been accepted!";
            new Mailer().SendMail(subject, user.Email, message);

            return RedirectToAction("RequestDriver");
        }

        public IActionResult Deny(string id)
        {
            MyIdentityUser user = _context.GetUser(id);

            user.DriverStatus = "Denied";

            _context.Update(user);
            _context.SaveChanges();

            var subject = "Request to Become Driver Denied";
            var message = "We are sorry, but your request to become a driver has been denied.";
            new Mailer().SendMail(subject, user.Email, message);

            return RedirectToAction("RequestDriver");
        }
    }
}
