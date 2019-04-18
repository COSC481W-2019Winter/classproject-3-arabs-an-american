using System.Collections.Generic;
using Authentication2.Identity;
using Authentication2.Models;

namespace Authentication2.DataAccessLayer
{
    public interface IDbContext
    {
        void AddRequest(RequestModel request);
        RequestModel GetRequestById(int id);
        List<RequestModel> GetRequests();
        List<Address> GetUserAddresses(string id);
        Address GetAddressById(string id);
        void RemoveRequest(RequestModel requestModel);
        void UpdateRequest(RequestModel request);
        bool IfExistingAddress(Address address);
        int GetAddressId(Address address);
        bool CheckActive(string driverId);
        MyIdentityUser GetUser(string UserId);
    }
}
