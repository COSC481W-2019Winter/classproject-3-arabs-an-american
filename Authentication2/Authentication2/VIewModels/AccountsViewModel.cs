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

    public  class Account
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

        public MyIdentityUser RegisteredAccount { get; set; }
        public IdentityRole AccountRole { get; set; }
    }
}
