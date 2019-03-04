using System;
using System.ComponentModel.DataAnnotations.Schema;
using Authentication2.Identity;
using Authentication2.VIewModels;

namespace Authentication2.Models
{
    public class RequestModel
    {
        public int Id{get;set;}
        public string UserId { get; set; }
        public string DriverId { get; set; }
        public string Status { get; set; }

        [ForeignKey("PickupAddress")]
        public int PickupAddressId { get; set; }
        public Address PickupAddress { get; set; }

        [ForeignKey("DropOffAddress")]
        public int DropOffAddressId { get; set; }
        public Address DropOffAddress { get; set; }

        public string Item { get; set; }
        public string PickUpInstructions { get; set; }
        public string DropOffInstructions { get; set; }
    }
}
