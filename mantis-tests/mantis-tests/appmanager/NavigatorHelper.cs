using OpenQA.Selenium;
using System.Linq;

namespace mantis_tests
{
    public class NavigatorHelper : HelperBase
    {
        private string baseURL;

        public NavigatorHelper(ApplicationManager manager, string baseURL) : base(manager)
        {
            this.baseURL = baseURL;
        }

        public void GoToManagmentProjectPage()
        {
            if (driver.Url == baseURL + "/manage_proj_page.php")
                return;

            driver.FindElement(By.CssSelector(".nav-list")).FindElements(By.TagName("li")).Last().Click();
            driver.FindElement(By.CssSelector(".nav-tabs")).FindElements(By.TagName("li"))[2].Click();
        }
    }
}
