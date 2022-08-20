using Engine.Data.Items.Used;
using System;

namespace Engine.Data.Factories
{

    public class UsedItemActionsFactory : ActionFactoryBase<UseItemActionBase>
    {

        #region Singleton

        private static readonly Lazy<UsedItemActionsFactory> instance = new Lazy<UsedItemActionsFactory>(() => new UsedItemActionsFactory());
        public static UsedItemActionsFactory Instance { get { return instance.Value; } }

        private UsedItemActionsFactory() { }

        #endregion

    }

}
