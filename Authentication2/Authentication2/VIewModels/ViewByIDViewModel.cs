using Authentication2.Identity;

namespace Authentication2.VIewModels
{
    public class ViewByIDViewModel
    {
        public int Id{get;set;}
        public Address PickupAddress { get; set; }
        public Address DropOffAddress { get; set; }
        public string Item { get; set; }
        public string PickUpInstructions { get; set; }
        public string DropOffInstructions { get; set; }
    }
}