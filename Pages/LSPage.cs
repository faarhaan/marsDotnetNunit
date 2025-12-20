using mars.Utilities;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.DevTools.V135.Audits;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mars.Pages
{
    public class LSPage : CommonDriver
    {
        // Locators as private fields
        private readonly By addNewButton = By.XPath("//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/form/div[2]/div/div[2]/div/table/thead/tr/th[3]/div");
        private readonly By enterLanguageName = By.XPath("//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/form/div[2]/div/div[2]/div/div/div[1]/input");
        private readonly By dropDwnLevel = By.XPath("//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/form/div[2]/div/div[2]/div/div/div[2]/select");
        private readonly By clickOnAddButton = By.XPath("//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/form/div[2]/div/div[2]/div/div/div[3]/input[1]");
        private readonly By lastLanguage = By.XPath("//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/form/div[2]/div/div[2]/div/table/tbody[last()]/tr/td[1]");
        private readonly By deleteIcons = By.XPath("//*[@id='account-profile-section']/div/section[2]/div/div/div/div[3]/form/div[2]/div/div[2]/div/table/tbody/tr/td[3]/span[2]/i");
        private readonly By languageTab = By.XPath("//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/form/div[1]/a[1]");
        private readonly By totalRows = By.XPath("//*[@id='account-profile-section']/div/section[2]/div/div/div/div[3]/form/div[2]/div/div[2]/div/table/tbody/tr");


        // Actions as methods
        public void languagePage( String Languages, String Level)
        {
            // Click on Add new button to add Language
            driver.FindElement(addNewButton).Click();

            //Now enter Languae 
            driver.FindElement(enterLanguageName).SendKeys(Languages);

            // Select Level from dropdown using SelectElement
            //  new SelectElement(driver.FindElement(dropDownLevel)).SelectByText(Level);  OR
            IWebElement dropDownLevel = driver.FindElement(dropDwnLevel);
            var selectElement = new SelectElement(dropDownLevel);
            selectElement.SelectByText(Level);


            // Click on Add button to add 2nd  language in list
            driver.FindElement(clickOnAddButton).Click();
            Thread.Sleep(3000);

        }
        public String GetLastLanguage(String Languages, String Level)
        {
            Thread.Sleep(3000);
            // Verify Languages are created Successfully.
            return driver.FindElement(lastLanguage).Text;
            // driver.FindElement(lastLanguage); return lastLanguage.Text;
            

        }


        public void NotAvailableAddNewButton()
        {
            // Capture the of Add New Button when available 
            driver.FindElement(addNewButton).Click();

        }
        public void DeleteAllLanguages()
        {
             driver.FindElement(languageTab).Click(); 

            // Loop through language rows, click delete for each
            while (true)
            {
                var deleteIconsList = driver.FindElements(deleteIcons);

                if (deleteIconsList.Count == 0)
                    break;
                new WebDriverWait(driver, TimeSpan.FromSeconds(10)).Until(ExpectedConditions.ElementToBeClickable(deleteIconsList[0])).Click();
                /*    deleteIcons[0].Click();
                    Thread.Sleep(1000); // Wait for row to be removed*/

                try
                {
                    // Click the first delete icon
                    deleteIconsList[0].Click();
                    // Wait for the row to be removed
                    Thread.Sleep(1000);
                }
                catch (StaleElementReferenceException)
                {
                    // If a stale element exception occurs, continue the loop to re-find elements
                    continue;
                }
            }
        }  
        
        public void uPdateLanguage(String Languages,String NewLanguage, String NewLevel)
        {
            // Activate Language tab so that languages can be updated
            driver.FindElement(languageTab).Click();


            // Capture all rows of table
            var rows = driver.FindElements(totalRows);
            foreach(var row in rows)                
            {
                // Check if the first cell contains the language to be updated
                var languageCell = row.FindElement(By.XPath("./td[1]"));
                if (languageCell.Text.Trim().Equals(Languages))
                {
                    // Click on the pencil icon to edit the language
                    var editIcon = row.FindElement(By.XPath("./td[3]/span[1]/i"));
                    editIcon.Click();
                    Thread.Sleep(1000);
                    
                    // enter new language name
                    var languageInput = row.FindElement(By.XPath("td/div/div[1]/input"));
                    languageInput.Clear();
                    languageInput.SendKeys(NewLanguage);
                    // Select new level from dropdown
                    var levelDropdown = row.FindElement(By.XPath("td/div/div[2]/select"));
                    var selectElement = new SelectElement(levelDropdown);
                    selectElement.SelectByText(NewLevel);
                    // Click on Update button
                    var updateButton = row.FindElement(By.XPath("td/div/span/input[1]"));                   
                    updateButton.Click();
                    Thread.Sleep(2000); // Wait for update to complete
                    break; // Exit loop after updating the first matching language
                }
            }
        }
    }   
}
