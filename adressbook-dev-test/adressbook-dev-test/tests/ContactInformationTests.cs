using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactInformationTests : AuthTestBase
    {
        [Test]
        public void ContactInformationTest()
        {
            ContactData fromDb = ContactData.GetAll()[0];
            ContactData fromTable = app.Contacts.GetContactInformationFromTable(fromDb.Id);

            Assert.AreEqual(fromDb, fromTable);

            Assert.AreEqual(fromDb.Address ?? "", fromTable.Address);
            Assert.AreEqual(fromDb.AllPhone ?? "", fromTable.AllPhone);
            Assert.AreEqual(fromDb.AllEmail ?? "", fromTable.AllEmail);
        }
    }
} 