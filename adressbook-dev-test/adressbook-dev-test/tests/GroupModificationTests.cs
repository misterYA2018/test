using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupModification : AuthTestBase
    {
        [SetUp]
        public void SetUp()
        {
            Assert.IsFalse(app.Groups.TableIsEmpty(true), "Таблица с группами пустая");
        }

        [Test]
        public void GroupModificationTest()
        {
            var newGroup = new GroupData("Test group new");
            newGroup.Header = "Test header new";
            newGroup.Footer = "Test footer new";

            app.Groups.Modify(1, newGroup);
        }
    }
}