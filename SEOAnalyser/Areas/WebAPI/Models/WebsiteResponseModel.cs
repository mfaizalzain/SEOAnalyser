using System.Collections.Generic;

namespace SEOAnalyser.Areas.WebAPI.Models
{
    public class WebsiteResponseModel
    {
        public string ErrorMessage { get; set; }
        public int StatusCode { get; set; }
        public string Content { get; set; }
        public List<string> ExtLinks { get; set; }
        public string MetaData { get; set; }
    }
}
