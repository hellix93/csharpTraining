using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace addressbook_tests_autoit
{
    public class GroupDeleteTests : TestBase
    {
        [Test]
        public void TestGroupDelete()
        {
            GroupData newGroupForDelete = new GroupData()
            {
                Name = "deleteTest"
            };
            app.Groups.Add(newGroupForDelete);

            List<GroupData> groupsBeforeDelete = app.Groups.GetGroupList();

            if (groupsBeforeDelete.Count() < 2) //проверка что групп достаточно для удаления
            {
                app.Groups.Add(newGroupForDelete); //создаем еще группу для удаления
                groupsBeforeDelete = app.Groups.GetGroupList(); //пересчитываем список
            }

            app.Groups.Delete();

            List<GroupData> groupsAfterDelete = app.Groups.GetGroupList();
            groupsAfterDelete.Add(newGroupForDelete);
            groupsBeforeDelete.Sort();
            groupsAfterDelete.Sort();

            Assert.AreEqual(groupsBeforeDelete, groupsAfterDelete);
        }
    }
}
