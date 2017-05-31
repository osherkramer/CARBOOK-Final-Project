using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FinalProjectV1.Models;
using FinalProjectV1.Helpers;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Web.Security;
using System.Text.RegularExpressions;

namespace FinalProjectV1.Controllers
{
    public class HistoryController : Controller
    {
        //
        // GET: /History/Index
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        //
        // POST: /History/Index
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(TemproryUsers model, string returnUrl)
        {
            return RedirectToAction("History", "History", new { carNumber = model.carID, password = model.password, isOwner = false });
        }

        public ActionResult GeneratePassword(string carNumber)
        {
            TemproryUsers tu = new TemproryUsers();
            int carID;
            int.TryParse(carNumber, out carID);

            if (carID == 0)
                return View();

            tu.carID = carID;
            string tempPassword = Membership.GeneratePassword(20, 4);
            tempPassword = Regex.Replace(tempPassword, @"[^a-zA-Z0-9]", m => "");
            tu.password = tempPassword;
            DateTime dateNow = DateTime.Now;
            dateNow = dateNow.AddMinutes(20);
            tu.expiryDate = dateNow;

            DBHelper DBHelp = new DBHelper();
            if (DBHelp.insertTemproryUsers(tu))
                return View(tu);

            return View();

        }

        // GET: History
        public ActionResult History(String carNumber, String password, bool isOwner = false)
        {
            if(carNumber == null)
            {
                carNumber = "0";
            }

            if(password == null)
            {
                password = "";
            }
            DBHelper DBhelp = new DBHelper();
            if (!isOwner)
            {
                DateTime dt = DBhelp.getTemproryUsersByCarID(Int32.Parse(carNumber), password);
                if (dt < DateTime.Now)
                {
                    ViewBag.Message = "Car number or password incurrect or expired";
                    return View();
                }
            }

            Car car = DBhelp.getCarByNumber(carNumber);
            HistoryCar historyCar = getHistoryCar(car);
            historyCar.isOwnerRequest = isOwner;
            historyCar.isPublished = isPublish(carNumber);

            return View(historyCar);
        }

        private bool isPublish(string carNumber)
        {
            DBHelper db = new DBHelper();
            return db.isPublish(carNumber);
        }


        private HistoryCar getHistoryCar(Car car)
        {
            HistoryCar hc = new HistoryCar();
            hc.car = car;

            DBHelper DBhelp = new DBHelper();
            hc.historyItems = DBhelp.getHistoryByCarNumber(int.Parse(car.CarNumber));
            return hc;
        }

        private void UpdateCarHistory(string CarNumber)
        {
            /*
            //Create connection to GarageServer
            TcpClient tc = new TcpClient();
            IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 4050);
            tc.Connect(serverEndPoint);
            NetworkStream clientStream = tc.GetStream();

            //Send request for update cars
            ASCIIEncoding encoder = new ASCIIEncoding();
            byte[] buffer = new byte[4096];
            String str = "update:" + CarNumber;
            buffer = encoder.GetBytes(str);
            clientStream.Write(buffer, 0, buffer.Length);
            clientStream.Flush();

            //Read Path of car
            int byteRead;
            byteRead = clientStream.Read(buffer, 0, buffer.Length);*/
            DBHelper db = new DBHelper();
            
            string buf = "Send data for " + CarNumber;
            /*while ((buf = encoder.GetString(buffer, 0, byteRead)).Contains("Send data for"))
            {*/
                string carNumber = buf.ToString().Split(' ')[3];

               /* byteRead = clientStream.Read(buffer, 0, 4096);
                string folderPath = encoder.GetString(buffer, 0, byteRead);*/

                string[] files = Directory.GetFiles(Path.Combine(@"C:\CarBook\Cars", carNumber));

                foreach (var file in files)
                {
                    if (file.Contains("Details"))
                    {
                        Car car = XMLHelper.ReadFromFile<Car>(file);
                        db.Open();
                        db.InsertCar(car);
                        db.Close();
                    }
                    else if (file.Contains("CarHistory"))
                    {
                        HistoryItem hi = XMLHelper.ReadFromFile<HistoryItem>(file);
                        db.Open();
                        db.InsertHistoryItem(hi);
                        db.Close();
                    }
                }
            /*}*/

            db.Open();
            DateTime dateMax = db.getMaxDateCarTreatment(CarNumber);
            if(dateMax.Equals(new DateTime()))
            {
                dateMax = db.getCarByNumber(CarNumber).RoadDate;
            }
            bool flag = db.updateDateCarTreatment(CarNumber, dateMax);
            String KM = db.getMaxKM(dateMax, CarNumber);
            flag = db.updateKMCar(CarNumber, KM);
            db.Close();

        }

        //
        // GET: /History/AddAd
        public ActionResult AddAD(string carNumber)
        {
            Advertisement AD = new Advertisement();
            return View(AD);
        }
        private string getValueFromUserTable(string userId, string column)
        {
            DBHelper db = new DBHelper();
            return db.getValueFromUserTable(userId, column);
        }

        //
        // POST: /History/AddAd
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddAD(Advertisement model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userId = User.Identity.GetUserId();
            string ID = getValueFromUserTable(userId, "IsraeliIdentify");
            model.DatePublished = DateTime.Now;
            model.Location = getValueFromUserTable(userId, "Address");
            model.SellerName = getValueFromUserTable(userId, "Name");
            model.Tel = getValueFromUserTable(userId, "PhoneNumber");

            DBHelper db = new DBHelper();
            db.insertAdvertisment(model);

            return RedirectToAction("History", "History", new { carNumber = model.CarNumber, isOwner = true });
        }

        public async Task<ActionResult> DeleteAD(string carNumber)
        {
            DBHelper db = new DBHelper();
            db.deleteAd(carNumber);

            return RedirectToAction("History", "History", new { carNumber = carNumber, isOwner = true });
        }
    }
}
