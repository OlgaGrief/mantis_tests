using NUnit.Framework;

namespace mantis_tests
{
    public class AuthTestBase : TestBase
    {
        [SetUp]
        public void SetupLogin()
        {
            app.Auth.Login(
                new AccountData()
                {
                    Name = "administrator",
                    Password = "root"
                });
        }
    }
}