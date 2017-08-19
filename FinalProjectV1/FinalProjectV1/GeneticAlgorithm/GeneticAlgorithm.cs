﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalProjectV1.Models;
using FinalProjectV1.Helpers;


namespace FinalProjectV1
{
    public class GeneticAlgorithm<T>
    {
        private int number_of_mutations;
        private int number_of_iterations;

        public GeneticAlgorithm(int number_of_iterations, int number_of_mutations)
        {
            this.number_of_iterations = number_of_iterations;
            this.number_of_mutations = number_of_mutations;
        }

        public Individual<T> run(List<Individual<T>> population, int? startYear, int? endYear, string minPrice, string MaxPrice)
        {
            Random r = new Random();
            int rand = 0;

            if (population == null || population.Count == 0)
                return null;

            Individual<T> best_individual = population.ElementAt(0);
            Individual<T> last_individual = population.ElementAt(0);
            int count = 0;
            double min_fitness = best_individual.getFitness(startYear, endYear, minPrice, MaxPrice);

            for (int iteration = 0; iteration < number_of_iterations; iteration++)
            {
                // Find minimum individual
                foreach (Individual<T> individual in population)
                {
                    double fitness = individual.getFitness(startYear, endYear, minPrice, MaxPrice);
                    if ( fitness < min_fitness)
                    {
                        min_fitness = fitness;
                        best_individual = individual;
                    }
                }

                if (best_individual.isEqual(last_individual.getGenes()))
                {
                    count++;
                    if (count == 5)
                        break;
                }
                
                // Split population into two parent groups
                List<Individual<T>> sub_population_a = new List<Individual<T>>();
                List<Individual<T>> sub_population_b = new List<Individual<T>>();
                int population_size = population.Count;
                for (int i = 0; i < population_size / 2; i++)
                {
                    rand = r.Next(0, population.Count);
                    sub_population_a.Add(population.ElementAt(rand));
                    population.RemoveAt(rand);

                    rand = r.Next(0, population.Count);
                    sub_population_b.Add(population.ElementAt(rand));
                    population.RemoveAt(rand);
                }

                sub_population_a.Sort(delegate(Individual<T> x, Individual<T> y)
                {
                    return x.getFitness(startYear, endYear, minPrice, MaxPrice).CompareTo(y.getFitness(startYear, endYear, minPrice, MaxPrice));
                });

                sub_population_b.Sort(delegate (Individual<T> x, Individual<T> y)
                {
                    return x.getFitness(startYear, endYear, minPrice, MaxPrice).CompareTo(y.getFitness(startYear, endYear, minPrice, MaxPrice));
                });

                // Perform crosuving
                population = new List<Individual<T>>();
                for (int i = 0; i < sub_population_a.Count; i++)
                {
                    Individual<T> parent_a = sub_population_a.ElementAt(i);
                    Individual<T> parent_b = sub_population_b.ElementAt(i);

                    population.Add(parent_a.crossover(parent_b));
                    population.Add(parent_b.crossover(parent_a));
                }

                // Add mutations
                for (int i = 0; i < number_of_mutations; i++)
                {
                    rand = r.Next(0, population.Count);
                    population.ElementAt(rand).mutate();
                }
            }

            return best_individual;
        }
    }
}