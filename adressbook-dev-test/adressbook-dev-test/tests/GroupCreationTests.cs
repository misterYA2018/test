using NUnit.Framework;
using System.Collections.Generic;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupCreationTests : AuthTestBase
    {
        [Test]
        public void GroupCreationTest()
        {
            var group = new GroupData("Test group");
            group.Header = "Test header";
            group.Footer = "Test footer";

            List<GroupData> oldGroups = app.Groups.GetGroupList();
            oldGroups.Add(group);
            oldGroups.Sort();

            app.Groups.Create(group);

            List<GroupData> newGroups = app.Groups.GetGroupList();
            newGroups.Sort();

            Assert.AreEqual(oldGroups, newGroups);
        }

        [Test]
        public void EmptyGroupCreationTest()
        {
            var group = new GroupData("");
            group.Header = "";
            group.Footer = "";

            List<GroupData> oldGroups = app.Groups.GetGroupList();
            oldGroups.Add(group);
            oldGroups.Sort();

            app.Groups.Create(group);

            List<GroupData> newGroups = app.Groups.GetGroupList();
            newGroups.Sort();

            Assert.AreEqual(oldGroups, newGroups);
        }

        [Test]
        public void BadNameGroupCreationTest()
        {
            var group = new GroupData("a'a");
            group.Header = "";
            group.Footer = "";

            List<GroupData> oldGroups = app.Groups.GetGroupList();
            oldGroups.Sort();

            app.Groups.Create(group);

            List<GroupData> newGroups = app.Groups.GetGroupList();
            newGroups.Sort();

            Assert.AreEqual(oldGroups, newGroups);
        }
    }
}