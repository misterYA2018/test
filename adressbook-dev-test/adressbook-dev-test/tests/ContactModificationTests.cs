using NUnit.Framework;
using System.Collections.Generic;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactModificationTests : AuthTestBase
    {
        [SetUp]
        public void SetUp()
        {
            Assert.IsFalse(app.Contacts.TableIsEmpty(true), "Таблица контактов пустая");
        }

        [Test]
        public void ContactModificationTest()
        {
            var newContact = new ContactData("TestName New", "TestLastName New");


            List<ContactData> oldContacts = app.Contacts.GetContactList();
            oldContacts[0].FirstName = newContact.FirstName;
            oldContacts[0].LastName = newContact.LastName;
            oldContacts.Sort();

            app.Contacts.Modify(0, newContact);

            List<ContactData> newContacts = app.Contacts.GetContactList();
            newContacts.Sort();

            Assert.AreEqual(oldContacts, newContacts);
        }
    }
} 