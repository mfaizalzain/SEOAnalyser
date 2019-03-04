using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using SEOAnalyser.Areas.WebAPI.Common;
using SEOAnalyser.Areas.WebAPI.Controllers;
using SEOAnalyser.Areas.WebAPI.Interface;

namespace Tests
{
    public class Tests
    {
        [SetUp]
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