using com.baensi.sdon.db.entity;
using System.Collections.Generic;

namespace com.baensi.sdon.server.cache
{

    public class UserBagCacheModel
    {

        public List<UserBagModel> Bags { get; set; }

        public List<UserEquip> Equips { get; set; }

    }

}
