using NUnit.Framework;
using System;
using SeleniumPom_v1.Utilities;
using SeleniumPom_v1.Pages;

namespace SeleniumPom_v1.TestCases
{
    public class InsightsCarouselArticle : BaseTest
    {
        [Test]
        public void ValidateCarouselArticle()
        {

            Logger.LogInfo("Starting carousel article validation test");

            InsightsPage = HomePage.NavigateToInsights();

            InsightsPage.SwipeCarouselNext();

            //get carousel article title
            string carouselTitle = InsightsPage.GetCarouselArticleTitle();

            //click read more button
            var readMoreBtn = InsightsPage.GetReadMoreButton();
            ArticlePage = InsightsPage.ClickReadMoreButton(readMoreBtn);

            //validate article title
            var articleTitle = ArticlePage.GetArticleTitle();

            Assert.That(articleTitle, Is.EqualTo(carouselTitle),
                    $"Article title '{articleTitle}' does not match carousel title '{carouselTitle}'");

            Logger.LogInfo("Carousel article validation completed successfully");
        }
    }
}
