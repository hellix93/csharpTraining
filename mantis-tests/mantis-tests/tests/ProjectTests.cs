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
        }

        [Test]
        public void TestProjectRemoving()
        {
            int i = 1;
            List<ProjectData> oldprojects = new List<ProjectData>();
            oldprojects = app.Project.GetProjectList();

            ProjectData removedProject = oldprojects[i];

            app.Project.RemoveProject(i);

            oldprojects.Remove(removedProject);
            List<ProjectData> newprojects = app.Project.GetProjectList();

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