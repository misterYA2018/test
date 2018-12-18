using OpenQA.Selenium;

namespace WebAddressbookTests
{
    public class HelperBase
    {
        protected IWebDriver driver;
        private ApplicationManager manager;

        public HelperBase(ApplicationManager manager)
        {
            this.driver = manager.Driver;
            this.manager = manager;
        }
    }
}