using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace FinalProjectV1.Models
{
     public class TemproryUsers
    {
        [Display(Name = "מספר רכב")]
        public int carID { set; get; }
        public DateTime expiryDate { set; get; }
        [Display(Name = "סיסמה זמנית")]
        public string password { set; get; }
    }
}
