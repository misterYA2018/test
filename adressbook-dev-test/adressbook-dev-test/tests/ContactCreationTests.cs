using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactCreationTests : AuthTestBase
    {
        [Test]
        public void ContactCreationTest()
        {
            var contact = new ContactData("TestName", "TestLastName");

            app.Contacts.Create(contact);
        }
    }
} 