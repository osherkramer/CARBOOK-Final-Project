using FinalProjectV1;
using FinalProjectV1.Helpers;
using FinalProjectV1.Models;
using System;
using System.Collections.Generic;
using System.Device.Location;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinalProjectV1.Controllers
{
    public class DataUpdateController : Controller
    {

        // GET: DataUpdate
        public ActionResult Index()
        {
            UpdateCars();
            UpdateGrades();

            return View();
        }

        private void UpdateCars()
        {
            string[] files = Directory.GetDirectories(@"C:\CarBook\Cars");
            List<string> carsNumbers = new List<string>();

            foreach (var file in files)
            {
                string temp = file.Split('\\')[3];

                UpdateCarHistory(temp);
            }
        }

        private void UpdateCarHistory(string CarNumber)
        {
            string[] files = Directory.GetFiles(Path.Combine(@"C:\CarBook\Cars", CarNumber));

            foreach (var file in files)
            {
                if (file.Contains("Details"))
                {
                    Car car = XMLHelper.ReadFromFile<Car>(file);
                    DBHelper.Open();
                    DBHelper.InsertCar(car);
                    DBHelper.Close();
                }
                else if (file.Contains("CarHistory"))
                {
                    HistoryItem hi = XMLHelper.ReadFromFile<HistoryItem>(file);
                    DBHelper.Open();
                    DBHelper.InsertHistoryItem(hi);
                    DBHelper.Close();
                }
            }

            DBHelper.Open();
            DateTime dateMax = DBHelper.getMaxDateCarTreatment(CarNumber);
            if (dateMax.Equals(new DateTime()))
            {
                dateMax = DBHelper.getCarByNumber(CarNumber).RoadDate;
            }
            bool flag = DBHelper.updateDateCarTreatment(CarNumber, dateMax);
            String KM = DBHelper.getMaxKM(dateMax, CarNumber);
            flag = DBHelper.updateKMCar(CarNumber, KM);
            DBHelper.Close();

        }

        private void UpdateGrades()
        {
            List<Car> allCars = DBHelper.getAllCars();
            List<Parts> allParts = DBHelper.getAllParts();
            float oldestCarAge = 20;
            float fitness = 0;
            int currentYear = DateTime.Now.Year;
            foreach (Car car in allCars)
            {
                Dictionary<string, int> partsToYear = new Dictionary<string, int>();
                List<HistoryItem> HI = DBHelper.getHistoryByCarNumber(Int32.Parse(car.CarNumber));

                if (HI != null)
                {
                    foreach (var hi in HI)
                    {
                        foreach (var part in hi.Treatment)
                        {
                            if (partsToYear.ContainsKey(part) && partsToYear[part] <= hi.Date.Year)
                                partsToYear.Remove(part);

                            partsToYear.Add(part, hi.Date.Year);
                        }
                    }
                }

                foreach (var part in allParts)
                {
                    if (!partsToYear.ContainsKey(part.partName))
                    {
                        partsToYear.Add(part.partName, Int32.Parse(car.Year));
                    }
                }

                foreach (var part in allParts)
                {

                    int partYear = partsToYear[part.partName];
                    int partShipua = -1 * part.partValue;

                    fitness += (float)(partShipua * (currentYear - partYear)) / oldestCarAge;

                }
                DBHelper.UpdateGradeFitness(fitness, car.CarNumber);
            }
        }
    }
    }

