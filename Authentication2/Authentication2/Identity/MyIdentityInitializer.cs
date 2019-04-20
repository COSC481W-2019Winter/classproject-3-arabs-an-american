using Microsoft.AspNetCore.Identity;
using System.Diagnostics;
using Authentication2.DataAccessLayer;
using Authentication2.Models;

namespace Authentication2.Identity
{
    public class MyIdentityInitializer
    {
        public static void SeedData(
        UserManager<MyIdentityUser> userManager, 
            RoleManager<IdentityRole> roleManager,
            MyProductionDbContext context
            )
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);
            SeedRequests(context, userManager);
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
                MyIdentityUser zafer = new MyIdentityUser
                {
                    UserName = "zafer",
                    Email = "khourdaji@gmail.com",
                    Address = new Address
                    {
                        StreetName = "main st",
                        StreetNumber = "123",
                        City = "Ann Arbor",
                        State = "MI",
                        ZipCode = 48103
                    },
                    Password = "zafer",
                    PhoneNumber = "7349254343"

                };

                IdentityResult zaferResult = userManager.CreateAsync(zafer, zafer.Password).Result;
                IdentityResult zaferRoleResult = userManager.AddToRoleAsync(zafer, "User").Result;

                MyIdentityUser anas = new MyIdentityUser
                {
                    UserName = "anas",
                    Email = "anasmilhem1997@gmail.com",
                    Address = new Address
                    {
                        StreetName = "main st",
                        StreetNumber = "123",
                        City = "Ypsilanti",
                        State = "MI",
                        ZipCode = 48197
                    },
                    Password = "anas",
                    CarMake = "Honda",
                    CarYear = "1992",
                    CarModel = "Toyota",
                    CarColor = "Purple",
                    DriversLicense = "K123456789",
                    LicensePlate = "DQT1234",
                    PhoneNumber = "7341234567"
                };

                IdentityResult anasResult = userManager.CreateAsync(anas, anas.Password).Result;
                IdentityResult anasRoleResult = userManager.AddToRoleAsync(anas, "Driver").Result;

                MyIdentityUser josh = new MyIdentityUser
                {
                    UserName = "josh",
                    Email = "jthonnis@emich.edu",
                    Address = new Address
                    {
                        StreetName = "main st",
                        StreetNumber = "123",
                        City = "Ypsilanti",
                        State = "MI",
                        ZipCode = 48197
                    },
                    Password = "josh",
                    PhoneNumber = "5172704123"
                };

                IdentityResult jsohResult = userManager.CreateAsync(josh, josh.Password).Result;
                IdentityResult joshRoleResult = userManager.AddToRoleAsync(josh, "User").Result;


                MyIdentityUser sean = new MyIdentityUser
                {
                    UserName = "sean",
                    Email = "sleona12@emich.edu",
                    Address = new Address
                    {
                        StreetName = "main st",
                        StreetNumber = "123",
                        City = "Ypsilanti",
                        State = "MI",
                        ZipCode = 48197
                    },
                    Password = "sean",
                    CarMake = "Honda",
                    CarYear = "2012",
                    CarModel = "Accord",
                    CarColor = "Blue",
                    LicensePlate = "GHH123",
                    DriversLicense = "K4423123",
                    PhoneNumber = "7341234567"
                };

                IdentityResult seanResult = userManager.CreateAsync(sean, sean.Password).Result;
                IdentityResult seanRoleResult = userManager.AddToRoleAsync(sean, "Driver").Result;

                MyIdentityUser moe = new MyIdentityUser
                {
                    UserName = "mattar",
                    Email = "Mmattar@emich.edu",
                    Address = new Address
                    {
                        StreetName = "main st",
                        StreetNumber = "123",
                        City = "Dearborn",
                        State = "MI",
                        ZipCode = 48000
                    },
                    Password = "mattar",
                    PhoneNumber = "7341234567"
                };

                IdentityResult moeResult = userManager.CreateAsync(moe, moe.Password).Result;
                IdentityResult moeRoleResult = userManager.AddToRoleAsync(moe, "User").Result;


                MyIdentityUser gavin = new MyIdentityUser
                {
                    UserName = "gavin",
                    Email = "gmaierde@emich.edu",
                    Address = new Address
                    {
                        StreetName = "main st",
                        StreetNumber = "123",
                        City = "Ann Arbor",
                        State = "MI",
                        ZipCode = 48193
                    },
                    Password = "gavin",
                    CarYear = "3045",
                    CarMake = "Tesla",
                    CarColor = "Black",
                    CarModel = "S3",
                    LicensePlate = "RTG1234",
                    DriversLicense = "K123123",
                    PhoneNumber = "7341234567"
                };

                IdentityResult gavinResult = userManager.CreateAsync(gavin, gavin.Password).Result;
                IdentityResult gavinRoleResult = userManager.AddToRoleAsync(gavin, "Driver").Result;

            }



        }

  
        public static void SeedRequests(MyProductionDbContext context, 
            UserManager<MyIdentityUser> userManager)
        {
            // users
            MyIdentityUser zafer = userManager.FindByNameAsync("zafer").Result;
            MyIdentityUser mattar = userManager.FindByNameAsync("mattar").Result;
            MyIdentityUser josh = userManager.FindByNameAsync("josh").Result;

            //drivers
            MyIdentityUser gavin = userManager.FindByNameAsync("gavin").Result;
            MyIdentityUser sean = userManager.FindByNameAsync("sean").Result;
            MyIdentityUser anas = userManager.FindByNameAsync("anas").Result;

            RequestModel zaferRequest = new RequestModel
            {
                Item = "Vape",
                UserId = zafer.Id,
                DriverId = anas.Id,
                PickupAddress = anas.Address,
                DropOffAddress = zafer.Address,
                Status = "Accepted By Driver",
                ImageName = "vape.png"

            };
            context.Requests.Add(zaferRequest);

            RequestModel mattarRequest = new RequestModel
            {
                Item = "Hijab",
                UserId = mattar.Id,
                PickupAddress = gavin.Address,
                DropOffAddress = gavin.Address,
                Status = "Awaiting Driver",
                ImageName = "hijab.jpg"
            };
            context.Requests.Add(mattarRequest);

            RequestModel joshRequest = new RequestModel
            {
                Item = "American flag",
                UserId = josh.Id,
                PickupAddress = sean.Address,
                DropOffAddress = sean.Address,
                Status = "Awaiting Driver",
                ImageName = "flag.jpg"
            };
            context.Requests.Add(joshRequest);
            context.SaveChanges();

        }
    }
}