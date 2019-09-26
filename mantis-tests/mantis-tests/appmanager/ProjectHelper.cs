using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;


namespace mantis_tests
{
    public class ProjectHelper : HelperBase
    {
        public ProjectHelper(ApplicationManager manager) : base(manager) { }

        public void AddProject(ProjectData project)
        {
            OpenManagePage();
            OpenManageProgectPage();
            InitProjectCreation();
            FillNewProjectForm(project);
            SubmitButtonForm();
        }

        public void RemoveProject(int i)
        {
            OpenManagePage();
            OpenManageProgectPage();
            driver.FindElement(By.XPath("/html/body/table[3]/tbody/tr[" + (i + 3) + "]/td[1]/a")).Click();
            driver.FindElement(By.XPath("//input[@value='Delete Project']")).Click();
            manager.Registration.SubmitButtonForm();

        }

        public void FillNewProjectForm(ProjectData project)
        {
            driver.FindElement(By.Name("name")).SendKeys(project.Name);
            driver.FindElement(By.Name("description")).SendKeys(project.Description);
        }

        public void InitProjectCreation()
        {
            driver.FindElement(By.XPath("/html/body/table[3]/tbody/tr[1]/td/form/input[2]")).Click();
        }

        public void OpenManageProgectPage()
        {
            driver.FindElement(By.XPath("/html/body/div[2]/p/span[2]/a")).Click();
        }

        public void OpenManagePage()
        {
            driver.FindElement(By.XPath("/html/body/table[2]/tbody/tr/td[1]/a[7]")).Click();
        }

        public List<ProjectData> GetProjectList()
        {
            OpenManagePage();
            OpenManageProgectPage();

            List<ProjectData> projects = new List<ProjectData>();

            ICollection<IWebElement> elements = driver.FindElement(By.XPath("/html/body/table[3]/tbody"))
                .FindElements(By.XPath(".//tr[@class='row-1' or @class='row-2']"));

            foreach (IWebElement element in elements)
            {
                ProjectData project = new ProjectData(element.FindElement(By.XPath(".//td[1]")).Text, element.FindElement(By.XPath(".//td[5]")).Text);
                projects.Add(project);
            }

            return new List<ProjectData>(projects);
        }
    }
}