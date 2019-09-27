using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using NUnit.Framework;

namespace mantis_tests
{
    [TestFixture]
    public class MantisProjectTests : TestBase
    {
        [SetUp]
        public void LoginAsAdmin()
        {
            AccountData admin = new AccountData
            {
                Name = "administrator",
                Password = "root"
            };
            app.Registration.OpenMainPage();
            app.Registration.FillAuthForm(admin);
            app.Registration.SubmitButtonForm();
        }

        [Test]
        public void TestProjectCreation()
        {
            /*
            List<ProjectData> oldprojects = new List<ProjectData>();
            oldprojects = app.Project.GetProjectList();

            ProjectData project = new ProjectData
            {
                Name = "testProject1",
                Description = "testProject1Creation"
            };

            app.Project.AddProject(project);
            oldprojects.Add(project);
            List<ProjectData> newprojects = app.Project.GetProjectList();
            oldprojects.Sort();
            newprojects.Sort();
            Assert.AreEqual(oldprojects, newprojects);
            */

            AccountData admin = new AccountData
            {
                Name = "administrator",
                Password = "root"
            };

            List<ProjectData> oldprojects2 = new List<ProjectData>();
            oldprojects2 = app.API.APIGetProjectList(admin);

            ProjectData project = new ProjectData
            {
                Name = "testProject1",
                Description = "testProject1Creation"
            };

            app.Project.AddProject(project);

            oldprojects2.Add(project);

            List<ProjectData> newprojects = app.API.APIGetProjectList(admin);
            oldprojects2.Sort();
            newprojects.Sort();
            Assert.AreEqual(oldprojects2, newprojects);
        }

        [Test]
        public void TestProjectRemoving()
        {
            /*
            int i = 3;
            List<ProjectData> oldprojects = new List<ProjectData>();
            oldprojects = app.Project.GetProjectList();

            while (oldprojects.Count <= i)
            {
                ProjectData project = new ProjectData
                {
                    Name = "projectForDelete " + oldprojects.Count.ToString(),
                    Description = "forRemovingTest"
                };

                app.Project.AddProject(project);
                oldprojects = app.Project.GetProjectList();
            }

            ProjectData removedProject = oldprojects[i];

            app.Project.RemoveProject(i);

            oldprojects.Remove(removedProject);
            List<ProjectData> newprojects = app.Project.GetProjectList();

            oldprojects.Sort();
            newprojects.Sort();
            Assert.AreEqual(oldprojects, newprojects);
            */

            AccountData admin = new AccountData
            {
                Name = "administrator",
                Password = "root"
            };

            int i = 3;

            List<ProjectData> oldprojects = new List<ProjectData>();

            oldprojects = app.API.APIGetProjectList(admin);

            while (oldprojects.Count <= i)
            {
                ProjectData project = new ProjectData
                {
                    Name = "projectForDelete " + oldprojects.Count.ToString(),
                    Description = "forRemovingTest"
                };

                app.Project.AddProject(project);
                oldprojects = app.Project.GetProjectList();
            }

            ProjectData removedProject = oldprojects[i];

            app.Project.RemoveProject(i);

            oldprojects.Remove(removedProject);
            List<ProjectData> newprojects = app.API.APIGetProjectList(admin);

            oldprojects.Sort();
            newprojects.Sort();
            Assert.AreEqual(oldprojects, newprojects);
        }

        [TearDown]

        public void Logout()
        {
            app.Registration.InitLogOut();
        }
    }
}