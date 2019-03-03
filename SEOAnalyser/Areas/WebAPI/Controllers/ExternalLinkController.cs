using Microsoft.AspNetCore.Mvc;
using SEOAnalyser.Areas.WebAPI.Interface;
using SEOAnalyser.Areas.WebAPI.Models;
using SEOAnalyser.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net;
using SEOAnalyser.Areas.WebAPI.Common;
using System.Linq;

namespace SEOAnalyser.Areas.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExternalLinkController : ControllerBase
    {
        private readonly IWebsiteUtil _websiteUtil;

        public ExternalLinkController(IWebsiteUtil websiteUtil)
        {
            _websiteUtil = websiteUtil;
        }

        // GET: api/ExternalLink/Get
        [HttpPost]
        public async Task<IActionResult> GetAsync([FromBody] SEOAnalyserInputModel query)
        {
            var result = new List<BaseResultModel>();
            var resultMsg = await _websiteUtil.IsDataValidAsync(query);

            //if statuscode is not OK or metadata is empty return empty result
            if (resultMsg.StatusCode != (int)HttpStatusCode.OK || resultMsg.ExtLinks == null)
                return Ok(new ApiResponseModel() { ErrorMessage = resultMsg.ErrorMessage, Data = result, Count = 0, Result = "Failed" });

            //get external links from results
            var ExtLinks = resultMsg.ExtLinks;
            
            result = await Utilities.WordOccurenceCount(ExtLinks);

            return Ok(new ApiResponseModel() { Result = "Success", Data = result, Count = result.Count() }); 
        }

       
    }
}
