using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimpleBrowser.WebDriver;
using System.Threading.Tasks;
using OpenQA.Selenium;
using System.Text.RegularExpressions;
using mantis_tests.Mantis;

namespace mantis_tests
{
    public class APIHelper : HelperBase
    {
        private Mantis.ProjectData[] projectsArr;
        private Mantis.ProjectData ppp;
        private Mantis.ProjectData mmm;
        public APIHelper(ApplicationManager manager) : base(manager) { }

        public void CreateNewIssue(AccountData account, ProjectData project, IssueData issueData)
        {
            Mantis.MantisConnectPortTypeClient client = new Mantis.MantisConnectPortTypeClient();
            Mantis.IssueData issue = new Mantis.IssueData();
            issue.summary = issueData.Summary;
            issue.description = issueData.Description;
            issue.category = issueData.Category;
            issue.project = new Mantis.ObjectRef();
            issue.project.id = project.Id;
            client.mc_issue_add(account.Name, account.Password, issue);
        }

        public void APIAddMantisProject(AccountData account, ProjectData project)
        {
            Mantis.MantisConnectPortTypeClient client = new Mantis.MantisConnectPortTypeClient();

            Mantis.ProjectData projectm = new Mantis.ProjectData()
            {
                name = project.Name,
                description = project.Description

            };

            client.mc_project_add(account.Name, account.Password, projectm);


        }

        public List<ProjectData> APIGetProjectList(AccountData account)
        {
            Mantis.MantisConnectPortTypeClient client = new Mantis.MantisConnectPortTypeClient();
            List<ProjectData> projects = new List<ProjectData>();

            projectsArr = client.mc_projects_get_user_accessible(account.Name, account.Password);

            foreach (Mantis.ProjectData pro in projectsArr)
            {
                ProjectData project = new ProjectData();
                project.Name = pro.name;
                project.Id = pro.id;
                project.Description = pro.description;
                projects.Add(project);
            }

            return projects;
        }
    }
}