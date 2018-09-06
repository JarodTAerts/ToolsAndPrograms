using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LifeSimulation
{
    /// <summary>
    /// Class which represents a species of organisms 
    /// </summary>
    class Species
    {
        private string name;
        private string DNA;
        private ulong population;
        private Dictionary<string, SubSpecies> subSpecies;
        private Dictionary<string, Substance> substances;
        private Dictionary<string, int> DNADic;
        private int lifeSpan;
        private int generationsAlive;
        private int startingTime;
        private bool isAlive;

        /// <summary>
        /// Constructor for a species
        /// </summary>
        /// <param name="Name">Name of the species</param>
        /// <param name="DNA">String of text which represents the DNA for the organism</param>
        public Species(string Name, string DNA, int currentTime)
        {
            this.name = Name;
            this.DNA = DNA;
            this.DNADic = null;
            this.population = 1;
            this.subSpecies = new Dictionary<string, SubSpecies>();
            this.substances = TextParsers.GetSubstancesFromDNA(DNA);
            this.DNADic = TextParsers.GetDNACodesFromDNA(DNA);
            this.lifeSpan = FindLifeSpan();
            this.generationsAlive = 0;
            this.startingTime = currentTime;
            isAlive = true;
        }

        /// <summary>
        /// Method which puts the whole species through reproduction
        /// </summary>
        public void Reproduce()
        {
            this.population *= 2;
        }

        /// <summary>
        /// Function to create a new SubSpecies of this species
        /// </summary>
        /// <param name="SubDNA">The DNA of the SubSpecies with Mutations</param>
        private void CreateSubSpecies(string SubDNA)
        {
            string name = FindNameFromSubDNA(SubDNA);
            this.subSpecies.Add(name, new SubSpecies(name,SubDNA,(startingTime+generationsAlive)));
        }

        /// <summary>
        /// Function that will look at the DNA differences of a SubSpecies and name it accordingly
        /// </summary>
        /// <returns>String that is the name of a SubSpecies</returns>
        private string FindNameFromSubDNA(string SubDNA)
        {
            //TODO: Make This function work right
            return "SubSpecies";
        }

        /// <summary>
        /// Function which looks at the various aspects of the Cells anatomy and determins how long it will take to reproduce
        /// </summary>
        /// <returns>Int representing the lifetime in minutes</returns>
        private int FindLifeSpan()
        {
            //TODO: Finish this
            return 0;
        }

        /// <summary>
        /// Method to get the population of the species and all of it's SubSpecies
        /// </summary>
        /// <returns>ULong of the total population</returns>
        public ulong GetPopulationWithSubSpecies()
        {
            ulong totalPop = population;
            //Get populations of subspecies and add them to the totalPopulation
            foreach(SubSpecies s in subSpecies.Values)
            {
                totalPop += s.GetPopulation();
            }
            return totalPop;
        }

        /// <summary>
        /// Method to get the population of only this direct species
        /// </summary>
        /// <returns></returns>
        public ulong GetPopulation()
        {
            return population;
        }

        /// <summary>
        /// Method to return a string representation of of the species and it's subspecies
        /// </summary>
        /// <returns>string of the species and it's subspecies</returns>
        public override string ToString()
        {
            //Get string for this species
            string returnString= String.Format("Name: {0} | Population: {1} | Number of Subspecies: {2} | Generations Alive: {3}\n"
                ,this.name, this.population, this.subSpecies.Count, this.generationsAlive);
            returnString += "\tSubSpecies:\n";
            //Get representation for all the subspecies
            foreach (SubSpecies s in subSpecies.Values)
            {
                returnString += "\t - " + s.ToString();
            }
            return returnString;
        }

        public bool timeToReproduce()
        {
            return (generationsAlive%lifeSpan)==0;
        }
    }
}
