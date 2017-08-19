using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalProjectV1.Models;
using FinalProjectV1.Helpers;


namespace FinalProjectV1
{
    public class CarsIndividual : Individual<List<CarAD>>
    {
        private static float price_const = 25;
        private static float distance_const = 65;
        private static float year_const = 25;
        private static float grade_const = 50;

        private List<CarAD> individual_cars;
        private List<CarAD> all_cars;
        private string buyer_location;

        public CarsIndividual(List<CarAD> individual_cars, List<CarAD> all_cars, string buyer_location)
        {
            this.individual_cars = individual_cars;
            this.all_cars = all_cars;
            this.buyer_location = buyer_location;
        }

        public Individual<List<CarAD>> crossover(Individual<List<CarAD>> partner)
        {
            // Take two parents
            List<CarAD> parent_a = this.getGenes();
            List<CarAD> parent_b = partner.getGenes();
            List<CarAD> son = new List<CarAD>();

            int total_len = parent_a.Count;
            int split = total_len / 2;

            son.AddRange(parent_a.GetRange(0, split));
            son.AddRange(parent_b.GetRange(split, total_len - split));

            if(isDuplicates(son))
                son = parent_a;
            
            return new CarsIndividual(son, this.all_cars,this.buyer_location);
        }

        private bool isDuplicates(List<CarAD> cars)
        {
            for (int i = 0; i < cars.Count; i++)
                for (int j = 0; j < cars.Count; j++)
                    if ((i != j) && (cars.ElementAt(i).CarNumber == cars.ElementAt(j).CarNumber))
                        return true;

            return false;
        }

        public void mutate()
        {
            int rand;
            Random r = new Random();

            rand = r.Next(0, this.individual_cars.Count);
            this.individual_cars.RemoveAt(rand);

            rand = r.Next(0, all_cars.Count);
            individual_cars.Add(all_cars.ElementAt(rand));
        }

        public List<CarAD> getGenes()
        {
            return this.individual_cars;
        }

        public double getFitness(int? startYear, int? endYear, string minPrice, string MaxPrice)
        {
            double totalFitness = 0;
            foreach (CarAD car in this.individual_cars)
            {
                totalFitness += getSingleCarFitness(car, startYear, endYear, minPrice, MaxPrice);
            }
            return totalFitness;
        }

        private double getSingleCarFitness(CarAD car, int? startYear, int? endYear, string MinPrice, string MaxPrice)
        {
            
            double fitness = 0;
            Dictionary<string, float> gradeFitness = CarFeatureExtractor.FitnessGrades;
            Dictionary<string, int> carFeature = CarFeatureExtractor.getFeatureOfCar(car, buyer_location);
            double price = Normalization(carFeature["price"], String.IsNullOrEmpty(MaxPrice) ? CarFeatureExtractor.MaxPrice : Double.Parse(MaxPrice), String.IsNullOrEmpty(MinPrice) ? CarFeatureExtractor.MinPrice : Double.Parse(MinPrice));
            double distance = Normalization(carFeature["distance"], CarFeatureExtractor.MaxDistance, CarFeatureExtractor.MinDistance);
            double year = Normalization(Double.Parse(car.Year),endYear == null ? CarFeatureExtractor.MaxYear : (double)endYear, startYear == null ? CarFeatureExtractor.MinYear : (double)startYear);
            double grade = Normalization(gradeFitness[car.CarNumber], CarFeatureExtractor.MaxGrade, CarFeatureExtractor.MinGrade);

            year = year < 1 && year > 0 ? 1 - year : year; //New car is first.

            year = year < 0 && year > -1 ? year * -1 + 1 : year < 0 ? year * - 1 : year;
            price = price < 0 && price > -1 ? price * -1 + 1 : price < 0 ? price * -1 : price;
            
            if(!String.IsNullOrEmpty(MinPrice) || !String.IsNullOrEmpty(MaxPrice))            
                fitness += price_const * price;

            fitness += distance_const * distance;

            if(startYear != null || endYear != null)
                fitness += year_const * year;

            fitness += grade_const * grade;

            return fitness;
        }

        public double Normalization(double value, double max, double min)
        {
            if(max == min)
            {
                max += 0.25;
                if (min > 0)
                    min -= 0.25;
            }
            return (value - min) / (max - min);
        }

        public bool isEqual(List<CarAD> carsIndividual)
        {
            foreach(var car in carsIndividual)
            {
                if (!individual_cars.Contains(car))
                    return false;
            }

            return true;
        }
    }
}
