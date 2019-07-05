using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactRemovalTests : AuthTestBase
    {
        [Test]
        public void ContactRemovalTest()
        {
            int i = 0; //номер элемента который будем изменять

            app.Contacts.CreateContactIfNotExist(i); //создать контакт если не существует

            app.Contacts.Remove(i);
        }
    }
}
