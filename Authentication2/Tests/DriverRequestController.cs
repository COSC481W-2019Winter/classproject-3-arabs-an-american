﻿using Xunit;
using Moq;
using Authentication2.DataAccessLayer;
using Authentication2.Areas.Driver.Controllers;
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
    public class DriverRequestController
    {
        private CreateRequestViewModel MockCreateRequestViewModel()
        {
            return new CreateRequestViewModel
            {
                UserId = "Josh is the coolest",
                Status = "Accepted By Driver",
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
                DropoffState = "Dropoff State",
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
            Assert.Equal(expected.UserId,
                actual.UserId);
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
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                 new Claim(ClaimTypes.NameIdentifier, "1")
            }));
            var controller = new RequestController(contextMock.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext() { User = user }
                }
            };

            var response = controller.Index();

            Assert.IsType<ViewResult>(response);
        }

        [Fact]
        public void AcceptedRequests_Success()
        {
            var contextMock = new Mock<IDbContext>();
            var hostingEnv = new Mock<IHostingEnvironment>();
            var requestMock = MockRequestModel();
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                 new Claim(ClaimTypes.NameIdentifier, "1")
            }));
            var controller = new RequestController(contextMock.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext() { User = user }
                }
            };

            contextMock.Setup(x => x.GetRequests())
                .Returns(new List<RequestModel> { requestMock });

            var response = controller.AcceptedRequests();
            var result = response as ViewResult;

            Assert.IsType<ViewResult>(response);
            //Assert.Equal(1, (result.Model as List<CreateRequestViewModel>).Count);
        }

        [Theory]
        [InlineData(20)]
        public void UpdateStatus_Success(int id)
        {
            var contextMock = new Mock<IDbContext>();
            var hostingEnv = new Mock<IHostingEnvironment>();
            var controller = new RequestController(contextMock.Object);
            var requestMock = MockRequestModel();
            var modelMock = new CreateRequestViewModel(requestMock);

            contextMock.Setup(x => x.GetRequestById(It.IsAny<int>()))
                .Returns(requestMock);

            var response = controller.UpdateStatus(id);
            var result = response as ViewResult;

            Assert.IsType<ViewResult>(response);
            CheckModelValues(modelMock, result.Model as CreateRequestViewModel);
        }

        [Fact]
        public void UpdateStatus_Database_Success()
        {
            var contextMock = new Mock<IDbContext>();
            var hostingEnv = new Mock<IHostingEnvironment>();
            var controller = new RequestController(contextMock.Object);
            var requestMock = MockRequestModel();
            var modelMock = new CreateRequestViewModel(requestMock);

            contextMock.Setup(x => x.GetRequestById(It.IsAny<int>()))
                .Returns(requestMock);
            contextMock.Setup(x => x.UpdateRequest(It.IsAny<RequestModel>()));

            var response = controller.UpdateStatus(modelMock);

            contextMock.Verify(x => x.UpdateRequest(It.IsAny<RequestModel>()), Times.Once());
            Assert.IsType<RedirectToActionResult>(response);
        }

        [Fact]
        public void Open_Success()
        {
            var contextMock = new Mock<IDbContext>();
            var hostingEnv = new Mock<IHostingEnvironment>();
            var requestMock = MockRequestModel();
            var modelMock = new CreateRequestViewModel(requestMock);
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                 new Claim(ClaimTypes.NameIdentifier, "1")
            }));
            var controller = new RequestController(contextMock.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext() { User = user }
                }
            };

            contextMock.Setup(x => x.GetRequests())
                .Returns(new List<RequestModel> { requestMock });
            contextMock.Setup(x => x.UpdateRequest(It.IsAny<RequestModel>()));

            var response = controller.Open();
            var result = response as ViewResult;

            Assert.IsType<ViewResult>(response);
           //Assert.Equal(1, (result.Model as List<CreateRequestViewModel>).Count);
        }

        [Theory]
        [InlineData(20)]
        public void Pickup_Success(int id)
        {
            var contextMock = new Mock<IDbContext>();
            var hostingEnv = new Mock<IHostingEnvironment>();
            var controller = new RequestController(contextMock.Object);
            var requestMock = MockRequestModel();
            var modelMock = new CreateRequestViewModel(requestMock);

            contextMock.Setup(x => x.GetRequestById(id))
                .Returns(requestMock);

            var response = controller.Pickup(id);
            var result = response as ViewResult;

            Assert.IsType<ViewResult>(response);
            CheckModelValues(modelMock, result.Model as CreateRequestViewModel);
        }

        [Fact]
        public void PickupRequest_Success()
        {
            var contextMock = new Mock<IDbContext>();
            var hostingEnv = new Mock<IHostingEnvironment>();
            var controller = new RequestController(contextMock.Object);
            var requestMock = MockRequestModel();
            var modelMock = new CreateRequestViewModel(requestMock);

            contextMock.Setup(x => x.GetRequestById(It.IsAny<int>()))
                .Returns(requestMock);
            contextMock.Setup(x => x.UpdateRequest(It.IsAny<RequestModel>()));

            var response = controller.UpdateStatus(modelMock);

            contextMock.Verify(x => x.UpdateRequest(It.IsAny<RequestModel>()), Times.Once());
            Assert.IsType<RedirectToActionResult>(response);
        }

        [Fact]
        public void ConfirmPickup_Success()
        {
            var contextMock = new Mock<IDbContext>();
            var hostingEnv = new Mock<IHostingEnvironment>();
            var controller = new RequestController(contextMock.Object);

            var response = controller.ConfirmPickup();
            var result = response as ViewResult;

            Assert.IsType<ViewResult>(response);
        }
    }
}
