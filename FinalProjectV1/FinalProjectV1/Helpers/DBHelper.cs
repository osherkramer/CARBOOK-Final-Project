using FinalProjectV1.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


namespace FinalProjectV1.Helpers
{
    public class DBHelper
    {
        private string connectionString = @"Server=db.cs.colman.ac.il;Database=CarBook;User Id=carbook;password=Car@Book;MultipleActiveResultSets=true";
        private SqlConnection sqlConnection { get; set; }

        public DBHelper()
        {
            sqlConnection = new SqlConnection(connectionString);
            Open();
        }

        public void Open()
        {
            if(sqlConnection.State != ConnectionState.Open)
                sqlConnection.Open();
        }

        public void Close()
        {
            if (sqlConnection.State == ConnectionState.Open)
                sqlConnection.Close();
        }

        public Car getCarByNumber(string carNumber)
        {
            int carNum = 0;
            if(carNumber != null && (carNumber.Length != 7 || carNumber[0].Equals('0') || !Int32.TryParse(carNumber, out carNum)))
                return null;

            SqlCommand cmd = new SqlCommand(string.Format("SELECT * FROM Car WHERE CarID = {0}", carNum));
            cmd.Connection = sqlConnection;

            SqlDataReader sqlDR;

            try
            {
                sqlDR = cmd.ExecuteReader();
                if (!sqlDR.Read())
                    return null;




                Car car = new Car();

                car.CarNumber = sqlDR["CarID"].ToString();
                car.RoadDate = Convert.ToDateTime(sqlDR["RoadDate"].ToString());
                car.Yad = sqlDR["Yad"].ToString();
                car.Year = sqlDR["StartYear"].ToString();
                car.CarVIN = sqlDR["ShildaNumber"].ToString();
                car.EngineCapacity = sqlDR["EngineCapacity"].ToString();
                car.HorsePower = sqlDR["HorsePower"].ToString();
                car.AirBags = sqlDR["AirBags"].ToString();
                car.ABS = sqlDR["CarABS"].ToString();
                car.PowerWindow = sqlDR["PowerWindow"].ToString();
                car.Roof = sqlDR["Roof"].ToString();
                car.MagnesiumWheels = sqlDR["MagnesiumWheels"].ToString();
                car.CarOwnerID = Int32.Parse(sqlDR["OwnerID"].ToString());
                car.ProductName = sqlDR["ProductName"].ToString();
                car.FuelType = sqlDR["FuelType"].ToString();
                car.CarColor = sqlDR["CarColor"].ToString();
                car.Gaer = sqlDR["Gaer"].ToString();
                car.CommericalAlias = sqlDR["CarModel"].ToString();
                car.ownerShip = sqlDR["Ownerships"].ToString();
                car.AC = sqlDR["AC"].ToString();
                car.KM = sqlDR["KM"].ToString();
                car.ShildaNumber = sqlDR["ShildaNumber"].ToString();

                return car;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return new Car();
        }

        public string getValueFromUserTable(string userId, string column)
        {
            SqlCommand cmd = new SqlCommand(string.Format("SELECT {0} FROM AspNetUsers WHERE Id = '{1}'", column, userId));
            cmd.Connection = sqlConnection;

            SqlDataReader sqlDR = cmd.ExecuteReader();

            if (!sqlDR.Read())
                return null;

            return sqlDR[column].ToString();
        }

        public List<Car> getCarByID(int ID)
        {
            SqlCommand cmd = new SqlCommand(string.Format("SELECT * FROM Car WHERE OwnerID = {0}", ID));
            cmd.Connection = sqlConnection;

            SqlDataReader sqlDR = cmd.ExecuteReader();

            if (!sqlDR.Read())
                return null;

            List<Car> cars = new List<Car>();
            do
            {
                Car car = new Car();

                car.CarNumber = sqlDR["CarID"].ToString();
                car.RoadDate = Convert.ToDateTime(sqlDR["RoadDate"].ToString());
                car.Yad = sqlDR["Yad"].ToString();
                car.Year = sqlDR["StartYear"].ToString();
                car.CarVIN = sqlDR["ShildaNumber"].ToString();
                car.EngineCapacity = sqlDR["EngineCapacity"].ToString();
                car.HorsePower = sqlDR["HorsePower"].ToString();
                car.AirBags = sqlDR["AirBags"].ToString();
                car.ABS = sqlDR["CarABS"].ToString();
                car.PowerWindow = sqlDR["PowerWindow"].ToString();
                car.Roof = sqlDR["Roof"].ToString();
                car.MagnesiumWheels = sqlDR["MagnesiumWheels"].ToString();
                car.CarOwnerID = Int32.Parse(sqlDR["OwnerID"].ToString());
                car.ProductName = sqlDR["ProductName"].ToString();
                car.FuelType = sqlDR["FuelType"].ToString();
                car.CarColor = sqlDR["CarColor"].ToString();
                car.Gaer = sqlDR["Gaer"].ToString();
                car.CommericalAlias = sqlDR["CarModel"].ToString();
                car.ownerShip = sqlDR["Ownerships"].ToString();
                car.AC = sqlDR["AC"].ToString();
                cars.Add(car);
                
            } while (sqlDR.Read());



            return cars;
        }

        public Car getCar()
        {
            SqlCommand cmd = new SqlCommand(string.Format("SELECT * FROM Car"));
            cmd.Connection = sqlConnection;

            SqlDataReader sqlDR = cmd.ExecuteReader();

            if (!sqlDR.Read())
                return null;

            Car car = new Car();

            car.CarNumber = sqlDR["CarID"].ToString();
            car.RoadDate = Convert.ToDateTime(sqlDR["RoadDate"].ToString());
            car.Yad = sqlDR["Yad"].ToString();
            car.Year = sqlDR["StartYear"].ToString();
            car.CarVIN = sqlDR["ShildaNumber"].ToString();
            car.EngineCapacity = sqlDR["EngineCapacity"].ToString();
            car.HorsePower = sqlDR["HorsePower"].ToString();
            car.AirBags = sqlDR["AirBags"].ToString();
            car.ABS = sqlDR["CarABS"].ToString();
            car.PowerWindow = sqlDR["PowerWindow"].ToString();
            car.Roof = sqlDR["Roof"].ToString();
            car.MagnesiumWheels = sqlDR["MagnesiumWheels"].ToString();
            car.CarOwnerID = Int32.Parse(sqlDR["OwnerID"].ToString());
            car.ProductName = sqlDR["ProductName"].ToString();
            car.FuelType = sqlDR["FuelType"].ToString();
            car.CarColor = sqlDR["CarColor"].ToString();
            car.Gaer = sqlDR["Gaer"].ToString();
            car.CommericalAlias = sqlDR["CarModel"].ToString();

            return car;

        }

        public List<string> getCarList()
        {
            SqlCommand cmd = new SqlCommand(string.Format("SELECT ProductName FROM Car GROUP BY ProductName"));
            cmd.Connection = sqlConnection;

            SqlDataReader sqlDR = cmd.ExecuteReader();

            if (!sqlDR.Read())
                return null;

            List<string> cars = new List<string>();

            do
            {
                string car = sqlDR["ProductName"].ToString();
                cars.Add(car);
            } while (sqlDR.Read());

            return cars;
        }

        public List<string> getCarModelList(string car)
        {
            SqlCommand cmd = new SqlCommand(string.Format("SELECT CarModel FROM Car WHERE ProductName = '{0}' GROUP BY CarModel", car));
            cmd.Connection = sqlConnection;

            SqlDataReader sqlDR = cmd.ExecuteReader();

            if (!sqlDR.Read())
                return null;

            List<string> cars = new List<string>();

            do
            {
                string c = sqlDR["ProductName"].ToString();
                cars.Add(c);
            } while (sqlDR.Read());

            return cars;
        }



        public bool InsertCar(Car car)
        {
            car.AirBags = car.AirBags.Equals("Yes") ? "כן" : "לא";
            car.ABS = car.ABS.Equals("Yes") ? "כן" : "לא";
            car.PowerWindow = car.PowerWindow.Equals("Yes") ? "כן" : "לא";
            car.Roof = car.Roof.Equals("Yes") ? "כן" : "לא";
            car.MagnesiumWheels = car.MagnesiumWheels.Equals("Yes") ? "כן" : "לא";
            car.FuelType = car.FuelType.Equals("1") ? "בנזין" : "דיזל";
            car.Gaer = car.Gaer.Equals("Automatic") ? "אוטומטי" : "ידני";
            car.ownerShip = car.ownerShip.Equals("Rente") ? "השכרה" : car.ownerShip.Equals("Leasing") ? "ליסינג" : "פרטי";
            car.AC = car.AC.Equals("Yes") ? "כן" : "לא";

            SqlCommand cmd = new SqlCommand(string.Format("INSERT INTO Car (CarID, RoadDate, Yad, StartYear, ShildaNumber, EngineCapacity, HorsePower, AirBags, CarABS, PowerWindow, Roof, MagnesiumWheels, CarTreatment, OwnerID, ProductName, FuelType, CarColor, Gaer, CarModel, Ownerships, AC) VALUES ('{0}' , '{1}' , '{2}' , '{3}' , '{4}' , '{5}' , '{6}' , '{7}' , '{8}' , '{9}' , '{10}' , '{11}' , '{12}' , '{13}' , '{14}' , '{15}' , '{16}' , '{17}' , '{18}', '{19}', '{20}')", car.CarNumber, car.RoadDate, car.Yad, car.Year, car.CarVIN, car.EngineCapacity, car.HorsePower, car.AirBags, car.ABS, car.PowerWindow, car.Roof, car.MagnesiumWheels, "", car.CarOwnerID, car.ProductName, car.FuelType, car.CarColor, car.Gaer, car.CommericalAlias, car.ownerShip, car.AC));
            cmd.Connection = sqlConnection;

            try {
                if (cmd.ExecuteNonQuery() != -1)
                    return true;
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        } 

        public Parts getPart(string PartName)
        {
           
            SqlCommand cmd = new SqlCommand(string.Format("SELECT * FROM Parts WHERE PartName = '{0}'", PartName ));
            cmd.Connection = sqlConnection;

            SqlDataReader sqlDR = cmd.ExecuteReader();

            if (!sqlDR.Read())
                return null;

            Parts part = new Parts();
            part.partID = Int32.Parse(sqlDR["PartID"].ToString());
            part.partValue = Int32.Parse(sqlDR["PartValue"].ToString());
            part.partName = sqlDR["PartName"].ToString();

            return part;

        }

        public HistoryItem getHistoryByID(int ID)
        {
            SqlCommand cmd = new SqlCommand(string.Format("SELECT * FROM Treatment WHERE TreatmentID = {0}", ID));
            cmd.Connection = sqlConnection;

            SqlDataReader sqlDR = cmd.ExecuteReader();

            if (!sqlDR.Read())
                return null;

            HistoryItem HI = new HistoryItem();
            HI.CarNumber = sqlDR["CarID"].ToString();
            HI.Date = Convert.ToDateTime(sqlDR["CareDate"].ToString());
            HI.TreatmentID = Int32.Parse(sqlDR["TreatmentID"].ToString());
            HI.CareType = sqlDR["CareType"].ToString();
            HI.KM = Int32.Parse(sqlDR["KM"].ToString());
            HI.GarageName = sqlDR["GarageName"].ToString();

            return HI;
        }

        public List<HistoryItem> getHistoryByCarNumber(int CarNumber)
        {
            SqlCommand cmd = new SqlCommand(string.Format("SELECT * FROM Treatment WHERE CarID = {0} ORDER BY CareDate ASC", CarNumber));
            cmd.Connection = sqlConnection;
            
            SqlDataReader sqlDR = cmd.ExecuteReader();

            if (!sqlDR.Read())
                return null;

            
            List<HistoryItem> HIList = new List<HistoryItem>();
            do
            {
                HistoryItem HI = new HistoryItem();
                HI.CarNumber = sqlDR["CarID"].ToString();
                HI.Date = Convert.ToDateTime(sqlDR["CareDate"].ToString());
                HI.TreatmentID = Int32.Parse(sqlDR["TreatmentID"].ToString());
                HI.CareType = sqlDR["CareType"].ToString();
                HI.KM = Int32.Parse(sqlDR["KM"].ToString());
                HI.GarageName = sqlDR["GarageName"].ToString();
                HIList.Add(HI);
            } while (sqlDR.Read());
           
            return HIList;
        }

        public bool InsertHistoryItem(HistoryItem HI)
        {
            if (isExistHistoryItem(HI))
                return true;

            SqlCommand cmd = new SqlCommand(string.Format("SELECT Max(TreatmentID) AS LastTreatment FROM Treatment"));
            cmd.Connection = sqlConnection;
            SqlDataReader sqlDR = cmd.ExecuteReader();
            sqlDR.Read();
            int id = 0;
            Int32.TryParse(sqlDR["LastTreatment"].ToString(), out id);
            id++;

            HI.CareType = updateCareTypeName(HI.CareType);

            cmd = new SqlCommand(string.Format("INSERT INTO Treatment (CarID, CareDate, TreatmentID, CareType, KM, GarageName) VALUES ('{0}' , '{1}' , '{2}' , '{3}' , '{4}' , '{5}')", HI.CarNumber, HI.Date, id, HI.CareType, HI.KM, HI.GarageName));
            cmd.Connection = sqlConnection;

            if (cmd.ExecuteNonQuery() == -1)
                return false;

            bool success = true;

            foreach (var part in HI.Treatment)
            {
                if (!InsertTreatmentToPart(id, getPartID(part)))
                    success = false;
            }

            return success;
        }

        private string updateCareTypeName(string careType)
        {
            switch (careType)
            {
                case "EngineAndGear":
                    careType = "מנוע וגיר";
                    break;
                case "TurningCarParts":
                    careType = "חלקי פנים וחוץ";
                    break;
                case "ElectricalMechanics":
                    careType = "רכיבים אלקטרוניים";
                    break;
                case "CarAccessories":
                    careType = "אביזרי רכב";
                    break;
                case "Tretment_15000":
                    careType = "טיפול 15 אלף";
                    break;
                case "Tretment_30000":
                    careType = "טיפול 30 אלף";
                    break;
                case "Tretment_45000":
                    careType = "טיפול 45 אלף";
                    break;
                case "Tretment_60000":
                    careType = "טיפול 60 אלף";
                    break;
                case "Tretment_75000":
                    careType = "טיפול 75 אלף";
                    break;
                case "Tretment_90000":
                    careType = "טיפול 90 אלף";
                    break;
                case "Tretment_105000":
                    careType = "טיפול 105 אלף";
                    break;
                case "Tretment_120000":
                    careType = "טיפול 120 אלף";
                    break;
                case "Treatment_Test_Preparation":
                    careType = "הכנה לטסט";
                    break;
                case "Tretment_Checking_Vehicles_Purchase":
                    careType = "בדיקה לפני קנייה";
                    break;
                case "Tretment_Filling_Gas_AC":
                    careType = "מילוי גז מזגן";
                    break;
                case "Tretment_Front_Adjustment":
                    careType = "כיוון פרונט";
                    break;
                case "Tretment_Minor_Accident":
                    careType = "תאונה קלה";
                    break;
                case "Tretment_Moderate_Accident":
                    careType = "תאונה בינונית";
                    break;
                case "Tretment_Serious_Accident":
                    careType = "תאונה קשה";
                    break;
                case "Tretment_Winter_Check":
                    careType = "בדיקת חורף";
                    break;
                default:
                    break;
            }

            return careType;
        }

        public int getPartID(string part)
        {
            if (part.Contains("מכונאות וחשמל - כללי - מצמד- קלאץ") ||
                part.Contains("משאבות - משאבת מצמד - קלאץ'"))
                part = part.Substring(0, part.Length - 1);
            SqlCommand cmd = new SqlCommand(string.Format("SELECT PartID FROM Parts WHERE PartName = '{0}'", part));
            cmd.Connection = sqlConnection;
            SqlDataReader sqlDR = cmd.ExecuteReader();
            if (!sqlDR.Read())
                return -1;

            int PartID;
            Int32.TryParse(sqlDR["PartID"].ToString(), out PartID);

            return PartID;
        }

        public bool isExistHistoryItem(HistoryItem HI)
        {
            HI.CareType = updateCareTypeName(HI.CareType);
            SqlCommand cmd = new SqlCommand(string.Format("SELECT TreatmentID FROM Treatment WHERE CarID = {0} AND CareDate = '{1}' AND CareType = '{2}' AND KM = {3} AND GarageName = '{4}'", HI.CarNumber, HI.Date, HI.CareType, HI.KM, HI.GarageName));
            cmd.Connection = sqlConnection;
            SqlDataReader sqlDR = cmd.ExecuteReader();
            if (!sqlDR.Read())
                return false;

            return true;
        }

        public bool InsertTreatmentToPart(int TreatmentID, int PartID)
        {
            SqlCommand cmd = new SqlCommand(string.Format("INSERT INTO TreatmentToParts (TreatmentID, PartID) VALUES ('{0}' , '{1}' )", TreatmentID, PartID));
            cmd.Connection = sqlConnection;

            try {
                if (cmd.ExecuteNonQuery() != -1)
                    return true;
            }
            catch(Exception e)
            {
                if (e.HResult == -2146232060)
                    return true;
            }

            return false;
        }

        public List<Parts> getPartsOfTreatment(int TreatmentID)
        {
            SqlCommand cmd = new SqlCommand(string.Format("SELECT * FROM TreatmentToParts WHERE TreatmentID = {0}", TreatmentID));
            cmd.Connection = sqlConnection;

            SqlDataReader sqlDR = cmd.ExecuteReader();

            if (!sqlDR.Read())
                return null;

            List<Parts> parts = new List<Parts>();

            do
            {
                Parts part = getPart(Int32.Parse(sqlDR["PartID"].ToString()));
                parts.Add(part);
            } while (sqlDR.Read());

            return parts;
        }

        public Parts getPart(int PartID)
        {
            SqlCommand cmd = new SqlCommand(String.Format("SELECT * FROM Parts WHERE PartID = {0}", PartID));
            cmd.Connection = sqlConnection;
            SqlDataReader sqlDR = cmd.ExecuteReader();

            if (!sqlDR.Read())
                return null;

            Parts part = new Parts();
            part.partID = Int32.Parse(sqlDR["PartID"].ToString());
            part.partValue = Int32.Parse(sqlDR["PartValue"].ToString());
            part.partName = sqlDR["PartName"].ToString();

            return part; 
        }
		
		public bool insertPart(Parts part)
        {
            SqlCommand cmd = new SqlCommand(String.Format("INSERT INTO Parts (PartID, PartValue, PartName) VALUES ('{0}' , '{1}' , '{2}' ) ", part.partID, part.partValue, part.partName));
            cmd.Connection = sqlConnection;

            if (cmd.ExecuteNonQuery() != -1)
                return true;

            return false;
        }

        public int returnPartValueBYID(int partID)
        {
            SqlCommand cmd = new SqlCommand(String.Format("SELECT PartValue FROM Parts WHERE PartID = {0}", partID));
            cmd.Connection = sqlConnection;
            SqlDataReader sqlDR = cmd.ExecuteReader();

            if (!sqlDR.Read())
                return 65535;

            int partValue = Int32.Parse(sqlDR["PartValue"].ToString());
            return partValue;
        }

        public int returnPartValueByPartName(string partName)
        {
            SqlCommand cmd = new SqlCommand(string.Format("SELECT PartValue FROM Parts WHERE PartName = '{0}'", partName));
            cmd.Connection = sqlConnection;
            SqlDataReader sqlDR = cmd.ExecuteReader();

            if (!sqlDR.Read())
                return 65535;

            int partValue = Int32.Parse(sqlDR["PartValue"].ToString());
            return partValue;


        }

        public DateTime getTemproryUsersByCarID(int carID, string password)
        {
            SqlCommand cmd = new SqlCommand(String.Format("SELECT ExpiryDate FROM TemproryUsers WHERE CarID = '{0}' AND Password = '{1}'", carID, password));
            cmd.Connection = sqlConnection;
            SqlDataReader sqlDR = cmd.ExecuteReader();

            DateTime expiryDate = new DateTime();
            if (!sqlDR.Read())
                return expiryDate;


            expiryDate = Convert.ToDateTime(sqlDR["ExpiryDate"].ToString());
            return expiryDate;

        }

        public bool insertTemproryUsers(TemproryUsers user)
        {
            SqlCommand cmd = new SqlCommand(string.Format("INSERT INTO TemproryUsers (CarID, ExpiryDate, Password) VALUES ('{0}' , '{1}' , '{2}' ) ", user.carID, user.expiryDate, user.password));
            cmd.Connection = sqlConnection;

            if (cmd.ExecuteNonQuery() != -1)
                return true;

            return false;

        }

        public List<Advertisement> returnAdvertisments()
        {
            SqlCommand cmd = new SqlCommand(String.Format("SELECT * FROM Advertisement"));
            cmd.Connection = sqlConnection;
            SqlDataReader sqlDR = cmd.ExecuteReader();

            if (!sqlDR.Read())
                return null;

            List<Advertisement> ad = new List<Advertisement>();

            do
            {
                Advertisement ad1 = new Advertisement();
                ad1.CarNumber= sqlDR["CarNumber"].ToString();
                ad1.SellerName = sqlDR["SellerName"].ToString();
                ad1.Tel = sqlDR["Telephone"].ToString();
                ad1.Pic = sqlDR["Picture"].ToString();
                ad1.Description = sqlDR["Describe"].ToString();
                ad1.Location = sqlDR["Location"].ToString();
                ad1.Price = sqlDR["Price"].ToString();
                ad1.DatePublished = Convert.ToDateTime(sqlDR["DatePublished"].ToString());
                ad.Add(ad1);

            } while (sqlDR.Read());

            return ad;
        }


        public bool insertAdvertisment(Advertisement ad)
        {
            SqlCommand cmd = new SqlCommand(string.Format("INSERT INTO Advertisement (CarNumber, SellerName, Telephone, Picture, Describe) VALUES ('{0}' , '{1}' , '{2}', '{3}', '{4}', '{5}' ) ", ad.CarNumber, ad.SellerName, ad.Tel, ad.Pic, ad.Description, ad.Location));
            cmd.Connection = sqlConnection;

            if (cmd.ExecuteNonQuery() != -1)
                return true;

            return false;
        }

        public DateTime getMaxDateCarTreatment(String carNumber)
        {
            List<HistoryItem> historyItems = getHistoryByCarNumber(int.Parse(carNumber));
            if(historyItems == null)
            {
                return new DateTime();
            }
            SqlCommand cmd = new SqlCommand(String.Format("SELECT MAX (CareDate) AS 'Max' FROM Treatment where CarID='{0}'", carNumber));
            cmd.Connection = sqlConnection;
            SqlDataReader sqlDR = cmd.ExecuteReader();

            if (!sqlDR.Read())
                return new DateTime();
            DateTime max = Convert.ToDateTime(sqlDR["Max"].ToString());
            return max;
        }

        public bool updateDateCarTreatment(String carNumber, DateTime maxDate)
        {

            SqlCommand cmd = new SqlCommand(String.Format(" UPDATE Car SET CarTreatment = '{0}' WHERE CarID='{1}'", maxDate, carNumber));
            cmd.Connection = sqlConnection;
            if (cmd.ExecuteNonQuery() != -1)
                return true;

            return false;

        }

        public String getMaxKM(DateTime date, String carNumber)
        {
            SqlCommand cmd = new SqlCommand(String.Format(" SELECT MAX (KM) AS 'MAX' FROM Treatment where CareDate='{0}' AND CarID={1}", date, carNumber));
            cmd.Connection = sqlConnection;
            SqlDataReader sqlDR = cmd.ExecuteReader();
            if (!sqlDR.Read())
                return null;

            String KM = sqlDR["MAX"].ToString();
            return KM;
        }

        public bool updateKMCar(String carNumber, String KM)
        {
            SqlCommand cmd = new SqlCommand(String.Format(" UPDATE Car SET KM = '{0}' WHERE CarID='{1}'", KM, carNumber));
            cmd.Connection = sqlConnection;
            if (cmd.ExecuteNonQuery() != -1)
                return true;

            return false;


        }

        public List<CarBoard> search(string productName, string model, int? startYear, int? endYear, string gear, string location, string minPrice, string maxPrice)
        {
            bool flag1 = false;
            String query1 = "SELECT * from Advertisement";

            if (!String.IsNullOrEmpty(location))
            {

                query1 = String.Format(query1 + " WHERE Location = '{0}'", location);
                flag1 = true;
            }

            if (!String.IsNullOrEmpty(minPrice))
            {
                if (!flag1)
                {
                    query1 = String.Format(query1 + " WHERE Price >= '{0}'", minPrice);
                }
                else
                {
                    query1 = String.Format(query1 + " AND Price >= '{0}'", minPrice);
                }

                flag1 = true;
            }

            if (!String.IsNullOrEmpty(maxPrice))
            {
                if (!flag1)
                {
                    query1 = String.Format(query1 + " WHERE Price <= '{0}'", maxPrice);
                }
                else
                {
                    query1 = String.Format(query1 + " AND Price <= '{0}'", maxPrice);
                }

                flag1 = true;
            }

            SqlCommand cmd = new SqlCommand(query1); //String.Format("select * from Car where ProductName='{0}' AND StartYear>='{1}' AND StartYear<='{4}' AND Gaer='{2}' AND CarModel='{3}'", productName, startYear, gear, model,endYear));
            cmd.Connection = sqlConnection;
            SqlDataReader sqlDR = cmd.ExecuteReader();

            if (!sqlDR.Read())
                return null;

            List<Advertisement> ads = new List<Advertisement>();
            do
            {
                Advertisement ad1 = new Advertisement();
                ad1.CarNumber = sqlDR["CarNumber"].ToString();
                ad1.SellerName = sqlDR["SellerName"].ToString();
                ad1.Tel = sqlDR["Telephone"].ToString();
                ad1.Pic = sqlDR["Picture"].ToString();
                ad1.Description = sqlDR["Describe"].ToString();
                ad1.Location = sqlDR["Location"].ToString();
                ad1.Price = sqlDR["Price"].ToString();
                ad1.DatePublished = Convert.ToDateTime(sqlDR["DatePublished"].ToString());
                ads.Add(ad1);

            } while (sqlDR.Read());

            if (ads == null)
                return null;

            List<String> carsNum = new List<string>();
            foreach (var ad in ads)
            {
                string str = ad.CarNumber;
                carsNum.Add(str);
            }

            
            String query = "SELECT * from Car WHERE CarID IN (";
            foreach (var carNum in carsNum)
            {
                query += carNum + ",";
            }

            query = query.Substring(0, query.Length - 1) + ")";
            if (!String.IsNullOrEmpty(productName))
            {

                query = String.Format(query + " AND ProductName = '{0}'", productName);

            }

            if (!String.IsNullOrEmpty(model))
            {

                query = String.Format(query + " AND CarModel = '{0}'", model);

            }

            if (startYear != null)
            {

                query = String.Format(query + " AND StartYear >= '{0}'", startYear);

            }

            if (endYear != null)
            {

                query = String.Format(query + " AND StartYear <= '{0}'", endYear);

            }

            if (!String.IsNullOrEmpty(gear))
            {

                query = String.Format(query + " AND Gaer = '{0}'", gear);

            }


            cmd = new SqlCommand(query);
            cmd.Connection = sqlConnection;
            sqlDR = cmd.ExecuteReader();

            if (!sqlDR.Read())
                return null;

            List<Car> cars = new List<Car>();
            do
            {
                Car car = new Car();
                car.CarNumber = sqlDR["CarID"].ToString();
                car.RoadDate = Convert.ToDateTime(sqlDR["RoadDate"].ToString());
                car.Yad = sqlDR["Yad"].ToString();
                car.Year = sqlDR["StartYear"].ToString();
                car.CarVIN = sqlDR["ShildaNumber"].ToString();
                car.EngineCapacity = sqlDR["EngineCapacity"].ToString();
                car.HorsePower = sqlDR["HorsePower"].ToString();
                car.AirBags = sqlDR["AirBags"].ToString();
                car.ABS = sqlDR["CarABS"].ToString();
                car.PowerWindow = sqlDR["PowerWindow"].ToString();
                car.Roof = sqlDR["Roof"].ToString();
                car.MagnesiumWheels = sqlDR["MagnesiumWheels"].ToString();
                car.CarOwnerID = Int32.Parse(sqlDR["OwnerID"].ToString());
                car.ProductName = sqlDR["ProductName"].ToString();
                car.FuelType = sqlDR["FuelType"].ToString();
                car.CarColor = sqlDR["CarColor"].ToString();
                car.Gaer = sqlDR["Gaer"].ToString();
                car.CommericalAlias = sqlDR["CarModel"].ToString();
                car.KM = sqlDR["KM"].ToString();
                car.ownerShip = sqlDR["Ownerships"].ToString();
                car.AC = sqlDR["AC"].ToString();
                cars.Add(car);

            } while (sqlDR.Read());


            List<CarBoard> carB = new List<CarBoard>();
            foreach (var car in cars)
            {
                CarBoard cb = new CarBoard();
                cb.ABS = car.ABS;
                cb.AC = car.AC;
                cb.AirBags = car.AirBags;
                cb.CarColor = car.CarColor;
                cb.CarNumber = car.CarNumber;
                cb.CommericalAlias = car.CommericalAlias;
                cb.EngineCapacity = car.EngineCapacity;
                cb.FuelType = car.FuelType;
                cb.Gaer = car.Gaer;
                cb.KM = car.KM;
                cb.Ownership = car.ownerShip;
                cb.PowerWindow = car.PowerWindow;
                cb.ProductName = car.ProductName;
                cb.Roof = car.Roof;
                cb.Yad = car.Yad;
                cb.Year = car.Year;

                foreach (var ad in ads)
                {
                    if (ad.CarNumber.Equals(car.CarNumber))
                    {
                        cb.DatePublished = ad.DatePublished;
                        cb.Description = ad.Description;
                        cb.Location = ad.Location;
                        cb.Pic = ad.Pic;
                        cb.Price = ad.Price;
                        cb.SellerName = ad.SellerName;
                        cb.Tel = ad.Tel;

                        ads.Remove(ad);
                        break;
                    }
                }
                carB.Add(cb);


            }

            return carB;
        }

        public List<String> getProductNameCars()
        {
            SqlCommand cmd = new SqlCommand(String.Format("SELECT ProductName FROM Car GROUP BY ProductName"));
            cmd.Connection = sqlConnection;
            SqlDataReader sqlDR = cmd.ExecuteReader();
            if (!sqlDR.Read())
                return null;

            List<String> productName = new List<string>();
            do {
                string str= sqlDR["ProductName"].ToString();
                productName.Add(str);
            } while (sqlDR.Read());

            return productName;

        }

        public List<String> getModelCar(string ProductName)
        {
            SqlCommand cmd = new SqlCommand(String.Format("SELECT CarModel FROM Car Where ProductName = '{0}' Group BY CarModel", ProductName));
            cmd.Connection = sqlConnection;
            SqlDataReader sqlDR = cmd.ExecuteReader();
            if (!sqlDR.Read())
                return null;

            List<String> model = new List<string>();
            do
            {
                string str = sqlDR["CarModel"].ToString();
                model.Add(str);
            } while (sqlDR.Read());

            return model;

        }

        public List<int> getPartsIDs(int treatmentID)
        {
            SqlCommand cmd = new SqlCommand(String.Format("SELECT PartID FROM TreatmentToParts WHERE TreatmentID = '{0}'", treatmentID));
            cmd.Connection = sqlConnection;
            SqlDataReader sqlDR = cmd.ExecuteReader();
            if (!sqlDR.Read())
                return null;

            List<int> PartsIDs = new List<int>();
            do
            {
                int id = Int32.Parse(sqlDR["PartID"].ToString());
                PartsIDs.Add(id);
            } while (sqlDR.Read());

            return PartsIDs;
        }

        public List<int> getTreatmentIDs(int carID)
        {
            SqlCommand cmd = new SqlCommand(String.Format("SELECT TreatmentID FROM Treatment WHERE CarID = '{0}'", carID));
            cmd.Connection = sqlConnection;
            SqlDataReader sqlDR = cmd.ExecuteReader();
            if (!sqlDR.Read())
                return null;

            List<int> PartsIDs = new List<int>();
            do
            {
                int id = Int32.Parse(sqlDR["TreatmentID"].ToString());
                PartsIDs.Add(id);
            } while (sqlDR.Read());

            return PartsIDs;
        }

        public bool setGrade(string carNum, int grade)
        {
            SqlCommand cmd = new SqlCommand(string.Format("UPDATE Car SET Grade = '{0}' WHERE CarID = '{1}'", grade, carNum));
            cmd.Connection = sqlConnection;

            try
            {
                if (cmd.ExecuteNonQuery() != -1)
                    return true;
            }
            catch (Exception e)
            {
                return false;
            }

            return false;
        }

        public List<int> getTreatmentIDByCarNum(string CarNumber)
        {
            SqlCommand cmd = new SqlCommand(String.Format("select TreatmentID FROM Treatment where CarID= '{0}'", CarNumber));
            cmd.Connection = sqlConnection;
            SqlDataReader sqlDR = cmd.ExecuteReader();
            if (!sqlDR.Read())
                return null;
            List<int> treatmentID = new List<int>();
            do {
                int id = Int32.Parse(sqlDR["TreatmentID"].ToString());
                treatmentID.Add(id);

            } while (sqlDR.Read());

            return treatmentID;
        }

        public List<int> getPartValue(List<int> treatmentID, string carNumber)
        {
            SqlCommand cmd = new SqlCommand(String.Format("select TreatmentID FROM Treatment where CarID= '{0}'", CarNumber));
            cmd.Connection = sqlConnection;
            SqlDataReader sqlDR = cmd.ExecuteReader();
            if (!sqlDR.Read())
                return null;
            return null;
        }

        public PersonalArea getPrivateDetails(string Email)

        {
            SqlCommand cmd = new SqlCommand(String.Format("SELECT * FROM PersonalArea WHERE Email = '{0}'", Email));
            cmd.Connection = sqlConnection;
            SqlDataReader sqlDR = cmd.ExecuteReader();
            if (!sqlDR.Read())
                return null;

            PersonalArea personal = new PersonalArea();

            personal.Email = sqlDR["Email"].ToString();
            personal.FirstName = sqlDR["FirstName"].ToString();
            personal.ID = Int32.Parse(sqlDR["ID"].ToString());
            personal.LastName = sqlDR["LastName"].ToString();
            personal.OwnedCars = sqlDR["OwnedCars"].ToString();
            personal.Residence = sqlDR["Residence"].ToString();
            personal.Telephone= sqlDR["Telephone"].ToString();
            
            return personal;

        }

        public Dictionary<string, string> getCarsByOwner(string ownerId)
        {
            SqlCommand cmd = new SqlCommand(String.Format("SELECT CarID, ProductName FROM Car WHERE OwnerID = '{0}'", ownerId));
            cmd.Connection = sqlConnection;
            SqlDataReader sqlDR = cmd.ExecuteReader();
            if (!sqlDR.Read())
                return null;

            Dictionary<string, string> tempDictonary = new Dictionary<string, string>();

            do
            {
                string carNumber = sqlDR["CarID"].ToString();
                string carProduct = sqlDR["ProductName"].ToString();
                tempDictonary.Add(carNumber, carProduct);
            } while (sqlDR.Read());

            return tempDictonary;
        }



        ~DBHelper()
        {
            if (sqlConnection.State == ConnectionState.Open)
                try {
                    sqlConnection.Close();
                }
                catch(Exception e)
                {
                    
                }
        }
    }
}