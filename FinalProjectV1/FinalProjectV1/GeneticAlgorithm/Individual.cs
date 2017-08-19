using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FinalProjectV1
{
    public interface Individual<T>
    {
        Individual<T> crossover(Individual<T> partner);
        double getFitness(int? startYear, int? endYear, string minPrice, string MaxPrice);
        void mutate();
        T getGenes();
        bool isEqual(T carsIndividual);
    }
}
