using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactInformationTests : AuthTestBase
    {
        [Test]
        public void TestContactInformation()
        {
            int i = 0;
            ContactData fromTable = app.Contacts.GetContactInformationFromTable(i);
            ContactData fromForm = app.Contacts.GetContactInformationEditForm(i);
            Assert.AreEqual(fromTable, fromForm);
            Assert.AreEqual(fromTable.Address, fromForm.Address);
            Assert.AreEqual(fromTable.AllEmails, fromForm.AllEmails);
            Assert.AreEqual(fromTable.AllPhones, fromForm.AllPhones);
        }

        [Test]
        public void TestContactInformation2()
        {
            int i = 0;
            String fromContactDetailsString = app.Contacts.GetContactInformationFromContactPage(i);
            ContactData fromForm = app.Contacts.GetContactInformationEditForm(i);
            String fromFormString = MakeStringFromEditContact(fromForm);
            Assert.AreEqual(fromContactDetailsString, fromFormString);
        }

        private string MakeStringFromEditContact(ContactData fromForm)
        {
            string name = "";
            string middleName = "";
            string lastName = "";
            string nickName = "";
            string title = "";
            string company = "";
            string address = "";
            string allPhones = "";
            string fax = "";
            string allEmails = "";
            string homePage = "";

            if (fromForm.Firstname != "")
            {
                name = fromForm.Firstname + " ";
            }

            if (fromForm.Middlename != "")
            {
                middleName = fromForm.Middlename + " ";
            }

            if (fromForm.Lastname != "")
            {
                lastName = fromForm.Lastname + "\r\n";
            }

            if (fromForm.Nickname != "")
            {
                nickName = fromForm.Nickname + "\r\n";
            }

            if (fromForm.Title != "")
            {
                title = fromForm.Title + "\r\n";
            }

            if (fromForm.Company != "")
            {
                company = fromForm.Company + "\r\n";
            }

            if (fromForm.Address != "")
            {

                address = fromForm.Address + "\r\n";
            }

            if (fromForm.AllPhones != "")
            {
                allPhones = fromForm.AllPhones + "\r\n";
            }

            if (fromForm.Fax != "")
            {
                fax = fromForm.Fax + "\r\n";
            }

            if (fromForm.AllEmails != "")
            {
                allEmails = fromForm.AllEmails + "\r\n";
            }

            if (fromForm.Homepage != "")
            {
                homePage = "Homepage:\r\n" + fromForm.Homepage + "\r\n";
            }

            if (name+ middleName + lastName + nickName + title + company + address + allPhones + fax + allEmails + homePage == "")
            {
                return "\r\n";
            }
            else
            {
                return name + middleName + lastName + nickName + title + company + address + "\r\n" + allPhones + fax + "\r\n" + allEmails + homePage;
            }
        }
    }
}