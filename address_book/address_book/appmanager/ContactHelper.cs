using System;
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

        public ContactHelper Modify(int i, ContactData newData)
        {
            manager.Navigator.GoToMainPage();
            CreateContactIfNotExist(i); //создать контакт если не существует
            InitContactModification(i);
            FillContactForm(newData);
            SubmitContactModofication();

            manager.Navigator.GoToMainPage();
            return this;
        }

        public ContactHelper Remove(int i)
        {
            manager.Navigator.GoToMainPage();
            CreateContactIfNotExist(i); //создать контакт если не существует
            SelectContact(i);
            RemoveContact();
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
            new SelectElement(driver.FindElement(By.Name("bday"))).SelectByText(contact.Bday);
            new SelectElement(driver.FindElement(By.Name("bmonth"))).SelectByText(contact.Bmonth);
            Type(By.Name("byear"), contact.Byear);
            new SelectElement(driver.FindElement(By.Name("aday"))).SelectByText(contact.Aday);
            new SelectElement(driver.FindElement(By.Name("amonth"))).SelectByText(contact.Amonth);
            Type(By.Name("ayear"), contact.Ayear);
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
            return this;
        }

        public ContactHelper InitContactModification(int index)
        {
            driver.FindElement(By.XPath("(//img[@alt='Edit'])[" + index + "]")).Click();
            return this;
        }

        public ContactHelper SubmitContactModofication()
        {
            driver.FindElement(By.Name("update")).Click();
            return this;
        }

        public ContactHelper SelectContact(int index)
        {
            driver.FindElement(By.XPath("(//input[@name='selected[]'])[" + index + "]")).Click();
            return this;
        }

        public ContactHelper RemoveContact()
        {
            driver.FindElement(By.XPath("//input[@value='Delete']")).Click();
            driver.SwitchTo().Alert().Accept();
            manager.Navigator.GoToMainPage();
            return this;
        }

        public ContactHelper CreateContactIfNotExist(int v) //проверка есть ли контакт(-ы), если нет то создается
        {
            while (!IsElementPresent(By.XPath("(//input[@name='selected[]'])[" + v + "]")))
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
    }
}
