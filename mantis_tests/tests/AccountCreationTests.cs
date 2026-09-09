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
                app.Ftp.Upload("/config/config_inc.php", localFile); // загружаем локальный файл config_inc.php на сервер, чтобы отключить капчу
            }           
        }
        [Test]

        // Тестовый метод для проверки регистрации учетной записи
        // cd C:\james\bin
        // .\run.bat
        public void TestAccountRegistration()
        {           
            string suffix = DateTime.Now.ToString("yyyyMMddHHmmss");

            AccountData account = new AccountData()
            {
                Name = "testuser" + suffix,
                Password = "password",
                Email = "testuser" + suffix + "@localhost.localdomain"
            };

            List<AccountData> accounts = app.Admin.GetAllAccounts(); // получаем список всех учетных записей с сервера

            AccountData existingAccount = accounts.Find(x => x.Name.Equals(account.Name)); // ищем учетную запись с таким же именем в списке существующих учетных записей
            if (existingAccount != null)
            {
                app.Admin.DeleteAccount(existingAccount);
            }

            app.James.Delete(account); // удаляем учетную запись на сервере James, если она существует
            app.James.Add(account); // добавляем учетную запись на сервер James           

            app.Registration.Register(account);
        }

        [TestFixtureTearDown]

        // Метод для восстановления конфигурационного файла после тестов
        public void RestoreConfig()
        {
            app.Ftp.RestoreBackupFile("/config/config_inc.php"); // восстанавливаем резервную копию файла config_inc.php на сервере
        }
    }
}
