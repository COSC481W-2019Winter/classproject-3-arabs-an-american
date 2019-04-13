using Authentication2.Identity;
using Authentication2.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

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
            Add(request);

            SaveChanges();
        }

        public RequestModel GetRequestById(int id)
        {
            return Requests
                .Where(req => req.Id == id)
                .Include(req => req.DropOffAddress)
                .Include(req => req.PickupAddress)
                .FirstOrDefault();
        }

        public List<RequestModel> GetRequests()
        {
            return Requests
                .Include(req => req.DropOffAddress)
                .Include(req => req.PickupAddress)
                .ToList();
        }

        public List<Address> GetUserAddresses(string id)
        {
            return Addresses
                .Where(x => x.UserId == id)
                .ToList();
        }

        public Address GetAddressById(string id)
        {
            return Users
                .Where(x => x.Id == id)
                .Include(x => x.Address)
                .ToList().ElementAt(0).Address;
        }

        public void RemoveRequest(RequestModel request)
        {
            Requests.Remove(request);

            SaveChanges();
        }

        public void UpdateRequest(RequestModel request)
        {
            Update(request);

            SaveChanges();
        }

        public bool IfExistingAddress(Address address)
        {
            return Addresses.Any(
                x => x.UserId == address.UserId
                && x.StreetNumber == address.StreetNumber
                && x.StreetName == address.StreetName
                && x.City == address.City
                && x.State == address.State
                && x.ZipCode == address.ZipCode);
        }

        public int GetAddressId(Address address)
        {
            return Addresses
                .Where(
                    x => x.UserId == address.UserId
                    && x.StreetNumber == address.StreetNumber
                    && x.StreetName == address.StreetName
                    && x.City == address.City
                    && x.State == address.State
                    && x.ZipCode == address.ZipCode)
                .FirstOrDefault()
                .Id;
        }

        public bool CheckActive(string driverId)
        {
            return Requests.Any(x => x.DriverId == driverId && x.Status != "Delivered");
        }

        public MyIdentityUser GetUser(string Userid)
        {
            var user = Users.FirstOrDefault(x => x.Id == Userid);
            return user;
        }
    }
}
