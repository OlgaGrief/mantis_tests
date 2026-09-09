using NUnit.Framework;
using System.Collections.Generic;

namespace mantis_tests
{
    [TestFixture]
    public class ProjectRemovalTests : AuthTestBase
    {
        [Test]
        public void ProjectRemovalTest()
        {
            AccountData account = new AccountData()
            {
                Name = "administrator",
                Password = "root"
            };

            List<ProjectData> oldProjects = app.API.GetProjectList(account);

            if (oldProjects.Count == 0)
            {
                ProjectData project = new ProjectData("test" + GenerateRandomString(10));

                app.API.CreateProject(account, project); // Создание проекта через API

                oldProjects = app.API.GetProjectList(account);
            }

            ProjectData toBeRemoved = oldProjects[0];

            app.Menu.GoToManagementMenu(); // Selenium Переход в меню управления 
            app.Menu.OpenProjectTab(); // Selenium Переход в меню управления проектами

            app.Project.Remove(toBeRemoved); // Selenium Удаление проекта через UI

            List<ProjectData> newProjects = app.API.GetProjectList(account);

            oldProjects.RemoveAt(0);

            oldProjects.Sort();
            newProjects.Sort();

            Assert.AreEqual(oldProjects, newProjects);
        }
    }
}