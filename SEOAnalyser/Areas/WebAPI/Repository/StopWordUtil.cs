using SEOAnalyser.Areas.WebAPI.Interface;
using SEOAnalyser.Areas.WebAPI.Common;
using System;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace SEOAnalyser.Areas.WebAPI.Repository
{
    public class StopWordUtil : IStopWordUtil
    {
        public Task<List<string>> RemoveStopWord(string input)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Remove stopwords from string.
        /// </summary>
        public async Task<string> RemoveStopWordAsync(string input)
        {
            return await Task.Run(() =>
            {
                // split parameter into words
                var words = input.Split(Utilities.delimiters,
                StringSplitOptions.RemoveEmptyEntries);
                
                var lsWords = words.Where(x => !Utilities.stopWords.Contains(x.Trim().ToLower())).Select(x => x.ToLower().Trim()).ToList();
               return lsWords.Aggregate(string.Concat);


            });
        }

        public async Task<List<string>> RemoveStopWords(string input)
        {
            return await Task.Run(() =>
            {
                // split parameter into words
                var words = input.Split(Utilities.delimiters,
                StringSplitOptions.RemoveEmptyEntries);


                var lsWords = words.Where(x => !Utilities.stopWords.Contains(x.Trim().ToLower())).Select(x => x.ToLower().Trim()).ToList();
                return lsWords;

            });
        }
    }
}
