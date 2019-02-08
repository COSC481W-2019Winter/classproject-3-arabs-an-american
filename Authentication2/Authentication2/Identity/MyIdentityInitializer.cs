using Microsoft.AspNetCore.Identity;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Authentication2.Identity
{
    public class MyIdentityInitializer
    {
        public static void SeedData(UserManager<MyIdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);
        }

        public static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
       
            if (!roleManager.RoleExistsAsync("User").Result)
            {
                IdentityResult result = roleManager.CreateAsync(new IdentityRole("User")).Result;
            }
            if (!roleManager.RoleExistsAsync("Driver").Result)
            {
                IdentityResult result = roleManager.CreateAsync(new IdentityRole("Driver")).Result;
            }
        }

        public static void SeedUsers(UserManager<MyIdentityUser> userManager)
        {
            if (userManager.FindByNameAsync("zafer").Result == null)
            {
                MyIdentityUser zafer = new MyIdentityUser() {
                    UserName = "zafer",
                    Email = "khourdaji@gmail.com",
                    Address = new Address {
                        StreetName = "main st",
                        StreetNumber = "123",
                        City = "Ann Arbor",
                        State = "MI",
                        ZipCode = 48103
                    },
                    Password = "zafer"
                };

                IdentityResult zaferResult = userManager.CreateAsync(zafer, zafer.Password).Result;
                if (zaferResult.Succeeded)
                {
                    IdentityResult zaferRoleResult = userManager.AddToRoleAsync(zafer, "User").Result;
                    if (zaferRoleResult.Succeeded)
                    {
                        Debug.WriteLine(zafer.UserName + " User created successfuly");
                    }
                }


                MyIdentityUser anas = new MyIdentityUser()
                {
                    UserName = "anas",
                    Email = "anas@gmail.com",
                    Address = new Address
                    {
                        StreetName = "main st",
                        StreetNumber = "123",
                        City = "Ypsilanti",
                        State = "MI",
                        ZipCode = 48197
                    },
                    Password = "anas"
                };

                IdentityResult anasResult = userManager.CreateAsync(anas, anas.Password).Result;
                if (anasResult.Succeeded)
                {
                    IdentityResult anasRoleResult = userManager.AddToRoleAsync(anas, "Driver").Result;
                    if (anasRoleResult.Succeeded)
                    {
                        Debug.WriteLine(anas.UserName + " Driver created successfuly");
                    }
                }

            }
        }
        
    }
}
