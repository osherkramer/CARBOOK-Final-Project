using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FinalProjectV1.Models;
using FinalProjectV1.Helpers;

namespace FinalProjectV1.Controllers
{
    public class HistoryController : Controller
    {


        // GET: History
        public ActionResult Index()
        {
            List<HistoryItem> historyI = new List<HistoryItem>();
            DBHelper DBhelp = new DBHelper();
            Car car = new Car();
            List<HistoryCar> historyCar = new List<HistoryCar>();

            do
            {
                car = DBhelp.getCar();
                historyI = DBhelp.getHistoryByCarNumber(int.Parse(car.CarNumber));
            } while (historyI == null);

            foreach (var hi in historyI)
            {
                HistoryCar hc = new HistoryCar();
                hc = getHistoryCar(car, hi);
                historyCar.Add(hc);

            }
            return View(historyCar);
        }

        //check all every var show the car
        private HistoryCar getHistoryCar(Car car, HistoryItem item)
        {
            HistoryCar hc = new HistoryCar();

            if (car.CarNumber.Equals(item.CarNumber))
            {
                hc.CarNumber = car.CarNumber;
                hc.CarColor = car.CarColor;
                hc.ShildaNumber = car.ShildaNumber;
                hc.RoadDate = car.RoadDate;
                hc.ProductName = car.ProductName;
                hc.Year = car.Year;

                hc.CareType = item.CareType;
                hc.Date = item.Date;
                hc.GarageName = item.GarageName;
                hc.KM = item.KM;
                hc.Treatment = item.Treatment;
                hc.TreatmentID = item.TreatmentID;

            }
         
            return hc;
        }
    }
}