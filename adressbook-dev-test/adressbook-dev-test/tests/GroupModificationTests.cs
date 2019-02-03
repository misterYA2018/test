using NUnit.Framework;
using System.Collections.Generic;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupModification : GroupTestBase
    {
        [SetUp]
        public void SetUp()
        {
            Assert.IsFalse(app.Groups.TableIsEmpty(true), "Таблица с группами пустая");
        }

        [Test]
        public void GroupModificationTest()
        {
            var newGroup = new GroupData("Test group new");
            newGroup.Header = "Test header new";
            newGroup.Footer = "Test footer new";

            List<GroupData> oldGroups = GroupData.GetAll();

            var oldGroup = oldGroups[0];

            app.Groups.Modify(oldGroup.Id, newGroup);

            Assert.AreEqual(oldGroups.Count, app.Groups.CountRowsInTable);

            oldGroups[0].Name = newGroup.Name;
            oldGroups.Sort();

            List<GroupData> newGroups = GroupData.GetAll();
            newGroups.Sort();

            Assert.AreEqual(oldGroups, newGroups);

            foreach(var group in newGroups)
            {
                if(group.Id == oldGroup.Id)
                {
                    Assert.AreEqual(oldGroup.Name, group.Name);
                }
            }
        }
    }
}