using NUnit.Framework;
using System.Collections.Generic;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactCreationTests : AuthTestBase
    {
        [Test]
        public void ContactCreationTest()
        {
            var contact = new ContactData("TestName", "TestLastName");

            List<ContactData> oldContacts = app.Contacts.GetContactList();

            app.Contacts.Create(contact);

            Assert.AreEqual(oldContacts.Count + 1, app.Contacts.CountRowsInTable);

            List<ContactData> newContacts = app.Contacts.GetContactList();
            newContacts.Sort();

            oldContacts.Add(contact);
            oldContacts.Sort();

            Assert.AreEqual(oldContacts, newContacts);
        }
    }
} 