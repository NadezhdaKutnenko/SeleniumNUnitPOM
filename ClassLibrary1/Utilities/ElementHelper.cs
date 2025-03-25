using OpenQA.Selenium.Interactions;
using OpenQA.Selenium;

namespace SeleniumPom_v1.Utilities
{
    public static class ElementHelper
    {
        public static void Click(this IWebElement element, IWebDriver driver)
        {
            element.Click();
            return;
        }

        public static void ScrollIntoView(this IWebElement element, IWebDriver driver)
        {
            new Actions(driver).ScrollToElement(element).Perform();
        }

        public static bool IsDisplayed(this IWebElement element)
        {
                return element.Displayed;
        }
    }
}
