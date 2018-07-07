using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace LearningAPIs
{
    public class WikipediaSearchItem
    {
        public string Title { get; set; }
        public int Wordcount { get; set; }
        public int Size { get; set; }
        private string _snippet;
        public string Snippet {
            get => _snippet;
            set => _snippet=StripHTML(value);
        }
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
