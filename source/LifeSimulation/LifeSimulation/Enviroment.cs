using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeSimulation
{
    /// <summary>
    /// Class which represents an enviroment where organisms will reside
    /// </summary>
    class Enviroment
    {
        private string name;
        private int temperature;
        private int currentTime;
        private Dictionary<string, Substance> substances;
        private Dictionary<string,Species> species;
        private int length;
        private int width;
        private int height;
        private int volume;



        /// <summary>
        /// Constructor for Enviroment class
        /// </summary>
        /// <param name="name">Name of Enviroment</param>
        /// <param name="startingTemp">Temperature that the enviroment will start at</param>
        /// <param name="substancesFileName">File name of the file where the substances data for the enviroment is stored</param>
        /// <param name="speciesFileName">Name of file where the data for the species that will live in the enviroment is stored</param>
        /// <param name="length">Length of the enviroment</param>
        /// <param name="width">Width of the enviroment</param>
        /// <param name="height">Height of the enviroment</param>
        public Enviroment(string name, int startingTemp, string substancesFileName, string speciesFileName, int length, int width, int height)
        {
            this.name = name;
            this.temperature = startingTemp;
            this.currentTime = 0;
            substances = TextParsers.ParseSubstances(substancesFileName);
            species = TextParsers.ParseSpecies(speciesFileName);
            this.volume = length * width * height;
        }

        /// <summary>
        /// Alternate Constructor for the Enviroment Class
        /// </summary>
        /// <param name="name">Name of Enviroment</param>
        /// <param name="startingTemp">Temperature that the enviroment will start at</param>
        /// <param name="substancesFileName">File name of the file where the substances data for the enviroment is stored</param>
        /// <param name="speciesFileName">Name of file where the data for the species that will live in the enviroment is stored</param>
        /// <param name="volume">Total Enviroment of the Enviroment</param>
        public Enviroment(string name, int startingTemp, string substancesFileName, string speciesFileName, int volume)
        {
            this.name = name;
            this.temperature = startingTemp;
            this.substances = TextParsers.ParseSubstances(substancesFileName);
            this.species = TextParsers.ParseSpecies(speciesFileName);
            this.volume = volume;
        }

        /// <summary>
        /// Function to get the total population of all organisms living in the enviroment
        /// </summary>
        /// <returns>ulong with total population</returns>
        public ulong GetPopulation()
        {
            ulong population = 0;
            foreach(Species s in species.Values)
            {
                population += s.GetPopulationWithSubSpecies();
            }

            return population;
        }

        public void SpeciesReproduce()
        {
            foreach (Species s in species.Values)
            {
                if (s.timeToReproduce())
                {
                    s.Reproduce();
                }
            }
        }
        
    }
}
