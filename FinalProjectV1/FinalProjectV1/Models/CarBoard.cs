using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProjectV1.Models
{
    public class CarBoard
    {
        public List<CarAD> cars { get; set; }

        public List<string> carsProduct { get; set; }

        public List<string> areas { get; set; }
    }
}