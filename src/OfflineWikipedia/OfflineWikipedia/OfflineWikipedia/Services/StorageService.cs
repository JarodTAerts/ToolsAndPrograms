using LearningAPIs;
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
    public class StorageService
    {
        private static string dirPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);


        public async static Task SaveHTMLFileToStorage(string title)
        {
            string HTMLText = "";
            HTMLText = await APIServices.GetAllHTMLFromWikipediaArticle(title);
            string fileName= Path.Combine(dirPath,(title+".wik")); 
            File.WriteAllText(fileName, HTMLText);
            Debug.WriteLine("Wrote To file: " + dirPath + title);
        }
 
        public async static Task<string> GetHTMLTextFromFile(string title)
        {
            string fileName = Path.Combine(dirPath, (title + ".wik"));
            return File.ReadAllText(fileName);
        }

        public async static Task<List<string>> GetNamesOfSavedArticles()
        {

            string[] resultsStrings= Directory.GetFiles(dirPath);
            List<string> results = new List<string>();
            foreach (string s in resultsStrings)
            {
                results.Add(Path.GetFileName(s).Substring(0,Path.GetFileName(s).Length-4));
            }

            return results;
        }
    }
}
