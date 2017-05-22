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
        public ActionResult Index()
        {
            
            return View();
        }

        private bool genratePassword(string carNumber)
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

    }
}