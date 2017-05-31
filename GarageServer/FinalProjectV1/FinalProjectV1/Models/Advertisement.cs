using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProjectV1.Models
{
    public class Advertisement
    {

        public string CarNumber { get; set; }
        public string SellerName { get; set; }
        public string Tel { get; set; }
        public string Pic { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string Price { get; set; }
        public DateTime DatePublished { set; get; }
    }
}