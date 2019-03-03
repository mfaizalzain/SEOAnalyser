using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SEOAnalyser.Areas.WebAPI.Common
{
    public static class ExternalLinkUtil
    {
        /// <summary>
        /// Get external links from input string
        /// </summary>
        public static List<string> GetExternalLink(string input, string url)
        {
            MatchCollection ExtUrlList;
            var lstExternalLink = new List<string>();
            string regexPattern = "(<a href=\"http://.*?>.*?</a>)";

           
            ExtUrlList = Regex.Matches(input, regexPattern);

            //no match then return empty list
            if (ExtUrlList.Count == 0)
                return lstExternalLink;

            //if url is not null/empty then filter out external link, if not null, list all matched links in input string
            if (!string.IsNullOrEmpty(url))
            {
               foreach (var item in ExtUrlList)
                {
                    //filter out links that match url
                    if (item.ToString().IndexOf(url) < 0)
                        lstExternalLink.Add(item.ToString());

                }
            }
            else
                lstExternalLink = ExtUrlList.Select(x => x.Value.ToString()).ToList();
           
          
            
            return lstExternalLink;
        }
    }
}
