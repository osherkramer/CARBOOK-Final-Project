using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using System.Data;
using Excel = Microsoft.Office.Interop.Excel;
using System.Collections.Generic;

namespace GarageServer
{
    class Server
    {
        private TcpListener listener;
        private Thread thread;
        private Config conf;
        private bool running;
        private int port = 4050;

        public Server()
        {
            listener = new TcpListener(IPAddress.Any, port);
            conf = new Config();
            conf.ReadDataFromConfigFile();
        }

        public void beginWork()
        {
            Logger.writeMessage("Garage server running now...");
            running = true;
            thread = new Thread(new ThreadStart(Listen));
    
            thread.Start();
        }

        private void Listen()
        {
            listener.Start();
            Logger.writeMessage("Garage server listener running now for any IP on port 4050 and will wait for 1000ms for any client");

            TcpClient client;

            while (running)
            {
                try
                {
                    Logger.writeMessage("Waiting for client...");
                    client = listener.AcceptTcpClient();

                    Thread clientThread = new Thread(new ParameterizedThreadStart(HandleClientComm));
                    clientThread.Start(client);
                }
                catch (SocketException e)
                {
                    if(!running && e.HResult.ToString().Equals("-2147467259"))
                    {
                        Logger.writeMessage("Shouting down the garage server...");
                        break;
                    }
                    Logger.writeError(e.ErrorCode + " " + e.ToString());
                    continue;
                }
            }
        }

        private void HandleClientComm(object client)
        {
            TcpClient clientTCP = (TcpClient)client;
            NetworkStream netStream = clientTCP.GetStream();
            String clientName = ((IPEndPoint)clientTCP.Client.RemoteEndPoint).Address.ToString() + ":" + ((IPEndPoint)clientTCP.Client.RemoteEndPoint).Port.ToString();
            Logger.writeMessage("Client connected from " + clientName);

            byte[] message = new byte[4096];
            int bytesRead;

            while(true)
            {
                try
                {
                    bytesRead = netStream.Read(message, 0, 4096);
                }
                catch (IOException e)
                {
                    if(e.HResult.ToString().Equals("-2146232800"))
                    {
                        Logger.writeMessage("Client " + clientName + " disconnect");
                        break;
                    }

                    Logger.writeError(e.ToString());
                    continue;
                }
                catch (Exception e)
                {
                    Logger.writeError(e.ToString());
                    continue;
                }
                //bytesRead = netStream.Read(message, 0, 4096);

                if (bytesRead == 0)
                {
                    Logger.writeError("Client " + clientName + " send empty message");
                    continue;
                }

                ASCIIEncoding encoder = new ASCIIEncoding();
                string cmd = encoder.GetString(message, 0, bytesRead);

                string[] command = cmd.Split(':');

                if (command.Length != 2 || !string.Equals(command[0], "update", StringComparison.CurrentCultureIgnoreCase))
                {
                    Logger.writeError("Client " + clientName + " send unexpected message");
                    continue;
                }

                Logger.writeMessage("Client " + clientName + " ask for: " + cmd);

                string[] cars = command[1].Split(',');

                if(string.Equals(cars[0], "all", StringComparison.CurrentCultureIgnoreCase))
                {
                    Logger.writeMessage("Start to compress all data for " + clientName);
                    string FileToStoredZip = Path.Combine(conf.ZippedData, "All.zip");
                    if (File.Exists(FileToStoredZip))
                        File.Delete(FileToStoredZip);

                    bool success = ZipHelper.Compress(conf.SavedDataLocation, FileToStoredZip);

                    Logger.writeMessage((success ? "Success" : "Unsucess") + " to compress all data");
                    
                    if(!success)
                    {
                        String str = "Unsuccess send all data";
                        byte[] buffer = encoder.GetBytes(str);
                        netStream.Write(buffer, 0, buffer.Length);
                        netStream.Flush();
                        continue;
                    }
                    //Send data to client

                    //Send Finish to client
                }
                else
                {
                   for(int i = 0; i < cars.Length; i++)
                    {
                        Logger.writeMessage("Start to compress all data about " + cars[i] + " for " + clientName);
                        string pathOfCar = Path.Combine(conf.SavedDataLocation, cars[i]);

                        if (!Directory.Exists(pathOfCar))
                        {
                            Logger.writeError("Client " + clientName + " ask for " + cars[i] + " data, but this data not exist");
                            String str1 = "Data for " + cars[i] + " not found";
                            byte[] buffer1 = encoder.GetBytes(str1);
                            netStream.Write(buffer1, 0, buffer1.Length);
                            netStream.Flush();
                            continue;
                        }

                        string FileToStoredZip = Path.Combine(conf.ZippedData, cars[i] + ".zip");
                        if (File.Exists(FileToStoredZip))
                            File.Delete(FileToStoredZip);

                        bool success = ZipHelper.Compress(pathOfCar, FileToStoredZip);

                        Logger.writeMessage((success ? "Success" : "Unsucess") + " to compress data for " + cars[i]);

                        if (!success)
                        {
                            String str1 = "Unsuccess send " + cars[i] + " data";
                            byte[] buffer1 = encoder.GetBytes(str1);
                            netStream.Write(buffer1, 0, buffer1.Length);
                            netStream.Flush();
                            continue;
                        }
                        //Send data to client
                        String str = "Send data for " + cars[i];
                        byte[] buffer = encoder.GetBytes(str);
                        netStream.Write(buffer, 0, buffer.Length);
                        netStream.Flush();

                        buffer = encoder.GetBytes("Size: " + File.ReadAllBytes(FileToStoredZip).Length);
                        netStream.Write(buffer, 0, buffer.Length);
                        netStream.Flush();

                        string[] fileLines = File.ReadAllLines(FileToStoredZip, Encoding.GetEncoding("windows-1255"));
                        string fileData = "";

                        foreach (var _str in fileLines)
                            fileData = _str + "\n";

                        buffer = encoder.GetBytes(fileData);

                        int numIteration = buffer.Length / 4096;
                        numIteration++;

                        for (int j = 0; j < numIteration; j++)
                        {
                            if (j*4096 >= buffer.Length - 4096)
                                netStream.Write(buffer, j * 4096, buffer.Length);
                            else
                                netStream.Write(buffer, j * 4096, (j + 1) * 4096);

                            netStream.Flush();
                        }
                    }

                    //Send Finish to client;
                }
            }

            clientTCP.Close();
        }

