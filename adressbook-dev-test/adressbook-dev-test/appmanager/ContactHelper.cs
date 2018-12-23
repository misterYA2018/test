using OpenQA.Selenium;

namespace WebAddressbookTests
{
    public class ContactHelper : HelperBase
    {
        private ApplicationManager manager;

        public ContactHelper(ApplicationManager manager)
            : base(manager)
        {
            this.manager = manager;
        }

        public ContactHelper Create(ContactData contact)
        {
            InitContactCreation();
            FillContactForm(contact);
            SubmitCreation();

            manager.Navigator.ReturnToHomePage();
            return this;
        }

        public ContactHelper Modify(int index, ContactData newContact)
        {
            if (CountRowsInTable < 1)
                Create(new ContactData("Generated firstName", "Generated lastName"));

            InitContactModification(index);
            FillContactForm(newContact);
            SubmitModification();

            manager.Navigator.ReturnToHomePage();
            return this;
        }

        public ContactHelper Remove(int index)
        {
            if (CountRowsInTable < 1)
                Create(new ContactData("Generated firstName", "Generated lastName"));

            SelectContact(index);
            Remove();

            manager.Navigator.ReturnToHomePage();
            return this;
        }

        public ContactHelper SelectContact(int index)
        {
            index += 1;
            driver.FindElement(By.XPath($"//table/tbody/tr[{index}]/td[1]")).Click();

            return this;
        }

        public ContactHelper Remove()
        {
            driver.FindElement(By.XPath("//input[@value='Delete']")).Click();
            driver.SwitchTo().Alert().Accept();

            return this;
        }

        public ContactHelper SubmitModification()
        {
            driver.FindElement(By.Name("update")).Click();

            return this;
        }

        public ContactHelper InitContactModification(int index)
        {
            // + 1, так как первая строка - шапка таблицы
            index += 1;

            driver.FindElement(By.XPath($"//table/tbody/tr[{index}]/td[8]")).Click();

            return this;
        }

        public ContactHelper FillContactForm(ContactData contact)
        {
            Type(By.Name("firstname"), contact.FirstName);
            Type(By.Name("lastname"), contact.LastName);

            return this;
        }

        public ContactHelper InitContactCreation()
        {
            driver.FindElement(By.LinkText("add new")).Click();

            return this;
        }

        public ContactHelper SubmitCreation()
        {
            driver.FindElement(By.Name("submit")).Click();

            return this;
        }

        public int CountRowsInTable
        {
            get
            {
                // -1, так как первая строка это шапка 
                return driver.FindElement(By.TagName("tbody"))
                               .FindElements(By.TagName("tr")).Count - 1;
            }
        }
    }
}
