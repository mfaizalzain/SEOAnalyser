using System.Collections.Generic;
using System.Threading.Tasks;

namespace SEOAnalyser.Areas.WebAPI.Interface
{
    public interface IStopWordUtil
    {
        Task<string> RemoveStopWordAsync(string input);
        Task<List<string>> RemoveStopWords(string input);
    }
}
