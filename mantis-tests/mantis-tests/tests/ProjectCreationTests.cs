using NUnit.Framework;
using System.IO;

namespace mantis_tests
{
    [TestFixture]
    public class ProjectCreationTests : AuthTestBase
    {
        [SetUp]
        public void SetUp()
        {
            app.Navigator.GoToManagmentProjectPage();
        }

        [Test]
        public void TestProjectCreation()
        {
            var newProject = new ProjectData(GenerateRandomString(8));

            app.Project.RemoveIfExist(Administrator, newProject);

            var oldProjects = app.API.GetProjectList(Administrator);
            oldProjects.Add(newProject);
            oldProjects.Sort();

            app.Project.Create(newProject);

            var actualProjects = app.API.GetProjectList(Administrator);
            actualProjects.Sort();

            Assert.AreEqual(oldProjects, actualProjects);
        }
    }
}
