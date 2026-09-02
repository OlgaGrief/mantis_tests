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
            List<ProjectData> oldProjects = app.Project.GetProjectList();

            if (oldProjects.Count == 0)
            {
                ProjectData project = new ProjectData("test" + GenerateRandomString(10));

                app.Project.Create(project);

                oldProjects = app.Project.GetProjectList();
            }

            ProjectData toBeRemoved = oldProjects[0];

            app.Project.Remove(toBeRemoved);

            List<ProjectData> newProjects = app.Project.GetProjectList();

            oldProjects.RemoveAt(0);

            oldProjects.Sort();
            newProjects.Sort();

            Assert.AreEqual(oldProjects, newProjects);
        }
    }
}