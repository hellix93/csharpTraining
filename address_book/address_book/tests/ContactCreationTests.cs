using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactCreationTests : ContactTestBase
    {
        public static IEnumerable<ContactData> RandomContactDataProvider()
        {
            List<ContactData> contacts = new List<ContactData>();

            for (int i = 0; i < 5; i++)
            {
                contacts.Add(new ContactData(GenerateRandomString(30), GenerateRandomString(30))
                {
                    Address = GenerateRandomString(100),
                    Home = GenerateRandomString(10),
                    Email = GenerateRandomString(100)
                });
            }

            return contacts;
        }

        public static IEnumerable<ContactData> ContactDataFromXmlFile()
        {
            List<ContactData> groups = new List<ContactData>();

            return (List<ContactData>)
                 new XmlSerializer(typeof(List<ContactData>))
                 .Deserialize(new StreamReader(@"contacts.xml"));

        }

        public static IEnumerable<ContactData> ContactDataFromJsonFile()
        {
            return JsonConvert.DeserializeObject<List<ContactData>>(File.ReadAllText(@"contacts.json"));
        }

        [Test, TestCaseSource("ContactDataFromXmlFile")]
        public void ContactCreationTest(ContactData contact)
        {
            /*
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
            */
            List<ContactData> oldContacts = ContactData.GetAll();

            app.Contacts.Create(contact);

            Assert.AreEqual(oldContacts.Count + 1, app.Contacts.GetContactCount());

            List<ContactData> newContacts = ContactData.GetAll();

            oldContacts.Add(contact);
            oldContacts.Sort();
            newContacts.Sort();
            
            Assert.AreEqual(oldContacts, newContacts);
        }
    }
}
