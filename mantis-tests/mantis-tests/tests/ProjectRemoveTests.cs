using NUnit.Framework;

namespace mantis_tests
{
    [TestFixture]
    public class ProjectRemoveTests : AuthTestBase
    {
        [SetUp]
        public void SetUp()
        {
            app.Navigator.GoToManagmentProjectPage();

            Assert.IsFalse(app.Project.TableIsEmpty(true));
        }

        [Test]
        public void TestProjectRemove()
        {
            var oldProjects = app.Project.GetProjectList();

            app.Project.Remove(oldProjects[0]);

            var actualProjects = app.Project.GetProjectList();
            actualProjects.Sort();

            oldProjects.RemoveAt(0);
            oldProjects.Sort();

            Assert.AreEqual(oldProjects, actualProjects);
        }
    }
}
