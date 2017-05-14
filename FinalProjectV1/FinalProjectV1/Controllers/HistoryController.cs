using FinalProjectV1.Helpers;
using FinalProjectV1.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace FinalProjectV1.Controllers
{
    public class HistoryController : Controller
    {
        // GET: History
        public ActionResult Index()
        {
            UpdateCarHistory("1658490");
            return View();
        }

        private void UpdateCarHistory(string CarNumber)
        {
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
            byteRead = clientStream.Read(buffer, 0, buffer.Length);
            DBHelper db = new DBHelper();

            string buf = "";
            while ((buf = encoder.GetString(buffer, 0, byteRead)).Contains("Send data for"))
            {
                string carNumber = buf.ToString().Split(' ')[3];

                byteRead = clientStream.Read(buffer, 0, 4096);
                string folderPath = encoder.GetString(buffer, 0, byteRead);

                string[] files = Directory.GetFiles(folderPath);

                foreach(var file in files)
                {
                    if(str.Contains("Details"))
                    {
                        Car car = XMLHelper.ReadFromFile<Car>(file);
                        db.Open();
                        db.InsertCar(car);
                        db.Close();
                    }
                    else if(str.Contains("CarHistory"))
                    {
                        HistoryItem hi = XMLHelper.ReadFromFile<HistoryItem>(file);
                        db.Open();
                        db.InsertHistoryItem(hi);
                        db.Close();
                    }
                }
            }
        }
    }
}