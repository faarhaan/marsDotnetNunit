using mars.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.BiDi.Modules.Log;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ReqnrollProject1.Pages
{
    public class SkilPage : CommonDriver
    {
        // Locators as private fields
        private readonly By skillTab = By.XPath("//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/form/div[1]/a[2]");
        private readonly By addNewButton = By.XPath("//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/form/div[3]/div/div[2]/div/table/thead/tr/th[3]/div");
        private readonly By enterFirstSkill = By.XPath("//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/form/div[3]/div/div[2]/div/div/div[1]/input");
        private readonly By levelDropdown = By.XPath("//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/form/div[3]/div/div[2]/div/div/div[2]/select");
        private readonly By addButton = By.XPath("//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/form/div[3]/div/div[2]/div/div/span/input[1]");
        private readonly By lastSkill = By.XPath("//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/form/div[3]/div/div[2]/div/table/tbody[last()]/tr/td[1]");
        private readonly By deleteIcons = By.XPath("//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/form/div[3]/div/div[2]/div/table/tbody/tr/td[3]/span[2]/i");
        private readonly By totalRows = By.XPath("//*[@id='account-profile-section']/div/section[2]/div/div/div/div[3]/form/div[3]/div/div[2]/div/table/tbody/tr");
        private readonly By pad = By.XPath("//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/form/div[3]/div/div[2]/div/div/div[1]");
        
        // POM methods
        public void SkillPage( String Skills, String Level)
        {
            // Go to Skill tab
            driver.FindElement(skillTab).Click();

            // Click on Add New Button
            driver.FindElement(addNewButton).Click();

            // Click on the skill pad to enter the skill
            driver.FindElement(pad);

            //Enter 1st skill 
            driver.FindElement(enterFirstSkill).Clear();
            driver.FindElement(enterFirstSkill).SendKeys(Skills);

            // click on dropdown and select level "Intermediate"
            //new SelectElement(driver.FindElement(levelDropdown).SelectByText(Level);  OR
            IWebElement dropDownLevel1 = driver.FindElement(levelDropdown);
            var selectElement1 = new SelectElement(dropDownLevel1);
            selectElement1.SelectByText(Level);

            //  Click on Add button to add the skill in the list             
            driver.FindElement(addButton).Click();
            Thread.Sleep(2000);
        }


        public String GetLastSkill( String Skills, String Level)
        {
            Thread.Sleep(2000);
            // Verify languages are successfully added
            return driver.FindElement(lastSkill).Text; 
            
            // or driver.FindElement(lastSkill)); return lastSkill.Text
            //Assert.That(lastSkill.Text == "Selenium", "Skills are not added in database! Test is Failed");

        }


        public void DeleteAllSkills()
        {
            /*  // Activate Skill tab so that skills can be deleted
                  driver.FindElement(sKillTab).Click(); */

            // Loop through skill rows, click delete for each
            while (true)
            {
                var deleteIconList2 = driver.FindElements(deleteIcons);
                if (deleteIconList2.Count == 0)
                    break;
                deleteIconList2[0].Click();
                Thread.Sleep(1000); 

            }
        }

        
        public int GetSkillCount()
        {
            var rows = driver.FindElements(totalRows);
            /*  to access the table rows for logic, First I fetch the table frame xpath
                which is till div, then you go to table, then move cursor to trace, then
                 rows.count willl work.*/
            return rows.Count;
        }

        public void UpdateSkill(string oldSkill, string newSkill, string newLevel)
        {
            // Go to Skill tab
            driver.FindElement(skillTab).Click();
            // capture all rows 
            var rows = driver.FindElements(totalRows);
            foreach (var row in rows)
            {
                var skillCell = row.FindElement(By.XPath("td[1]"));    // td[1] is the first cell in the row which contains the skill name
                if (skillCell.Text.Trim().Equals(oldSkill, StringComparison.OrdinalIgnoreCase))
                {
                    // Click the edit (pencil) icon
                    var editIcon = row.FindElement(By.XPath("td[3]/span[1]/i"));  // td[3] is the third cell which contains the edit icon   
                    editIcon.Click();
                    Thread.Sleep(1000);

                    // Update skill name
                    var skillInput = row.FindElement(By.XPath("td/div/div[1]/input"));
                    skillInput.Clear();
                    skillInput.SendKeys(newSkill);

                    // Update level
                    var levelDropdown = row.FindElement(By.XPath("td/div/div[2]/select"));
                    var selectElement = new OpenQA.Selenium.Support.UI.SelectElement(levelDropdown);
                    selectElement.SelectByText(newLevel);

                    // Click update button
                    var updateButton = row.FindElement(By.XPath("td/div/span/input[1]"));
                    updateButton.Click();
                    Thread.Sleep(1000);
                    break;
                }
            }


        }

        
    }

    
}
