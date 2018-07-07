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
        private const string baseAPIUrl = @"https://en.wikipedia.org/w/api.php?";
        private const string basePageUrl = @"https://en.wikipedia.org/wiki/";
        private const int defaultNumberOfResults = 5;

        public static async Task<WikipediaSearchResult> SearchTopic(string search,int numberOfResults)
        {

            
                //await Task.Delay(3000);
                var client = new HttpClient();

                string searchEncoded = HttpUtility.HtmlEncode(search);
                string addon = "action=query&list=search&srsearch=" + searchEncoded + "&utf8=&format=json&srlimit=" + numberOfResults + "&srwhat=text";

                var jsonstr = await client.GetStringAsync(baseAPIUrl + addon);

                JObject obj = JObject.Parse(jsonstr);

                var token = (JArray)obj.SelectToken("query.search");
                int hits = Convert.ToInt32(obj.SelectToken("query.searchinfo.totalhits"));

                var searchItems = JsonConvert.DeserializeObject<List<WikipediaSearchItem>>(token.ToString());

                WikipediaSearchResult result = new WikipediaSearchResult();
                result.Totalhits = hits;
                result.Items = searchItems;

                return result;
        }


        public static async Task<string> GetAllHTMLFromWikipediaArticle(string title)
        {
            var client = new HttpClient();
            string titleEncoded = HttpUtility.HtmlEncode(title);

            string htmlStr = (string)await client.GetStringAsync(basePageUrl + titleEncoded);

            return htmlStr;
        }
    }
}
