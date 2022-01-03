using System.Collections.Generic;

namespace com.baensi.sdon.protocol.entities
{

    public class LoadResponseModel : TransportEntity
    {

        public IEnumerable<UserBag> UserBags { get; set; }

        public IEnumerable<UserEquip> UserEquips { get; set; }

        public UserExp UserExp { get; set; }

        public IEnumerable<UserSkill> UserSkills { get; set; }

        public IEnumerable<UserItem> UserItems { get; set; }

        public UserState UserState { get; set; }

    }

}
