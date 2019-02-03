using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactDetailsTests : AuthTestBase
    {
        [Test]
        public void ContactDetailsTest()
        {
            ContactData fromDb = ContactData.GetAll()[0];
            ContactData fromDetails = app.Contacts.GetContactDetails(fromDb.Id);

            Assert.AreEqual(fromDb.Details, fromDetails.Details);
        }
    }
} 