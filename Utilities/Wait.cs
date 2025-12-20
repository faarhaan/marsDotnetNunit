using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReqnrollProject1.Utilities
{
    public class Wait
    {
        // Generic Function for wait for Element to be Clickable
        public static void WaitToBeClickable(IWebDriver driver, string locatorType, string locatorValue, int Seconds)
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, Seconds));
            if (locatorType == "XPath")
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath(locatorValue)));
            if (locatorType == "Id")
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id(locatorValue)));
        }
        public static void WaitToBeVisible(IWebDriver driver, string locatorType, string locatorValue, int Seconds)
        {
            var waitFor = new WebDriverWait(driver, new TimeSpan(0, 0, Seconds));
            if (locatorType == "XPath")
                waitFor.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath(locatorValue)));
            if (locatorType == "Id")
                waitFor.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id(locatorValue)));
        }


    }
}
