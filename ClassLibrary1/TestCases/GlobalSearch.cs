using NUnit.Framework;
using SeleniumPom_v1.Utilities;

namespace SeleniumPom_v1.TestCases
{
    public class GlobalSearch : BaseTest
    {
        [TestCase("Automation")]
        [TestCase("Cloud")]
        public void ValidateGlobalSearch(string searchText)
        {
            Logger.LogInfo($"Starting global search test with search term: {searchText}");

            //perform search and get results page
            SearchResultsPage = HomePage.PerformSearch(searchText);

            //validate search results
            var failingLinks = SearchResultsPage.GetFailingLinks(searchText);

            Assert.That(failingLinks, Is.Empty,
                $"{failingLinks.Count} links did not contain the search term: {searchText}");

            Logger.LogInfo("Global search test completed successfully");
        }
    }
}
