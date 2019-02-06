using System;

namespace WebAddressbookTests
{
    public class RelationHelper : HelperBase
    {
        private ApplicationManager manager;

        public RelationHelper(ApplicationManager manager)
            : base(manager)
        {
            this.manager = manager;
        }

        public void AddGroupContactRelationInDB(GroupContactRelation gcr)
        {
            ExecuteCmd( $"insert into address_in_groups set group_id = {gcr.GroupID}, id = {gcr.ContactID}, created = '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}', modified = '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}'");
        }

        public void RemoveGroupContactRelationFromDB(GroupContactRelation gcr)
        {
            ExecuteCmd($"DELETE FROM `address_in_groups` WHERE `address_in_groups`.`id` = {gcr.ContactID} AND `address_in_groups`.`group_id` = {gcr.GroupID}");
        }
    }
}
