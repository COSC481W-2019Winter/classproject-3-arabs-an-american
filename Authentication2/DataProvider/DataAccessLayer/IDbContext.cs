using System;
using System.Collections.Generic;
using Authentication2.Models;
using DataProvider.Identity;

namespace DataProvider.DataAccessLayer
{
    public interface IDbContext
    {
        void AddRequest(RequestModel request);
        RequestModel GetRequestById(int id);
        List<RequestModel> GetRequests();
        List<Address> GetUserAddresses(string id);
        Address GetAddressById(int id);
        void RemoveRequest(RequestModel requestModel);
        void UpdateRequest(RequestModel request);
        bool IfExistingAddress(Address address);
        int GetAddressId(Address address);
    }
}
