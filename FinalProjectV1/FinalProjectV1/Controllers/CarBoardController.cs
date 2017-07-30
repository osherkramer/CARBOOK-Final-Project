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
        private CarAD setAndGetCarBoardAd(Advertisement ad, Car car)
        {

            if (ad == null && car == null)
                return null;

            if (ad.CarNumber.Equals(car.CarNumber))
            {
                CarAD cb = new CarAD();
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
            //DBHelper db = new DBHelper();

            List<CarAD> carsBoard = DBHelper.search(productName, model, startYear, endYear, gear, location, minPrice, maxPrice);
            //List<CarAD> carsBoard = DBHelper.search(productName, model, startYear, endYear, gear, "", minPrice, maxPrice);
            //carsBoard = this.geneticSort(carsBoard, location);

            CarBoard cb = new CarBoard();
            cb.cars = carsBoard;
            cb.carsProduct = DBHelper.getCarList();
            cb.carsProduct.Sort();
            cb.areas = DBHelper.getAreas();

            return View(cb);
        }

        private List<CarAD> geneticSort(List<CarAD> cars, string buyerLocation)
        {
            int randCar = 0;
            int randIndividual = 0;

            // Build all cars features
            List<Dictionary<string, int>> allCarsFeatures = new List<Dictionary<string, int>>();
            foreach (CarAD car in cars)
            {
                Dictionary<string, int> feature = getFeatureOfCar(car, buyerLocation);
                allCarsFeatures.Add(feature);
            }

            // Build cars population
            Random r = new Random();
            List<List<Dictionary<string, int>>> carsPopulation = new List<List<Dictionary<string, int>>>();
            for (int i = 0; i < 100; i++)
            {
                List<Dictionary<string, int>> carsIndividual = new List<Dictionary<string, int>>();
                for (int j = 0; j < 5; j++)
                {
                    randCar = r.Next(0, allCarsFeatures.Count);
                    carsIndividual.Add(allCarsFeatures.ElementAt(randCar));
                }
                carsPopulation.Add(carsIndividual);
            }

            // Will store the best result
            float minFitness = -1;
            float prevMinFitness = -1;
            List<Dictionary<string, int>> bestIndividual = null;

            // Do 20 generations at most
            for(int iteration = 0; iteration < 100; iteration++)
            {
                // Save the previous min fitness, will be used in the stop criterea
                prevMinFitness = minFitness;

                // Find minimum individual
                foreach (List<Dictionary<string, int>> carsIndividual in carsPopulation)
                {
                    float totalFitness = 0;
                    foreach (Dictionary<string, int> carFeature in carsIndividual)
                    {
                        totalFitness += getFitnessValue(carFeature);
                    }

                    if (totalFitness < minFitness || minFitness == -1)
                    {
                        minFitness = totalFitness;
                        bestIndividual = carsIndividual;
                    }
                }

                // If there is no change in fitness we get convergence
                // if (prevMinFitness == minFitness && prevMinFitness != -1)
                //    break;

                // Split population into two parent groups
                List<List<Dictionary<string, int>>> carsSubPopulationA = new List<List<Dictionary<string, int>>>();
                List<List<Dictionary<string, int>>> carsSubPopulationB = new List<List<Dictionary<string, int>>>();
                for (int i = 0; i < 50; i++)
                {
                    randCar = r.Next(0, carsPopulation.Count);
                    carsSubPopulationA.Add(carsPopulation.ElementAt(randCar));
                    carsPopulation.RemoveAt(randCar);

                    randCar = r.Next(0, carsPopulation.Count);
                    carsSubPopulationB.Add(carsPopulation.ElementAt(randCar));
                    carsPopulation.RemoveAt(randCar);
                }

                // Perform crosuving
                for (int i = 0; i < 50; i++)
                {
                    // Take two parents
                    List<Dictionary<string, int>> parentA = carsSubPopulationA.ElementAt(i);
                    List<Dictionary<string, int>> parentB = carsSubPopulationB.ElementAt(i);

                    // Craete both sons
                    List<Dictionary<string, int>> sonA = new List<Dictionary<string, int>>();
                    List<Dictionary<string, int>> sonB = new List<Dictionary<string, int>>();

                    sonA.AddRange(parentA.GetRange(0, 3));
                    sonA.AddRange(parentB.GetRange(3, 2));

                    sonB.AddRange(parentB.GetRange(0, 3));
                    sonB.AddRange(parentA.GetRange(3, 2));

                    // Add the to the population
                    carsPopulation.Add(sonA);
                    carsPopulation.Add(sonB);
                }

                // Add mutations to 2 percent
                for(int i = 0; i < 2; i++)
                {
                    randCar = r.Next(0, allCarsFeatures.Count);
                    Dictionary<string, int> mutantFeature = allCarsFeatures.ElementAt(randCar);
                    randIndividual = r.Next(0, carsPopulation.Count);
                    // Remove the middle feature, can come from either of the parents
                    carsPopulation.ElementAt(randIndividual).RemoveAt(3);
                    carsPopulation.ElementAt(randIndividual).Add(mutantFeature);
                }
            }

            // Retreive the cars of the best individual
            List<CarAD> geneticCars = new List<CarAD>();
            foreach(Dictionary<string, int> feature in bestIndividual)
                geneticCars.Add(getCarOfFeature(feature, cars));

            return geneticCars;
        }

        private Dictionary<string, int> getFeatureOfCar(CarAD car, string buyerLocation)
        {
            int CarNumber = int.Parse(car.CarNumber);
            int yearOfManufactoring = int.Parse(car.Year);
            Dictionary<string, int> feature = new Dictionary<string, int>();
            feature["car_number"] = CarNumber;

            // Calculate distance feature
            Dictionary<string, Dictionary<string, int>> distanceCity2City = new Dictionary<string, Dictionary<string, int>>();
            distanceCity2City["גבעתיים"] = new Dictionary<string, int>()
            {
                { "גבעתיים", 0 },
                { "חולון - בת ים", 10 },
                { "ראשון לציון", 20 },
                { "אשדוד", 40 },
                { "תל אביב", 5 }
            };
            distanceCity2City["חולון - בת ים"] = new Dictionary<string, int>()
            {
                { "גבעתיים", 10 },
                { "חולון - בת ים", 0 },
                { "ראשון לציון", 10 },
                { "אשדוד", 30 },
                { "תל אביב", 5 }
            };
            distanceCity2City["ראשון לציון"] = new Dictionary<string, int>()
            {
                { "גבעתיים", 20 },
                { "חולון - בת ים", 10 },
                { "ראשון לציון", 0 },
                { "אשדוד", 30 },
                { "תל אביב", 20 }
            };
            distanceCity2City["אשדוד"] = new Dictionary<string, int>()
            {
                { "גבעתיים", 40 },
                { "חולון - בת ים", 20 },
                { "ראשון לציון", 30 },
                { "אשדוד", 0 },
                { "תל אביב", 40 }
            };
            distanceCity2City["תל אביב"] = new Dictionary<string, int>()
            {
                { "גבעתיים", 5 },
                { "חולון - בת ים", 10 },
                { "ראשון לציון", 20 },
                { "אשדוד", 40 },
                { "תל אביב", 0 }
            };
            if(buyerLocation != null && buyerLocation != "")
            {
                int distance = distanceCity2City[buyerLocation][car.Location];
                feature["distance"] = distance;
            }
            else
            {
                feature["distance"] = 0;
            }
            

            // Save the price
            feature["price"] = int.Parse(car.Price);

            // Create a list where all the treatment years are set to the date of manufactoring
            List<Parts> allParts = DBHelper.getAllParts();
            foreach(Parts part in allParts)
                feature[part.partName] = yearOfManufactoring;

            // Update the treatment years of all the relevant parts
            List<HistoryItem> historyOfTreatments = DBHelper.getHistoryByCarNumber(CarNumber);
            if (historyOfTreatments == null)
                return feature;

            foreach(HistoryItem treatment in historyOfTreatments)
            {
                int visitYear = treatment.Date.Year;
                foreach(string treatedPartName in treatment.Treatment)
                    // Only if date is newer
                    if(visitYear > feature[treatedPartName])
                        feature[treatedPartName] = visitYear;
            }

            return feature;
        }

        private CarAD getCarOfFeature(Dictionary<string, int> feature, List<CarAD> cars)
        {
            foreach (CarAD car in cars)
                if (int.Parse(car.CarNumber) == feature["car_number"])
                    return car;
            return null;
        }

        private float getFitnessValue(Dictionary<string, int> carFeature)
        {
            float oldestCarAge = 20;
            float maxCarPrice = 100000;
            float maxCityDistance = 40;

            int currentYear = DateTime.Now.Year;
            List<Parts> allParts = DBHelper.getAllParts();

            float fitness = 0;

            // Add the parts fitness cost
            foreach (Parts part in allParts)
            {
                int partTreatmentYear = carFeature[part.partName];
                int partShipua = -1 * part.partValue;

                fitness += (float)(partShipua * (currentYear - partTreatmentYear)) / oldestCarAge;
            }

            int prince_const = 1;
            int distance_const = 1;

            // Add the distance and the price cost
            fitness += prince_const * carFeature["price"] / maxCarPrice;
            fitness += distance_const * carFeature["distance"] / maxCityDistance;

            return fitness;
        }

      
        public JsonResult getCars()
        {
            //DBHelper db = new DBHelper();
            return Json(DBHelper.getCarList());
        }

        public JsonResult getCarModel(string car)
        {
            //DBHelper db = new DBHelper();
            return Json(DBHelper.getCarModelList(car));
        }

        public double calculateGradeOfCar(string carNumber)
        {
            //DBHelper db = new DBHelper();
            List<int> treatmentID = DBHelper.getTreatmentIDs(Int32.Parse(carNumber));

            if (treatmentID==null) 
                return -1;

            List<int> partID = new List<int>();
            List<int> tempID = new List<int>();
            foreach(var id in treatmentID)
            {
                tempID = DBHelper.getPartsIDs(id);

                foreach(var temp in tempID)
                {
                    partID.Add(temp);
                }
            }

            int sum = 0;
            foreach(var id in partID)
            {
                sum += DBHelper.returnPartValueBYID(id);
            }

            return sum;
        }
    }
}