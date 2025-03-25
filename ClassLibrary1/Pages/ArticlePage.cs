using OpenQA.Selenium;
using SeleniumPom_v1.Utilities;

namespace SeleniumPom_v1.Pages
{
    public class ArticlePage : BasePage
    {
        private readonly By _articleTitle = By.CssSelector("div.text-ui-23 span.museo-sans-light");

        public ArticlePage(IWebDriver driver) : base(driver)
        {
        }

        public string GetArticleTitle()
        {
                WaitForPageLoad();
                var titleElement = WaitAndFindElement(_articleTitle);
                var title = titleElement.Text.Trim();
                Logger.LogInfo($"Found article title: {title}");
                return title;
        }
    }
}
