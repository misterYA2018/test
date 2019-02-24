using NUnit.Framework;
using System.Collections.Generic;

namespace mantis_tests
{
    [TestFixture]
    public class ProjectRemoveTests : AuthTestBase
    {
        List<ProjectData> projectBefore;

        [SetUp]
        public void SetUp()
        {
            Assert.IsFalse(app.Project.TableIsEmpty(true, Administrator));
            projectBefore = app.API.GetProjectList(Administrator);

            app.Navigator.GoToManagmentProjectPage();
        }

        [Test]
        public void TestProjectRemove()
        {
            app.Project.Remove(projectBefore[0]);

            var actualProjects = app.API.GetProjectList(Administrator);
            actualProjects.Sort();

            projectBefore.RemoveAt(0);
            projectBefore.Sort();

            Assert.AreEqual(projectBefore, actualProjects);
        }
    }
}
