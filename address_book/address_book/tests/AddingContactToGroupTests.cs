using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    class AddingContactToGroupTests : AuthTestBase
    {
        [Test]
        public void TestAddingContactToGroup()
        {
            app.Contacts.CreateContactIfNotExist(0);
            app.Groups.CreateGroupIfNotExist(0);

            List<GroupData> groups = GroupData.GetAll();
            for (int i = 0; i < groups.Count; i++)
            {
                GroupData group = groups[i];
                List<ContactData> oldList = group.GetContacts();
                ContactData contact = null;
                try
                {
                    contact = ContactData.GetAll().Except(oldList).First();
                }
                catch (Exception)
                {
                    
                }

                if (i + 1 == groups.Count && contact == null) // если чекаем последнюю группу и в ней также как и до этого все контакты
                {
                    ContactData contactToRemove = ContactData.GetAll().First();
                    app.Contacts.RemoveContactFromGroup(contactToRemove, group);
                    contact = contactToRemove;
                    oldList = group.GetContacts(); //пересчитываем после удаления контакта из группы
                }
                if (contact != null)
                {
                    app.Contacts.AddContactToGroup(contact, group);

                    List<ContactData> newList = group.GetContacts();
                    oldList.Add(contact);
                    newList.Sort();
                    oldList.Sort();

                    Assert.AreEqual(oldList, newList);
                    i = groups.Count;
                }
            }
        }
    }
}