        public bool CreateHistoryItem(string carNumber, int hiNumber)
        {
            Logger.writeMessage("Starting create " + hiNumber + " of history items for car " + carNumber);
            bool success = true;
            string directory = Path.Combine(conf.SavedDataLocation, carNumber);

            Directory.CreateDirectory(directory);

            if (!File.Exists(directory + @"\Details_" + carNumber + @".CBDF.xml"))
            {
                Logger.writeMessage("Car details file not exist for car " + carNumber + ". Start create one...");
                createCarItem(Int32.Parse(carNumber));
            }

            for (int i = 0; i < hiNumber; i++)
            {
                Logger.writeMessage("Create No." + i + 1 + " history item for car " + carNumber);
                HistoryItem hi = CreateHistoryItem(carNumber);

                String file = "CarHistory_" + carNumber + "_" + hi.Date.ToString().Replace('-', '_').Replace(' ', '_').Replace(':','_') + ".CBDF.xml";
                string fileName = Path.Combine(directory, file);

                if (!XMLHelper.WriteToFile(hi, getFilePath(fileName)))
                {
                    success = false;
                }
            }

            return success;
        }

        public Car createCarItem(int carNumber)
        {
            Car car = new Car();

            string[] AllCars = System.IO.File.ReadAllLines(conf.CarsData, Encoding.GetEncoding("windows-1255")); //Read in hebrew
            Random rnd = new Random();
            int row = rnd.Next(0, AllCars.Length);

            string[] CarData = ((string)AllCars.GetValue(row)).Split(';');

            car.CarNumber = carNumber.ToString();
            car.CarVIN = null;
            car.Yad = rnd.Next(0, 9).ToString();
            car.CarColor = "Red";

            car.CodeTable = CarData.GetValue(0).ToString();
            car.ProductSymbol = CarData.GetValue(1).ToString();
            car.ProductName = CarData.GetValue(2).ToString();
            car.ProductCode = CarData.GetValue(3).ToString();
            car.ProductDescription = CarData.GetValue(4).ToString();
            car.CommericalAlias = CarData.GetValue(5).ToString();
            car.EngineCapacity = CarData.GetValue(6).ToString();
            car.TotalWeight = CarData.GetValue(7).ToString();
            car.Propulsion = CarData.GetValue(8).ToString();
            car.AC = CarData.GetValue(9).ToString().Equals("X") ? "Yes" : "No";
            car.ABS = CarData.GetValue(10).ToString().Equals("X") ? "Yes" : "No";
            car.AirBags = CarData.GetValue(11).ToString().Equals("X") ? "Yes" : "No";
            car.PowerSteering = CarData.GetValue(12).ToString().Equals("X") ? "Yes" : "No";
            car.Gaer = CarData.GetValue(13).ToString().Equals("X") ? "Automatic" : "Manual";
            car.PowerWindow = CarData.GetValue(14).ToString().Equals("X") ? "Yes" : "No";
            car.Roof = CarData.GetValue(15).ToString().Equals("X") ? "Yes" : "No";
            car.MagnesiumWheels = CarData.GetValue(16).ToString().Equals("X") ? "Yes" : "No";
            car.Box = CarData.GetValue(17).ToString().Equals("X") ? "Yes" : "No";
            car.Body = CarData.GetValue(18).ToString();
            car.TrimLevel = CarData.GetValue(19).ToString();
            car.FuelType = CarData.GetValue(20).ToString();
            car.DorNumber = CarData.GetValue(21).ToString();
            car.HorsePower = CarData.GetValue(22).ToString();
            car.SeatNumber = CarData.GetValue(23).ToString();
            car.ESP = CarData.GetValue(24).ToString().Equals("X") ? "Yes" : "No";
            car.TowingCapacityWithBrakes = CarData.GetValue(25).ToString();
            car.TowingCapacityWithoutBrakes = CarData.GetValue(26).ToString();
            car.RegulatoryType = CarData.GetValue(27).ToString();
            car.ConverterType = CarData.GetValue(28).ToString();
            car.Hiberdi = CarData.GetValue(29).ToString().Equals("X") ? "Yes" : "No";
            car.GreenIndex = CarData.GetValue(30).ToString();
            car.InfectionGroup = CarData.GetValue(31).ToString();
            car.ControlDeviationFromPath = CarData.GetValue(32).ToString().Equals("X") ? "Yes" : "No";
            car.BounceForward = CarData.GetValue(33).ToString().Equals("X") ? "Yes" : "No";
            car.IdentificationOfDeadArea = CarData.GetValue(34).ToString().Equals("X") ? "Yes" : "No";
            car.AdaptiveCruiseControl = CarData.GetValue(35).ToString().Equals("X") ? "Yes" : "No";
            car.IdentificationOfPedestrians = CarData.GetValue(36).ToString().Equals("X") ? "Yes" : "No";
            car.BrakeSystem = CarData.GetValue(37).ToString().Equals("X") ? "Yes" : "No";
            car.ReverseCamera = CarData.GetValue(38).ToString().Equals("X") ? "Yes" : "No";
            car.TirePressureSensors = CarData.GetValue(39).ToString().Equals("X") ? "Yes" : "No";
            car.BeltSensors = CarData.GetValue(40).ToString().Equals("X") ? "Yes" : "No";
            car.SafetyScore = CarData.GetValue(41).ToString();
            car.LevelOfSafetyAccessories = CarData.GetValue(42).ToString();
            car.AutoForwardLighting = CarData.GetValue(43).ToString().Equals("X") ? "Yes" : "No";
            car.AutomaticControlOfHighLights = CarData.GetValue(44).ToString().Equals("X") ? "Yes" : "No";
            car.IdentifyingDangerousZoomingMode = CarData.GetValue(45).ToString().Equals("X") ? "Yes" : "No";
            car.IdentifyingTrafficSigns = CarData.GetValue(46).ToString().Equals("X") ? "Yes" : "No";

            int i = rnd.Next(0, (CarData.Length-47) / 2);
            car.Year = CarData.GetValue(47 + i * 2).ToString();
            car.AgraGroup = CarData.GetValue(47 + i * 2 + 1).ToString();

            car.RoadDate = new DateTime(Int32.Parse(car.Year), rnd.Next(1, 13), 1);

            string path = Path.Combine(conf.SavedDataLocation, carNumber.ToString());
            string file =  @"Details_" + carNumber + @".CBDF.xml";
            string fileName = Path.Combine(path, file);
            Logger.writeMessage("Finish create details file for car " + carNumber.ToString());
            XMLHelper.WriteToFile(car, getFilePath(fileName));
            return car;
        }

