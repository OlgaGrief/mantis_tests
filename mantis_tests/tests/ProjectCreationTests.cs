using NUnit.Framework;
using System.Collections.Generic;

namespace mantis_tests
{
    [TestFixture]
    public class ProjectCreationTests : AuthTestBase
    {
        [Test]
        public void ProjectCreationTest()
        {
            AccountData account = new AccountData()
            {
                Name = "administrator",
                Password = "root"
            };

            ProjectData project = new ProjectData("test" + GenerateRandomString(10)); // Создаем новый проект с уникальным именем

            List<ProjectData> oldProjects = app.API.GetProjectList(account); // Получаем список проектов до создания нового проекта

            app.Project.Create(project); // Создаем новый проект через UI

            List<ProjectData> newProjects = app.API.GetProjectList(account);

            oldProjects.Add(project);

            oldProjects.Sort();
            newProjects.Sort();

            Assert.AreEqual(oldProjects, newProjects);
        }
    }
}