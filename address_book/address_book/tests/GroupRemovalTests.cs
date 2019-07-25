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

            List<GroupData> newGroups = app.Groups.GetGroupList();

            oldGroups.RemoveAt(i);
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);
        }
    }
}
