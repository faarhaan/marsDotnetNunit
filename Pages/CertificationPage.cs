using mars.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReqnrollProject1.Pages
{
    public class CertificationPage : CommonDriver
    {
        // Locators as private fields
        private readonly By certificationTab = By.XPath("//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/form/div[1]/a[4]");
        private readonly By addNewButton = By.XPath("//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/form/div[5]/div[1]/div[2]/div/table/thead/tr/th[4]/div");
        private readonly By enterCertificationName = By.XPath("//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/form/div[5]/div[1]/div[2]/div/div/div[1]/div/input");
        private readonly By enterCertifiedFrom = By.XPath("//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/form/div[5]/div[1]/div[2]/div/div/div[2]/div[1]/input");
        private readonly By dropDownYear = By.XPath("//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/form/div[5]/div[1]/div[2]/div/div/div[2]/div[2]/select");
        private readonly By addButton = By.XPath("//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/form/div[5]/div[1]/div[2]/div/div/div[3]/input[1]");
        private readonly By lastCertificate = By.XPath("//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/form/div[5]/div[1]/div[2]/div/table/tbody[last()]/tr/td[1]");
        private readonly By deleteIcon = By.XPath("//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/form/div[5]/div[1]/div[2]/div/table/tbody/tr/td[4]/span[2]/i");
        private readonly By totalRows = By.XPath("//*[@id='account-profile-section']/div/section[2]/div/div/div/div[3]/form/div[5]/div[1]/div[2]/div/table/tbody/tr");
        private readonly By updateButton = By.XPath("//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/form/div[5]/div[1]/div[2]/div/table/tbody[2]/tr/td/div/span/input[1]");
                                        
        // Actions as a Methods

        public void InputCertifications(CertificationModel data)
        {
            // Go to the Certificate tab
            driver.FindElement(certificationTab).Click();

            // Click on the Add New Button
            driver.FindElement(addNewButton).Click();

            // Enter Certificate Name
            driver.FindElement(enterCertificationName).Clear();
            driver.FindElement(enterCertificationName).SendKeys(data.Certificate);

            // Enter the input for Certified From
            driver.FindElement(enterCertifiedFrom).Clear();
            driver.FindElement(enterCertifiedFrom).SendKeys(data.From);

            // Select year from dropdown menu
            IWebElement dropDownYear1 = driver.FindElement(dropDownYear);
            var _selectElement = new SelectElement(dropDownYear1);
            _selectElement.SelectByText(data.Year);

            // Press Add button to enter the record in the table
            driver.FindElement(addButton).Click();
            Thread.Sleep(1000);
        }

        public String GetLastCertificate(string Certificate, string From, string Year)
        {
            Thread.Sleep(1000);
            // take code of last certificate and make as a private field name it last certificate and use here
            return driver.FindElement(lastCertificate).Text;
        }

        public void DeleteCertificate2()
        {

            Thread.Sleep(1000);
            // Click on delete button to remove the certificate
            driver.FindElement(deleteIcon).Click();
        }

        public void DeleteCertificate(CertificationModel data)
        {
            driver.FindElement(certificationTab).Click();
            Thread.Sleep(1000);

            var rows = driver.FindElements(totalRows);

            foreach (var row in rows)
            {
                var certificateCell = row.FindElement(By.XPath("td[1]"));
                if (certificateCell.Text.Trim().Equals(data.Certificate, StringComparison.OrdinalIgnoreCase))
                {
                    // Click the delete icon
                    var deleteIcon = row.FindElement(By.XPath("td[4]/span[2]/i"));
                    deleteIcon.Click();
                    Thread.Sleep(1000);
                    break; // Exit the loop after deleting the current row
                }
            }
        }

        public void UpdateCertificate(CertificationModel data)
        {
            driver.FindElement(certificationTab).Click();
            Thread.Sleep(1000);

            var rows = driver.FindElements(totalRows);

            foreach (var row in rows)
            {
                var certificateCell = row.FindElement(By.XPath("td[1]"));
                
                // Find the row to edit using the ORIGINAL certificate name
                if (certificateCell.Text.Trim().Contains(data.OriginalCertificate, StringComparison.OrdinalIgnoreCase))
                {
                    // Click the edit (pencil) icon for this specific row
                    var editIcon = row.FindElement(By.XPath("td[4]/span[1]/i"));
                    editIcon.Click();

                    // Wait for the input fields to become visible
                    var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
                    var certificateInput = wait.Until(drv => drv.FindElement(By.XPath("//input[@value='Update']//ancestor::tr//input[@placeholder='Certificate or Award']")));

                    // Update the fields with the NEW data from the model
                    certificateInput.Clear();
                    certificateInput.SendKeys(data.Certificate); // Send the new certificate name

                    var fromInput = driver.FindElement(By.XPath("//input[@value='Update']//ancestor::tr//input[@placeholder='Certified From (e.g. Adobe)']"));
                    fromInput.Clear();
                    fromInput.SendKeys(data.From); // Send the new 'From' value

                    var yearDropdown = driver.FindElement(By.XPath("//input[@value='Update']//ancestor::tr//select[@class='ui fluid dropdown']"));
                    var selectYear = new SelectElement(yearDropdown);
                    selectYear.SelectByText(data.Year); // Select the new year

                    // Click the update button to save the changes
                    var updateBtn = driver.FindElement(By.XPath("//input[@value='Update']"));
                    updateBtn.Click();
                    Thread.Sleep(1000);

                    // Exit the loop since I've found and updated the correct certificate
                    break; 
                }
            }
        }

        public void UpdateFirstCertificate(CertificationModel data)
        {
            driver.FindElement(certificationTab).Click();
            Thread.Sleep(1000);

            var rows = driver.FindElements(totalRows);

            if (rows.Count > 0)
            {
                // Always update the first row (index 0)
                var firstRow = rows[0];
                
                // Click the edit (pencil) icon on the first row
                var editIcon = firstRow.FindElement(By.XPath("td[4]/span[1]/i"));
                editIcon.Click();

                // Wait for the input field to be visible
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
                wait.Until(drv => drv.FindElement(By.XPath("//td/div/div/div[1]/input")).Displayed);

                // Update the certificate details
                var certificateInput = driver.FindElement(By.XPath("//td/div/div/div[1]/input"));
                certificateInput.Clear();
                certificateInput.SendKeys(data.Certificate);

                var fromInput = driver.FindElement(By.XPath("//td/div/div/div[2]/input"));
                fromInput.Clear();
                fromInput.SendKeys(data.From);

                var yearDropdown = driver.FindElement(By.XPath("//td/div/div/div[3]/select"));
                var selectYear = new SelectElement(yearDropdown);
                selectYear.SelectByText(data.Year);

                // Click the update button
                var updateBtn = driver.FindElement(By.XPath("//td/div/span/input[1]"));
                updateBtn.Click();
                Thread.Sleep(1000);
            }
        }

        public int GetCertificationCount()
        {
            driver.FindElement(certificationTab).Click();
            Thread.Sleep(1000); // Wait for table to load
            return driver.FindElements(totalRows).Count;
        }

        public CertificationModel? GetCertificateDetails(string certificateName)
        {
            driver.FindElement(certificationTab).Click();
            Thread.Sleep(1000);

            var rows = driver.FindElements(totalRows);

            foreach (var row in rows)
            {
                var certificateCell = row.FindElement(By.XPath("td[1]"));
                
                // Instead of an exact match, check if the UI text CONTAINS the expected name to handles UI trimming or formatting issues.
                if (certificateCell.Text.Trim().Contains(certificateName, StringComparison.OrdinalIgnoreCase))
                {
                    var fromCell = row.FindElement(By.XPath("td[2]"));
                    var yearCell = row.FindElement(By.XPath("td[3]"));

                    return new CertificationModel
                    {
                        Certificate = certificateCell.Text.Trim(),
                        From = fromCell.Text.Trim(),
                        Year = yearCell.Text.Trim()
                    };
                }
            }

            return null; // Certificate not found
        }

        public CertificationModel? GetFirstCertificateDetails()
        {
            driver.FindElement(certificationTab).Click();
            Thread.Sleep(1000);

            var rows = driver.FindElements(totalRows);
            if (rows.Count > 0)
            {
                var firstRow = rows[0];
                var certificateCell = firstRow.FindElement(By.XPath("td[1]"));
                var fromCell = firstRow.FindElement(By.XPath("td[2]"));
                var yearCell = firstRow.FindElement(By.XPath("td[3]"));

                return new CertificationModel
                {
                    Certificate = certificateCell.Text.Trim(),
                    From = fromCell.Text.Trim(),
                    Year = yearCell.Text.Trim()
                };
            }
            return null;
        }
    }
}
