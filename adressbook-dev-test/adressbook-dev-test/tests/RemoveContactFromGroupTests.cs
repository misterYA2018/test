using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class RemoveContactFromGroupTests : AuthTestBase
    {
        [SetUp]
        public void SetUp()
        {
            Assert.IsFalse(app.Groups.TableIsEmpty(true), "Таблица групп пустая");
            Assert.IsFalse(app.Contacts.TableIsEmpty(true), "Таблица контактов пустая");
        }

        [Test]
        public void TestRemoveContactFromGroupTests()
        {
            var group = GroupData.GetAll()[0];
            var oldList = group.GetContacts();
            ContactData contact;

            if (oldList.Count == 0)
            {
                contact = ContactData.GetAll()[0];

                app.Relation.AddGroupContactRelationInDB(new GroupContactRelation
                {
                    GroupID = group.Id,
                    ContactID = contact.Id
                });

                oldList.Add(contact);
            }
            else
            {
                contact = oldList[0];
            }

            app.Contacts.RemoveContactFromGroup(contact, group);

            var newList = group.GetContacts();

            oldList.RemoveAt(0);
            oldList.Sort();
            newList.Sort();

            Assert.AreEqual(oldList, newList);
        }
    }
}
