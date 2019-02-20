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
            var projectForRemove = app.Project.GetRandomExistingProject();

            var countBefore = app.Project.CountRowsInTable;

            app.Project.Remove(projectForRemove);

            var countAfter = app.Project.CountRowsInTable;

            Assert.AreEqual(countBefore - 1, countAfter);
            Assert.IsFalse(app.Project.Exist(projectForRemove));
        }
    }
}
