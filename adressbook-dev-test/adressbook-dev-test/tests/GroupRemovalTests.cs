using NUnit.Framework;
using System.Collections.Generic;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupRemovalTests : AuthTestBase
    {
        [SetUp]
        public void SetUp()
        {
            Assert.IsFalse(app.Groups.TableIsEmpty(true), "Таблица с группами пустая");
        }

        [Test]
        public void GroupRemovalTest()
        {
            List<GroupData> oldGroups = app.Groups.GetGroupList();
            var removeGroupId = oldGroups[0].Id;

            app.Groups.Remove(0);

            Assert.AreEqual(oldGroups.Count - 1, app.Groups.CountRowsInTable);

            oldGroups.RemoveAt(0);

            List<GroupData> newGroups = app.Groups.GetGroupList();

            Assert.AreEqual(oldGroups, newGroups);

            foreach( var group in newGroups)
            {
                Assert.AreNotEqual(removeGroupId, group.Id);
            }
        }
    }
}