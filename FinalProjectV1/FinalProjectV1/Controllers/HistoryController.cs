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

namespace FinalProjectV1.Controllers
{
    public class HistoryController : Controller
    {
        // GET: History
        public ActionResult Index(String carNumber, String password)
        {
            
            /*string[] cars = Directory.GetDirectories(@"C:\CarBook\Cars");
            foreach (var c in cars)
            {
                carNumber = c.Split('\\')[3];
                UpdateCarHistory(carNumber);
                int grade = 50;
                DBHelper db = new DBHelper();
                List<int> treatmentID = db.getTreatmentIDs(Int32.Parse(carNumber));
                if (treatmentID == null)
                    continue;

                foreach (var t in treatmentID)
                {
                    List<int> partID = db.getPartsIDs(t);
                    if (partID == null)
                        continue;

                    foreach(var p in partID)
                    {
                        int value = db.returnPartValueBYID(p);
                        if (value > 5 || value < -5)
                            value = 0;
                        grade += value;
                    }
                }
                db.setGrade(carNumber, grade);
            }*/
            carNumber = "6719498";
            password = "Aa123456";
            //List<HistoryItem> historyI = new List<HistoryItem>();
            DBHelper DBhelp = new DBHelper();
            DateTime dt = DBhelp.getTemproryUsersByCarID(Int32.Parse(carNumber), password);
            if(dt < DateTime.Now)
            {
                ModelState.AddModelError("UP", "Car number or password incurrect or expired");
                HistoryCar hc = new HistoryCar();
                hc.historyItems = new List<HistoryItem>();

                return View(hc);
            }

            Car car = DBhelp.getCarByNumber(carNumber);
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
            hc.Gaer = car.Gaer;
            hc.Yad = car.Yad;

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
    }
}
