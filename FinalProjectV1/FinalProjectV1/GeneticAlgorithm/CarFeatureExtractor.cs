using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Device.Location;
using System.Threading.Tasks;
using FinalProjectV1.Models;
using FinalProjectV1.Helpers;

namespace FinalProjectV1
{
    public class CarFeatureExtractor
    {
        private static List<HistoryItem> care_history;
        private static Dictionary<string, double> location_long;
        private static Dictionary<string, double> location_lat;
        public static Dictionary<string, float> FitnessGrades;

        public static void init()
        {
            location_long = new Dictionary<string, double>();
            location_lat = new Dictionary<string, double>();

            location_long["ראשון לציון"] = 34.800429;
            location_lat["ראשון לציון"] = 31.968774;
            location_long["חיפה והסביבה"] = 35.008736;
            location_lat["חיפה והסביבה"] = 32.80375;
            location_long["קריות והסביבה"] = 35.071278;
            location_lat["קריות והסביבה"] = 32.841592;
            location_long["עכו - נהריה והסביבה"] = 35.084374;
            location_lat["עכו - נהריה והסביבה"] = 32.928098;
            location_long["הכנרת והסביבה"] = 35.583333;
            location_lat["הכנרת והסביבה"] = 32.833333;
            location_long["כרמיאל והסביבה"] = 35.291778;
            location_lat["כרמיאל והסביבה"] = 32.911625;
            location_long["נצרת - שפרעם והסביבה"] = 35.294986;
            location_lat["נצרת - שפרעם והסביבה"] = 32.701888;
            location_long["ראש פינה החולה"] = 35.542457;
            location_lat["ראש פינה החולה"] = 32.969085;
            location_long["גליל תחתון"] = 35.294986;
            location_lat["גליל תחתון"] = 32.701888;
            location_long["הגולן"] = 35.749444;
            location_lat["הגולן"] = 32.981667;
            location_long["חדרה והסביבה"] = 34.918515;
            location_lat["חדרה והסביבה"] = 32.435675;
            location_long["קיסריה והסביבה"] = 34.891767;
            location_lat["קיסריה והסביבה"] = 32.502506;
            location_long["יקנעם טבעון והסביבה"] = 35.115013;
            location_lat["יקנעם טבעון והסביבה"] = 32.654549;
            location_long["עמק בית שאן"] = 35.497801;
            location_lat["עמק בית שאן"] = 32.497338;
            location_long["עפולה והעמקים"] = 35.29;
            location_lat["עפולה והעמקים"] = 32.61;
            location_long["רמת מנשה"] = 35.056695;
            location_lat["רמת מנשה"] = 32.597325;
            location_long["נתניה והסביבה"] = 34.855;
            location_lat["נתניה והסביבה"] = 32.332;
            location_long["רעננה - כפר סבא"] = 34.870202;
            location_lat["רעננה - כפר סבא"] = 32.185769;
            location_long["הוד השרון והסביבה"] = 34.893963;
            location_lat["הוד השרון והסביבה"] = 32.152373;
            location_long["דרום השרון"] = 0;
            location_lat["דרום השרון"] = 0;
            location_long["צפון השרון"] = 0;
            location_lat["צפון השרון"] = 0;
            location_long["תל אביב"] = 34.789471;
            location_lat["תל אביב"] = 32.076323;
            location_long["אשדוד"] = 34.649661;
            location_lat["אשדוד"] = 31.795507;
            location_long["חולון - בת ים"] = 34.779155;
            location_lat["חולון - בת ים"] = 32.016861;
            location_long["פתח תקווה והסביבה"] = 34.886246;
            location_lat["פתח תקווה והסביבה"] = 32.085081;
            location_long["ראש העין והסביבה"] = 34.956657;
            location_lat["ראש העין והסביבה"] = 32.095609;
            location_long["בקעת אונו"] = 0;
            location_lat["בקעת אונו"] = 0;
            location_long["רמלה - לוד"] = 34.866379;
            location_lat["רמלה - לוד"] = 31.928205;
            location_long["בני ברק - גבעת שמואל"] = 34.834866;
            location_lat["בני ברק - גבעת שמואל"] = 32.086744;
            location_long["עמק  איילון"] = 34.975;
            location_lat["עמק  איילון"] = 31.848056;
            location_long["שוהם והסביבה"] = 34.944751;
            location_lat["שוהם והסביבה"] = 31.99783;
            location_long["מודיעין והסביבה"] = 35.012585;
            location_lat["מודיעין והסביבה"] = 31.895414;
            location_long["ירושלים"] = 35.21933;
            location_lat["ירושלים"] = 31.782649;
            location_long["בית שמש והסביבה"] = 34.988959;
            location_lat["בית שמש והסביבה"] = 31.736834;
            location_long["הרי יהודה - מבשרת והסביבה"] = 35.166667;
            location_lat["הרי יהודה - מבשרת והסביבה"] = 31.666667;
            location_long["מעלה אדומים והסביבה"] = 35.30569;
            location_lat["מעלה אדומים והסביבה"] = 31.779543;
            location_long["ישובי שומרון"] = 35.195;
            location_lat["ישובי שומרון"] = 32.276111;
            location_long["גוש עציון"] = 0;
            location_lat["גוש עציון"] = 0;
            location_long["בקעת הירדן וצפון ים המלח"] = 35.57;
            location_lat["בקעת הירדן וצפון ים המלח"] = 32.317222;
            location_long["אריאל וישובי יהודה"] = 35.189928;
            location_lat["אריאל וישובי יהודה"] = 32.105139;
            location_long["נס ציונה - רחובות"] = 34.798182;
            location_lat["נס ציונה - רחובות"] = 31.926653;
            location_long["גדרה - יבנה והסביבה"] = 34.780092;
            location_lat["גדרה - יבנה והסביבה"] = 31.813145;
            location_long["קרית גת והסביבה"] = 34.764897;
            location_lat["קרית גת והסביבה"] = 31.609097;
            location_long["באר שבע והסביבה"] = 34.786712;
            location_lat["באר שבע והסביבה"] = 31.253107;
            location_long["אילת וערבה"] = 34.94422;
            location_lat["אילת וערבה"] = 29.553917;
            location_long["ישובי הנגב"] = 34.793178;
            location_lat["ישובי הנגב"] = 30.873414;
            location_long["הנגב המערבי"] = 34.5945;
            location_lat["הנגב המערבי"] = 31.52777;
            location_long["דרום ים המלח"] = 35.5;
            location_lat["דרום ים המלח"] = 31.5;
            location_long["אשקלון"] = 34.574073;
            location_lat["אשקלון"] = 31.668719;
            location_long["אשדוד"] = 34.649661;
            location_lat["אשדוד"] = 31.795507;
            location_long["גבעתיים"] = 34.809934;
            location_lat["גבעתיים"] = 32.071429;
            location_long["רמת גן"] = 34.824135;
            location_lat["רמת גן"] = 32.069038;

            //care_history = DBHelper.getAllHistory();
            FitnessGrades = DBHelper.getGradeFitness();

        }

        //calculate distance between 2 cities
        private static int getDistanceBetweenCities(string start_city, string end_city)
        {
            var sCoord = new GeoCoordinate(location_lat[start_city], location_long[start_city]);
            var eCoord = new GeoCoordinate(location_lat[end_city], location_long[end_city]);

            return (int)sCoord.GetDistanceTo(eCoord)/1000;
        }


        //create dictionery of a car (price,distance,halafim tipul)
        public static Dictionary<string, int> getFeatureOfCar(CarAD car, string buyerLocation)
        {
            int CarNumber = int.Parse(car.CarNumber);
            int yearOfManufactoring = int.Parse(car.Year);
            Dictionary<string, int> feature = new Dictionary<string, int>();

            // Save the price
            feature["price"] = int.Parse(car.Price);

            // Save distance
            if (buyerLocation != null && buyerLocation != "")
                feature["distance"] = CarFeatureExtractor.getDistanceBetweenCities(buyerLocation, car.Location);
            else
                feature["distance"] = 0;

            return feature;
        }
    }
}
