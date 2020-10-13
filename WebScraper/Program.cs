using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Threading;

namespace WebScraper
{
    public class Program
    {
        const string postcode = "DL3 0NJ";
        const string url = "https://www.darlington.gov.uk/environment-and-planning/street-scene/weekly-refuse-and-recycling-collection-lookup/";
        private static IWebDriver driver;

        //cc_b_ok overlay agree button

        static void Main(string[] args)
        {

            driver = new ChromeDriver();
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
            addressCombo.SendKeys("78");
            var SubmitButton = driver.FindElement(By.Id("mode"));
            SubmitButton.Click();
            Thread.Sleep(50);

            var nextCollect = driver.FindElements(By.ClassName("refuse-results"))[0];
            var NextCollectionType = nextCollect.FindElement(By.ClassName("panel-heading")).Text;
            var NextCollectionDate = nextCollect.FindElement(By.ClassName("collectionDate")).Text;

            driver.Quit();
        }
    }
}
