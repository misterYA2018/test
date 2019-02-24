using System;
using OpenQA.Selenium;
using System.Collections.Generic;

namespace mantis_tests
{
    public class APIHelper : HelperBase
    {
        public APIHelper(ApplicationManager manager)
            : base(manager)
        {
        }

        public List<ProjectData> GetProjectList(AccountData account)
        {
            var projects = new List<ProjectData>();

            var client = new Mantis.MantisConnectPortTypeClient();

            var apiProject = client.mc_projects_get_user_accessible(account.Name, account.Password);

            foreach( var project in apiProject)
            {
                projects.Add(new ProjectData { Name = project.name, Id = project.id });
            }

            return projects;
        }

        public void CreateProject(AccountData account, ProjectData projectData)
        {
            var client = new Mantis.MantisConnectPortTypeClient();

            var project = new Mantis.ProjectData
            {
                name = projectData.Name
            };

            client.mc_project_add(account.Name, account.Password, project);
        }

        public void RemoveProject(AccountData account, string projectId)
        {
            var client = new Mantis.MantisConnectPortTypeClient();

            client.mc_project_delete(account.Name, account.Password, projectId);
        }
    }
}
