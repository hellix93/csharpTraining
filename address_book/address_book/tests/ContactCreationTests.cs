using System;
using System.Text;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactCreationTests : AuthTestBase
    {
        [Test]
        public void ContactCreationTest()
        {
            ContactData contact = new ContactData("firstname", "lastname");
            //заполняем значениями
            //contact.Firstname = "firstname";
            contact.Middlename = "middle";
            //contact.Lastname = "lastname";
            contact.Nickname = "nick";
            contact.Title = "title";
            contact.Company = "Microsoft";
            contact.Address = "zabil";
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

            List<ContactData> oldContacts = app.Contacts.GetContactList();

            app.Contacts.Create(contact);

            Assert.AreEqual(oldContacts.Count + 1, app.Contacts.GetContactCount());

            List<ContactData> newContacts = app.Contacts.GetContactList();

            oldContacts.Add(contact);
            oldContacts.Sort();
            newContacts.Sort();
            
            Assert.AreEqual(oldContacts, newContacts);
        }
    }
}
