using NUnit.Framework;
using System.Collections.Generic;

namespace addressbook_tests_autoit
{
    [TestFixture]
    public class GroupRemoveTests : TestBase
    {
        [SetUp]
        public void SetUp()
        {
            app.Groups.CreateGroupIfExistLessTwoGroups();
        }

        [Test]
        public void TestGroupRemove()
        {
            List<GroupData> oldGroups = app.Groups.GetGroupList();

            var groupForRemove = oldGroups[0];

            app.Groups.Remove(groupForRemove);

            List<GroupData> newGroups = app.Groups.GetGroupList();

            oldGroups.RemoveAt(0);

            oldGroups.Sort();
            newGroups.Sort();

            Assert.AreEqual(oldGroups, newGroups);
        }
    }
}
