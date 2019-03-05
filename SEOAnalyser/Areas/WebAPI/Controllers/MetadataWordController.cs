using Microsoft.AspNetCore.Mvc;
using SEOAnalyser.Areas.WebAPI.Common;
using SEOAnalyser.Areas.WebAPI.Interface;
using SEOAnalyser.Areas.WebAPI.Models;
using SEOAnalyser.Models;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Linq;

namespace SEOAnalyser.Areas.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MetadataWordController : ControllerBase
    {
        private readonly IWebsiteUtil _websiteUtil;
        private readonly IStopWordUtil _stopwordUtil;

        public MetadataWordController(IStopWordUtil stopwordUtil, IWebsiteUtil websiteUtil)
        {
            _stopwordUtil = stopwordUtil;
            _websiteUtil = websiteUtil;
        }

        // GET: api/MetadataWord/Get
        [HttpPost]
        public async Task<IActionResult> GetAsync([FromBody] SEOAnalyserInputModel query)
        {
            var result = new List<BaseResultModel>();
            var lstWords = new List<string>();
            string processInput = query.analyseInput;

            if (await Utilities.IsInputUrl(query.analyseInput))
            {
                var resultMsg = await _websiteUtil.IsDataValidAsync(query);
                //if statuscode is not OK or metadata is empty return empty result
                if (resultMsg.StatusCode != (int)HttpStatusCode.OK || string.IsNullOrEmpty(resultMsg.MetaData))
                    return Ok(new ApiResponseModel() { ErrorMessage = resultMsg.ErrorMessage, Data = result, Count = 0, Result = "Failed" });

                processInput = resultMsg.MetaData;

                if (query.checkStopWord)
                    lstWords = await _stopwordUtil.RemoveStopWords(processInput);
                else
                    lstWords = await Utilities.StringToWordList(processInput);

                result = await Utilities.WordOccurenceCount(lstWords);
               

            }

            return Ok(new ApiResponseModel() { Result = "Success", Data = result, Count = result.Count() });

        }


    }
}
