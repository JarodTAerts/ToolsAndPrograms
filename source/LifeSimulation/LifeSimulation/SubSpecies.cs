using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeSimulation
{
    /// <summary>
    /// Class that represents a Subspecies, a SubSpecies is very similar to a Species
    /// The differences are that it does not have a SubSpecies array and will have a different mutate method
    /// It may be better to combine these in the future and have a parent child class at some point
    /// </summary>
    public class SubSpecies
    {
        private string name;
        private string DNA;
        private ulong population;
        private int generationsAlive;
        private int startingTime;

        /// <summary>
        /// Constructor for the SubSpecies Class
        /// </summary>
        /// <param name="name">Name of the Subspecies</param>
        /// <param name="DNA">String which represents the DNA of the SubSpecies</param>
        public SubSpecies(string name, string DNA, int currentTime)
        {
            this.name = name;
            this.DNA = DNA;
            this.population = 1;
            this.generationsAlive = 0;
            this.startingTime = currentTime;
        }
    
        /// <summary>
        /// Method to get the population of the SubSpecies
        /// </summary>
        /// <returns>ULong of the population of the SubSpecies</returns>
        public ulong GetPopulation()
        {
            return population;
        }

        /// <summary>
        /// Method to get a string representation of the SubSpecies
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return String.Format("Name: {0} | Population: {1} | Generations Alive: {2}\n"
                , this.name, this.population, this.generationsAlive);
        }
    }
}
