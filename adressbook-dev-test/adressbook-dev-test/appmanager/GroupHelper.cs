using OpenQA.Selenium;
using System.Collections.Generic;

namespace WebAddressbookTests
{
    public class GroupHelper : HelperBase
    {
        private ApplicationManager manager;

        public GroupHelper(ApplicationManager manager)
            : base(manager)
        {
            this.manager = manager;
        }

        public GroupHelper Create(GroupData group)
        {
            manager.Navigator.GoToGroupsPage();

            InitGroupCreation();
            FillGroupForm(group);
            SubmitCreation();
            ReturnToGroupsPage();

            return this;
        }

        public GroupHelper Modify(int index, GroupData newGroup)
        {
            manager.Navigator.GoToGroupsPage();

            SelectGroup(index);
            InitGroupModication();
            FillGroupForm(newGroup);
            SubmitModification();
            ReturnToGroupsPage();

            return this;
        }

        public GroupHelper Modify(string oldGroupId, GroupData newGroup)
        {
            manager.Navigator.GoToGroupsPage();

            SelectGroup(oldGroupId);
            InitGroupModication();
            FillGroupForm(newGroup);
            SubmitModification();
            ReturnToGroupsPage();

            return this;
        }

        public GroupHelper Remove(int index)
        {
            manager.Navigator.GoToGroupsPage();

            SelectGroup(index);
            RemoveGroup();
            ReturnToGroupsPage();

            return this;
        }

        public GroupHelper Remove(GroupData toBeRemove)
        {
            manager.Navigator.GoToGroupsPage();

            SelectGroup(toBeRemove.Id);
            RemoveGroup();
            ReturnToGroupsPage();

            return this;
        }

        public GroupHelper RemoveGroup()
        {
            driver.FindElement(By.Name("delete")).Click();
            groupCache = null;

            return this;
        }

        public GroupHelper SubmitModification()
        {
            driver.FindElement(By.Name("update")).Click();
            groupCache = null;

            return this;
        }

        public GroupHelper InitGroupModication()
        {
            driver.FindElement(By.Name("edit")).Click();

            return this;
        }

        public GroupHelper SelectGroup(int index)
        {
            index++;

            driver.FindElement(By.XPath("(//input[@name='selected[]'])[" + index + "]")).Click();
            return this;
        }

        public GroupHelper SelectGroup(string id)
        {
            driver.FindElement(By.XPath($"(//input[@name='selected[]' and @value='{id}'])")).Click();
            return this;
        }

        public GroupHelper ReturnToGroupsPage()
        {
            driver.FindElement(By.LinkText("group page")).Click();

            return this;
        }

        public GroupHelper FillGroupForm(GroupData group)
        {
            Type(By.Name("group_name"), group.Name);
            Type(By.Name("group_header"), group.Header);
            Type(By.Name("group_footer"), group.Footer);

            return this;
        }

        public GroupHelper InitGroupCreation()
        {
            driver.FindElement(By.Name("new")).Click();

            return this;
        }

        public GroupHelper SubmitCreation()
        {
            driver.FindElement(By.Name("submit")).Click();
            groupCache = null;

            return this;
        }


        public int CountRowsInTable
        {
            get
            {
                return driver.FindElements(By.ClassName("group")).Count;
            }
        }

        public bool TableIsEmpty(bool createIfEmpty)
        {
            if (CountRowsInTable < 1 && createIfEmpty)
                Create(new GroupData("Generated firstName"));

            return CountRowsInTable < 1;
        }

        private List<GroupData> groupCache = null;

        public List<GroupData> GetGroupList()
        {
            manager.Navigator.GoToGroupsPage();

            if (groupCache == null)
            {
                groupCache = new List<GroupData>();

                var elements = driver.FindElements(By.CssSelector("span.group"));

                foreach (IWebElement element in elements)
                {

                    groupCache.Add(new GroupData(element.Text)
                    {
                        Id = element.FindElement(By.TagName("input")).GetAttribute("value")
                    });
                }
            }

            return new List<GroupData>(groupCache);
        }
    }
}
