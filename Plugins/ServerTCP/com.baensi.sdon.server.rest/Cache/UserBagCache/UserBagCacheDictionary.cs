using com.baensi.sdon.db.dao;
using com.baensi.sdon.db.entity;
using System.Linq;

namespace com.baensi.sdon.server.cache
{

    public class UserBagCacheDictionary : DataDictionary<UserBagCacheModel>
    {

        public override UserBagCacheModel AutoUpdate(int id)
        {
            var bags   = DaoFactory.Instance.UserBag.GetWhere(o => o.UserId == id).ToList();
            var equips = DaoFactory.Instance.UserEquip.GetWhere(o => o.UserId == id).ToList();

            return new UserBagCacheModel()
            {
                Bags = bags.Select(o => MakeBag(o)).ToList(),
                Equips = equips
            };
        }

        public override UserBagCacheModel EagerUpdate(int id)
        {
            var tmp = Get(id);

            tmp.Bags   = DaoFactory.Instance.UserBag.GetWhere(o => o.UserId == id).Select(o => MakeBag(o)).ToList();
            tmp.Equips = DaoFactory.Instance.UserEquip.GetWhere(o => o.UserId == id).ToList();

            return tmp;
        }

        private UserBagModel MakeBag(UserBag bag)
        {
            var model = new UserBagModel();

            model.Bag   = bag;
            model.Items = DaoFactory.Instance.UserItem.GetWhere(o => o.BagId == bag.Id).ToList();

            return model;
        }

    }

}
