using Microsoft.AspNetCore.Identity;

namespace Authentication2.Identity
{
    public class MyIdentityUser : IdentityUser
    {

        // UNENCRYPTED PASSWORD FOR DEVELOPMENT PURPOSES ONLY
        // so we dont have to remember passwords
        public string Password { get; set; }
        public Address Address { get; set; }
        public string CarMake { get; set; }
        public string CarModel { get; set; }
        public string CarColor { get; set; }
        public string CarYear { get; set; }
        public string DriversLicense { get; set; }
        public string LicensePlate { get; set; }
        public int AddressId { get; set; }
        public string DriverStatus { get; set; }
    }

    public class Address
    {
        public string UserId { get; set; }
        public int Id { get; set; }
        public string StreetNumber { get; set; }
        public string StreetName { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int ZipCode { get; set; }
    }

    public enum States
    {
        AL,
        AK,
        AZ,
        AR,
        CA,
        CO,
        CT,
        DE,
        FL,
        GA,
        HI,
        ID,
        IL,
        IN,
        IA,
        KS,
        KY,
        LA,
        ME,
        MD,
        MA,
        MI,
        MN,
        MS,
        MO,
        MT,
        NE,
        NV,
        NH,
        NJ,
        NM,
        NY,
        NC,
        ND,
        OH,
        OK,
        OR,
        PA,
        RI,
        SC,
        SD,
        TN,
        TX,
        UT,
        VT,
        VA,
        WA,
        WV,
        WI,
        WY
    }
}
