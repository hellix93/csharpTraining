using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactModificationTests : AuthTestBase
    {
        [Test]
        public void ContactModificationTest()
        {
            ContactData newData = new ContactData("firstname", "lastname");
            //заполняем значениями
            newData.Middlename = "middleModificated";
            newData.Lastname = "lastnameModificated";
            newData.Nickname = "nickModificated";
            newData.Title = "titleModificated";
            newData.Company = "MicrosoftModificated";
            newData.Address = "Modificated";
            newData.Home = "Modificated";
            newData.Mobile = "123";
            newData.Work = "123";
            newData.Fax = "123";
            newData.Email = "1@Modificated.ru";
            newData.Email2 = "3@Modificated.ru";
            newData.Email3 = "5@Modificated.com";
            newData.Homepage = "Modificated.ru";
            newData.Bday = "22";
            newData.Bmonth = "October";
            newData.Byear = "1980";
            newData.Aday = "29";
            newData.Amonth = "November";
            newData.Ayear = "2004";
            newData.Address2 = "add2Modificated";
            newData.Phone2 = "999";
            newData.Notes = "Contact was modificated";

            int i = 0; //номер элемента который будем изменять

            app.Contacts.CreateContactIfNotExist(i); //создать контакт если не существует

            app.Contacts.Modify(i, newData);
        }
    }
}
