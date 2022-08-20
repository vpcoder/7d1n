using System.Collections.Generic;
using Engine.Data;
using Engine.Data.Blueprints.Base;
using Engine.Data.Factories;
using UnityEngine;
using UnityEngine.UICellView;

namespace Engine.Logic
{

    public class CraftController : CellView<CraftBlueprintItem, IBlueprint>
    {
        
        [SerializeField] private BlueprintGroupType groupType = BlueprintGroupType.Survive;

        private CraftBlueprintItem selected;
        
        #region Events

        public override void InitItem(IBlueprint model, CraftBlueprintItem item, int index)
        {
            item.Init(model);
        }

        public override void DisposeItem(CraftBlueprintItem item)
        { }

        public override ICollection<IBlueprint> ProvideModels()
        {
            var knownBlueprintIds = Game.Instance.Character.Skills.GetBlueprints();
            var list = new LinkedList<IBlueprint>();
            foreach (var item in ItemFactory.Instance.GetAll(knownBlueprintIds))
            {
                var blueprint = item as IBlueprint;
                if (blueprint == null)
                {
#if UNITY_EDITOR
                    Debug.LogError("item id '" + item.ID + "' can't be blueprint!");
#endif
                    continue;
                }
                if (blueprint.BlueprintGroupType == groupType)
                    list.AddLast(blueprint);
            }
            return list;
        }

        #endregion

        public void OnGroupClick(GroupItemBehaviour group)
        {
            if (groupType == group.Group)
                return;
            this.groupType = group.Group;
            Redraw();
        }

        public void SelectBlueprint(CraftBlueprintItem item, IBlueprint blueprint)
        {
            if (selected == item)
                return;
            
            selected?.OnDeselect();
            selected = item;
        }

    }

}
