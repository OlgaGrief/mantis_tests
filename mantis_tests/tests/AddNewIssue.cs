using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace mantis_tests
{
    [TestFixture]
    public class AddNewIssueTests : TestBase
    {
        [Test]
        public void AddNewIssue()
        {
            AccountData account = new AccountData()
            {
                Name = "administrator",
                Password = "root"
            };

            ProjectData project = new ProjectData()
            {
                Id = "5"
            };

            IssueData issue = new IssueData()
            {
                Summary = "some short text",
                Description = "some lohg text",
                Category = "General"
            };
            app.API.CreateNewIssue(account, project, issue);
        }
    }
}
