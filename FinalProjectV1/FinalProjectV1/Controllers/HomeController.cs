using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FinalProjectV1.Models;
using System.Net;
using System.Net.Mail;
using System;
using System.Collections.Generic;
using FinalProjectV1.Models;

namespace FinalProjectV1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            CarFeatureExtractor.init();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact(Contact contact)
        {
            if (ModelState.IsValid)
            {
                string subject = "[Don't reply] CarBook. נשלח מייל מאת  " + contact.Name + ": " + contact.Subject;

                string message = "הודעה מאת: " + contact.Name + ". \nכתובת המייל: " + contact.Email + ". \nתוכן הפניה: " + contact.Message;

                bool success = SendMail(subject, message, "carbook.israel@gmail.com", true);

                if(success)
                {
                    SendMail("CarBook - תודה על פנייתך", "זוהי הודעה לאישור קבלת הפניה, נציגנו יחזרו אליך בהקדם. תוכן פנייתך:\n" + message, contact.Email);
                }
                /*
                if (success)
                {
                    _context.Contact.Add(contact);
                    _context.SaveChanges();
                }*/

                return View("Contact");
            }

            return View("Contact");
        }

        public JsonResult GetLocations()
        {
            List<Locations> temp = new List<Locations>();
            Locations location = new Locations();
            location.X = 31.969738;
            location.Y = 34.772787;
            location.AddressLocation = "אלי ויזל 2, ראשון לציון, ישראל";
            location.NameLocation = "המסלול האקדמי המכללה למנהל - CarBook";
            temp.Add(location);
            return Json(temp, JsonRequestBehavior.AllowGet);
        }

        private bool SendMail(string subject, string message, string sendTo, bool isWebRequest = false)
        {
            try
            {
                MailMessage mail = new MailMessage(); //Class of mail message
                SmtpClient smtpServer = new SmtpClient("smtp.gmail.com"); //Object of SmtpClient with the path of gmail smtp server
                mail.From = new MailAddress("carbook.isreal@gmail.com"); //define the mail address that send the message

                mail.To.Add(sendTo); //add our mail address for the distributed list (mailing list)
                
                mail.Subject = subject; //add the subject of the mail
                mail.Body = message; //add the message of the mail

                smtpServer.Port = 587; //define the port of smtp server for gmail
                smtpServer.Credentials = new NetworkCredential("carbook.israel@gmail.com", "koobrac123"); //connect to the server with the user name and password
                smtpServer.EnableSsl = true; //enable the ssl protocol

                smtpServer.Send(mail); //send the mail
            }
            catch (Exception e)
            {
                if(isWebRequest)
                    ModelState.AddModelError("", "שליחה ההודעה נכשלה. נסה שנית");
                return false;
            }

            if(isWebRequest)
                ModelState.AddModelError("", "שליחת ההודעה בוצעה בהצלחה! תודה ונחזור אליך בהקדם");
            return true;
        }
    }
}