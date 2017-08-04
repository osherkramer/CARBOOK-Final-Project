using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalProjectV1.Models;
using FinalProjectV1.Helpers;


namespace FinalProjectV1
{
    public class CarsPopulation
    {
        public static List<Individual<List<CarAD>>> create(string buyer_location, List<CarAD> all_cars, int population_size, int cars_in_individual)
        {
            Random r = new Random();
            int rand;

            if (all_cars == null || all_cars.Count < cars_in_individual)
                return null;

            List<Individual<List<CarAD>>> population = new List<Individual<List<CarAD>>>();
            for (int i = 0; i < population_size; i++)
            {
                List<CarAD> cars_of_individual = new List<CarAD>();

                while(cars_of_individual.Count < cars_in_individual)
                {
                    rand = r.Next(0, all_cars.Count);
                    CarAD car2add = all_cars.ElementAt(rand);
                    if (CarsPopulation.carExists(cars_of_individual, car2add))
                        continue;

                    cars_of_individual.Add(all_cars.ElementAt(rand));
                }
                population.Add(new CarsIndividual(cars_of_individual, all_cars, buyer_location));
            }

            return population;
        }

        private static bool carExists(List<CarAD> cars, CarAD car)
        {
            for (int i = 0; i < cars.Count; i++)
                if (cars.ElementAt(i).CarNumber == car.CarNumber)
                    return true;
            return false;
        }
    }
}
