﻿using System;
using System.Data;
using System.Data.SqlClient;


namespace FinalProjectV1.Helpers
{
    public class DBHelper
    {
        private string connectionString = @"Server=db.cs.colman.ac.il;Database=CarBook;User Id=carbook;password=Car@Book";
        private SqlConnection sqlConnection { get; set; }

        public DBHelper()
        {
            sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
        }

        public Car getCarByNumber(string carNumber)
        {
            int carNum;
            if(carNumber.Length != 7 || carNumber[0].Equals('0') || !Int32.TryParse(carNumber, out carNum))
                return null;

            SqlCommand cmd = new SqlCommand(String.Format("SELECT * From Car WHERE CarID = {0}", carNum));
            cmd.Connection = sqlConnection;

            SqlDataReader sqlDR = cmd.ExecuteReader();

            if (!sqlDR.Read())
                return null;

            Car car = new Car();

            car.CarNumber = sqlDR["CarID"].ToString();
            car.RoadDate = (DateTime)sqlDR["RoadDate"]; //Check it!!!!!!!!!!!!!
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

        public bool InsertCar(Car car)
        {
            SqlCommand cmd = new SqlCommand(String.Format("INSERT INTO Car (CarID, RoadDate, Yad, StartYear, ShildaNumber, EngineCapacity, HorsePower, AirBags, CarABS, PowerWindow, Roof, MagnesiumWheels, CarTreatment, OwnerID, ProductName, FuelType, CarColor, Gaer, CarModel) VALUES ('{0}' , '{1}' , '{2}' , '{3}' , '{4}' , '{5}' , '{6}' , '{7}' , '{8}' , '{9}' , '{10}' , '{11}' , '{12}' , '{13}' , '{14}' , '{15}' , '{16}' , '{17}' , '{18}')", car.CarNumber, car.RoadDate, car.Yad, car.Year, car.CarVIN, car.EngineCapacity, car.HorsePower, car.AirBags, car.ABS, car.PowerWindow, car.Roof, car.MagnesiumWheels, "", car.CarOwnerID, car.ProductName, car.FuelType, car.CarColor, car.Gaer, car.CommericalAlias));
            cmd.Connection = sqlConnection;

            if (cmd.ExecuteNonQuery() != -1)
                return true;

            return false;
        } 

        ~DBHelper()
        {
            if(sqlConnection.State == ConnectionState.Open)
                sqlConnection.Close();
        }
    }
}