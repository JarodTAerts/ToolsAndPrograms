using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LifeSimulation
{
    /// <summary>
    /// Class that holds tools for getting data from text files and putting it into objects
    /// </summary>
    public static class TextParsers
    {
        /// <summary>
        /// Method to get a Dictionary of substances for an enviroment from a datafile
        /// </summary>
        /// <param name="fileName">Name of datafile</param>
        /// <returns>Dictionary of Substances with name as the key</returns>
        internal static Dictionary<string, Substance> ParseSubstances(string fileName)
        {
            Dictionary<string,Substance> substances = new Dictionary<string, Substance>();
            //Get Text and array of lines from the text
            string fileText = GetTextFromFile(fileName);
            string[] lines = GetLinesFromText(fileText);
            //Loop through lines and get object from each one and add it to ArrayList
            for(int i = 0; i < lines.Length; i++)
            {
                string[] properties = lines[i].Split(':');
                substances.Add(properties[0],new Substance(properties[0],Convert.ToInt32(properties[1]),Convert.ToBoolean(properties[2])));
            }

            return substances;
        }

        /// <summary>
        /// Method to get a Dictionary of species from a datafile
        /// </summary>
        /// <param name="fileName">Name of Sspecies datafile</param>
        /// <returns>Dictionary of Species with their names as their keys</returns>
        internal static Dictionary<string,Species> ParseSpecies(string fileName)
        {
            Dictionary<string,Species> species = new Dictionary<string, Species>();
            //Get Text and array of lines from the text
            string fileText = GetTextFromFile(fileName);
            string[] lines = GetLinesFromText(fileText);
            //Loop through lines and get object from each one and add it to ArrayList
            for (int i = 0; i < lines.Length; i++)
            {
                string[] properties = lines[i].Split(':');
                species.Add(properties[0],new Species(properties[0], properties[1],0));
            }

            return species;
        }

        /// <summary>
        /// Function that will search through DNA and return a Dictionary of Substances that the organism is made up of
        /// </summary>
        /// <param name="DNA">String representing DNA</param>
        /// <returns></returns>
        internal static Dictionary<string,Substance> GetSubstancesFromDNA(string DNA)
        {
            //TODO: Finish this 
            return null;
        }

        /// <summary>
        /// Function that will split up DNA string into the codes that define different parts of the cell
        /// </summary>
        /// <param name="DNA">String containing the DNA</param>
        /// <returns>Dictonaty with Code Keys as the Key and Code values as the values</returns>
        internal static Dictionary<string, int> GetDNACodesFromDNA(string DNA)
        {
            //TODO: Write This
            return null;
        }


        /// <summary>
        /// Method to get lines from text block, seperated by semi-colons
        /// </summary>
        /// <param name="text">String of text to be split into lines</param>
        /// <returns>Array of Strings</returns>
        private static string[] GetLinesFromText(string text)
        {
            string textSansLines= Regex.Replace(text, @"\t|\n|\r", "");
            return text.Split(';');
        }

        /// <summary>
        /// Method to get all the text from a file
        /// </summary>
        /// <param name="fileName">Name of File to get text from</param>
        /// <returns>String with all the text from a file</returns>
        private static string GetTextFromFile(string fileName)
        {
            try
            {
                string text = File.ReadAllText(fileName);
                return text;
            }
            catch(Exception e)
            {
                Console.WriteLine("Could not read from file: " + fileName);
            }
            return null;
        }



    }
}
