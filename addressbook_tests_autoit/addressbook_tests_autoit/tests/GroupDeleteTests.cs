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

            app.Groups.Delete();

            List<GroupData> groupsAfterDelete = app.Groups.GetGroupList();
            groupsAfterDelete.Add(newGroupForDelete);
            groupsBeforeDelete.Sort();
            groupsAfterDelete.Sort();

            Assert.AreEqual(groupsBeforeDelete, groupsAfterDelete);
        }
    }
}
