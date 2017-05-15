using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProjectV1.Models
{
    public class HistoryCar
    {
        //Car

        public  string year { get; set; }
        public string shildaNmber { get; set; }
        public string carColor { get; set; }
        public DateTime roadDate { get; set; }
        public string productName { get; set;}


        //History Item

        public int TreatmentID { get; set; }
        public string CarNumber { get; set; }
        public string CareType { get; set; }
        public DateTime Date { get; set; }
        public string[] Treatment { get; set; }
        public int KM { get; set; }
        public string GarageName { get; set; }



    }
}