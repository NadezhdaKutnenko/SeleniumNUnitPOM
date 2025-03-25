using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using SeleniumPom_v1.Utilities;

namespace SeleniumPom_v1.Pages
{
    public class SearchResultsPage : BasePage
    {
        private readonly By _searchResults = By.CssSelector(".search-results__item");
        private readonly By _resultTitleLink = By.CssSelector("a.search-results__title-link");

        public SearchResultsPage(IWebDriver driver) : base(driver)
        {
        }

        public IReadOnlyCollection<IWebElement> GetSearchResults()
        {
            var results = WaitAndFindElements(_searchResults);

            if (!results.Any())
            {
                throw new Exception("No search results found");
            }

            Logger.LogInfo($"Found {results.Count} search results");
            return results;
        }

        public List<string> GetFailingLinks(string searchText)
        {
            var results = GetSearchResults();

            var failingLinks = results
                .Where(result => !result.Text.Contains(searchText))
                .Select(result => result.FindElement(_resultTitleLink).Text)
                .ToList();

            if (failingLinks.Any())
            {
                foreach (var link in failingLinks)
                {
                    Logger.LogError($"Failed link: {link}");
                }
            }

            return failingLinks;
        }
    }
}
