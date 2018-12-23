using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class LoginTests : TestBase 
    {
        [SetUp]
        public void SetUp()
        {
            app.Auth.Logout();
        }

        [Test]
        public void LoginWithValidCredentials()
        {
            var account = new AccountData("admin", "secret");

            app.Auth.Login(account);

            Assert.IsTrue(app.Auth.IsLoggedIn(account));
        }

        [Test]
        public void LoginWithInvalidCredentials()
        {
            var account = new AccountData("admin", "123");

            app.Auth.Login(account);

            Assert.IsFalse(app.Auth.IsLoggedIn(account));
        }
    }
}
