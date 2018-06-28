using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LearningAPIs
{
    class APIServices
    {
        private const string baseUrl = @"https://en.wikipedia.org/w/api.php?";
        private const int defaultNumberOfResults = 5;
        public static async Task<WikipediaSearchResult> SearchTopic(string search)
        {
            Console.WriteLine("Searching Wikipedia.....");

            var client = new HttpClient();

            string searchEncoded = HttpUtility.HtmlEncode(search);
            string addon = "action=query&list=search&srsearch="+searchEncoded+"&utf8=&format=json&srlimit="+defaultNumberOfResults+"&srwhat=text";

            var jsonstr = await client.GetStringAsync(baseUrl+addon);

            JObject obj = JObject.Parse(jsonstr);

            var token = (JArray)obj.SelectToken("query.search");
            int hits = Convert.ToInt32(obj.SelectToken("query.searchinfo.totalhits"));

            var searchItems = JsonConvert.DeserializeObject<List<WikipediaSearchItem>>(token.ToString());

            WikipediaSearchResult result = new WikipediaSearchResult();
            result.Totalhits = hits;
            result.Items = searchItems;

            return result;
        }


    }
}
