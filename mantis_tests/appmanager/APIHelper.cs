using mantis_tests.Mantis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace mantis_tests
{
    public class APIHelper : HelperBase
    {
        public APIHelper (ApplicationManager manager) : base(manager) { }

        // Создание новой задачи через API
        public void CreateNewIssue(AccountData account, ProjectData project, IssueData issueData)
        {
            Mantis.MantisConnectPortTypeClient client = new Mantis.MantisConnectPortTypeClient();
            Mantis.IssueData issue = new Mantis.IssueData();
            issue.summary = issueData.Summary;
            issue.description = issueData.Description;
            issue.category = issueData.Category;
            issue.project = new Mantis.ObjectRef();
            issue.project.id = project.Id;
            client.mc_issue_add(account.Name, account.Password, issue); // задача http://localhost/mantisbt-2.28.4/view.php?id=2
        }

        // Получение списка проектов через API
        public List<ProjectData> GetProjectList(AccountData account)
        {
            List<ProjectData> projects = new List<ProjectData>();

            Mantis.MantisConnectPortTypeClient client = new Mantis.MantisConnectPortTypeClient();

            Mantis.ProjectData[] mantisProjects = client.mc_projects_get_user_accessible(account.Name, account.Password);

            foreach (Mantis.ProjectData project in mantisProjects)
            {
                projects.Add(new ProjectData()
                {
                    Id = project.id,
                    ProjectName = project.name
                });
            }

            return projects;
        }

        // Создание нового проекта через API
        public void CreateProject(AccountData account, ProjectData project)
        {
            Mantis.MantisConnectPortTypeClient client = new Mantis.MantisConnectPortTypeClient();

            Mantis.ProjectData mantisProject = new Mantis.ProjectData();

            mantisProject.name = project.ProjectName;

            client.mc_project_add(account.Name, account.Password, mantisProject);
        }
    }
}
