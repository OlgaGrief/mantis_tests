using OpenQA.Selenium;

namespace mantis_tests
{
    public class ManagementMenuHelper : HelperBase
    {
        public ManagementMenuHelper(ApplicationManager manager) : base(manager)
        {
        }

        // Метод для перехода в меню управления
        public ManagementMenuHelper GoToManagementMenu()
        {
            driver.FindElement(By.LinkText("Manage")).Click();
            return this;
        }

        // Метод для перехода на страницу управления пользователями 
        public ManagementMenuHelper OpenProjectTab() // метод для перехода на страницу управления проектами
        {
            driver.FindElement(
                By.CssSelector("a[href*='manage_proj_page.php']")).Click();

            return this;
        }
    }
}