using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication2.VIewModels
{
    public class BecomeDriverViewModel
    {
        [Required]
        public string CarMake { get; set; }
        [Required]
        public string CarModel { get; set; }
        [Required]
        public string CarColor { get; set; }
        [Required]
        public string CarYear { get; set; }
        [Required]
        public string CarLicensePlate { get; set; }
        [Required]
        public string DriversLicense { get; set; }
    }
}
