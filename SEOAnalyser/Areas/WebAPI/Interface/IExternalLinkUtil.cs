using System.Collections.Generic;
using System.Threading.Tasks;

namespace SEOAnalyser.Areas.WebAPI.Interface
{
    public interface IExternalLinkUtil
    {
        Task<List<string>> GetExternalLinkAsync(string input, string url);
    }
}
