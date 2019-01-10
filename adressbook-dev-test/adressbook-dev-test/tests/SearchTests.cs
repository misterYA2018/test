using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class SearchTests : AuthTestBase
    {
        [Test]
        public void SearchTest()
        {
            System.Console.Out.Write(app.Contacts.GetNumberOfSearchResults());
        }
    }
} 