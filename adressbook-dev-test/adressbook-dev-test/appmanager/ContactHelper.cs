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
            driver.FindElements(By.Name("entry"))[index].FindElements(By.TagName("td"))[0].Click();

            return this;
        }

        public ContactHelper Remove()
        {
            driver.FindElement(By.XPath("//input[@value='Delete']")).Click();
            driver.SwitchTo().Alert().Accept();
            contactCache = null;

            return this;
        }

        public ContactHelper SubmitModification()
        {
            driver.FindElement(By.Name("update")).Click();
            contactCache = null;

            return this;
        }

        public ContactHelper InitContactModification(int index)
        {
            driver.FindElements(By.Name("entry"))[index].FindElements(By.TagName("td"))[7].Click();
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
            contactCache = null;

            return this;
        }

        public int CountRowsInTable
        {
            get
            {
                return driver.FindElements(By.Name("entry")).Count;
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

                var elements = driver.FindElements(By.Name("entry"));

                foreach (IWebElement element in elements)
                {
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
