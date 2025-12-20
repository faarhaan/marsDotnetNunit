using mars.Pages;
using mars.Utilities;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using ReqnrollProject1.Pages;

namespace nUnitPtoject.TestCases
{
    [TestFixture]
    public class CertificationTestCases : CommonDriver  
    {
        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            // Initialize Extent Report once before all tests
            ExtentReportManager.InitReport();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            // Flush Extent Report once after all tests are completed
            ExtentReportManager.FlushReport();
        }
        [SetUp]
        public void setUp()
        {
            // To Handle leak password Detection
            ChromeOptions options = new ChromeOptions();
            options.AddUserProfilePreference("profile.password_manager_leak_detection", false);

            //  Open Chrome Browser
            driver = new ChromeDriver(options);
            // LoginPage Object initiallization and definition
            LoginPage loginpageObj = new LoginPage();
            loginpageObj.LoginActions(driver);

            // HomePage Object initilization and definition
            HomePage homePageObj = new HomePage();
            String userIsInHomePage = homePageObj.UserIsInHomePage();
            Assert.That(userIsInHomePage == "Mars Logo", "User is not in Home Page! Test is Failed!");
        }

        [TearDown]
        public void TearDown()
        {
            // Close the driver after each test
            driver.Quit();
        }

        [Test, Order(1)]
        public void AddCertificationTest()
        {
            ExtentReportManager.test = ExtentReportManager.extent.CreateTest("Add Certification Test");

            // Load data
            var dataList = JsonDataLoader.LoadData<CertificationModel>("F:\\IndustryConnect\\MVP\\GitRepo\\Newfolder2\\marsDotnetNunit\\nUnitPtoject\\TestData\\certifications.json");
            var certPage = new CertificationPage();

            foreach (var data in dataList)
            {
                try
                {
                    // ***  Add certification  from the certification Page POM****
                    certPage.InputCertifications(data);

                    // Get last certificate text from the UI
                    var lastCertificate = certPage.GetLastCertificate(data.Certificate, data.From, data.Year);

                    // Assert last certificate
                    if (lastCertificate == data.Certificate)
                    {
                        string screenshotPath = ScreenshotHelper.CaptureScreenshot(driver, "TestPass");
                        ExtentReportManager.test.Pass($"Certification '{data.Certificate}' added successfully.");
                    }
                  
                    else
                    {
                        ExtentReportManager.test.Fail($"Certification '{data.Certificate}' not found. Last certificate is '{lastCertificate}'.");
                    }
                }
                catch (Exception ex)
                {
                    // Capture screenshot on error
                    string screenshotPath = ScreenshotHelper.CaptureScreenshot(driver, "TestFailure");
                    ExtentReportManager.test.Fail($"Exception: {ex.Message}. Screenshot saved at: {screenshotPath}");
                }
            }
            
        }

        [Test, Order(2)]
        public void UpdateCertificationTest()
        {
            ExtentReportManager.test = ExtentReportManager.extent.CreateTest("Update Certification Test");

            // Load data with original and new values
            var dataList = JsonDataLoader.LoadData<CertificationModel>("F:\\IndustryConnect\\MVP\\GitRepo\\Newfolder2\\marsDotnetNunit\\nUnitPtoject\\TestData\\certifications-update.json");
            var certPage = new CertificationPage();

            foreach (var data in dataList)
            {
                try
                {
                    // Call the updated method, which finds the certificate by its original name
                    certPage.UpdateCertificate(data);

                    // Wait for UI to refresh
                    Thread.Sleep(2000);

                    // Verify that the new certificate exists and has the correct details
                    var updatedCertificate = certPage.GetCertificateDetails(data.Certificate);
                    
                    if (updatedCertificate != null &&
                        updatedCertificate.Certificate == data.Certificate && 
                        updatedCertificate.From == data.From &&
                        updatedCertificate.Year == data.Year)
                    {
                        string screenshotPath = ScreenshotHelper.CaptureScreenshot(driver, "UpdateSuccess");
                        ExtentReportManager.test.Pass($"Certificate '{data.OriginalCertificate}' updated to '{updatedCertificate.Certificate}' successfully.");
                    }
                    else
                    {
                        string screenshotPath = ScreenshotHelper.CaptureScreenshot(driver, "UpdateFailure");
                        ExtentReportManager.test.Fail($"Update failed for '{data.OriginalCertificate}'. " +
                            $"Expected: Certificate='{data.Certificate}', From='{data.From}'. " +
                            $"Actual: Certificate='{updatedCertificate?.Certificate}', From='{updatedCertificate?.From}'. " +
                            $"Screenshot: {screenshotPath}");
                    }
                }
                catch (Exception ex)
                {
                    // Capture screenshot on error
                    string screenshotPath = ScreenshotHelper.CaptureScreenshot(driver, "TestFailure");
                    ExtentReportManager.test.Fail($"Exception during update of '{data.OriginalCertificate}': {ex.Message}. Screenshot saved at: {screenshotPath}");
                }
            }
        }



