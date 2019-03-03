using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using SEOAnalyser.Areas.WebAPI.Interface;

namespace SEOAnalyser.Areas.WebAPI.Repository
{
    public class ExternalLinkUtil : IExternalLinkUtil
    {
        public async Task<List<string>> GetExternalLinkAsync(string input, string url)
        {
            return await Task.Run(() =>
            {
                MatchCollection ExtUrlList;
                var lstExternalLink = new List<string>();
                string regexPattern = @"((http|ftp|https):\/\/[\w\-_]+(\.[\w\-_]+)+([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])?)"; ;


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
                        if (!item.ToString().Contains(url))
                            lstExternalLink.Add(item.ToString());
                    }
                }
                else
                    lstExternalLink = ExtUrlList.Select(x => x.Value.ToString()).ToList();

                return lstExternalLink;
            });
        }
    }
}
