using Microsoft.AspNetCore.Mvc;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Linq;
using System.Threading;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebScrapeApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RefuseController : ControllerBase
    {
        const string url = "https://www.darlington.gov.uk/environment-and-planning/street-scene/weekly-refuse-and-recycling-collection-lookup/";
        private IWebDriver driver;

        // GET: api/<RefuseController>

        [HttpGet]
        public RefuseColletionInfoModel Get(string houseNo, string postcode)
        {
            //starting the browser in headless mode
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("headless");

            using var driver = new ChromeDriver(chromeOptions);
            driver.Navigate().GoToUrl(url);
            
            var element = driver.FindElement(By.Id("postcode"));
            var postcodeSearchButton = driver.FindElement(By.Id("postcodeLookupButton"));
            driver.FindElement(By.ClassName("cc_b_ok")).Click();
            //insert the postcode 
            element.SendKeys(postcode);
            //click the search button
            postcodeSearchButton.Click();
            Thread.Sleep(50);

            var addressCombo = driver.FindElement(By.Id("address"));
            addressCombo.SendKeys(houseNo);
            var SubmitButton = driver.FindElement(By.Id("mode"));
            SubmitButton.Click();
            Thread.Sleep(300);

            var nextCollect = driver.FindElements(By.ClassName("refuse-results")).FirstOrDefault();
            var NextCollectionType = nextCollect.FindElement(By.ClassName("panel-heading")).Text;
            var NextCollectionDate = nextCollect.FindElement(By.ClassName("collectionDate")).Text;

            var result = new RefuseColletionInfoModel
            {
                CollectionDate = NextCollectionDate,
                CollectionType = NextCollectionType
            };
            return result;
        }
    }
}
