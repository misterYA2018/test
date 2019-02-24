using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using System.Threading;

namespace mantis_tests
{
    public class ApplicationManager
    {
        private IWebDriver driver;
        protected static string baseURL;

        private static ThreadLocal<ApplicationManager> app = new ThreadLocal<ApplicationManager>();

        private ApplicationManager()
        {
            driver = new FirefoxDriver();
            baseURL = "http://localhost/mantisbt-2.19.0";

            Registration = new RegistrationHelper(this);
            Ftp = new FtpHelper(this);
            James = new JamesHelper(this);
            Mail = new MailHelper(this);
            Auth = new LoginHelper(this);
            Navigator = new NavigatorHelper(this, baseURL);
            Project = new ProjectHelper(this);

            API = new APIHelper(this);
        }

        ~ApplicationManager()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
        }

        public static ApplicationManager GetInstance()
        {
            if (!app.IsValueCreated)
            {
                var newInstance = new ApplicationManager();
                newInstance.driver.Url = baseURL + "/login_page.php";

                app.Value = newInstance;
            }

            return app.Value;
        }

        public IWebDriver Driver
        {
            get { return driver; }
        }

        public RegistrationHelper Registration { get; private set; }

        public FtpHelper Ftp { get; private set; }
        public JamesHelper James { get; private set; }
        public MailHelper Mail { get; private set; }
        public LoginHelper Auth { get; private set; }
        public NavigatorHelper Navigator { get; private set; }
        public ProjectHelper Project { get; private set; }
        public APIHelper API { get; private set; }
    }
}
