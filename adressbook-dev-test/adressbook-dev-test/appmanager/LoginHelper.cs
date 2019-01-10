using System;
using OpenQA.Selenium;

namespace WebAddressbookTests
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

            Type(By.Name("user"), account.Username);
            Type(By.Name("pass"), account.Password);
          
            driver.FindElement(By.XPath("//input[@value='Login']")).Click();
        }

        public bool IsLoggedIn()
        {
            return IsElementPresent(By.Name("logout"));
        }

        public bool IsLoggedIn(AccountData account)
        {
            return IsLoggedIn() && GetLoggedUserName() == account.Username;
        }

        public string GetLoggedUserName()
        {
            var text = driver.FindElement(By.Name("logout")).FindElement(By.TagName("b")).Text;

            return text.Substring(1, text.Length - 2);
        }

        public void Logout()
        {
            if(IsLoggedIn())
                driver.FindElement(By.LinkText("Logout")).Click();
        }
    }
}
