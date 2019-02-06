using NUnit.Framework;
using System.Linq;

namespace WebAddressbookTests
{
    [TestFixture]
    public class AddingContactToGroupTests : AuthTestBase
    {
        [SetUp]
        public void SetUp()
        {
            Assert.IsFalse(app.Groups.TableIsEmpty(true), "Таблица групп пустая");
        }

        [Test]
        public void TestAddingContactToGroup()
        {
            app.Contacts.CreateContactInDB(new ContactData
            {
                FirstName = "Not in group",
                LastName = "Not in group",
            });

            var group = GroupData.GetAll()[0];

            var oldList = group.GetContacts();

            var contact = ContactData.GetAll().Except(oldList).First();
            app.Contacts.AddContactToGroup(contact, group);

            var newList = group.GetContacts();

            oldList.Add(contact);
            oldList.Sort();
            newList.Sort();

            Assert.AreEqual(oldList, newList);
        }
    }
}
