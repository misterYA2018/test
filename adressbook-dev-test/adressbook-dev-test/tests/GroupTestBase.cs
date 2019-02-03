using NUnit.Framework;

namespace WebAddressbookTests
{
    public class GroupTestBase : AuthTestBase
    {
        [TearDown]
        public void CompareGroupsUI_DB()
        {
            if(PERFORM_LONG_UI_CHECKS)
            {
                var fromUI = app.Groups.GetGroupList();
                var fromDB = GroupData.GetAll();

                fromUI.Sort();
                fromDB.Sort();

                Assert.AreEqual(fromUI, fromDB);
            }
        }
    }
}
