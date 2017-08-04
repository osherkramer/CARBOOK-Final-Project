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
        float getFitness();
        void mutate();
        T getGenes();
    }
}
