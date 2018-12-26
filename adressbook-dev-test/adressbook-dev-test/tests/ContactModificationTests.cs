using NUnit.Framework;

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

            app.Contacts.Modify(1, newContact);
        }
    }
} 