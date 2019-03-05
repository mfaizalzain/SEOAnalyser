using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using SEOAnalyser.Areas.WebAPI.Common;
using SEOAnalyser.Areas.WebAPI.Controllers;
using SEOAnalyser.Areas.WebAPI.Interface;
using SEOAnalyser.Areas.WebAPI.Repository;
using SEOAnalyser.Controllers;

namespace Tests
{
    public class Tests
    {
     
        [OneTimeSetUp]
        public void Setup()
        {
         
        }

       
        [Test]
        public async System.Threading.Tasks.Task Test1Async()
        {
           
            var result = await Utilities.IsInputUrl("https://www.google.com");

            Assert.IsTrue(result);
        }

        [Test]
        public async System.Threading.Tasks.Task Test2Async()
        {

            var result = await Utilities.IsInputUrl("this is a test string");

            Assert.IsFalse(result);
        }

       

    }
}