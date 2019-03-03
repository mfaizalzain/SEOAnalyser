using System.Collections.Generic;

namespace SEOAnalyser.Areas.WebAPI.Models
{
    public class ApiResponseModel
    {
        public string ErrorMessage { get; set; }
        public string Result { get; set; }
        public List<BaseResultModel> Data { get; set; }
        public int Count { get; set; }
    }
}
