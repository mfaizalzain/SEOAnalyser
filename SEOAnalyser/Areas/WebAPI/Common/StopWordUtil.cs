using System;
using System.Collections.Generic;
using System.Text;

namespace SEOAnalyser.Areas.WebAPI.Common
{
    //reference from https://www.dotnetperls.com/stopword-dictionary
    public static class CheckStopWordUtil
    {
       

        /// <summary>
        /// Remove stopwords from string.
        /// </summary>
        public static string RemoveStopWords(string input)
        {
           // Split parameter into words
            var words = input.Split(delimiters,
                StringSplitOptions.RemoveEmptyEntries);
            
            // Allocate new dictionary to store found words
            var found = new Dictionary<string, bool>();
           
            // Store results in this StringBuilder
            StringBuilder builder = new StringBuilder();
           
           foreach (string currentWord in words)
            {
               // Convert to lowercase
                string lowerWord = currentWord.ToLower();
               
                // If this is a usable word, add it
                if (!stopWords.ContainsKey(lowerWord) &&
                    !found.ContainsKey(lowerWord))
                {
                    builder.Append(currentWord).Append(' ');
                    found.Add(lowerWord, true);
                }
            }
           
            // Return string with words removed
            return builder.ToString().Trim();
        }
    }
}
