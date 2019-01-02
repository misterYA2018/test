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

            app.Groups.Remove(0);

            List<GroupData> newGroups = app.Groups.GetGroupList();
            oldGroups.RemoveAt(0);

            Assert.AreEqual(oldGroups, newGroups);
        }
    }
}