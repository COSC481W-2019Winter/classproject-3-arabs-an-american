using System.ComponentModel.DataAnnotations.Schema;
using Authentication2.Identity;
using Authentication2.VIewModels;
using Microsoft.AspNetCore.Http;

namespace Authentication2.Models
{
    public class RequestModel
    {
        public RequestModel() { }
        public RequestModel(CreateRequestViewModel request) 
        {
            Id = request.Id;
            UserId = request.UserId;
            DriverId = request.DriverId;
            Status = request.Status;
            PickupAddress = new Address
            {
                UserId = request.UserId,
                StreetNumber = request.PickupStreetName,
                StreetName = request.PickupStreetName,
                City = request.PickupCity,
                State = request.PickupState,
                ZipCode = request.PickupZipcode
            };
            DropOffAddress = new Address
            {
                UserId = request.UserId,
                StreetNumber = request.DropoffStreetName,
                StreetName = request.DropoffStreetName,
                City = request.DropoffCity,
                State = request.DropoffState,
                ZipCode = request.DropoffZipcode
            };
            Item = request.Item;
            PickUpInstructions = request.PickupInstructions;
            DropOffInstructions = request.DropoffInstructions;
            
    }

        public int Id{get;set;}
        public string UserId { get; set; }
        public string DriverId { get; set; }
        public string Status { get; set; }

        [ForeignKey("PickupAddress")]
        public int? PickupAddressId { get; set; }
        public Address PickupAddress { get; set; }

        [ForeignKey("DropOffAddress")]
        public int? DropOffAddressId { get; set; }
        public Address DropOffAddress { get; set; }

        public string Item { get; set; }
        public string PickUpInstructions { get; set; }
        public string DropOffInstructions { get; set; }

        public string ImagePath { get; set; }
    }
}
