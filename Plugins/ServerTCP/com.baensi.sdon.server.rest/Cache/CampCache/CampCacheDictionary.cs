using com.baensi.sdon.db.dao;
using com.baensi.sdon.db.entity;
using System.Linq;

namespace com.baensi.sdon.server.cache
{

    public class CampCacheDictionary : DataDictionary<CampCacheModel>
    {

        public override CampCacheModel AutoUpdate(int id)
        {
            var camps = DaoFactory.Instance.Camp.GetWhere(o => o.UserId == id).ToList();

            return new CampCacheModel()
            {
                Camps = camps.Select(o => MakeCamp(o)).ToList()
            };
        }

        public override CampCacheModel EagerUpdate(int id)
        {
            var tmp = Get(id);

            tmp.Camps = DaoFactory.Instance.Camp.GetWhere(o => o.UserId == id).Select(o => MakeCamp(o)).ToList();

            return tmp;
        }

        private CampModel MakeCamp(Camp camp)
        {
            var model = new CampModel();

            model.Camp   = camp;
            model.Builds = DaoFactory.Instance.CampBuild.GetWhere(o => o.CampId == camp.Id).ToList();
            model.Loot   = DaoFactory.Instance.CampLoot.GetWhere(o => o.CampId == camp.Id).ToList();

            return model;
        }

    }

}
