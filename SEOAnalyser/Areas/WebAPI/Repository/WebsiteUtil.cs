using HtmlAgilityPack;
using SEOAnalyser.Areas.WebAPI.Common;
using SEOAnalyser.Areas.WebAPI.Interface;
using SEOAnalyser.Areas.WebAPI.Models;
using SEOAnalyser.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SEOAnalyser.Areas.WebAPI.Repository
{
    public class WebsiteUtil : IWebsiteUtil
    {
       
        private readonly IStopWordUtil _stopwordUtil;
        private readonly IExternalLinkUtil _extLinkUtil;

        public WebsiteUtil(IStopWordUtil stopwordUtil, IExternalLinkUtil extLinkUtil)
        {
            _stopwordUtil = stopwordUtil;
            _extLinkUtil = extLinkUtil;
        }

        /// <summary>
        /// Check whether URL is a valid website, then process
        /// </summary>
        public async Task<WebsiteResponseModel> IsDataValidAsync(SEOAnalyserInputModel query)
        {
           
            var result = new WebsiteResponseModel();
            string input = query.analyseInput;
            string url = string.Empty;
            string metadata = string.Empty;
           
            if (await Utilities.IsInputUrl(input))
            {
                url = input;

                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        var response = await client.GetAsync(input);
                        int StatusCode = (int)response.StatusCode;
                        result.StatusCode = StatusCode;

                        //statuscode is OK, return website content
                        if (StatusCode == (int)HttpStatusCode.OK)
                        {
                            input = await response.Content.ReadAsStringAsync();

                            if (query.checkExternalLink)
                                result.ExtLinks = await _extLinkUtil.GetExternalLinkAsync(input, response.RequestMessage.RequestUri.Host);


                            var doc = new HtmlDocument();
                            var sb = new List<string>();
                            doc.LoadHtml(input);
                            var words = doc.DocumentNode.SelectNodes("//body").Select(x => x.InnerText);

                            var str = string.Empty;
                            
                            foreach (var node in words)
                            {
                                char[] arr = node.Trim().ToCharArray();
                                arr = Array.FindAll(arr, (c => (char.IsLetter(c)
                                                             || char.IsWhiteSpace(c)
                                                             || c == '-')));
                                str = new string(arr);

                                if(!string.IsNullOrEmpty(str.Trim()))
                                    sb.Add(str.Trim());
                            }

                            input = sb.Aggregate(string.Concat);
                           
                            //get metadata only if the metadata checkbox is ticked
                            if (query.checkMetadata)
                                metadata = await Utilities.GetMetaData(response.Content.ReadAsStringAsync().ToString());


                        }
                        
                           
                    }
                }
                catch (WebException ex)
                {
                    result.ErrorMessage = ex.Message;
                }
            }
            else
            {
                //if the input is text then pass as statuscode OK
                result.StatusCode = (int)HttpStatusCode.OK;
               
            }

            result.Content = input.ToLower();
            result.MetaData = metadata;

            return result;
        }
    }
}
