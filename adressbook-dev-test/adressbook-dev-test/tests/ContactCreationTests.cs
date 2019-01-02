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
            oldContacts.Add(contact);
            oldContacts.Sort();

            app.Contacts.Create(contact);

            List<ContactData> newContacts = app.Contacts.GetContactList();
            newContacts.Sort();

            Assert.AreEqual(oldContacts, newContacts);
        }
    }
} 