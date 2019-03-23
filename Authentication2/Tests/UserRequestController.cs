using System;
using Xunit;
using Moq;
using Authentication2.DataAccessLayer;
using Authentication2.Areas.Controllers;
using Authentication2.Identity;
using Authentication2.Models;
using Authentication2.VIewModels;

namespace Tests
{
    public class UserRequestController
    {
        [Fact]
        public void Create_Success()
        {
            var contextMock =new Mock<IDbContext>();
            var controller = new RequestController(contextMock.Object);
            var address = new Address
            {
                UserId = "Josh is the Best",
                Id = 1,
                StreetNumber = "12345",
                StreetName = "My Street",
                City = "City",
                State = "ST",
                ZipCode = 12345
            };
            var model = new CreateRequestViewModel
            {
                UserId = "Josh",
                Status = "i hate testing",
                Item = "comics",
                PickupInstructions = "dont test anymore",
                DropoffInstructions = "no really dont",
                PickupStreetNumber = address.StreetNumber,
                PickupStreetName= address.StreetName,
                PickupCity = address.City,
                PickupState = address.State,
                PickupZipcode = address.ZipCode,
                DropoffStreetNumber = address.StreetNumber,
                DropoffStreetName= address.StreetName,
                DropoffCity = address.City,
                DropoffState = address.State,
                DropoffZipcode = address.ZipCode,
            };

            contextMock.Setup(x => x.IfExistingAddress(It.IsAny<Address>())).Returns(false);
            contextMock.Setup(x => x.GetAddressId(It.IsAny<Address>())).Returns(2);
            contextMock.Setup(x => x.AddRequest(It.IsAny<RequestModel>()));

            controller.Create(model);

            contextMock.Verify(x => x.AddRequest(It.IsAny<RequestModel>()), Times.Exactly(1));
        }
    }
}
