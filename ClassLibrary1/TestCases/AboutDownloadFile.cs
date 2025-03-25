using NUnit.Framework;
using System;
using System.IO;
using SeleniumPom_v1.Utilities;
using SeleniumPom_v1.Pages;

namespace SeleniumPom_v1.TestCases
{
    public class AboutDownloadFile : BaseTest
    {
        [TestCase("EPAM_Corporate_Overview_Q4FY-2024.pdf")]
        public void ValidateFileDownload(string expectedFileName)
        {
                Logger.LogInfo($"Starting file download test for: {expectedFileName}");

                AboutPage = HomePage.NavigateToAbout();

                //download file
                AboutPage.ScrollToGlanceSection();
                AboutPage.DownloadCorporateOverview();

                //verify download
                var downloadPath = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                    "Downloads",
                    expectedFileName);

                Assert.That(WaitForFileDownload(downloadPath), Is.True,
                    $"File {expectedFileName} was not downloaded successfully");

                Logger.LogInfo("File download test completed successfully");
            }

        private bool WaitForFileDownload(string filePath, int timeoutInSeconds = EXTENDED_TIMEOUT)
        {
            var timeout = TimeSpan.FromSeconds(timeoutInSeconds);
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();

            while (stopwatch.Elapsed < timeout)
            {
                if (File.Exists(filePath))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
