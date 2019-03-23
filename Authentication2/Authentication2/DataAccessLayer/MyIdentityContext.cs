using Authentication2.Identity;
using Authentication2.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication2.DataAccessLayer
{
    public class MyIdentityContext : IdentityDbContext<MyIdentityUser>, IDbContext
    {
        public DbSet<Models.RequestModel> Requests { get; set; }

        public DbSet<Address> Addresses { get; set; }

        public MyIdentityContext(DbContextOptions options) : base(options)
        {
            this.Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public void AddRequest(RequestModel request)
        {
            throw new NotImplementedException();
        }

        public RequestModel GetRequestById(int id)
        {
            throw new NotImplementedException();
        }

        public List<RequestModel> GetRequests()
        {
            throw new NotImplementedException();
        }

        public List<Address> GetUserAddresses(string id)
        {
            throw new NotImplementedException();
        }

        public Address GetAddressById(int id)
        {
            throw new NotImplementedException();
        }

        public void RemoveRequest(RequestModel requestModel)
        {
            throw new NotImplementedException();
        }

        public void UpdateRequest(RequestModel request)
        {
            throw new NotImplementedException();
        }

        public bool IfExistingAddress(Address address)
        {
            throw new NotImplementedException();
        }

        public int GetAddressId(Address address)
        {
            throw new NotImplementedException();
        }
    }
}
