using System.Net;

namespace SEOAnalyser.Areas.WebAPI.Common
{
    public static class WebsiteUtil
    {
        /// <summary>
        /// Check whether URL is a valid website
        /// </summary>
        public static bool IsWebSiteValid(string URL)
        {
            string Message = string.Empty;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);

            // set the credentials to the current user account
            request.Credentials = System.Net.CredentialCache.DefaultCredentials;
            request.Method = "GET";

            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {

                }
            }
            catch (WebException ex)
            {
                Message = ex.Message;
            }

            //if no webexception, return true/valid, else false/invalid
            return (Message.Length == 0);
        }
    }
}
