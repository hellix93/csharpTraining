using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAddressbookTests
{
    public class RemoveContactFromGroup : AuthTestBase
    {
        [Test]
        public void TestRemoveContactFromGroup()
        {
            app.Groups.CreateGroupIfNotExist(0);
            app.Contacts.CreateContactIfNotExist(0);
            List<GroupData> groups = GroupData.GetAll();

            for (int i = 0; i < groups.Count; i++)
            {
                GroupData group = groups[i];
                List<ContactData> oldList = group.GetContacts();

                if (i + 1 == groups.Count && oldList.Count == 0) //если проверяется последняя группа и в ней нет контактов
                {
                    //берем первый контакт из всех существующих
                    ContactData contact = ContactData.GetAll().First();
                    //добавляем в последнюю группу
                    app.Contacts.AddContactToGroup(contact, group);
                    //обновляем список контактов у группы
                    oldList = group.GetContacts();
                }

                if (oldList.Count != 0)
                {
                    ContactData contactToRemove = oldList[0];
                    app.Contacts.RemoveContactFromGroup(contactToRemove, group);

                    List<ContactData> newList = group.GetContacts();
                    oldList.Remove(contactToRemove);
                    newList.Sort();
                    oldList.Sort();

                    Assert.AreEqual(oldList, newList);

                    i = groups.Count;
                }
            }
        }
    }
}
