using NUnit.Framework;
using System.Collections.Generic;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupRemovalTests : GroupTestBase
    {
        [SetUp]
        public void SetUp()
        {
            Assert.IsFalse(app.Groups.TableIsEmpty(true), "Таблица с группами пустая");
        }

        [Test]
        public void GroupRemovalTest()
        {
            List<GroupData> oldGroups = GroupData.GetAll();
            var toBeRemove = oldGroups[0];

            app.Groups.Remove(toBeRemove);

            Assert.AreEqual(oldGroups.Count - 1, app.Groups.CountRowsInTable);

            oldGroups.RemoveAt(0);

            List<GroupData> newGroups = GroupData.GetAll();

            Assert.AreEqual(oldGroups, newGroups);

            foreach( var group in newGroups)
            {
                Assert.AreNotEqual(toBeRemove.Id, group.Id);
            }
        }
    }
}