﻿using Authentication2.DataAccessLayer;
using Authentication2.Identity;
using Authentication2.VIewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace Authentication2.Controllers
{
    public class AccountsController : Controller
    {

        UserManager<MyIdentityUser> _userManager;
        SignInManager<MyIdentityUser> _signInManager;
        RoleManager<IdentityRole> _roleManager;
        MyIdentityContext _identityContext;

        public AccountsController(UserManager<MyIdentityUser> userManager,
            SignInManager<MyIdentityUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            MyIdentityContext identityContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _identityContext = identityContext;
        }
        public async Task<IActionResult> IndexAsync()
        {
            AccountsViewModel accountsViewModel = new AccountsViewModel();
            //accountsViewModel.Accounts. = _identityContext.Users.ToList();
            var users = _identityContext.Users.ToList();
            foreach(var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var role = roles.First();
                accountsViewModel.Accounts.Add(new Account { Username=user.UserName,
                Password = user.Password,
                Role =  role
                });
            }
            return View("Index", accountsViewModel);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginAsync(string username, string password)
        {
            MyIdentityUser user = await _userManager.FindByNameAsync(username);
            SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, password, false);

            if (result.Succeeded)
            {
                var roles = await _userManager.GetRolesAsync(user);
                string role;
                if (roles.Count != 0)
                    role = roles.First().ToString();
                else
                    role = "None";
                return Content("You are now logged in as " + user.UserName + " your role is " + role.ToString());
            }
            else
            {
                return Content("Failed to login");
            }

        }

        public IActionResult RegisterDriver()
        {
            return View();
        }

        public IActionResult RegisterUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUserAsync(UserViewModel userViewModel)
        {
            var user = new MyIdentityUser
            {
                UserName = userViewModel.Username,
                Address = userViewModel.Address,
                Password = userViewModel.Password

            };
            IdentityResult result = await _userManager.CreateAsync(
               user, userViewModel.Password);

            if (!await _roleManager.RoleExistsAsync("User"))
            {
                await _roleManager.CreateAsync(new IdentityRole("User"));
            }
            var roleResult = await _userManager.AddToRoleAsync(user, "User");

            if (result.Succeeded)
                return Content("User Account created");
            else
                return Content(result.Errors.First().Description);
        }

        [HttpPost]
        public async Task<IActionResult> RegisterDriverAsync(DriverViewModel driverViewModel)
        {
            var user = new MyIdentityUser
            {
                UserName = driverViewModel.Username,
                LicensePlate = driverViewModel.LicensePlate,
                Password = driverViewModel.Password

            };
            IdentityResult result = await _userManager.CreateAsync(
               user, driverViewModel.Password);

            if (!await _roleManager.RoleExistsAsync("Driver"))
            {
                await _roleManager.CreateAsync(new IdentityRole("Driver"));
            }
            var roleResult = await _userManager.AddToRoleAsync(user, "Driver");

            if (result.Succeeded)
                return Content("Driver Account created");
            else
                return Content(result.Errors.First().Description);
        }
    }
}