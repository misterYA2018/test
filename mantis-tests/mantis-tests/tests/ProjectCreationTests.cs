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

            Assert.IsFalse(app.Project.Exist(newProject), "Проект с таким именем уже существует");

            var oldProjects = app.Project.GetProjectList();
            oldProjects.Add(newProject);
            oldProjects.Sort();

            app.Project.Create(newProject);

            var actualProjects = app.Project.GetProjectList();
            actualProjects.Sort();

            Assert.AreEqual(oldProjects, actualProjects);
        }
    }
}
