using com.baensi.sdon.db.dao;
using System.Linq;

namespace com.baensi.sdon.server.cache
{

    public class UserCacheDictionary : DataDictionary<UserCacheModel>
    {

        public override UserCacheModel AutoUpdate(int id)
        {
            var user    = DaoFactory.Instance.User.Get(id);
            var exp     = DaoFactory.Instance.UserExp.GetFirst(o => o.UserId == id);
            var state   = DaoFactory.Instance.UserState.GetFirst(o => o.UserId == id);
            var devices = DaoFactory.Instance.UserDevice.GetWhere(o => o.UserId == id).ToList();
            var skills  = DaoFactory.Instance.UserSkill.GetWhere(o => o.UserId == id).ToList();

            return new UserCacheModel()
            {
                User    = user,
                Exp     = exp,
                State   = state,
                Devices = devices,
                Skills  = skills
            };
        }

        public override UserCacheModel EagerUpdate(int id)
        {
            var tmp = Get(id);

            tmp.User    = DaoFactory.Instance.User.Get(id);
            tmp.Exp     = DaoFactory.Instance.UserExp.GetFirst(o => o.UserId == id);
            tmp.State   = DaoFactory.Instance.UserState.GetFirst(o => o.UserId == id);
            tmp.Devices = DaoFactory.Instance.UserDevice.GetWhere(o => o.UserId == id).ToList();
            tmp.Skills  = DaoFactory.Instance.UserSkill.GetWhere(o => o.UserId == id).ToList();

            return tmp;
        }

    }

}
