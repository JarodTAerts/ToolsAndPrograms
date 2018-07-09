﻿using LearningAPIs;
using OfflineWikipedia.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace OfflineWikipedia.Services
{
    /// <summary>
    /// Class that will contain all the functions to connect to and do operations with the local storage
    /// </summary>
    public class StorageService
    {
        // String that gets a path to the local storage of the device being used
        private static string dirPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);


        /// <summary>
        /// Function to save the HTML of a article to a local file.
        /// </summary>
        /// <param name="title">The title of the article to get the html from and the name of file to be saved to</param>
        /// <returns>Nothing</returns>
        public async static Task SaveHTMLFileToStorage(string title)
        {
            string HTMLText = "";
            //Call the API service to get the HTML text from wikipedia
            HTMLText = await APIServices.GetAllHTMLFromWikipediaArticle(title);
            //Get the path to the file where it will be stored
            string fileName= Path.Combine(dirPath,(title+".wik")); 
            //Write to file
            File.WriteAllText(fileName, HTMLText);
            Debug.WriteLine("Wrote To file: " + dirPath + title);
        }
 
        /// <summary>
        /// Function to get the Text from a file 
        /// </summary>
        /// <param name="title">Title of the article file to get text from</param>
        /// <returns>String that contains all the text from the file</returns>
        public async static Task<string> GetHTMLTextFromFile(string title)
        {
            //Get the path of the file and get all the text
            string fileName = Path.Combine(dirPath, (title + ".wik"));
            return File.ReadAllText(fileName);
        }

        /// <summary>
        /// Get the names of all of file downloaded to local storage
        /// </summary>
        /// <returns></returns>
        public async static Task<List<string>> GetNamesOfSavedArticles()
        {
            //Get an array of all the file names, including paths
            string[] resultsStrings= Directory.GetFiles(dirPath);
            //Make a list and add all the file names, only names, to it. then return it
            List<string> results = new List<string>();
            foreach (string s in resultsStrings)
            {
                results.Add(Path.GetFileName(s).Substring(0,Path.GetFileName(s).Length-4));
            }

            return results;
        }
    }
}
