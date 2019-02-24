using NUnit.Framework;

namespace mantis_tests
{
    public class AuthTestBase : TestBase
    {
        public AccountData Administrator { get; private set; }

        [SetUp]
        public void SetupLogin()
        {
            Administrator = new AccountData("administrator", "root");
            app.Auth.Login(Administrator);
        }
    }
}
