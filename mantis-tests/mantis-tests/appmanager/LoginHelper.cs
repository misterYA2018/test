using System;
using OpenQA.Selenium;

namespace mantis_tests
{
    public class LoginHelper : HelperBase
    {
        public LoginHelper(ApplicationManager manager)
            : base(manager)
        {
            this.driver = manager.Driver;
        }

        public void Login(AccountData account)
        {
            if (IsLoggedIn())
            {
                if (IsLoggedIn(account))
                    return;
                Logout();
            }

            Type(By.Id("username"), account.Name);
            driver.FindElement(By.XPath("//input[@type='submit']")).Click();


            Type(By.Id("password"), account.Password);
            driver.FindElement(By.XPath("//input[@type='submit']")).Click();
        }

        public bool IsLoggedIn()
        {
            return IsElementPresent(By.ClassName("user-info"));
        }

        public bool IsLoggedIn(AccountData account)
        {
            return IsLoggedIn() && GetLoggedUserName() == account.Name;
        }

        public string GetLoggedUserName()
        {
            return driver.FindElement(By.ClassName("user-info")).Text;
        }

        public void Logout()
        {
            if (IsLoggedIn())
            {
                driver.FindElement(By.ClassName("user-info")).Click();
                driver.FindElement(By.CssSelector(".user-menu")).FindElements(By.TagName("li"))[3].Click();
            }
        }
    }
}
