using Authentication2.DataAccessLayer;
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

        private readonly UserManager<MyIdentityUser> _userManager;
        private readonly SignInManager<MyIdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly MyIdentityContext _identityContext;

        public AccountsController(UserManager<MyIdentityUser> userManager,
            SignInManager<MyIdentityUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<MyIdentityUser> signinManager,
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
            var users = _identityContext.Users.ToList();
            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var role = roles.First();
                accountsViewModel.Accounts.Add(new Account
                {
                    Username = user.UserName,
                    Password = user.Password,
                    Role = role
                });
            }
            return View("Index", accountsViewModel);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            MyIdentityUser user = await _userManager.FindByNameAsync(username);
            if (user != null)
            {
                SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
                if (result.Succeeded)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    string role;
                    if (roles.Count != 0)
                        role = roles.First().ToString();
                    else
                        role = "None";
                    _signInManager.SignInAsync(user, true).Wait();
                    if (roles.Contains("Driver")){
                        return RedirectToAction("Open", "Request", new { area = "Driver" });
                    }
                    else
                    {
                        return RedirectToAction("List", "Request", new { area = "User" });
                    }
                }
                //TODO: dont return content
                else
                {
                    return Content("Failed to login");
                }
            }
            //TODO: dont return content
            else
            {
                return Content("Failed to login. User doesnt exist");
            }

        }


        public IActionResult Signup()
        {
            return View();
        }

        public IActionResult Logout()
        {
            _signInManager.SignOutAsync().Wait();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult Signup(SignUpViewModel signUpViewModel)
        {
            MyIdentityUser user = new MyIdentityUser()
            {
                UserName = signUpViewModel.Username,
                Password = signUpViewModel.Password,
                Email = signUpViewModel.Email,
                Address = signUpViewModel.Address,
                PhoneNumber = signUpViewModel.PhoneNumber
            };

            IdentityResult result = _userManager.CreateAsync(user, signUpViewModel.Password).Result;
            if (result.Succeeded)
            {
                IdentityResult roleResult = _userManager.AddToRoleAsync(user, "User").Result;
                if (roleResult.Succeeded)
                {
                    _signInManager.SignInAsync(user, true);
                    return RedirectToAction("List", "Request", new { area = "User" });
                }
                else
                {
                    return Content("Adding role failed: " + roleResult.Errors.First().Description);
                }
            }
            else
            {
                return Content("User account creation failed: " + result.Errors.First().Description);
            }
        }

        public IActionResult BecomeDriver()
        {
            return View();
        }

        [HttpPost]
        public IActionResult BecomeDriver(BecomeDriverViewModel becomeDriverViewModel)
        {
            MyIdentityUser user = _userManager.FindByNameAsync(User.Identity.Name).Result;
            user.CarMake = becomeDriverViewModel.CarMake;
            user.CarModel = becomeDriverViewModel.CarModel;
            user.CarYear = becomeDriverViewModel.CarYear;
            user.CarColor = becomeDriverViewModel.CarColor;
            user.LicensePlate = becomeDriverViewModel.CarLicensePlate;
            IdentityResult roleResult = _userManager.AddToRoleAsync(user, "Driver").Result;
            IdentityResult updateResult = _userManager.UpdateAsync(user).Result;
            _signInManager.SignOutAsync().Wait();
            _signInManager.SignInAsync(user, true).Wait();
            return RedirectToAction("List", "Request", new { area = "User" });
        }
    }
}