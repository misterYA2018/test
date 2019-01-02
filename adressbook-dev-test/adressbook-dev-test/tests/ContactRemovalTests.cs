using NUnit.Framework;
using System.Collections.Generic;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactRemovalTests : AuthTestBase
    {
        [SetUp]
        public void SetUp()
        {
            Assert.IsFalse(app.Contacts.TableIsEmpty(true), "Таблица контактов пустая");
        }

        [Test]
        public void ContactRemovalTest()
        {
            List<ContactData> oldContacts = app.Contacts.GetContactList();
            oldContacts.RemoveAt(0);
            oldContacts.Sort();

            app.Contacts.Remove(0);

            List<ContactData> newContacts = app.Contacts.GetContactList();
            newContacts.Sort();

            Assert.AreEqual(oldContacts, newContacts);
        }
    }
} 