using Authentication2.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication2.VIewModels
{
    public class AccountsViewModel
    {
        public List<Account> Accounts { get; set; }

        public AccountsViewModel()
        {
            Accounts = new List<Account>();
        }
    }


    public class UserViewModel
    {
        public UserViewModel() { }

        public UserViewModel(MyIdentityUser user)
        {
            this.UserId = user.Id;
            this.UserName = user.UserName;
            this.Address = user.Address;
            this.Email = user.Email;
            this.Phone = user.PhoneNumber;
            this.DriversLicense = user.DriversLicense;
            this.CarMake = user.CarMake;
            this.CarModel = user.CarModel;
            this.CarYear = user.CarYear;
            this.CarColor = user.CarColor;
            this.LicensePlate = user.LicensePlate;
        }

        public string UserId { get; set; }
        public string UserName { get; set; }
        public Address Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string DriversLicense { get; set; }
        public string CarMake { get; set; }
        public string CarModel { get; set; }
        public string CarYear { get; set; }
        public string CarColor { get; set; }
        public string LicensePlate { get; set; }

    }


    public class Account
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

        public MyIdentityUser RegisteredAccount { get; set; }
        public IdentityRole AccountRole { get; set; }
    }
}
