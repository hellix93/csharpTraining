using System;
using System.Text;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupRemovalTests : AuthTestBase
    {
        [Test]
        public void GroupRemovalTest()
        {
            int i = 0; //номер удаляемого элемента

            app.Groups.CreateGroupIfNotExist(i); // вызов метода проверки существует ли группа, если группы нет создастся новая

            List<GroupData> oldGroups = app.Groups.GetGroupList();

            app.Groups.Remove(i);

            Assert.AreEqual(oldGroups.Count - 1, app.Groups.GetGroupCount());

            List<GroupData> newGroups = app.Groups.GetGroupList();

            GroupData toBeRemoved = oldGroups[i];
            oldGroups.RemoveAt(i);
            Assert.AreEqual(oldGroups, newGroups);

            foreach(GroupData group in newGroups)
            {
                Assert.AreNotEqual(group.Id, toBeRemoved.Id);
            }
        }
    }
}
