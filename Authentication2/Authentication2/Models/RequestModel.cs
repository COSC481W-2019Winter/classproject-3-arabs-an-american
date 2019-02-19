using System;
using Authentication2.Identity;

namespace Authentication2.Models
{
    public class RequestModel
    {
        public Address PickupAddress { get; set; }
        public Address DropOffAddress { get; set; }
        public string Item { get; set; }
        public string PickUpInstructions { get; set; }
        public string DropOffInstructions { get; set; }
    }
}
