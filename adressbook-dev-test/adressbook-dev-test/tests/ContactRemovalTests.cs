using NUnit.Framework;
using System.Collections.Generic;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactRemovalTests : ContactTestBase
    {
        [SetUp]
        public void SetUp()
        {
            Assert.IsFalse(app.Contacts.TableIsEmpty(true), "Таблица контактов пустая");
        }

        [Test]
        public void ContactRemovalTest()
        {
            List<ContactData> oldContacts = ContactData.GetAll();
            var removeContactId = oldContacts[0].Id;

            app.Contacts.Remove(removeContactId);

            Assert.AreEqual(oldContacts.Count - 1, app.Contacts.CountRowsInTable);

            List<ContactData> newContacts = ContactData.GetAll();
            newContacts.Sort();

            oldContacts.RemoveAt(0);
            oldContacts.Sort();

            Assert.AreEqual(oldContacts, newContacts);

            foreach (var group in newContacts)
            {
                Assert.AreNotEqual(removeContactId, group.Id);
            }
        }
    }
} 