using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactModificationTests : AuthTestBase
    {
        [Test]
        public void ContactModificationTest()
        {
            var newContact = new ContactData("TestName New", "TestLastName New");

            app.Contacts.Modify(1, newContact);
        }
    }
} 