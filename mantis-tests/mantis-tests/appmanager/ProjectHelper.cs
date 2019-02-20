using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Collections.ObjectModel;

namespace mantis_tests
{
    public class ProjectHelper : HelperBase
    {
        public ProjectHelper(ApplicationManager manager) : base(manager)
        {
        }

        public void Create(ProjectData project)
        {
            ClickCreate();
            FillCreationForm(project);
            Submit();

            new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                .Until(d => d.FindElements(By.TagName("tbody")).Count > 0);
        }

        public void Remove(ProjectData projectForRemove)
        {
            OpenProject(projectForRemove);
            Remove();
            Submit();
        }

        public void Remove()
        {
            driver.FindElement(By.XPath("//input[@value='Удалить проект']")).Click();
        }

        public void OpenProject(ProjectData project)
        {
            var rows = GetTableRows();

            foreach (var row in rows)
            {
                var name = row.FindElements(By.TagName("a"))[0];
                if (project.Name.Equals(name.Text))
                {
                    name.Click();
                    return;
                }
            }
        }

        public ProjectData GetRandomExistingProject()
        {
            var rows = GetTableRows();

            Random rnd = new Random();

            var name = rows[rnd.Next(0, rows.Count)].FindElements(By.TagName("a"))[0].Text;

            return new ProjectData(name);
        }

        private void Submit()
        {
            driver.FindElement(By.XPath("//input[@type='submit']")).Click();
        }

        private void FillCreationForm(ProjectData project)
        {
            Type(By.Id("project-name"), project.Name);
        }

        private void ClickCreate()
        {
            driver.FindElements(By.XPath("//button[@type='submit']"))[0].Click();
        }

        public bool Exist(ProjectData project)
        {
            var rows = GetTableRows();

            foreach ( var row in rows)
            {
                if(project.Name.Equals(row.FindElements(By.TagName("a"))[0].Text))
                    return true;
            }

            return false;
        }

        public int CountRowsInTable
        {
            get
            {
                return GetTableRows().Count;
            }
        }

        public bool TableIsEmpty(bool createIfEmpty)
        {
            if (CountRowsInTable < 1 && createIfEmpty)
                Create(new ProjectData("Generated project"));

            return CountRowsInTable < 1;
        }

        private ReadOnlyCollection<IWebElement> GetTableRows()
        {
            return driver.FindElement(By.TagName("tbody")).FindElements(By.TagName("tr"));
        }
    }
}