        [Test, Order(3)]
        public void DeleteCertificationTest()
        {
            ExtentReportManager.test = ExtentReportManager.extent.CreateTest("Delete Certification Test");

            // Load the data that was used for the update, as these are the current certificate names
            var dataList = JsonDataLoader.LoadData<CertificationModel>("F:\\IndustryConnect\\MVP\\GitRepo\\Newfolder2\\marsDotnetNunit\\nUnitPtoject\\TestData\\certifications-update.json");
            var certPage = new CertificationPage();

            foreach (var data in dataList)
            {
                try
                {
                    // Delete the certificate using its current name
                    certPage.DeleteCertificate(data);

                    // Verify that the certificate is no longer present
                    var deletedCertificate = certPage.GetCertificateDetails(data.Certificate);
                    if (deletedCertificate == null)
                    {
                        ExtentReportManager.test.Pass($"Certification '{data.Certificate}' deleted successfully.");
                    }
                    else
                    {
                        string screenshotPath = ScreenshotHelper.CaptureScreenshot(driver, "DeleteFailure");
                        ExtentReportManager.test.Fail($"Certification '{data.Certificate}' was not deleted. Screenshot: {screenshotPath}");
                    }
                }
                catch (Exception ex)
                {
                    string screenshotPath = ScreenshotHelper.CaptureScreenshot(driver, "TestFailure");
                    ExtentReportManager.test.Fail($"Exception during delete of '{data.Certificate}': {ex.Message}. Screenshot: {screenshotPath}");
                }
            }
        }
        [Test, Order(4)]
        public void AddEducationTest()
        {
            ExtentReportManager.test = ExtentReportManager.extent.CreateTest("Add Education Test");
            
            var dataList = JsonDataLoader.LoadData<EducationModel>("F:\\IndustryConnect\\MVP\\GitRepo\\Newfolder2\\marsDotnetNunit\\nUnitPtoject\\TestData\\education.json");
            var eduPage = new EducationPage();

            foreach (var data in dataList)
            {
                try
                {
                    eduPage.InputEducation(data);
                    var lastUniversity = eduPage.GetLastUniversity(data.University, data.Country, data.Title, data.Degree, data.GraduationYear);

                    if (data.University.Contains(lastUniversity))
                    {
                        string screenshotPath = ScreenshotHelper.CaptureScreenshot(driver, "EducationTestPass");
                        ExtentReportManager.test.Pass($"Education '{data.University}' was added successfully.");
                    }
                    else
                    {
                        ExtentReportManager.test.Fail($"Education '{data.University}' not found. Last university is '{lastUniversity}'.");
                    }
                }
                catch (Exception ex)
                {
                    string screenshotPath = ScreenshotHelper.CaptureScreenshot(driver, "TestFailure");
                    ExtentReportManager.test.Fail($"Exception: {ex.Message}. Screenshot saved at: {screenshotPath}");
                }
            }
        }

        [Test, Order(5)]
        public void UpdateEducationTest()
        {
            ExtentReportManager.test = ExtentReportManager.extent.CreateTest("Update Education Test");

            var dataList = JsonDataLoader.LoadData<EducationModel>("F:\\IndustryConnect\\MVP\\GitRepo\\Newfolder2\\marsDotnetNunit\\nUnitPtoject\\TestData\\education-update.json");
            var eduPage = new EducationPage();

            foreach (var data in dataList)
            {
                try
                {
                    eduPage.UpdateEducation(data);
                    Thread.Sleep(2000);

                    var updatedEducation = eduPage.GetEducationDetails(data.University);
                    if (updatedEducation != null && data.University.Contains(updatedEducation.University))
                    {
                        ExtentReportManager.test.Pass($"Education '{data.OriginalUniversity}' updated to '{data.University}' successfully.");
                    }
                    else
                    {
                        ExtentReportManager.test.Fail($"Update failed for '{data.OriginalUniversity}'.");
                       // Assert.Fail("update is failed");     Toggle off if you want to see fail status in Test Explorer
                    }
                }
                catch (Exception ex)
                {
                    string screenshotPath = ScreenshotHelper.CaptureScreenshot(driver, "TestFailure");
                    ExtentReportManager.test.Fail($"Exception during update of '{data.OriginalUniversity}': {ex.Message}. Screenshot: {screenshotPath}");
                    // Assert.Fail("update is failed");     Toggle off if you want to see fail status in Test Explorer
                }
            }
        }

        [Test, Order(6)]
        public void DeleteEducationTest()
        {
            ExtentReportManager.test = ExtentReportManager.extent.CreateTest("Delete Education Test");

            var dataList = JsonDataLoader.LoadData<EducationModel>("F:\\IndustryConnect\\MVP\\GitRepo\\Newfolder2\\marsDotnetNunit\\nUnitPtoject\\TestData\\education-update.json");
            var eduPage = new EducationPage();

            foreach (var data in dataList)
            {
                try
                {
                    eduPage.DeleteEducation(data);
                    var deletedEducation = eduPage.GetEducationDetails(data.University);
                    if (deletedEducation == null)
                    {
                        ExtentReportManager.test.Pass($"Education '{data.University}' deleted successfully.");
                    }
                    else
                    {
                        ExtentReportManager.test.Fail($"Education '{data.University}' was not deleted.");
                    }
                }
                catch (Exception ex)
                {
                    string screenshotPath = ScreenshotHelper.CaptureScreenshot(driver, "TestFailure");
                    ExtentReportManager.test.Fail($"Exception during delete of '{data.University}': {ex.Message}. Screenshot: {screenshotPath}");
                }
            }
        }
    }
}