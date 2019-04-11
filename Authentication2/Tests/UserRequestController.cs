using System;
using System.Linq;
using Xunit;
using Moq;
using Authentication2.DataAccessLayer;
using Authentication2.Areas.Controllers;
using Authentication2.Identity;
using Authentication2.Models;
using Authentication2.VIewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;

namespace Tests
{
    public class UserRequestController
    {
        private CreateRequestViewModel MockCreateRequestViewModel()
        {
            return new CreateRequestViewModel
            {
                UserId = "Josh is the coolest",
                Status = "I hate testing",
                Item = "comics",
                PickupInstructions = "I don't want to test anymore",
                DropoffInstructions = "No, I really don't",
                PickupStreetNumber = "1234",
                PickupStreetName = "Pickup Street",
                PickupCity = "Pickup City",
                PickupState = "Pickup State",
                PickupZipcode = 12345,
                DropoffStreetNumber = "Dropoff Street",
                DropoffStreetName = "Dropoff StreetName",
                DropoffCity = "Dropoff City",
                DropoffState = "Dropoff Stat",
                DropoffZipcode = 09876,
            };
        }

        private RequestModel MockRequestModel()
        {
            return new RequestModel
            {
                UserId = "Josh is awesome",
                Id = 12,
                DriverId = "DriverId",
                Status = "Status",
                PickupAddress = new Address
                {
                    UserId = "Josh is awesome",
                    StreetNumber = "PickupStreetName",
                    StreetName = "PickupStreetName",
                    City = "PickupCity",
                    State = "PickupState",
                    ZipCode = 45678
                },
                DropOffAddress = new Address
                {
                    UserId = "Josh is awesome",
                    StreetNumber = "DropoffStreetName",
                    StreetName = "DropoffStreetName",
                    City = "DropoffCity",
                    State = "DropoffState",
                    ZipCode = 12345
                },
                Item = "Item",
                PickUpInstructions = "PickupInstruction",
                DropOffInstructions = "DropoffInstructions"
            };
        }

        private Address MockAddress()
        {
            return new Address
            {
                UserId = "Josh Thonnissen",
                StreetNumber = "2003",
                StreetName = "Locust",
                City = "Morenci",
                State = "MI",
                ZipCode = 49256
            };
        }

        private void CheckModelValues(CreateRequestViewModel expected, CreateRequestViewModel actual)
        {
            Assert.Equal(expected.Id,
                actual.Id);
            Assert.Equal(expected.PickupStreetNumber,
                actual.PickupStreetNumber);
            Assert.Equal(expected.PickupStreetName,
                actual.PickupStreetName);
            Assert.Equal(expected.PickupCity,
                actual.PickupCity);
            Assert.Equal(expected.PickupState,
                actual.PickupState);
            Assert.Equal(expected.PickupZipcode,
                actual.PickupZipcode);
            Assert.Equal(expected.DropoffStreetNumber,
                actual.DropoffStreetNumber);
            Assert.Equal(expected.DropoffStreetName,
                actual.DropoffStreetName);
            Assert.Equal(expected.DropoffCity,
                actual.DropoffCity);
            Assert.Equal(expected.DropoffZipcode,
                actual.DropoffZipcode);
            Assert.Equal(expected.Item,
                actual.Item);
        }

        [Fact]
        public void Index_Load_Success()
        {
            var contextMock = new Mock<IDbContext>();
            var hostingEnv = new Mock<IHostingEnvironment>();
            var controller = new RequestController(contextMock.Object, hostingEnv.Object);

            var response = controller.Index();

            Assert.IsType<ViewResult>(response);
        }

