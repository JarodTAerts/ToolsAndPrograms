using LearningAPIs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace OfflineWikipedia.Services
{
    public class StorageService
    {
        private const string dirPath = "WikiArticlesHTML/";


        public async static void SaveHTMLFileToStorage(string title)
        {
            string HTMLText = "";
            HTMLText = await APIServices.GetAllHTMLFromWikipediaArticle(title);
            File.WriteAllText(dirPath+title, HTMLText);
            Debug.WriteLine("Wrote To file: " + dirPath + title);
        }

        public static string GetHTMLTextFromFile(string fileName)
        {
            return File.ReadAllText(dirPath+fileName);
        }
    }
}
