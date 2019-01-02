﻿using NUnit.Framework;
using System.Collections.Generic;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupModification : AuthTestBase
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

            List<GroupData> oldGroups = app.Groups.GetGroupList();
            oldGroups[0].Name = newGroup.Name;
            oldGroups.Sort();

            app.Groups.Modify(0, newGroup);

            List<GroupData> newGroups = app.Groups.GetGroupList();
            newGroups.Sort();

            Assert.AreEqual(oldGroups, newGroups);
        }
    }
}