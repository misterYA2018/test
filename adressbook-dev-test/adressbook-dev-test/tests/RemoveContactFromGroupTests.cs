using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class RemoveContactFromGroupTests : AuthTestBase
    {
        [Test]
        public void TestRemoveContactFromGroupTests()
        {
            var group = GroupData.GetAll()[0];
            var oldList = group.GetContacts();
            var contact = oldList[0];

            app.Contacts.RemoveContactFromGroup(contact, group);

            var newList = group.GetContacts();

            oldList.RemoveAt(0);
            oldList.Sort();
            newList.Sort();

            Assert.AreEqual(oldList, newList);
        }
    }
}
