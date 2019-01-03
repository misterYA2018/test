using OpenQA.Selenium;
using System.Collections.Generic;

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
            InitContactModification(index);
            FillContactForm(newContact);
            SubmitModification();

            manager.Navigator.ReturnToHomePage();
            return this;
        }

        public ContactHelper Remove(int index)
        {
            SelectContact(index);
            Remove();

            manager.Navigator.ReturnToHomePage();
            return this;
        }

        public ContactHelper SelectContact(int index)
        {
            index += 2;
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
            // + 1, чтобы индекс начинался с 0
            index += 2;

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

        public bool TableIsEmpty(bool createIfEmpty)
        {
            if (CountRowsInTable < 1 && createIfEmpty)
                    Create(new ContactData("Generated firstName", "Generated lastName"));

            return CountRowsInTable < 1;
        }

        private List<ContactData> contactCache = null;

        public List<ContactData> GetContactList()
        {
            if(contactCache == null)
            {
                contactCache = new List<ContactData>();

                manager.Navigator.GoHomePage();

                var elements = driver.FindElement(By.TagName("tbody"))
                                   .FindElements(By.TagName("tr"));

                var isHeader = true;

                foreach (IWebElement element in elements)
                {
                    if (isHeader)
                    {
                        isHeader = false;
                        continue;
                    }

                    var tds = element.FindElements(By.TagName("td"));
                    contactCache.Add(new ContactData(tds[2].Text, tds[1].Text)
                    {
                        Id = tds[0].FindElement(By.TagName("input")).GetAttribute("id")
                    });
                }
            }
            

            return contactCache;
        }
    }
}
