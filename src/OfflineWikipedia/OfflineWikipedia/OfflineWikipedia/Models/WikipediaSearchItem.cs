using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace LearningAPIs
{
    class WikipediaSearchItem
    {
        public string Title { get; set; }
        public int Wordcount { get; set; }
        public int Size { get; set; }
        public string Snippet { get; set; }
        public DateTime Timestamp { get; set; }

        public override string ToString()
        {
            return "\nTitle: " + Title + "\nWordCount: " + Wordcount + "\nSize: " + Size + "\nSnippet: " + StripHTML(Snippet) + "\nTime: " + Timestamp ;
        }

        public string StripHTML(string input)
        {
            return Regex.Replace(input, "<.*?>", String.Empty);
        }
    }
}
