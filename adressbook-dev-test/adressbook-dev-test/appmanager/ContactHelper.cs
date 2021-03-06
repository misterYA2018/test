﻿using OpenQA.Selenium;
using System.Collections.Generic;
using System;
using OpenQA.Selenium.Support.UI;

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

        public ContactData GetContactInformationFromTable(string contactId)
        {
            manager.Navigator.GoToHomePage();

            IList<IWebElement> cells = driver.FindElement(By.Id($"{contactId}")).FindElement(By.XPath("./..")).FindElement(By.XPath("./..")).FindElements(By.TagName("td"));
           
            string lastName = cells[1].Text;
            string firstName = cells[2].Text;
            string address = cells[3].Text;

            string allEmail = cells[4].Text;
            string allPhones = cells[5].Text;

            return new ContactData(firstName, lastName)
            {
                Address = address,
                AllPhone = allPhones,
                AllEmail = allEmail,
            };
        }

        public ContactData GetContactInformationFromEditForm(int index)
        {
            manager.Navigator.GoToHomePage();

            InitContactModification(index);

            string firstName = driver.FindElement(By.Name("firstname")).GetAttribute("value");
            string lastName = driver.FindElement(By.Name("lastname")).GetAttribute("value");
            string address = driver.FindElement(By.Name("address")).GetAttribute("value");

            string homePhone = driver.FindElement(By.Name("home")).GetAttribute("value");
            string mobilePhone = driver.FindElement(By.Name("mobile")).GetAttribute("value");
            string workPhone = driver.FindElement(By.Name("work")).GetAttribute("value");

            string email = driver.FindElement(By.Name("email")).GetAttribute("value");
            string email2 = driver.FindElement(By.Name("email2")).GetAttribute("value");
            string email3 = driver.FindElement(By.Name("email3")).GetAttribute("value");

            return new ContactData(firstName, lastName)
            {
                Address = address,
                HomePhone = homePhone,
                MobilePhone = mobilePhone,
                WorkPhone = workPhone,
                Email = email,
                Email2 = email2,
                Email3 = email3,
            };
        }

        public ContactData GetContactDetails(string contactId)
        {
            manager.Navigator.GoToHomePage();

            OpenContactDetails(contactId);

            var details = driver.FindElement(By.Id("content")).Text;

            return new ContactData()
            {
                Details = details.Replace("\r\n", ""),
            };
        }

        public void OpenContactDetails(string contactId)
        {
            driver.FindElement(By.XPath($"//a[@href='view.php?id={contactId}']")).Click();
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

        public ContactHelper Modify(string oldContactId, ContactData newContact)
        {
            InitContactModification(oldContactId);
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

        public ContactHelper Remove(string contactId)
        {
            SelectContact(contactId);
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

        public ContactHelper InitContactModification(string contactId)
        {
            driver.FindElement(By.XPath($"//a[@href='edit.php?id={contactId}']")).Click();
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

                manager.Navigator.GoToHomePage();

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

        public int GetNumberOfSearchResults()
        {
            manager.Navigator.GoToHomePage();

            return int.Parse(driver.FindElement(By.Id("search_count")).Text);
        }


        public void AddContactToGroup(ContactData contact, GroupData group)
        {
            manager.Navigator.GoToHomePage();
            ClearGroupFilter();
            SelectContact(contact.Id);
            SelectGroupToAdd(group.Name);
            CommitAddingContactToGroup();

            new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                .Until(d => d.FindElements(By.CssSelector("div.msgbox")).Count > 0);
        }

        public void CommitAddingContactToGroup()
        {
            driver.FindElement(By.Name("add")).Click();
        }

        public void SelectGroupToAdd(string name)
        {
            new SelectElement(driver.FindElement(By.Name("to_group"))).SelectByText(name);
        }

        public void SelectContact(string id)
        {
            driver.FindElement(By.Id(id)).Click();
        }

        public void ClearGroupFilter()
        {
            GroupFilter("[all]");
        }

        public void RemoveContactFromGroup(ContactData contact, GroupData group)
        {
            manager.Navigator.GoToHomePage();
            GroupFilter(group.Name);
            SelectContact(contact.Id);
            DeleteContactFromGroup();

            new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                .Until(d => d.FindElements(By.CssSelector("div.msgbox")).Count > 0);
        }

        public void GroupFilter(string name)
        {
            new SelectElement(driver.FindElement(By.Name("group"))).SelectByText(name);
        }

        public void DeleteContactFromGroup()
        {
            driver.FindElement(By.Name("remove")).Click();
        }

        public void CreateContactInDB(ContactData contact)
        {
            ExecuteCmd($"insert into addressbook set firstname= '{contact.FirstName}', lastname = '{contact.LastName}'");
        }
    }
}
