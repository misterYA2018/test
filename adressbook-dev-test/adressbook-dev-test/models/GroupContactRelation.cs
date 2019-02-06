﻿using LinqToDB.Mapping;

namespace WebAddressbookTests
{
    [Table(Name = "address_in_groups")]
    public class GroupContactRelation
    {
        [Column(Name = "group_id")]
        public string GroupID { get; set; }

        [Column(Name = "id")]
        public string ContactID { get; set; }

    }
}
