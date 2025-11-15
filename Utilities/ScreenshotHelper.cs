using OpenQA.Selenium;
using System;
using System.IO;

public static class ScreenshotHelper
{
    public static string CaptureScreenshot(IWebDriver driver, string screenshotName = "Screenshot")
    {
        // Create a folder for screenshots if it doesn't exist
        string screenshotsDir = Path.Combine(Directory.GetCurrentDirectory(), "Screenshots");
        if (!Directory.Exists(screenshotsDir))
        {
            Directory.CreateDirectory(screenshotsDir);
        }

        // Create a unique filename with timestamp
        string fileName = $"{screenshotName}_{DateTime.Now:yyyyMMdd_HHmmss}.png";
        string filePath = Path.Combine(screenshotsDir, fileName);

        // Take screenshot and save
        Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();
        screenshot.SaveAsFile(filePath);

        return filePath;
    }
}

