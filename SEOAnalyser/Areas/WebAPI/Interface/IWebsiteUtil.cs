using SEOAnalyser.Areas.WebAPI.Models;
using SEOAnalyser.Models;
using System.Threading.Tasks;

namespace SEOAnalyser.Areas.WebAPI.Interface
{
    public interface IWebsiteUtil
    {
        Task<WebsiteResponseModel> IsDataValidAsync(SEOAnalyserInputModel query);
       
    }
}
