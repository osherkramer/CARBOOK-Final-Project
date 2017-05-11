using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProjectV1.Models
{
    public class Treatment
    {
        public int CarID { get; set; }
        public DateTime CareDate { get; set; }
        public int TreatmentID { get; set; }
        public string CareType { get; set; }
        public int KM { get; set; }
    }
}