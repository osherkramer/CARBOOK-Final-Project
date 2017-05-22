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
        public ActionResult Index(String carNumber)
        {
            //List<HistoryItem> historyI = new List<HistoryItem>();
            DBHelper DBhelp = new DBHelper();
            Car car =DBhelp.getCarByNumber(carNumber);
            HistoryCar historyCar = getHistoryCar(car);

            /*do
            {
                car = DBhelp.getCar();
                historyI = DBhelp.getHistoryByCarNumber(int.Parse(car.CarNumber));
            } while (historyI == null);*/

            /*foreach (var hi in historyI)
            {
                HistoryCar hc = new HistoryCar();
                hc = getHistoryCar(car);
                historyCar.Add(hc);

            }*/

            return View(historyCar);
        }

       
        private HistoryCar getHistoryCar(Car car)
        {
            HistoryCar hc = new HistoryCar();


            hc.CarNumber = car.CarNumber;
            hc.CarColor = car.CarColor;
            hc.ShildaNumber = car.ShildaNumber;
            hc.RoadDate = car.RoadDate;
            hc.ProductName = car.ProductName;
            hc.Year = car.Year;

            DBHelper DBhelp = new DBHelper();
            hc.historyItems = DBhelp.getHistoryByCarNumber(int.Parse(car.CarNumber));
            return hc;
        }
    }
}