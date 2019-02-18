using OpenQA.Selenium;
using System.Text.RegularExpressions;

namespace mantis_tests
{
    public class RegistrationHelper : HelperBase
    {
        public RegistrationHelper(ApplicationManager manager) : base(manager) { }

        public void Register(AccountData account)
        {
            OpenMainPage();
            OpenRegistrationForm();
            FillRegistrationForm(account);
            SubmitRegistration();

            var url = GetConfirmationUrl(account);

            FillPasswordForm(url, account);
            SubmitPasswordForm();
        }

        public void SubmitPasswordForm()
        {
            driver.FindElement(By.XPath("//input[@type='submit']")).Click();
        }

        public void FillPasswordForm(string url, AccountData account)
        {
            driver.Url = url;
            driver.FindElement(By.Name("password")).SendKeys(account.Password);
            driver.FindElement(By.Name("password_xonfi")).SendKeys(account.Password);
        }

        public string GetConfirmationUrl(AccountData account)
        {
            var message = manager.Mail.GetLastMail(account);

            var match = Regex.Match(message, @"http://\S*");

            return match.Value;
        }

        public void SubmitRegistration()
        {
            driver.FindElement(By.XPath("//input[@type='submit']")).Click();
        }

        public void FillRegistrationForm(AccountData account)
        {
            driver.FindElement(By.Name("username")).SendKeys(account.Name);
            driver.FindElement(By.Name("email")).SendKeys(account.Email);
        }

        public void OpenMainPage()
        {
            driver.Url = "http://localhost/mantisbt-2.19.0/login_page.php";
        }


        public void OpenRegistrationForm()
        {
            driver.FindElement(By.CssSelector(".back-to-login-link")).Click();
        }

    }
}
