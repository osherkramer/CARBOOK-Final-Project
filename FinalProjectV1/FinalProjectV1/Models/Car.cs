using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


namespace ConsoleApplication1
{
    public class Car
    {
        private String number;
        private String producer; //
        private String model; // 
        private int year;
        private DateTime registerDate;
        private int hand;
        private String vehicleIdentifyNumber;
        private FuelTypeEnum fuel; //
        private int engineCapacity; //
        private String tireSizeFront; //
        private String tireSizeBack; //
        private ColorsEnum color;
        private String engineNumber;
        private String engineModel; //
        private String manufacturingEngieCountry; // 
        private int powerHorse; //
        private int airBags; //
        private GearBoxTypeEnum gearBox; //
        private bool ABS; //
        private bool ESP; //
        private String modelVartiants; //
        private int numberDoors; //
        private int electricalWindow; //
        private bool roof; //
        private bool magnesiumWheels; //
        private String carStatus;
        private DateTime testDate;
        private String ownerID;

        //Maybe need to move extend class
        private double price;
        private int KM;
        private String description;
        private String extras;
        private List<Image> images;

        public ColorsEnum getColor()
        {
        
             return color;
        }

       

        public void setColor(ColorsEnum color)
        {
               this.color = color;
            
        }

        public int getKM()
        {
            return KM;
        }
        public void setKM(int kM)
        {
            KM = kM;
        }
        public String getDescription()
        {
            return description;
        }
        public void setDescription(String description)
        {
            this.description = description;
        }
        public String getExtras()
        {
            return extras;
        }
        public void setExtras(String extras)
        {
            this.extras = extras;
        }
        public List<Image> getImages()
        {
            return images;
        }
        public void setImages(List<Image> images)
        {
            this.images = images;
        }
        public String getNumber()
        {
            return number;
        }
        public void setNumber(String number)
        {
            this.number = number;
        }
        public String getProducer()
        {
            return producer;
        }
        public void setProducer(String producer)
        {
            this.producer = producer;
        }
        public String getModel()
        {
            return model;
        }
        public void setModel(String model)
        {
            this.model = model;
        }
        public int getYear()
        {
            return year;
        }
        public void setYear(int year)
        {
            if (year < 0)
            {
                return;
            }
            this.year = year;
        }
        public DateTime getRegisterDate()
        {
            return registerDate;
        }
        public void setRegisterDate(DateTime registerDate)
        {
            this.registerDate = registerDate;
        }
        public int getHand()
        {
            return hand;
        }
        public void setHand(int hand)
        {
            if (hand < 0)
            {
                return;
            }
            this.hand = hand;
        }
        public String getVehicleIdentifyNumber()
        {
            return vehicleIdentifyNumber;
        }
        public void setVehicleIdentifyNumber(String vehicleIdentifyNumber)
        {
            this.vehicleIdentifyNumber = vehicleIdentifyNumber;
        }
        public FuelTypeEnum getFuel()
        {
            return fuel;
        }
        public void setFuel(FuelTypeEnum fuel)
        {
            this.fuel = fuel;
        }
        public int getEngineCapacity()
        {
            return engineCapacity;
        }
        public void setEngineCapacity(int engineCapacity)
        {
            if (engineCapacity < 0)
            {
                return;
            }
            this.engineCapacity = engineCapacity;
        }
        public String getTireSizeFront()
        {
            return tireSizeFront;
        }
        public void setTireSizeFront(String tireSizeFront)
        {
            this.tireSizeFront = tireSizeFront;
        }
        public String getTireSizeBack()
        {
            return tireSizeBack;
        }
        public void setTireSizeBack(String tireSizeBack)
        {
            this.tireSizeBack = tireSizeBack;
        }
   
        public String getEngineNumber()
        {
            return engineNumber;
        }
        public void setEngineNumber(String engineNumber)
        {
            this.engineNumber = engineNumber;
        }
        public String getEngineModel()
        {
            return engineModel;
        }
        public void setEngineModel(String engineModel)
        {
            this.engineModel = engineModel;
        }
        public String getManufacturingEngieCountry()
        {
            return manufacturingEngieCountry;
        }
        public void setManufacturingEngieCountry(String manufacturingEngieCountry)
        {
            this.manufacturingEngieCountry = manufacturingEngieCountry;
        }
        public int getPowerHorse()
        {
            return powerHorse;
        }
        public void setPowerHorse(int powerHorse)
        {
            if (powerHorse < 0)
            {
                return;
            }
            this.powerHorse = powerHorse;
        }
        public int getAirBags()
        {
            return airBags;
        }
        public void setAirBags(int airBags)
        {
            this.airBags = airBags;
        }
        public GearBoxTypeEnum getGearBox()
        {
            return gearBox;
        }
        public void setGearBox(GearBoxTypeEnum gearBox)
        {
            this.gearBox = gearBox;
        }
        public bool isABS()
        {
            return ABS;
        }
        public void setABS(bool aBS)
        {
            ABS = aBS;
        }
        public bool isESP()
        {
            return ESP;
        }
        public void setESP(bool eSP)
        {
            ESP = eSP;
        }
        public String getModelVartiants()
        {
            return modelVartiants;
        }
        public void setModelVartiants(String modelVartiants)
        {
            this.modelVartiants = modelVartiants;
        }
        public int getNumberDoors()
        {
            return numberDoors;
        }
        public void setNumberDoors(int numberDoors)
        {
            if (numberDoors < 0)
            {
                return;
            }
            this.numberDoors = numberDoors;
        }
        public int getElectricalWindow()
        {
            return electricalWindow;
        }
        public void setElectricalWindow(int electricalWindow)
        {
            this.electricalWindow = electricalWindow;
        }
        public bool isRoof()
        {
            return roof;
        }
        public void setRoof(bool roof)
        {
            this.roof = roof;
        }
        public bool isMagnesiumWheels()
        {
            return magnesiumWheels;
        }
        public void setMagnesiumWheels(bool magnesiumWheels)
        {
            this.magnesiumWheels = magnesiumWheels;
        }
        public String getCarStatus()
        {
            return carStatus;
        }
        public void setCarStatus(String carStatus)
        {
            this.carStatus = carStatus;
        }
        public DateTime getTestDate()
        {
            return testDate;
        }
        public void setTestDate(DateTime testDate)
        {
            this.testDate = testDate;
        }
        public String getOwnerID()
        {
            return ownerID;
        }
        public void setOwnerID(String ownerID)
        {
            this.ownerID = ownerID;
        }
        public double getPrice()
        {
            return price;
        }
        public void setPrice(double price)
        {
            this.price = price;
        }
    }
}
