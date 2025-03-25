using System.Collections.Generic;
using OpenQA.Selenium;
using SeleniumPom_v1.Utilities;

namespace SeleniumPom_v1.Pages
{
    public abstract class BasePage
    {
        protected readonly IWebDriver Driver;
        protected const int DEFAULT_TIMEOUT = 10;
        protected const int EXTENDED_TIMEOUT = 30;
        protected const int SHORT_TIMEOUT = 5;

        protected BasePage(IWebDriver driver)
        {
            Driver = driver;
        }

        protected IWebElement WaitAndFindElement(By locator, int timeoutInSeconds = DEFAULT_TIMEOUT)
        {
            return Driver.WaitUntilElementVisible(locator, timeoutInSeconds);
        }

        protected IWebElement WaitAndFindClickableElement(By locator, int timeoutInSeconds = DEFAULT_TIMEOUT)
        {
            return Driver.WaitUntilElementClickable(locator, timeoutInSeconds);
        }

        protected IReadOnlyCollection<IWebElement> WaitAndFindElements(By locator, int timeoutInSeconds = DEFAULT_TIMEOUT)
        {
            return Driver.WaitUntilElementsPresent(locator, timeoutInSeconds);
        }

        protected void WaitForPageLoad()
        {
            Driver.WaitUntilPageLoaded();
        }

        protected void ClickElement(IWebElement element)
        {
            element.Click(Driver);
        }

        protected void ScrollToElement(IWebElement element)
        {
            element.ScrollIntoView(Driver);
        }
    }
}
