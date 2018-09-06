using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeSimulation
{
    /// <summary>
    /// Class to represent a substance that will be part of an enviroment
    /// </summary>
    class Substance
    {
        private string name;
        private int substanceCode;
        private int density;
        private bool isFood;
        private int weight; //grams

        /// <summary>
        /// Constructor for the Substance class
        /// </summary>
        /// <param name="name">Name of the Substance</param>
        /// <param name="density">Density of the substance in g/cm3</param>
        /// <param name="isFood">Boolean which represents if the substance can be used as food by organisms</param>
        public Substance(string name, int density, bool isFood)
        {
            this.name = name;
            this.density = density;
            this.isFood = isFood;
        }

        /// <summary>
        /// Function to take a certain amount of the substance from this object
        /// It will try to take it all, but if there is not enough it will return all that is left
        /// </summary>
        /// <param name="amountInGrams">Amount of the substance requested</param>
        /// <returns>Amount of substance avaliable, either equal to the amount requested or less</returns>
        public int takeFrom(int amountInGrams)
        {
            //Find amount left after transaction and see if it is more than 0
            int amountLeft = weight - amountInGrams;
            if (amountLeft > 0)
            {
                //Substract the amount from the total weight and return that much
                weight -= amountInGrams;
                return amountInGrams;
            }
            else
            {
                //Return all that is left and set weight to 0
                weight = 0;
                return amountInGrams + amountLeft;
            }
        }

        /// <summary>
        /// Function to add an amount of a this substance to this object
        /// </summary>
        /// <param name="amountInGrams">The amount of the substance to add in grams</param>
        public void addTo(int amountInGrams)
        {
            weight += amountInGrams;
        }

    }
}
