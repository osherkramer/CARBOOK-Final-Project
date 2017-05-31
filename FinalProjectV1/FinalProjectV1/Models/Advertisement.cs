using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace FinalProjectV1.Models
{
    public class Advertisement
    {

        public string CarNumber { get; set; }
        public string SellerName { get; set; }
        public string Tel { get; set; }
        [Display(Name = "העלאת תמונה")]
        public string Pic { get; set; }
        [Display(Name = "תיאור או מידע")]
        public string Description { get; set; }
        public string Location { get; set; }
        [Display(Name = "מחיר הרכב")]
        public string Price { get; set; }
        public DateTime DatePublished { set; get; }
    }
}