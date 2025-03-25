using System;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumPom_v1.Utilities;
using NUnit.Framework;

namespace SeleniumPom_v1.Pages
{
    public class CareersPage : BasePage
    {
        private readonly By _keywordInput = By.Id("new_form_job_search-keyword");
        private readonly By _locationDropdown = By.CssSelector("span[aria-labelledby='select2-new_form_job_search-location-container']");
        private readonly By _allLocationsOption = By.XPath("//li[contains(text(), 'All Locations')]");
        private readonly By _remoteCheckbox = By.CssSelector("input[name='remote']");
        private readonly By _findButton = By.CssSelector("button[class*='job-search-button']");
        private readonly By _searchResults = By.CssSelector(".search-result__item");
        private readonly By _applyButton = By.XPath(".//a[contains(@class, 'search-result__item-apply')]");
        private readonly WebDriverWait wait;

        public CareersPage(IWebDriver driver) : base(driver)
        {
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(DEFAULT_TIMEOUT));
        }

        public void FillSearchCriteria(string keyword, string location)
        {
            EnterKeyword(keyword);
            SelectLocation(location);
            CheckRemoteOption();
            
            Logger.LogInfo($"Filled search criteria - Keyword: {keyword}, Location: {location}");
        }

        private void EnterKeyword(string keyword)
        {
            var keywordField = WaitAndFindElement(_keywordInput);
            keywordField.Clear();
            keywordField.SendKeys(keyword);
            
            Assert.That(keywordField.GetAttribute("value"), Is.EqualTo(keyword), "Keyword field value is not as expected");
            Logger.LogInfo($"Entered keyword: {keyword}");
        }

        private void SelectLocation(string location)
        {
            var locationDropdownElement = WaitAndFindClickableElement(_locationDropdown);
            ClickElement(locationDropdownElement);

            var locationOption = WaitAndFindClickableElement(_allLocationsOption);
            ClickElement(locationOption);
            
            Assert.That(locationDropdownElement.Text, Is.EqualTo(location), "Location dropdown value is not as expected");
            Logger.LogInfo($"Selected location: {location}");
        }

        private void CheckRemoteOption()
        {
            var remote = wait.Until(driver => driver.FindElement(_remoteCheckbox));
            ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].click();", remote);
            
            Assert.That(remote.Selected, Is.True, "Remote checkbox is not checked");
            Logger.LogInfo("Checked Remote checkbox");
        }

        public JobDetailsPage SearchAndSelectLastPosition()
        {
            ClickFindButton();
            var lastResult = GetLastSearchResult();
            ClickViewAndApply(lastResult);
            return new JobDetailsPage(Driver);
        }

        private void ClickFindButton()
        {
            var findButtonElement = WaitAndFindClickableElement(_findButton);
            ClickElement(findButtonElement);
            WaitForPageLoad();
            
            Logger.LogInfo("Clicked Find button");
        }

        private IWebElement GetLastSearchResult()
        {
            var results = WaitAndFindElements(_searchResults);
            if (!results.Any())
            {
                throw new Exception("No search results found");
            }
            Logger.LogInfo($"Found {results.Count()} search results");
            return results.Last();
        }

        private void ClickViewAndApply(IWebElement lastResult)
        {
            var applyButtonElement = lastResult.FindElement(_applyButton);
            ScrollToElement(applyButtonElement);
            ClickElement(applyButtonElement);
            WaitForPageLoad();
            
            Logger.LogInfo("Clicked View and Apply button");
        }
    }
}
