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

            var oldContact = oldContacts[0];

            app.Contacts.Modify(0, newContact);

            Assert.AreEqual(oldContacts.Count, app.Contacts.CountRowsInTable);

            List<ContactData> newContacts = app.Contacts.GetContactList();
            newContacts.Sort();

            oldContacts[0].FirstName = newContact.FirstName;
            oldContacts[0].LastName = newContact.LastName;
            oldContacts.Sort();

            Assert.AreEqual(oldContacts, newContacts);


            foreach (var contact in newContacts)
            {
                if (contact.Id == oldContact.Id)
                {
                    Assert.AreEqual(oldContact.FirstName, contact.FirstName);
                    Assert.AreEqual(oldContact.LastName, contact.LastName);
                }
            }
        }
    }
} 