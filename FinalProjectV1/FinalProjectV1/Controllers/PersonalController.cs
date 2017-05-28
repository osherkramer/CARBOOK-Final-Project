using FinalProjectV1.Helpers;
using FinalProjectV1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace FinalProjectV1.Controllers
{
    public class PersonalController : Controller
    {
        // GET: Personal
        public ActionResult Index(string Email)
        {
            PersonalPage person = getPersonalDetails(Email);
            return View(person);
        }

        private PersonalPage getPersonalDetails(string Email)
        {
            DBHelper db = new DBHelper();
            PersonalArea person = db.getPrivateDetails(Email);
            PersonalPage details = new PersonalPage();
            details.cars = db.getCarByID(person.ID);
            details.Email = person.Email;
            details.FirstName = person.FirstName;
            details.ID = person.ID;
            details.LastName = person.LastName;
            details.OwnedCars = person.OwnedCars;
            details.Residence = person.Residence;
            details.Telephone = person.Telephone;

            return details;

        }

        public bool genratePassword(string carNumber)
        {
            TemproryUsers tu = new TemproryUsers();
            int carID;
            int.TryParse(carNumber, out carID);

            if (carID == 0)
                return false;

            tu.carID = carID;
            string tempPassword = Membership.GeneratePassword(8, 4);
            tu.password = tempPassword;
            DateTime dateNow = DateTime.Now;
            dateNow.AddMinutes(20);
            tu.expiryDate = dateNow;

            DBHelper DBHelp = new DBHelper();
            bool flag = DBHelp.insertTemproryUsers(tu);
            return flag;
            
        }

        public bool publishNewAdvertisement(string CarNumber, string SellerName, string Tel, string Pic, string Description, string Location, string Price, DateTime DatePublished)
        {
            Advertisement ad = new Advertisement();
            ad.CarNumber = CarNumber;
            ad.DatePublished = DatePublished;
            ad.Description = Description;
            ad.Location = Location;
            ad.Pic = Pic;
            ad.Price = Price;
            ad.SellerName = SellerName;
            ad.Tel = Tel;

            DBHelper db = new DBHelper();
            bool flag = db.insertAdvertisment(ad);
            return flag;


        }

        public List<HistoryItem> getHistoryItemsCar(string carNumber)
        {
            DBHelper db = new DBHelper();
            List<HistoryItem> historyItems = db.getHistoryByCarNumber(Int32.Parse(carNumber));
            return historyItems;
        }

    }
}