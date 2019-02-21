using System;
using Authentication2.Identity;
using Authentication2.VIewModels;

namespace Authentication2.Models
{
    public class RequestModel
    {
        public int Id{get;set;}
        public Address PickupAddress { get; set; }
        public Address DropOffAddress { get; set; }
        public string Item { get; set; }
        public string PickUpInstructions { get; set; }
        public string DropOffInstructions { get; set; }
    }
}
