using System.ComponentModel.DataAnnotations;

namespace SEOAnalyser.Models
{
    public class SEOAnalyserInputModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter URL/Text")]
        [Display(Name = "Enter URL/Text in English Only")]
        public string analyseInput { get; set; }

        [Display(Name = "Filter out Stop-Word")]
        public bool checkStopWord { get; set; }

        [Display(Name = "Check Metadata Words")]
        public bool checkMetadata { get; set; }

        [Display(Name = "Check All Words")]
        public bool checkAllWord { get; set; }

        [Display(Name = "Check External Links")]
        public bool checkExternalLink { get; set; }
    }
}