        [Fact]
        public void Create_LoadSuccess()
        {
            var contextMock = new Mock<IDbContext>();
            var hostingEnv = new Mock<IHostingEnvironment>();
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                 new Claim(ClaimTypes.NameIdentifier, "1")
            }));
            var controller = new RequestController(contextMock.Object, hostingEnv.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext() { User = user }
                }
            };

            contextMock.Setup(x => x.GetUserAddresses(It.IsAny<string>()))
                .Returns(new List<Address>
                {
                    MockAddress(),
                });

            var response = controller.Create();

            Assert.IsType<ViewResult>(response);
        }

        [Fact]
        public void Create_Success()
        {
            var contextMock = new Mock<IDbContext>();
            var hostingEnv = new Mock<IHostingEnvironment>();
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                 new Claim(ClaimTypes.NameIdentifier, "1")
            }));
            var controller = new RequestController(contextMock.Object, hostingEnv.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext() { User = user }
                }
            };

            contextMock.Setup(x => x.IfExistingAddress(It.IsAny<Address>())).Returns(false);
            contextMock.Setup(x => x.GetAddressId(It.IsAny<Address>())).Returns(2);
            contextMock.Setup(x => x.AddRequest(It.IsAny<RequestModel>()));

            controller.Create(MockCreateRequestViewModel());

            contextMock.Verify(x => x.AddRequest(It.IsAny<RequestModel>()), Times.Exactly(1));
        }

        [Fact]
        public void ConfirmCreate_Load_Success()
        {
            var contextMock = new Mock<IDbContext>();
            var hostingEnv = new Mock<IHostingEnvironment>();
            var controller = new RequestController(contextMock.Object, hostingEnv.Object);
            var model = MockCreateRequestViewModel();

            var response = controller.ConfirmCreate(model);
            var result = response as ViewResult;

            Assert.IsType<ViewResult>(response);

            CheckModelValues(model, result.ViewData.Values.ElementAt(0) as CreateRequestViewModel);
        }

        [Fact]
        public void Delete_Load_Success()
        {
            var contextMock = new Mock<IDbContext>();
            var hostingEnv = new Mock<IHostingEnvironment>();
            var controller = new RequestController(contextMock.Object, hostingEnv.Object);

            var response = controller.Delete();

            Assert.IsType<ViewResult>(response);
        }

        [Theory]
        [InlineData(20)]
        public void ConfirmDelete_Load_Success(int id)
        {
            var contextMock = new Mock<IDbContext>();
            var hostingEnv = new Mock<IHostingEnvironment>();
            var controller = new RequestController(contextMock.Object, hostingEnv.Object);

            contextMock.Setup(x => x.GetRequestById(id)).Returns(MockRequestModel);

            var response = controller.Delete();

            Assert.IsType<ViewResult>(response);
        }

        [Theory]
        [InlineData(20)]
        public void DeleteConfirmed_Success(int id)
        {
            var contextMock = new Mock<IDbContext>();
            var hostingEnv = new Mock<IHostingEnvironment>();
            var controller = new RequestController(contextMock.Object, hostingEnv.Object);

            contextMock.Setup(x => x.GetRequestById(id))
                .Returns(MockRequestModel());

            var response = controller.DeleteConfirmed(id);

            Assert.IsType<RedirectToActionResult>(response);
        }

        [Theory]
        [InlineData(0)]
        public void DeleteConfirmed_Failure_Invalid_ID(int id)
        {
            var contextMock = new Mock<IDbContext>();
            var hostingEnv = new Mock<IHostingEnvironment>();
            var controller = new RequestController(contextMock.Object, hostingEnv.Object);

            contextMock.Setup(x => x.GetRequestById(id))
                .Returns(MockRequestModel());

            var response = controller.DeleteConfirmed(id);

            Assert.IsType<ContentResult>(response);
        }

        [Theory]
        [InlineData(20)]
        public void Detail_Success(int id)
        {
            var contextMock = new Mock<IDbContext>();
            var hostingEnv = new Mock<IHostingEnvironment>();
            var controller = new RequestController(contextMock.Object, hostingEnv.Object);
            var requestMock = MockRequestModel();
            var modelMock = new CreateRequestViewModel(requestMock);

            contextMock.Setup(x => x.GetRequestById(id))
                .Returns(requestMock);

            var response = controller.Detail(id);
            var result = (response as ViewResult);

            Assert.IsType<ViewResult>(response);
            CheckModelValues(modelMock, result.Model as CreateRequestViewModel);
        }

        [Theory]
        [InlineData(20)]
        public void Update_Success(int id)
        {
            var contextMock = new Mock<IDbContext>();
            var hostingEnv = new Mock<IHostingEnvironment>();
            var requestMock = MockRequestModel();
            var modelMock = new CreateRequestViewModel(requestMock);
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                 new Claim(ClaimTypes.NameIdentifier, "1")
            }));
            var controller = new RequestController(contextMock.Object, hostingEnv.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext() { User = user }
                }
            };
            contextMock.Setup(x => x.GetRequestById(id)).Returns(requestMock);
            contextMock.Setup(x => x.GetUserAddresses(It.IsAny<string>()))
                .Returns(new List<Address> { MockAddress() });

            var response = controller.Update(id);
            var result = response as ViewResult;

            Assert.IsType<ViewResult>(response);
            CheckModelValues(modelMock, result.Model as CreateRequestViewModel);
        }

        [Fact]
        public void UpdateDatabase_Success()
        {
            var contextMock = new Mock<IDbContext>();
            var hostingEnv = new Mock<IHostingEnvironment>();
            var controller = new RequestController(contextMock.Object, hostingEnv.Object);
            var requestMock = MockRequestModel();
            var modelMock = new CreateRequestViewModel(requestMock);
            var addressMock = MockAddress();

            contextMock.Setup(x => x.GetRequestById(modelMock.Id))
                .Returns(requestMock);
            contextMock.Setup(x => x.UpdateRequest(It.IsAny<RequestModel>()));
            contextMock.Setup(x => x.IfExistingAddress(It.IsAny<Address>()))
                .Returns(true);
            contextMock.Setup(x => x.GetAddressById(It.IsAny<int>()))
                .Returns(addressMock);

            var response = controller.Update(modelMock);

            Assert.IsType<RedirectToActionResult>(response);
        }

        [Fact]
        public void ConfirmUpdate_Success()
        {
            var contextMock = new Mock<IDbContext>();
            var hostingEnv = new Mock<IHostingEnvironment>();
            var controller = new RequestController(contextMock.Object, hostingEnv.Object);
            var modelMock = MockCreateRequestViewModel();

            var response = controller.ConfirmUpdate(modelMock);
            var result = response as ViewResult;

            Assert.IsType<ViewResult>(response);
            CheckModelValues(modelMock,
                (result.ViewData.Values.ElementAt(0) as CreateRequestViewModel));
        }

        [Fact]
        public void List_Success()
        { // Fails due to GetUserAddresses fix User.FindFirstValue(ClaimsTypes.NameIdentifier)
            var contextMock = new Mock<IDbContext>();
            var hostingEnv = new Mock<IHostingEnvironment>();
            var requestMock = MockRequestModel();
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                 new Claim(ClaimTypes.NameIdentifier, requestMock.UserId)
            }));
            var controller = new RequestController(contextMock.Object, hostingEnv.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext() { User = user }
                }
            };

            contextMock.Setup(x => x.GetRequests())
                .Returns(new List<RequestModel> { requestMock });

            var response = controller.List();
            var result = response as ViewResult;
            var list = result.Model as List<CreateRequestViewModel>;

            Assert.IsType<ViewResult>(response);
            CheckModelValues(new CreateRequestViewModel(requestMock), list.ElementAt(0));
        }

        [Fact]
        public void GetAddressList_Success()
        {
            var contextMock = new Mock<IDbContext>();
            var hostingEnv = new Mock<IHostingEnvironment>();
            var modelMock = MockCreateRequestViewModel();
            var addressMock = MockAddress();
            var str = addressMock.StreetNumber + " " +
                addressMock.StreetName + ", " +
                addressMock.City + ", " +
                addressMock.State + " " +
                addressMock.ZipCode;
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                 new Claim(ClaimTypes.NameIdentifier, "1")
            }));
            var controller = new RequestController(contextMock.Object, hostingEnv.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext() { User = user }
                }
            };

            contextMock.Setup(x => x.GetUserAddresses(It.IsAny<string>()))
                .Returns(new List<Address> { addressMock });

            var response = controller.GetAddressList();

            Assert.Equal(2, response.Count);
            Assert.Equal("Select Address", response[0].Text);
            Assert.Equal(str, response[1].Text);
        }

        [Fact]
        public void UpdateDropoffAddress_Success_Address_Found()
        {
            var contextMock = new Mock<IDbContext>();
            var hostingEnv = new Mock<IHostingEnvironment>();
            var controller = new RequestController(contextMock.Object, hostingEnv.Object);
            var requestMock = MockRequestModel();
            var modelMock = MockCreateRequestViewModel();

            contextMock.Setup(x => x.IfExistingAddress(It.IsAny<Address>())).Returns(true);
            contextMock.Setup(x => x.GetAddressId(It.IsAny<Address>())).Returns(1);

            controller.UpdateDropoffAddress(modelMock, requestMock);

            contextMock.Verify(x => x.GetAddressId(It.IsAny<Address>()), Times.Once());
            Assert.Equal(1, requestMock.DropOffAddressId);
        }

        [Fact]
        public void UpdateDropoffAddress_Success_Address_Not_Found()
        {
            var contextMock = new Mock<IDbContext>();
            var hostingEnv = new Mock<IHostingEnvironment>();
            var controller = new RequestController(contextMock.Object, hostingEnv.Object);
            var requestMock = MockRequestModel();
            var modelMock = MockCreateRequestViewModel();

            contextMock.Setup(x => x.IfExistingAddress(It.IsAny<Address>())).Returns(false);
            contextMock.Setup(x => x.GetAddressId(It.IsAny<Address>())).Returns(1);

            controller.UpdateDropoffAddress(modelMock, requestMock);

            contextMock.Verify(x => x.GetAddressId(It.IsAny<Address>()), Times.Never());
            Assert.Equal(requestMock.UserId, requestMock.DropOffAddress.UserId);
            Assert.Equal(modelMock.DropoffStreetNumber, requestMock.DropOffAddress.StreetNumber);
            Assert.Equal(modelMock.DropoffStreetName, requestMock.DropOffAddress.StreetName);
            Assert.Equal(modelMock.DropoffCity, requestMock.DropOffAddress.City);
            Assert.Equal(modelMock.DropoffState, requestMock.DropOffAddress.State);
            Assert.Equal(modelMock.DropoffZipcode, requestMock.DropOffAddress.ZipCode);
        }

        [Fact]
        public void UpdatePickupAddress_Success_Address_Found()
        {
            var contextMock = new Mock<IDbContext>();
            var hostingEnv = new Mock<IHostingEnvironment>();
            var controller = new RequestController(contextMock.Object, hostingEnv.Object);
            var requestMock = MockRequestModel();
            var modelMock = MockCreateRequestViewModel();

            contextMock.Setup(x => x.IfExistingAddress(It.IsAny<Address>())).Returns(true);
            contextMock.Setup(x => x.GetAddressId(It.IsAny<Address>())).Returns(1);

            controller.UpdatePickupAddress(modelMock, requestMock);

            contextMock.Verify(x => x.GetAddressId(It.IsAny<Address>()), Times.Once());
            Assert.Equal(1, requestMock.PickupAddressId);
        }

        [Fact]
        public void UpdatePickupAddress_Success_Address_Not_Found()
        {
            var contextMock = new Mock<IDbContext>();
            var hostingEnv = new Mock<IHostingEnvironment>();
            var controller = new RequestController(contextMock.Object, hostingEnv.Object);
            var requestMock = MockRequestModel();
            var modelMock = MockCreateRequestViewModel();

            contextMock.Setup(x => x.IfExistingAddress(It.IsAny<Address>())).Returns(false);
            contextMock.Setup(x => x.GetAddressId(It.IsAny<Address>())).Returns(1);

            controller.UpdatePickupAddress(modelMock, requestMock);

            contextMock.Verify(x => x.GetAddressId(It.IsAny<Address>()), Times.Never());
            Assert.Equal(requestMock.UserId, requestMock.PickupAddress.UserId);
            Assert.Equal(modelMock.PickupStreetNumber, requestMock.PickupAddress.StreetNumber);
            Assert.Equal(modelMock.PickupStreetName, requestMock.PickupAddress.StreetName);
            Assert.Equal(modelMock.PickupCity, requestMock.PickupAddress.City);
            Assert.Equal(modelMock.PickupState, requestMock.PickupAddress.State);
            Assert.Equal(modelMock.PickupZipcode, requestMock.PickupAddress.ZipCode);
        }
    }
}
