using OpenQA.Selenium;
using System.Collections.Generic;

namespace mantis_tests
{
    public class ProjectManagementHelper : HelperBase
    {
        public ProjectManagementHelper(ApplicationManager manager)
            : base(manager)
        {
        }

        // Эта функция создает новый проект в системе MantisBT, используя данные, предоставленные в объекте ProjectData.
        public ProjectManagementHelper Create(ProjectData project)
        {
            manager.Menu.GoToManagementMenu();
            manager.Menu.OpenProjectTab();
            InitProjectCreation();
            FillProjectForm(project);
            SubmitProjectCreation();
            //ReturnToProjectPage();

            return this;
        }

        // Эта функция инициализирует процесс создания нового проекта, нажимая на кнопку "Создать проект" на странице управления проектами.
        public ProjectManagementHelper InitProjectCreation()
        {
            driver.FindElement(
                By.XPath("//button[@type='submit']")).Click();

            return this;
        }

        // Эта функция заполняет форму создания проекта с использованием данных, предоставленных в объекте ProjectData.
        public ProjectManagementHelper FillProjectForm(ProjectData project)
        {
            Type(By.Id("project-name"), project.ProjectName);

            return this;
        }

        // Эта функция отправляет форму создания проекта, которая была заполнена ранее.
        public ProjectManagementHelper SubmitProjectCreation()
        {
            driver.FindElement(
                By.XPath("//input[@value='Add Project']")).Click();

            return this;
        }

        // Эта функция возвращает пользователя на страницу управления проектами после создания нового проекта.
        public ProjectManagementHelper ReturnToProjectPage()
        {
            driver.FindElement(By.LinkText("Proceed")).Click();

            return this;
        }

        // Эта функция возвращает список проектов, которые есть в системе MantisBT.
        // Она использует Selenium WebDriver для поиска элементов на странице управления проектами
        // и извлекает информацию о каждом проекте, включая его имя и идентификатор.
        public List<ProjectData> GetProjectList()
        {
            List<ProjectData> projects = new List<ProjectData>();

            manager.Menu.GoToManagementMenu();
            manager.Menu.OpenProjectTab();

            ICollection<IWebElement> elements =driver.FindElements(By.CssSelector("a[href*='manage_proj_edit_page.php?project_id=']"));

            foreach (IWebElement element in elements)
            {
                string href = element.GetAttribute("href");
                string id = href.Substring(href.IndexOf("project_id=") + 11);

                ProjectData project = new ProjectData(element.Text)
                {
                    Id = id
                };

                projects.Add(project);
            }

            return projects;
        }

        // Эта функция удаляет проект с указанным идентификатором из системы MantisBT.
        public ProjectManagementHelper Remove(ProjectData project)
        {
            OpenProject(project.Id);
            DeleteProject();
            SubmitProjectDeleting();

            return this;
        }

        // Эта функция открывает страницу управления проектом с указанным идентификатором.
        public ProjectManagementHelper OpenProject(string id)
        {
            driver.FindElement(By.CssSelector("a[href*='manage_proj_edit_page.php?project_id=" + id + "']")).Click();

            return this;
        }

        // Эта функция удаляет проект, который в данный момент открыт на странице управления проектами.
        public ProjectManagementHelper DeleteProject()
        {
            driver.FindElement(By.XPath("//button[contains(., 'Delete Project')]")).Click();

            return this;
        }

        // Эта функция подтверждает удаление проекта, который в данный момент открыт на странице управления проектами.
        public ProjectManagementHelper SubmitProjectDeleting()
        {
            driver.FindElement(By.XPath("//input[@value='Delete Project']")).Click();

            return this;
        }
    }
}