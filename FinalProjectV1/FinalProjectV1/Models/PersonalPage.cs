using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProjectV1.Models
{
    public class PersonalPage
    {
        //Details
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Residence { get; set; }//location
        public string Email { get; set; }
        public string OwnedCars { get; set; }
        public string Telephone { get; set; }
        public int ID { get; set; }

        //cars
        public List<Car> cars { get; set; }

    }
}