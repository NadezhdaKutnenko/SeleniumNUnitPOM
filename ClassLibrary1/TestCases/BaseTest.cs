using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumPom_v1.Pages;
using SeleniumPom_v1.Utilities;
using System;

namespace SeleniumPom_v1.TestCases
{
    public abstract class BaseTest
    {
        protected const int DEFAULT_TIMEOUT = 10;
        protected const int EXTENDED_TIMEOUT = 30;
        protected const int SHORT_TIMEOUT = 5;

        protected IWebDriver Driver;
        protected HomePage HomePage;
        protected SearchResultsPage SearchResultsPage;
        protected CareersPage CareersPage;
        protected JobDetailsPage JobDetailsPage;
        protected InsightsPage InsightsPage;
        protected ArticlePage ArticlePage;
        protected AboutPage AboutPage;
        protected virtual bool RequiresDownloadConfiguration => false;
        protected virtual bool IsHeadless => false;

        [SetUp]
        public virtual void BaseSetUp()
        {
            try
            {
                Driver = DriverHandler.InitializeDriver(
                    RequiresDownloadConfiguration,
                    IsHeadless);
                InitializePages();
                NavigateToHomePage();
                HomePage.AcceptCookies();
                Logger.LogInfo("Test setup completed successfully");
            }
            catch (Exception ex)
            {
                Logger.LogError($"Test setup failed: {ex.Message}");
                throw;
            }
        }

        private void InitializePages()
        {
            HomePage = new HomePage(Driver);
            SearchResultsPage = new SearchResultsPage(Driver);
            CareersPage = new CareersPage(Driver);
            JobDetailsPage = new JobDetailsPage(Driver);
            InsightsPage = new InsightsPage(Driver);
            ArticlePage = new ArticlePage(Driver);
            AboutPage = new AboutPage(Driver);
        }

        protected void NavigateToHomePage()
        {
            Driver.Navigate().GoToUrl("https://www.epam.com/");
            Driver.WaitUntilPageLoaded();
            Logger.LogInfo("Navigated to EPAM homepage");
        }

        [TearDown]
        public virtual void TearDown()
        {
            DriverHandler.QuitDriver();
        }
    }
}
