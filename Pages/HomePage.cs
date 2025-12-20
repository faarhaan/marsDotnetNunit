using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mars.Utilities;
using NUnit.Framework;
using OpenQA.Selenium;

namespace mars.Pages
{
    public class HomePage : CommonDriver
    {
        // Locator as Private Field 
        private readonly By marsLogo = By.XPath("//*[@id=\"account-profile-section\"]/div/div[1]/a");

        //verify user is login successfully
        public string UserIsInHomePage()
        {
            Thread.Sleep(3000);
            IWebElement MarsLogo = driver.FindElement(marsLogo);
            return MarsLogo.Text;

        }


    }
}
