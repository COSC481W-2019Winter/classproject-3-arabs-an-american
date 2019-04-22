using Authentication2.DataAccessLayer;
using Authentication2.Identity;
using Authentication2.VIewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace Authentication2.Controllers
{
    public class AccountsController : Controller
    {
        private readonly UserManager<MyIdentityUser> _userManager;
        private readonly SignInManager<MyIdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly MyProductionDbContext _identityContext;

        public AccountsController(UserManager<MyIdentityUser> userManager,
            SignInManager<MyIdentityUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<MyIdentityUser> signinManager,
            MyProductionDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _identityContext = context;
        }

        public IActionResult Index()
        {
            var user = _identityContext.Users
                .Where(x => x.Id == User.FindFirstValue(ClaimTypes.NameIdentifier))
                .Include(x => x.Address).ToList().ElementAt(0);

            UserViewModel userViewModel = new UserViewModel(user);
            return View("Index", userViewModel);
        }

        public IActionResult Update(UserViewModel user)
        {
            return View(user);
        }

        public IActionResult UpdateUser(UserViewModel user)
        {
            var identityUser = _identityContext.Users
                .Where(x => x.Id == User.FindFirstValue(ClaimTypes.NameIdentifier))
                .Include(x => x.Address).ToList().ElementAt(0);

            identityUser.Address.StreetNumber = user.Address.StreetNumber;
            identityUser.Address.StreetName = user.Address.StreetName;
            identityUser.Address.City = user.Address.City;
            identityUser.Address.State = user.Address.State;
            identityUser.Address.ZipCode = user.Address.ZipCode;

            identityUser.PhoneNumber = user.Phone;
            identityUser.Email = user.Email;

            _identityContext.Update(identityUser);
            _identityContext.SaveChanges();

            return RedirectToAction("Index");
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
                    if (roles.Contains("Driver"))
                    {
                        if (!_identityContext.CheckActive(user.Id))
                        {
                            return RedirectToAction("Open", "Request", new { area = "Driver" });
                        }
                        else
                        {
                            return RedirectToAction("AcceptedRequests", "Request", new { area = "Driver" });
                        }
                    }
                    else if (roles.Contains("Admin"))
                    {
                        return RedirectToAction("RequestDriver", "Accounts", new { area = "Admin" });
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

                    user = _userManager.FindByNameAsync(signUpViewModel.Username).Result;
                    Address address = _identityContext.Addresses
                         .Where(x => x.Id == user.AddressId)
                         .FirstOrDefault();

                    address.UserId = user.Id;

                    _identityContext.Update<Address>(address);
                    _identityContext.SaveChanges();

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
            string driverStatus = _identityContext.GetUser(User.FindFirstValue(ClaimTypes.NameIdentifier)).DriverStatus;
            ViewData["DriverStatus"] = driverStatus;
            return View();        }

        [HttpPost]
        public IActionResult BecomeDriver(BecomeDriverViewModel becomeDriverViewModel)
        {
            MyIdentityUser user = _userManager.FindByNameAsync(User.Identity.Name).Result;
            user.DriverStatus = "Pending";
            user.DriversLicense = becomeDriverViewModel.DriversLicense;
            user.CarMake = becomeDriverViewModel.CarMake;
            user.CarModel = becomeDriverViewModel.CarModel;
            user.CarYear = becomeDriverViewModel.CarYear;
            user.CarColor = becomeDriverViewModel.CarColor;
            user.LicensePlate = becomeDriverViewModel.CarLicensePlate;
            //IdentityResult roleResult = _userManager.AddToRoleAsync(user, "Driver").Result;
            IdentityResult updateResult = _userManager.UpdateAsync(user).Result;
            _signInManager.SignOutAsync().Wait();
            _signInManager.SignInAsync(user, true).Wait();
            return RedirectToAction("List", "Request", new { area = "User" });
        }
    }
}