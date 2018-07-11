using OfflineWikipedia.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace OfflineWikipedia.Helpers
{
    /// <summary>
    /// Class to handle the parsing and processing of HTML Text
    /// </summary>
    class HTMLHandler
    {
        public async static Task CleanHTMLFile(string title)
        {
            string fileText = await StorageService.GetHTMLTextFromFile(title);
            fileText = StripHTML(fileText);
            await StorageService.WriteTextToFile(title,fileText);
        }

        public static string StripHTML(string input)
        {
            input = Regex.Replace(input, "<script>.*</script>", String.Empty);
            input = Regex.Replace(input, "<script>.*\n.*</script>", String.Empty);
            input = Regex.Replace(input, "<.*?>", String.Empty);
            input = HttpUtility.HtmlDecode(Regex.Replace(input, "<.*?->", String.Empty));
            input = CutOutBeforeString(input,"Jump to search");
            input = CutOutAfterStringReference(input, "References[edit]");
            input = CutOutAfterStringReference(input, "References");
            input = CutOutAfterString(input, "<!-- \nNewPP limit report");
            input = CutOutAfterString(input, "Notes[edit]");
            input = Regex.Replace(input, "\nv\nt\ne\n",String.Empty);
            input = Regex.Replace(input, "\\[edit\\]", ":\n");
            input = CutOutAfterString(input, "See also:");
            input = Regex.Replace(input, "\n{3,}", "\n\n");
            return input.Trim();
        }

        public static string ReplaceColons(string input)
        {
            return Regex.Replace(input, ":", "-");
        }

        public static string SimpleHTMLStrip(string input)
        {
            return Regex.Replace(input, "<.*?>", String.Empty);
        }

        public static string CutOutBeforeString(string input, string cutString)
        {
            int indexOfSub = input.IndexOf(cutString);
            return input.Substring(indexOfSub+cutString.Length);
        }

        public static string CutOutAfterStringReference(string input, string cutString)
        {
            int indexOfSub = input.IndexOf(cutString);
            if (indexOfSub > 0)
            {
                indexOfSub = input.IndexOf(cutString, indexOfSub + cutString.Length);
                Debug.WriteLine("Index of: " + indexOfSub);
                if (indexOfSub > 0) {
                    return input.Substring(0, indexOfSub);
                }
            }
                return input;
            
        }

        public static string CutOutAfterString(string input, string cutString)
        {
            int indexOfSub = input.IndexOf(cutString);
                if (indexOfSub > 0)
                {
                    return input.Substring(0, indexOfSub);
                }

            return input;

        }
    }
}
