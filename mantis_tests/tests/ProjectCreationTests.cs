using NUnit.Framework;
using System.Collections.Generic;

namespace mantis_tests
{
    [TestFixture]
    public class ProjectCreationTests : AuthTestBase
    {

        // получили старый список проектов, создали новый проект, получили новый список,
        // добавили созданный проект к старому списку, отсортировали оба списка,сравнили

        [Test]
        public void ProjectCreationTest() 
        {
            ProjectData project = new ProjectData("test" + GenerateRandomString(10));

            List<ProjectData> oldProjects =
                app.Project.GetProjectList();

            app.Project.Create(project);

            List<ProjectData> newProjects =
                app.Project.GetProjectList();

            oldProjects.Add(project);

            oldProjects.Sort();
            newProjects.Sort();

            Assert.AreEqual(oldProjects, newProjects);
        }
    }
}