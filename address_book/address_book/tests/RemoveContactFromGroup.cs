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
            List<GroupData> groups = GroupData.GetAll();

            if (groups.Count != 0)
            {
                for (int i = 0; i < groups.Count; i++)
                {
                    GroupData group = groups[i];
                    List<ContactData> oldList = group.GetContacts();


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
            else
            {
                Console.Out.Write("Группы отсутвуют. Создайте группу, добавите в нее контакт(-ы) и повторите тест.");
            }
        }
    }
}
