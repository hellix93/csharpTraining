using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace WebAddressbookTests
{
    public class ContactHelper : HelperBase
    {
        public ContactHelper(ApplicationManager manager)
               : base(manager)
        {
        }

        public ContactHelper Create(ContactData contact)
        {
            manager.Navigator.GoToMainPage();
            InitContactCreation();
            FillContactForm(contact);
            SubmitContactCreation();

            manager.Navigator.GoToMainPage();
            return this;
        }

        private List<ContactData> contactCache = null;

        public List<ContactData> GetContactList()
        {
            if(contactCache == null)
            {
                contactCache = new List<ContactData>();
                manager.Navigator.GoToMainPage();

                ICollection<IWebElement> trFromTable = driver.FindElements(By.Name("entry")); //сбор всех строк из таблицы

                foreach (IWebElement element in trFromTable)
                {
                    List<IWebElement> cellsElements = element.FindElements((By.TagName("td"))).ToList(); //сбор всех ячеек из строки в массив

                    contactCache.Add(new ContactData(cellsElements[2].Text, cellsElements[1].Text)
                    {
                        Id = element.FindElement(By.TagName("input")).GetAttribute("value")
                    });
                }
            }
            
            return new List<ContactData>(contactCache);
        }

        public ContactHelper Modify(int i, ContactData newData)
        {
            manager.Navigator.GoToMainPage();
            InitContactModification(i);
            FillContactForm(newData);
            SubmitContactModofication();

            manager.Navigator.GoToMainPage();
            return this;
        }

        public ContactHelper Modify(ContactData oldData, ContactData newData)
        {
            manager.Navigator.GoToMainPage();
            InitContactModification(oldData.Id);
            FillContactForm(newData);
            SubmitContactModofication();

            manager.Navigator.GoToMainPage();
            return this;
        }

        public ContactHelper Remove(int i)
        {
            manager.Navigator.GoToMainPage();
            SelectContact(i);
            RemoveContact();
            manager.Navigator.GoToMainPage();
            return this;
        }

        public ContactHelper Remove(ContactData toBeRemoved)
        {
            manager.Navigator.GoToMainPage();
            SelectContact(toBeRemoved.Id);
            RemoveContact();
            manager.Navigator.GoToMainPage();
            return this;
        }

        public ContactHelper FillContactForm(ContactData contact)
        {
            Type(By.Name("firstname"), contact.Firstname);
            Type(By.Name("middlename"), contact.Middlename);
            Type(By.Name("lastname"), contact.Lastname);
            Type(By.Name("nickname"), contact.Nickname);
            Type(By.Name("title"), contact.Title);
            Type(By.Name("company"), contact.Company);
            Type(By.Name("address"), contact.Address);
            Type(By.Name("home"), contact.Home);
            Type(By.Name("mobile"), contact.Mobile);
            Type(By.Name("work"), contact.Work);
            Type(By.Name("fax"), contact.Fax);
            Type(By.Name("email"), contact.Email);
            Type(By.Name("email2"), contact.Email2);
            Type(By.Name("email3"), contact.Email3);
            Type(By.Name("homepage"), contact.Homepage);

            if (contact.Bday != null) new SelectElement(driver.FindElement(By.Name("bday"))).SelectByText(contact.Bday);
            if (contact.Bmonth != null) new SelectElement(driver.FindElement(By.Name("bmonth"))).SelectByText(contact.Bmonth);
            if (contact.Byear != null) Type(By.Name("byear"), contact.Byear);

            if (contact.Aday != null) new SelectElement(driver.FindElement(By.Name("aday"))).SelectByText(contact.Aday);
            if (contact.Amonth != null) new SelectElement(driver.FindElement(By.Name("amonth"))).SelectByText(contact.Amonth);
            if (contact.Ayear != null) Type(By.Name("ayear"), contact.Ayear);

            Type(By.Name("address2"), contact.Address2);
            Type(By.Name("phone2"), contact.Phone2);
            Type(By.Name("notes"), contact.Notes);

            return this;
        }

        public ContactHelper InitContactCreation()
        {
            driver.FindElement(By.LinkText("add new")).Click();
            return this;
        }

        public ContactHelper SubmitContactCreation()
        {
            driver.FindElement(By.XPath("//input[21]")).Click();
            contactCache = null;
            return this;
        }

        public ContactHelper InitContactModification(int index)
        {
            //driver.FindElement(By.XPath("(//img[@alt='Edit'])[" + (index + 1) + "]")).Click();
            driver.FindElements(By.Name("entry"))[index]
                .FindElements(By.TagName("td"))[7]
                .FindElement(By.TagName("a")).Click();
            contactCache = null;
            return this;
        }

        public ContactHelper InitContactModification(String index)
        {
            driver.FindElement(By.XPath("//a[@href='edit.php?id=" + index + "']")).Click();
            return this;
        }

        public ContactHelper SubmitContactModofication()
        {
            driver.FindElement(By.Name("update")).Click();
            contactCache = null;
            return this;
        }

        public ContactHelper SelectContact(int index)
        {
            driver.FindElement(By.XPath("(//input[@name='selected[]'])[" + (index + 1) + "]")).Click();
            return this;
        }

        public ContactHelper SelectContact(String id)
        {
            driver.FindElement(By.XPath("(//input[@name='selected[]' and @id='" + id + "'])")).Click();
            return this;
        }

        public ContactHelper RemoveContact()
        {
            driver.FindElement(By.XPath("//input[@value='Delete']")).Click();
            driver.SwitchTo().Alert().Accept();
            while (true) //ожидание сообщения об успешном удалении контакта
            {
                try
                {
                    driver.FindElement(By.CssSelector("div.msgbox"));
                    break;
                }
                catch (Exception)
                {

                }
            }
            contactCache = null;
            return this;
        }

        public ContactHelper CreateContactIfNotExist(int v) //проверка есть ли контакт(-ы), если нет то создается
        {
            manager.Navigator.GoToMainPage();
            while (!IsElementPresent(By.XPath("(//input[@name='selected[]'])[" + (v + 1) + "]")))
            {
                //создаем контакт
                ContactData contact = new ContactData("freshname", "lastnewname");
                //заполняем значениями
                contact.Middlename = "newmiddle";
                contact.Lastname = "newlastname";
                contact.Nickname = "newnick";
                contact.Title = "title";
                contact.Company = "Microsoft";
                contact.Address = "IfNotExist";
                contact.Home = "000";
                contact.Mobile = "911";
                contact.Work = "777";
                contact.Fax = "112";
                contact.Email = "1@2.ru";
                contact.Email2 = "3@4.ru";
                contact.Email3 = "5@6.com";
                contact.Homepage = "ya.ru";
                contact.Bday = "18";
                contact.Bmonth = "May";
                contact.Byear = "1992";
                contact.Aday = "19";
                contact.Amonth = "December";
                contact.Ayear = "2001";
                contact.Address2 = "add2";
                contact.Phone2 = "666";
                contact.Notes = "gotovo";

                Create(contact);
            }
            return this;
        }

        public int GetContactCount()
        {
            return driver.FindElements(By.CssSelector("#maintable td:nth-child(3)")).Count;
        }

        internal ContactData GetContactInformationFromTable(int index)
        {
            manager.Navigator.GoToMainPage();
            IList<IWebElement> cells = driver.FindElements(By.Name("entry"))[index]
                .FindElements(By.TagName("td"));
            string lastName = cells[1].Text;
            string firstName = cells[2].Text;
            string address = cells[3].Text;
            string allEmails = cells[4].Text;
            string allPhones = cells[5].Text;

            return new ContactData(firstName, lastName)
            {
                Address = address,
                AllEmails = allEmails,
                AllPhones = allPhones,
            };
        }

        internal ContactData GetContactInformationEditForm(int index)
        {
            manager.Navigator.GoToMainPage();
            InitContactModification(index);

            string firstName = driver.FindElement(By.Name("firstname")).GetAttribute("value");
            string lastName = driver.FindElement(By.Name("lastname")).GetAttribute("value");
            string middleName = driver.FindElement(By.Name("middlename")).GetAttribute("value");
            string nickName = driver.FindElement(By.Name("nickname")).GetAttribute("value");
            string company = driver.FindElement(By.Name("company")).GetAttribute("value");
            string title = driver.FindElement(By.Name("title")).GetAttribute("value");
            string address = driver.FindElement(By.Name("address")).GetAttribute("value");

            string email = driver.FindElement(By.Name("email")).GetAttribute("value");
            string email2 = driver.FindElement(By.Name("email2")).GetAttribute("value");
            string email3 = driver.FindElement(By.Name("email3")).GetAttribute("value");
            string homePage = driver.FindElement(By.Name("homepage")).GetAttribute("value");
            string bday = driver.FindElement(By.Name("bday")).GetAttribute("value");

            string homePhone = driver.FindElement(By.Name("home")).GetAttribute("value");
            string mobilePhone = driver.FindElement(By.Name("mobile")).GetAttribute("value");
            string workPhone = driver.FindElement(By.Name("work")).GetAttribute("value");
            string fax = driver.FindElement(By.Name("fax")).GetAttribute("value");
            string homePhone2 = driver.FindElement(By.Name("phone2")).GetAttribute("value");


            return new ContactData(firstName, lastName)
            {
                Middlename = middleName,
                Nickname = nickName,
                Company = company,
                Title = title,
                Address = address,
                Email = email,
                Email2 = email2,
                Email3 = email3,
                Homepage = homePage,
                Bday = bday,
                Home = homePhone,
                Mobile = mobilePhone,
                Work = workPhone,
                Fax = fax,
                Home2 = homePhone2
            };
        }

        internal String GetContactInformationFromContactPage(int index)
        {
            manager.Navigator.GoToMainPage();
            ViewContactPage(index);

            string infoFromPage = CleanUpInfo(driver.FindElement(By.Id("content")).Text);

            return infoFromPage + "\r\n";

        }

        private string CleanUpInfo(string info)
        {
            if (info == null || info == "")
            {
                return "";
            }
            string cleanPhones = Regex.Replace(info, "[HMWF]: ", "");
            string cleanEmails = Regex.Replace(cleanPhones, "[-()]", "");

            return cleanEmails;
        }

        public ContactHelper ViewContactPage(int index)
        {
            driver.FindElements(By.Name("entry"))[index]
                .FindElements(By.TagName("td"))[6]
                .FindElement(By.TagName("a")).Click();
            contactCache = null;
            return this;
        }

        public void AddContactToGroup(ContactData contact, GroupData group)
        {
            manager.Navigator.GoToMainPage();
            ClearGroupFilter();
            SelectContactToAdd(contact.Id);
            SelectGroupToAdd(group.Name);
            CommitAddingContactToGroup();
            new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                .Until(d => d.FindElements(By.CssSelector("div.msgbox")).Count > 0);
        }

        public void CommitAddingContactToGroup()
        {
            driver.FindElement(By.Name("add")).Click();
        }

        public void SelectGroupToAdd(string name)
        {
            new SelectElement(driver.FindElement(By.Name("to_group"))).SelectByText(name);
        }

        public void SelectContactToAdd(string contactId)
        {
            driver.FindElement(By.Id(contactId)).Click();
        }

        public void ClearGroupFilter()
        {
            new SelectElement(driver.FindElement(By.Name("group"))).SelectByText("[all]");
        }

        public void RemoveContactFromGroup(ContactData contact, GroupData group)
        {
            manager.Navigator.GoToMainPage();
            SelectRequiredGroup(group.Name);
            SelectContactToRemove(contact.Id);
            CommitRemovingContactFromGroup();
            new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                .Until(d => d.FindElements(By.CssSelector("div.msgbox")).Count > 0);
        }

        private void CommitRemovingContactFromGroup()
        {
            driver.FindElement(By.Name("remove")).Click();
        }

        private void SelectContactToRemove(string contactId)
        {
            driver.FindElement(By.Id(contactId)).Click();
        }

        private void SelectRequiredGroup(string name)
        {
            new SelectElement(driver.FindElement(By.Name("group"))).SelectByText(name);
        }
    }
}
