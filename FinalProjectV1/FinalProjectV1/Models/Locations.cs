using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectV1.Models
{
    public class Locations
    {
        public int ID { get; set; }

        [Display(Name = "Coordinate X: ")]
        [Required(ErrorMessage = "Coordinate X is required")]
        public double X { get; set; }

        [Display(Name = "Coordinate Y: ")]
        [Required(ErrorMessage = "Coordinate Y is required")]
        public double Y { get; set; }

        [Display(Name = "Location name: ")]
        [Required(ErrorMessage = "Location name is required")]
        public string NameLocation { get; set; }

        [Display(Name = "Location address: ")]
        [Required(ErrorMessage = "Location address is required")]
        public string AddressLocation { get; set; }
    }
}
