using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.IO;

namespace SeleniumPom_v1.Utilities
{
    public static class DriverHandler
    {
        private static IWebDriver _driver;

        public static IWebDriver InitializeDriver(bool configureDownload = false, bool headless = false)
        {
            var options = ConfigureChromeOptions(configureDownload, headless);
            _driver = new ChromeDriver(options);
            return _driver;
        }

        private static ChromeOptions ConfigureChromeOptions(bool configureDownload, bool headless)
        {
            var options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            options.AddArgument("--disable-notifications");

            if (headless)
            {
                options.AddArgument("--headless");
                options.AddArgument("--window-size=1920,1080");
            }

            if (configureDownload)
            {
                var downloadPath = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                    "Downloads");
                options.AddUserProfilePreference("download.default_directory", downloadPath);
            }

            return options;
        }

        public static void QuitDriver()
        {
            if (_driver != null)
            {
                try
                {
                    _driver.Quit();
                    _driver = null;
                }
                catch (Exception ex)
                {
                    Logger.LogError($"Error quitting driver: {ex.Message}");
                }
            }
        }
    }
}
