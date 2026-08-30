using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.IO;

namespace mantis_tests
{
    [TestFixture]
    public class AccountCreationTests : TestBase
    {
        [TestFixtureSetUp]
        public void SetUpConfig()
        {
            app.Ftp.BackupFile("/config/config_inc.php"); // делаем резервную копию файла config_inc.php на сервере
            using(Stream localFile = File.Open("config_inc.php", FileMode.Open))
            {
                app.Ftp.Upload("config_inc.php", localFile); // загружаем локальный файл config_inc.php на сервер, чтобы отключить капчу
            }           
        }
        [Test]
        public void TestAccountRegistration()
        {
            AccountData account = new AccountData()
            {
                Name = "testuser",
                Password = "password",
                Email = "testuser@localhost.localdomain"
            };

            app.Registration.Register(account);
        }

        [TestFixtureTearDown]

        // Метод для восстановления конфигурационного файла после тестов
        public void RestoreConfig()
        {
            app.Ftp.RestoreBackupFile("/config/config_inc.php.bak"); // восстанавливаем резервную копию файла config_inc.php на сервере
        }
    }
}
