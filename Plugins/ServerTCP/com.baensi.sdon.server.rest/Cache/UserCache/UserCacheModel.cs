using com.baensi.sdon.db.entity;
using System.Collections.Generic;

namespace com.baensi.sdon.server.cache
{

    public class UserCacheModel
    {

        public User User { get; set; }

        public List<UserDevice> Devices { get; set; }

        public UserExp Exp { get; set; }
        
        public List<UserSkill> Skills { get; set; }

        public UserState State { get; set; }

    }

}
