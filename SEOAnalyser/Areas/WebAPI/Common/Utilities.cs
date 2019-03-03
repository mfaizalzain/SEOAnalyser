using HtmlAgilityPack;
using SEOAnalyser.Areas.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEOAnalyser.Areas.WebAPI.Common
{
    public static class Utilities
    {
       

        /// <summary>
        ///  check whether string input is URL or not
        /// </summary>
        public static async Task<bool> IsInputUrl(string input)
        {
            return await Task.Run(() =>
            {
                if (Uri.IsWellFormedUriString(input, UriKind.Absolute))
                    return true;
                else
                    return false;
            });
        }

        /// <summary>
        /// List of words we want to remove. We can define the list based on application requirement, no standard list for stop words.
        /// </summary>
        public static string[] stopWords = new string[]
        {
         "a","about","above","across","after", "afterwards", "again",  "against", "all",  "almost", "along", "already",  "also", "although",
         "always","am","among","amongst", "amount", "an", "and", "another",  "any", "anyhow",  "anyone", "anything", "anyway",  "anywhere",
           "are", "around",  "as",  "at",  "back",  "be", "became", "because","become", "becomes", "becoming",  "been","before", "beforehand",
             "behind", "being", "below", "beside", "besides", "between", "beyond", "both", "bottom", "but",  "by", "can", "cannot", "cant",
           "co", "could", "couldnt", "do", "done", "down", "due", "during", "each","eg", "either",  "else", "elsewhere","etc", "even",
            "ever", "every", "everyone" , "everything", "everywhere","few", "for", "from","front", "further", "get", "give", "go",
           "had", "has", "have", "he",  "hence",  "her",  "here","hereafter", "hereby", "herein",  "hereupon", "hers", "herself", "him",
           "himself",  "his", "how", "however",  "i", "ie", "if", "in",  "inc", "into", "is", "it", "its", "itself",  "least",
           "less",  "many",  "may",  "me", "meanwhile",  "might", "mine",  "more", "moreover",  "most", "mostly", "move","much",
           "must", "my","myself", "name", "namely", "neither", "never",  "nevertheless", "no", "nobody", "none",  "nor",
            "not","nothing", "now", "nowhere", "of", "off", "often", "on", "once",  "one", "only","onto",  "or", "other",  "others",
            "otherwise","our", "ours", "ourselves", "out","over","part","per", "perhaps", "please", "put","rather", "re","see",
           "seem",  "seemed", "seeming", "seems", "several", "she"," should", "show","side", "since","so","some",  "somehow",
            "someone", "something","sometime",  "sometimes",  "somewhere", "still", "such", "system", "take", "than",  "that","the",
         "their", "them",  "themselves", "then","thence","there","thereafter","thereby", "therefore","therein",  "thereupon",
           "these",  "they", "thick",  "thin", "third","this", "those","though","three",  "through", "throughout", "thru", "thus",
           "to","together","too","top","toward",  "towards",  "un",  "under","until", "up", "upon", "us", "very", "via", "was",
            "we", "well", "were",  "what","whatever","when", "whence", "whenever", "where","whereafter","whereas", "whereby","wherein",
         "whereupon","wherever",  "whether","which", "while","whither","who", "whoever","whole","whom","whose",  "why","will",
         "with", "within","without", "would","yet","you","your","yours", "yourself","yourselves","ui","id","true","false","-"
    };

        /// <summary>
        /// delimiter that separate words. 
        /// </summary>
        public static char[] delimiters = new char[]
        {
        ' ',
        ',',
        ';',
        '.'
        };

        /// <summary>
        /// Uses HtmlAgilityPack to get the meta information from input string/html
        /// </summary>
        public static async Task<string> GetMetaData(string input)
        {
            return await Task.Run(() =>
            {
                var doc = new HtmlDocument();
              
                StringBuilder sb = new StringBuilder();
                doc.LoadHtml(input);
                var metaTags = doc.DocumentNode.SelectNodes("//meta");

                if (metaTags != null)
                {
                    foreach (var tag in metaTags)
                    {

                        var tagName = tag.Attributes["name"];
                        var tagContent = tag.Attributes["content"];
                        var tagProperty = tag.Attributes["property"];

                        if (tagName != null && tagContent != null)
                        {
                            if (tagName.Value.ToLower() == "description" || tagName.Value.ToLower() == "keywords")
                            {
                                sb.Append(tagContent.Value).Append(' ');
                            }
                        }
                        else if (tagProperty != null && tagContent != null)
                        {
                            if (tagProperty.Value.ToLower() == "og:description")
                            {
                                sb.Append(tagContent.Value).Append(' ');

                            }
                        }


                    }
                }

                input = sb.ToString();
                sb.Clear();
               
                return input;
            });
        }


        /// <summary>
        /// convert string to list of words
        /// </summary>
        public async static Task<List<string>> StringToWordList(string input)
        {
            return await Task.Run(() =>
            {
                var words = input.Split(delimiters,StringSplitOptions.RemoveEmptyEntries);
               return words.Select(x => x.Trim()).ToList();

            });
        }


        /// <summary>
        /// group words and occurence count
        /// </summary>
        public async static Task<List<BaseResultModel>> WordOccurenceCount(List<string> lsWords)
        {
            return await Task.Run(() =>
            {
                var result = new List<BaseResultModel>();

              if (lsWords.Count > 0)
              {
                    result = lsWords.Where(x => !string.IsNullOrEmpty(x) && x.Trim().Length > 3).GroupBy(x => x)
                          .Select(g => new BaseResultModel()
                          {
                              word = g.Key.ToString(),
                              total = g.Count()
                          }).OrderByDescending(x => x.total).ToList();

                }
                return result;
              

            });
        }

       

    }
}
