using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProjectV1.Models
{
    public class HistoryCar
    {
        //Car

        public string CarNumber { get; set; }
        public string Year { get; set; }
        public DateTime RoadDate { get; set; }
        public string ShildaNumber { get; set; }
        public string CarColor { get; set; }
        public string ProductName { get; set; }
        public string Yad { get; set; }
        public string Gaer { get; set; }
        public string EngineCapacity { get; set; }
        public string CommericalAlias { get; set; }
        //History Item

        /*public int TreatmentID { get; set; }
        public string CarNumber { get; set;}
        public string CareType { get; set; }
        public DateTime Date { get; set; }
        public string[] Treatment { get; set; }
        public int KM { get; set; }
        public string GarageName { get; set; }*/

        public List<HistoryItem> historyItems { get; set; }

        //Advertisment

        public string SellerName { get; set; }
        public string Tel { get; set; }
        public string Pic { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string Price { get; set; }
        public DateTime DatePublished { set; get; }

     

    }
}