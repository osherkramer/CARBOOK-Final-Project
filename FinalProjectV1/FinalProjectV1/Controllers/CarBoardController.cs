using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FinalProjectV1.Models;
using FinalProjectV1.Helpers;

namespace FinalProjectV1.Controllers
{

    public class CarBoardController : Controller
    {

        // GET: CarBoards
       /* public ActionResult Index()
        {

            List<Advertisement> ads = new List<Advertisement>();
            DBHelper DBhelp = new DBHelper();
            ads = DBhelp.returnAdvertisments();
            List<CarBoard> carB = new List<CarBoard>();

            foreach (var ad in ads)
            {
                Car car = DBhelp.getCarByNumber(ad.CarNumber);
                if (car != null)
                {
                    CarBoard cb = setAndGetCarBoardAd(ad, car);
                    carB.Add(cb);

                }
            }


            return View(carB);
        }*/

        //set and return one advertisment for the car board
        private CarBoard setAndGetCarBoardAd(Advertisement ad, Car car)
        {

            if (ad == null && car == null)
                return null;

            if (ad.CarNumber.Equals(car.CarNumber))
            {
                CarBoard cb = new CarBoard();
                cb.CarNumber = ad.CarNumber;
                cb.SellerName = ad.SellerName;
                cb.Tel = ad.Tel;
                cb.Pic = ad.Pic;
                cb.Description = ad.Description;
                cb.Location = ad.Location;
                cb.DatePublished = ad.DatePublished;
                cb.Price = ad.Price;

                cb.ABS = car.ABS;
                cb.AC = car.AC;
                cb.AirBags = car.AirBags;
                cb.CommericalAlias = car.CommericalAlias;
                cb.EngineCapacity = car.EngineCapacity;
                cb.FuelType = car.FuelType;
                cb.Gaer = car.Gaer;
                cb.KM = car.KM;
                cb.CarColor = car.CarColor;
                cb.PowerWindow = car.PowerWindow;

                cb.Ownership = car.ownerShip;
                cb.ProductName = car.ProductName;
                cb.Roof = car.Roof;
                cb.Yad = car.Yad;
                cb.Year = car.Year;
                return cb;
            }

            return null;
        }

        public ActionResult Index(string productName, string model, int? startYear, int? endYear, string gear, string location, string minPrice, string maxPrice)
        {
            DBHelper db = new DBHelper();
            List<CarBoard> carsBoard = db.search(productName, model, startYear, endYear, gear, location, minPrice, maxPrice);
            return View(carsBoard);
        }

      
        public JsonResult getCars()
        {
            DBHelper db = new DBHelper();
            return Json(db.getCarList());
        }

        public JsonResult getCarModel(string car)
        {
            DBHelper db = new DBHelper();
            return Json(db.getCarModelList(car));
        }

        public double calculateGradeOfCar(string carNumber)
        {
            DBHelper db = new DBHelper();
            List<int> treatmentID = db.getTreatmentIDs(Int32.Parse(carNumber));

            if (treatmentID==null) 
                return -1;

            List<int> partID = new List<int>();
            List<int> tempID = new List<int>();
            foreach(var id in treatmentID)
            {
                tempID=db.getPartsIDs(id);

                foreach(var temp in tempID)
                {
                    partID.Add(temp);
                }
            }

            int sum = 0;
            foreach(var id in partID)
            {
                sum += db.returnPartValueBYID(id);
            }

            return sum;
        }
    }
}