        public bool CreateHistoryItemRandom(int numberCars, int hiNumber)
        {
            bool success = true;

            for(int i = 0; i < numberCars; i++)
            {
                Random rnd = new Random();
                String carNumber = rnd.Next(1, 10).ToString();
                for(int j = 0; j < 6; j++)
                {
                    carNumber += rnd.Next(0, 10).ToString();
                }

                int numberHI = rnd.Next(1, hiNumber);

                if (!CreateHistoryItem(carNumber, numberHI))
                {
                    success = false;
                }
            }

            return success;
        }

        private HistoryItem CreateHistoryItem(string carNumber)
        {
            HistoryItem hi = new HistoryItem();
            string path = Path.Combine(conf.SavedDataLocation, carNumber);
            string file = @"Details_" + carNumber + @".CBDF.xml";
            string fileName = Path.Combine(path, file);
            Car car = XMLHelper.ReadFromFile<Car>(fileName);

            hi.CarNumber = carNumber;

            Random rnd = new Random();
            int TypeCare = rnd.Next(1, 5);

            Random gen = new Random();
            int range = (DateTime.Today - GetLastCareTime(carNumber)).Days;          
            DateTime randomDate = DateTime.Today.AddDays(-gen.Next(range));
            hi.Date = randomDate;

            switch (TypeCare)
            /*{
                case 1: //Tretment
                    int random = rnd.Next(GetLastYearTretment(carNumber) + 1, 16);
                    hi.CareType = ((CarsItems)random);
                    string[] lines = System.IO.File.ReadAllLines(conf.TreatmentsLocation + @"\" + hi.CareType + ".txt", Encoding.GetEncoding("windows-1255")); //Read in hebrew
                    hi.Treatment = lines;
                    break;
                case 2: //Body work and painting
                    DataTable datatable = importExelFileToDataTable(4);
                    hi.CareType = CarsItems.TurningCarParts;
                    int random1 = rnd.Next(1, 11);
                    List<string> lines1 = new List<string>();

                    int i = 0;

                    while(i < random1)
                    {
                        
                        int column = rnd.Next(0, 5);
                        int row = rnd.Next(0, 42);

                        string tretment = datatable.Rows[row][column].ToString();

                        if (tretment == "" || lines1.Contains(tretment))
                            continue;

                        lines1.Add(datatable.Columns[column].ToString() + " - " + tretment);                   
                        
                        i++;
                    }

                    hi.Treatment = lines1.ToArray();
                    break;
                case 3: //Mechanics and electricity
                    DataTable datatable1 = importExelFileToDataTable(2);
                    hi.CareType = CarsItems.ElectricalMechanics;
                    int random2 = rnd.Next(1, 10);
                    string[] lines2 = new string[random2];



                    hi.Treatment = lines2;
                    break;
                case 4: //Engine and gear
                    DataTable datatable2 = importExelFileToDataTable(3);
                    hi.CareType = CarsItems.EngineAndGear;
                    int random3 = rnd.Next(1, 10);
                    string[] lines3 = new string[random3];



                    hi.Treatment = lines3;
                    break;
                case 5: //Car accessories
                    DataTable datatable3 = importExelFileToDataTable(1);
                    hi.CareType = CarsItems.CarAccessories;
                    int random4 = rnd.Next(1, 10);
                    string[] lines4 = new string[random4];



                    hi.Treatment = lines4;
                    break;
            }           

            return hi;*/

            {
                case 1: //Tretment
                    int random = rnd.Next(GetLastYearTretment(carNumber) + 1, 16);
                    hi.CareType = ((CarsItems)random);
                    string[] lines = System.IO.File.ReadAllLines(conf.TreatmentsLocation + @"\" + hi.CareType + ".txt", Encoding.GetEncoding("windows-1255")); //Read in hebrew
                    hi.Treatment = lines;
                    break;
                case 2: //Body work and painting
                    DataTable datatable = importExelFileToDataTable(4);
                    hi.CareType = CarsItems.TurningCarParts;
                    //int random1 = rnd.Next(1, 11);
                    List<string> lines1 = new List<string>();

                    lines1 = getCareTreatment(datatable);

                    /* int i = 0;

                     while(i < random1)
                     {

                         int column = rnd.Next(0, 4);
                         int row = rnd.Next(0, 40);

                         string tretment = datatable.Rows[row][column].ToString();

                         if (tretment == "" || lines1.Contains(tretment))
                             continue;

                         lines1.Add(datatable.Columns[column].ToString() + " - " + tretment);                   

                         i++;
                     }*/

                    hi.Treatment = lines1.ToArray();
                    break;
                case 3: //Mechanics and electricity
                    DataTable datatable1 = importExelFileToDataTable(2);
                    hi.CareType = CarsItems.ElectricalMechanics;
                    //int random2 = rnd.Next(1, 10);
                    List<string> lines2 = new List<string>();
                    lines2 = getCareTreatment(datatable1);
                    hi.Treatment = lines2.ToArray();
                    break;
                case 4: //Engine and gear
                    DataTable datatable2 = importExelFileToDataTable(3);
                    hi.CareType = CarsItems.EngineAndGear;
                    //int random3 = rnd.Next(1, 10);
                    List<string> lines3 = new List<string>();
                    lines3 = getCareTreatment(datatable2);
                    hi.Treatment = lines3.ToArray();
                    break;
                case 5: //Car accessories
                    DataTable datatable3 = importExelFileToDataTable(1);
                    hi.CareType = CarsItems.CarAccessories;
                    //int random4 = rnd.Next(1, 10);
                    List<string> lines4 = new List<string>();
                    lines4 = getCareTreatment(datatable3);
                    hi.Treatment = lines4.ToArray();
                    break;
            }

            return hi;

        }

        private DateTime GetLastCareTime(string carNumber)
        {
            DateTime dt = new DateTime(1900,1,1);
            string path = Path.Combine(conf.SavedDataLocation, carNumber);
            string pattern = "CarHistory_*";
            string[] carsCares = Directory.GetFiles(path, pattern);

            if (carsCares == null || carsCares.Length == 0)
            {
                string pathFolder = Path.Combine(conf.SavedDataLocation, carNumber);
                string file = @"Details_" + carNumber + @".CBDF.xml";
                string fileName = Path.Combine(pathFolder, file);
                Car car = XMLHelper.ReadFromFile<Car>(fileName);

                return car.RoadDate;
            }

            foreach(var care in carsCares)
            {
                HistoryItem hi = XMLHelper.ReadFromFile<HistoryItem>(care);
                if (hi.Date > dt)
                    dt = hi.Date;
            }

            return dt;
        }

        private int GetLastYearTretment(string carNumber)
        {
            int i = 0;
            string path = Path.Combine(conf.SavedDataLocation, carNumber);
            string pattern = "CarHistory_*";
            string[] carsCares = Directory.GetFiles(path, pattern);

            foreach (var care in carsCares)
            {
                HistoryItem hi = XMLHelper.ReadFromFile<HistoryItem>(care);
                if (hi.CareType < (CarsItems)7 && hi.CareType > (CarsItems)i)
                    i = (int)hi.CareType;
            }

            if (i >= 7)
                return 6;

            return i;
        }

        public string getFilePath(string fileName)
        {
            if (!File.Exists(fileName))
                return fileName;

            string[] fileSplit = fileName.Split('.');
            string tempFile = "";
            foreach (var str in fileSplit)
            {
                tempFile += str + ".";
                if (str.Equals("CBDF"))
                {
                    tempFile = tempFile.Substring(0, tempFile.Length - 6);
                    break;
                }

                if (str.Equals("xml"))
                {
                    tempFile = tempFile.Substring(0, tempFile.Length - 5);
                    break;
                }

            }

            int i = 1;
            do
            {
                fileName = tempFile + "_" + i;
                i++;
            } while (File.Exists(fileName + ".CBDF.xml"));


            fileName += ".CBDF.xml";

            return fileName;
        }
        
        public DataTable importExelFileToDataTable(int index)
        {
            if(index < 0 || index > 4)
            {
                return null;
            }
            
            //Create COM Objects. Create a COM object for everything that is referenced
            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(Path.Combine(conf.TreatmentsLocation, "Treatments.xlsx"));
            Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[index];
            Excel.Range xlRange = xlWorksheet.UsedRange;

            int rowCount = xlRange.Rows.Count;
            int colCount = xlRange.Columns.Count;

            DataTable dataTable = new DataTable(xlWorksheet.Name);

            for (int i = 1; i < colCount; i++)
            {
                string name = ((Excel.Range)xlWorksheet.Cells[1,i]).Value2.ToString();
                dataTable.Columns.Add(name);
            }

            for (int i = 2; i < rowCount; i++)
            {
                string[] row = new string[colCount - 1];
                for (int j = 1; j < colCount; j++)
                {
                    string data = ((Excel.Range)xlWorksheet.Cells[i, j]).Value2 == null ? "" : ((Excel.Range)xlWorksheet.Cells[i, j]).Value2.ToString();
                    row[j-1] = data;
                }
                dataTable.Rows.Add(row);
            }

            return dataTable;
        }

        private List<String> getCareTreatment(DataTable datatable)
        {
            Random random = new Random();
            int random1 = random.Next(1, 11);
            List<string> lines = new List<string>();

            int i = 0;

            while (i < random1)
            {
                int columnTable = datatable.Columns.Count + 1;
                int rowTable = datatable.Rows.Count + 1;
                int column = random.Next(0, columnTable - 1);
                int row = random.Next(0, rowTable - 1);

                string tretment = datatable.Rows[row][column].ToString();

                if (tretment == "" || lines.Contains(tretment))
                    continue;

                lines.Add(datatable.Columns[column].ToString() + " - " + tretment);

                i++;
            }
            return lines;

        }

        public void CloseServer()
        {
            running = false;
            listener.Stop();
            while (thread.IsAlive);
           
        }
    }
}
