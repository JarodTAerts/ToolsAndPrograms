using System;
using System.Collections.Generic;
using System.Text;

namespace LearningAPIs
{
    public class WikipediaSearchResult
    {
        public int Totalhits { get; set; }

        public List<WikipediaSearchItem> Items { get; set; }

        public override string ToString()
        {
            StringBuilder str = new StringBuilder();
            str.Append("\nTotal Hits: " + Totalhits);
            str.Append("\n\n" + Items.Count + " Example Results: ");
            foreach(WikipediaSearchItem item in Items)
            {
                str.Append("\n"+item.ToString());
            }
            return str.ToString();
        }
    }
}
