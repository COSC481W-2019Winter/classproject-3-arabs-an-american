using System;
using Authentication2.Identity;

namespace Authentication2.Models
{
    public class RequestModel
    {
        public Address pickupAddress { get; set; }
        public Address dropOffAddress { get; set; }
        public string Item { get; set; }
        public string pickUpInstructions { get; set; }
        public string dropOffInstructions { get; set; }
    }
}