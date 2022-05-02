using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Engine.Logic.Locations
{

    /// <summary>
    /// Контроллер действий руками
    /// 
    /// Атаковать
    /// Бросить
    /// Перезарядить
    /// 
    /// </summary>
    public class HandsActionsController : MonoBehaviour
    {

        #pragma warning disable 0649, IDE0044, CS0414

        [SerializeField] private List<HandsActionItem> actionItems;
        [SerializeField] private HandsController handsController;

        #pragma warning restore 0649, IDE0044, CS0414

        public HandActionType SelectedAction { get; set; }


        public void ShowActions(params HandActionType[] actions)
        {
            foreach (var item in actionItems)
                if(actions.Contains(item.Action))
                    item.Show();
        }

        public void HideActions()
        {
            foreach (var item in actionItems)
            {
                item.DoUnselect();
                item.Hide();
            }
        }

        public void DoUnselectActions()
        {
            foreach (var item in actionItems)
                item.DoUnselect();
        }

        public void Select(HandsActionItem selected)
        {
            DoUnselectActions();
            selected.DoSelect();
            SelectedAction = selected.Action;
            handsController.DoSelectHandAction(SelectedAction);
        }

    }

}
