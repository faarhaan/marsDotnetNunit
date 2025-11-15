using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using System.IO;

public static class ExtentReportManager
{
    public static ExtentReports extent;
    public static ExtentTest test;

    public static void InitReport()
    {
        // Use an absolute path for the report directory
        var reportDir = Path.Combine(Directory.GetCurrentDirectory(), "TestResults");
        var reportPath = Path.Combine(reportDir, "ExtentReport.html");

        // Ensure the directory exists
        if (!Directory.Exists(reportDir))
        {
            Directory.CreateDirectory(reportDir);
        }

        var htmlReporter = new ExtentSparkReporter(reportPath);
        extent = new ExtentReports();
        extent.AttachReporter(htmlReporter);
    }

    public static void FlushReport()
    {
        extent.Flush();
    }
}