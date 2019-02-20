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
            var countBefore = app.Project.CountRowsInTable;

            app.Project.Create(newProject);

            var countAfter = app.Project.CountRowsInTable;

            Assert.AreEqual(countBefore + 1, countAfter);
            Assert.IsTrue(app.Project.Exist(newProject));
        }
    }
}
