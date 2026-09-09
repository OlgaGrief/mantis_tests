using OpenQA.Selenium;

namespace mantis_tests
{
    public class LoginHelper : HelperBase
    {
        public LoginHelper(ApplicationManager manager) : base(manager)
        {
        }

        // Эта функция выполняет вход в систему MantisBT с использованием данных учетной записи, предоставленных в объекте AccountData.
        public void Login(AccountData account)
        {
            if (IsLoggedIn())
            {
                return;
            }

            driver.Url = "http://localhost/mantisbt-2.28.4/login_page.php";

            driver.FindElement(By.Name("username")).SendKeys(account.Name);
            driver.FindElement(By.CssSelector("input[type='submit']")).Click();

            driver.FindElement(By.Name("password")).SendKeys(account.Password);
            driver.FindElement(By.CssSelector("input[type='submit']")).Click();
        }

        // Эта функция проверяет, выполнен ли вход в систему MantisBT, путем проверки наличия элемента с именем "username" на странице.
        // Если элемент отсутствует, это означает, что пользователь уже вошел в систему.
        public bool IsLoggedIn()
        {
            return !IsElementPresent(By.Name("username"));
        }
    }
}