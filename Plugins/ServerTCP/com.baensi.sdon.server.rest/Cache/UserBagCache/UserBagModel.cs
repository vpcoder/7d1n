using com.baensi.sdon.db.entity;
using System.Collections.Generic;

namespace com.baensi.sdon.server.cache
{

    public class UserBagModel
    {

        public UserBag Bag { get; set; }

        public List<UserItem> Items { get; set; }

    }

}
