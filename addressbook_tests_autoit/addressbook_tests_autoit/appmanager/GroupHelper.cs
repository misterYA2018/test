using AutoItX3Lib;
using System;
using System.Collections.Generic;

namespace addressbook_tests_autoit
{
    public class GroupHelper : HelperBase
    {
        private ApplicationManager applicationManager;

        public static string GROUPWINTITLE;
        public static string GROUPDELETETITLE;

        public GroupHelper(ApplicationManager manager) : base(manager)
        {
            applicationManager = manager;
            GROUPWINTITLE = "Group editor";
            GROUPDELETETITLE = "Delete group";
        }

        public List<GroupData> GetGroupList()
        {
            var list = new List<GroupData>();

            OpenGroupsDialog();

            string count = aux.ControlTreeView(GROUPWINTITLE, "", "WindowsForms10.SysTreeView32.app.0.2c908d51", "GetItemCount", "#0", "");

            for(int i = 0; i < int.Parse(count); i++)
            {
                string item = aux.ControlTreeView(GROUPWINTITLE, "", "WindowsForms10.SysTreeView32.app.0.2c908d51", "GetText", "#0|#" + i, "");

                list.Add(new GroupData
                {
                    Name = item
                });
            }

            CloseGroupsDialog();

            return list;
        }

        internal void Remove(GroupData groupForRemove)
        {
            OpenGroupsDialog();

            string count = aux.ControlTreeView(GROUPWINTITLE, "", "WindowsForms10.SysTreeView32.app.0.2c908d51", "GetItemCount", "#0", "");

            for (int i = 0; i < int.Parse(count); i++)
            {
                var id = "#0|#" + i;
                string item = aux.ControlTreeView(GROUPWINTITLE, "", "WindowsForms10.SysTreeView32.app.0.2c908d51", "GetText", id, "");

                if (item.Equals(groupForRemove.Name))
                {
                    aux.ControlTreeView(GROUPWINTITLE, "", "WindowsForms10.SysTreeView32.app.0.2c908d51", "Select", id, "");

                    break;
                }
            }

            aux.ControlClick(GROUPWINTITLE, "", "WindowsForms10.BUTTON.app.0.2c908d51");

            aux.WinWait(GROUPDELETETITLE);
            aux.ControlClick(GROUPDELETETITLE, "", "WindowsForms10.BUTTON.app.0.2c908d53");

            aux.WinWait(GROUPWINTITLE);
            CloseGroupsDialog();
        }

        public void Add(GroupData newGroup)
        {
            OpenGroupsDialog();

            aux.ControlClick(GROUPWINTITLE, "", "WindowsForms10.BUTTON.app.0.2c908d53");
            aux.Send(newGroup.Name);
            aux.Send("{ENTER}");

            CloseGroupsDialog();
        }

        public void OpenGroupsDialog()
        {
            aux.ControlClick(WINTITLE, "", "WindowsForms10.BUTTON.app.0.2c908d512");
            aux.WinWait(GROUPWINTITLE);
        }

        public void CloseGroupsDialog()
        {
            aux.ControlClick(GROUPWINTITLE, "", "WindowsForms10.BUTTON.app.0.2c908d54");
        }

        public int CountGroups()
        {
            OpenGroupsDialog();

            string count = aux.ControlTreeView(GROUPWINTITLE, "", "WindowsForms10.SysTreeView32.app.0.2c908d51", "GetItemCount", "#0", "");

            CloseGroupsDialog();

            return int.Parse(count);
        }

        public void CreateGroupIfExistLessTwoGroups()
        {
            if (CountGroups() <= 1)
                Add(new GroupData { Name = "Generated" } );
        }

    }
}