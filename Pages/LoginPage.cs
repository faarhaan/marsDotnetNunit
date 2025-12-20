using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mars.Utilities;
using OpenQA.Selenium;
using ReqnrollProject1.Utilities;

namespace mars.Pages
{
    public class LoginPage : CommonDriver
    {
        // Locators as private fields
        private readonly By signInButton = By.XPath("//*[@id=\"home\"]/div/div/div[1]/div/a");
        private readonly By enterUserName = By.XPath("/html/body/div[2]/div/div/div[1]/div/div[1]/input");
        private readonly By enterPassword = By.XPath("/html/body/div[2]/div/div/div[1]/div/div[2]/input");
        private readonly By clickOnLogin = By.XPath("/html/body/div[2]/div/div/div[1]/div/div[4]/button");

        // Below method performs the login actions to open the portal mar
        public void LoginActions(IWebDriver driver)
        {
            driver.Navigate().GoToUrl(" http://localhost:5003");
            Thread.Sleep(3000);
           // Maximiize the window
            driver.Manage().Window.Maximize();
            Thread.Sleep(3000);
            // Identify Signin Button and click on it
            driver.FindElement(signInButton).Click();                                   
            Thread.Sleep(2000);                                    

            // Identify User name Text box and enter user
            driver.FindElement(enterUserName).SendKeys("faarhaan786@gmail.com");
          

            // Identify password box and enter passwerd
            driver.FindElement(enterPassword).SendKeys("Pakistan_123");


            // Identify Login Button and click on it 
            driver.FindElement(clickOnLogin).Click();                                  
            Thread.Sleep(3000);

            // Maximize the browser window
            driver.Manage().Window.Maximize();


        }
    }
}
