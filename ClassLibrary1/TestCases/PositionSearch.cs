using NUnit.Framework;
using SeleniumPom_v1.Pages;
using SeleniumPom_v1.Utilities;

namespace SeleniumPom_v1.TestCases
{
    public class PositionSearch : BaseTest
    {
        [TestCase("Java", "All Locations", false)]
        [TestCase("Python", "All Locations", false)]
        [TestCase("Python", "All Locations", true)]
        public void ValidatePositionSearch(string keyword, string location, bool isHeadless)
        {

            Logger.LogInfo($"Starting position search test with keyword: {keyword}, location: {location}");

            CareersPage = HomePage.NavigateToCareers();

            //perform search
            CareersPage.FillSearchCriteria(keyword, location);

            //select last position and validate
            JobDetailsPage = CareersPage.SearchAndSelectLastPosition();

            Assert.That(JobDetailsPage.ValidateJobDescription(keyword), Is.True,
                $"Job description does not contain the keyword: {keyword}");

            Logger.LogInfo("Position search test completed successfully");
        }
    }
}
