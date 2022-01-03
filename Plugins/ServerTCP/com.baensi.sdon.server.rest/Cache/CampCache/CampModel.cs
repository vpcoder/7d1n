using com.baensi.sdon.db.entity;
using System.Collections.Generic;

namespace com.baensi.sdon.server.cache
{

    public class CampModel
    {

        public Camp Camp { get; set; }

        public List<CampBuild> Builds { get; set; }

        public List<CampLoot> Loot { get; set; }

    }

}
