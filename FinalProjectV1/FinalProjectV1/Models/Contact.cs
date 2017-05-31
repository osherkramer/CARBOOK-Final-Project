using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectV1.Models
{
    public class Contact
    {
        public int ID { get; set; }

        [Display(Name = "נושא: ")]
        [Required(ErrorMessage = "נושא הודעה הוא חובה")]
        public string Subject { get; set; }

        [Display(Name = "השם שלך: ")]
        [Required(ErrorMessage = "שמך הוא חובה")]
        public string Name { get; set; }
        
        [Display(Name = "כתובת המייל שלך: ")]
        [Required(ErrorMessage = "כתובת המייל שלך היא חובה")]
        [EmailAddress(ErrorMessage = "כתובת מייל שגויה")]
        public string Email { get; set; }

        [Display(Name = "תוכן ההודעה: ")]
        [Required(ErrorMessage = "תוכן ההודעה הוא חובה")]
        public string Message { get; set; }

        public DateTime Date { get; set; }
    }
}
