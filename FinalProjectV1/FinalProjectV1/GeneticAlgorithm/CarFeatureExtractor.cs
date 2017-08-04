using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalProjectV1.Models;
using FinalProjectV1.Helpers;

namespace FinalProjectV1
{
    public class CarFeatureExtractor
    {
        private static Dictionary<string, Dictionary<string, int>> car2feature;
        public static void buildAllCarFeatures()
        {
            // Do only once
            if (car2feature != null)
                return;

            List<CarAD> cars = DBHelper.search(null, null, null, null, null, null, null, null);
            car2feature = new Dictionary<string, Dictionary<string, int>>();
            foreach (CarAD car in cars)
                car2feature[car.CarNumber] = createCarFeature(car);
        }

        public static Dictionary<string, int> createCarFeature(CarAD car)
        {
            int CarNumber = int.Parse(car.CarNumber);
            int yearOfManufactoring = int.Parse(car.Year);
            Dictionary<string, int> feature = new Dictionary<string, int>();

            // Save the price
            feature["price"] = int.Parse(car.Price);

            // Create a list where all the treatment years are set to the date of manufactoring
            List<Parts> allParts = DBHelper.getAllParts();
            foreach (Parts part in allParts)
                feature[part.partName] = yearOfManufactoring;

            // Update the treatment years of all the relevant parts
            List<HistoryItem> historyOfTreatments = DBHelper.getHistoryByCarNumber(CarNumber);
            if (historyOfTreatments == null)
                return feature;

            foreach (HistoryItem treatment in historyOfTreatments)
            {
                int visitYear = treatment.Date.Year;
                foreach (string treatedPartName in treatment.Treatment)
                    // Only if date is newer
                    if (visitYear > feature[treatedPartName])
                        feature[treatedPartName] = visitYear;
            }

            return feature;
        }

        public static Dictionary<string, int> getFeatureOfCar(CarAD car, string buyerLocation)
        {
            Dictionary<string, int> feature = new Dictionary<string, int>(CarFeatureExtractor.car2feature[car.CarNumber]);

            if (buyerLocation != null && buyerLocation != "")
                feature["distance"] = CarFeatureExtractor.getDistanceBetweenCities(buyerLocation, car.Location);
            else
                feature["distance"] = 0;

            return feature;
        }

        private static int getDistanceBetweenCities(string start_city, string end_city)
        {
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

            if (!distanceCity2City.ContainsKey(start_city) || !distanceCity2City[start_city].ContainsKey(end_city))
                return 100;

            return distanceCity2City[start_city][end_city];
        }
    }
}
