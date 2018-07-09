using System;
using System.Collections.Generic;
using System.Text;

namespace OfflineWikipedia.Models
{
    class Article
    {
        public string Title { get; set; }
        public Dictionary<string,string> HeadingContentDictonary { get; set; }
        public ArrayList<string> References { get; set; }
    }
}
