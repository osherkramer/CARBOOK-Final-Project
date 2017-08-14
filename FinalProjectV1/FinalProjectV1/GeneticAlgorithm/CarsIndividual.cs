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
        private static float prince_const = 1;
        private static float distance_const = 100;

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

        public float getFitness()
        {
            float totalFitness = 0;
            foreach (CarAD car in this.individual_cars)
            {
                totalFitness += getSingleCarFitness(car);
            }
            return totalFitness;
        }

        private float getSingleCarFitness(CarAD car)
        {
            
            float oldestCarAge = 20;
            float maxCarPrice = 100000;
            float maxCityDistance = 40;

            int currentYear = DateTime.Now.Year;

            float fitness = 0;
            Dictionary<string, float> gradeFitness = CarFeatureExtractor.FitnessGrades;
            Dictionary<string, int> carFeature = CarFeatureExtractor.getFeatureOfCar(car, buyer_location);
            
            fitness = gradeFitness[car.CarNumber];

            // Add the distance and the price cost
            fitness += CarsIndividual.prince_const * carFeature["price"] / maxCarPrice;
            fitness += CarsIndividual.distance_const * carFeature["distance"] / maxCityDistance;

            return fitness;
        }

    }
}
