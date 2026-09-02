using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace mantis_tests
{
    public class RegistrationHelper : HelperBase
    {
        public RegistrationHelper(ApplicationManager manager) : base(manager) { }

        // Метод для регистрации нового пользователя
        public void Register(AccountData account)
        {
            OpenHomePage();
            OpenRegistrationForm();
            FillRegistrationForm(account);
            SubmitRegistration();
            String url = GetConfirmationUrl(account); // Получаем URL подтверждения из письма
            FillPasswordForm(url, account); // Заполняем форму для установки пароля
            SubmitPasswordForm(); // Отправляем форму для установки пароля
        }

        private void SubmitPasswordForm()
        {
            driver.FindElement(By.CssSelector("button[type='submit']")).Click();
        }

        private void FillPasswordForm(string url, AccountData account)
        {
            driver.Url = url;
            driver.FindElement(By.Name("password")).SendKeys(account.Password);
            driver.FindElement(By.Name("password_confirm")).SendKeys(account.Password);
        }

        private string GetConfirmationUrl(AccountData account)
        {
           String message = manager.Mail.GetLastMail(account);
           // Извлекаем URL подтверждения из тела письма
           // Используя регулярные выражения или строковые методы
            Match match = Regex.Match(message, @"http://\S*"); // Ищем URL, начинающийся с "http://"
            return match.Value;
        }

        private void OpenRegistrationForm()
        {
            driver.FindElement(By.CssSelector("a[href='signup_page.php']")).Click();
        }

        private void SubmitRegistration()
        {
            driver.FindElement(By.CssSelector("input[type='submit']")).Click();
        }

        private void FillRegistrationForm(AccountData account)
        {
            driver.FindElement(By.Name("username")).SendKeys(account.Name);
            driver.FindElement(By.Name("email")).SendKeys(account.Email);
         }

        private void OpenHomePage()
        {
            manager.Driver.Url = "http://localhost/mantisbt-2.28.4/login_page.php";
        }
    }
}
