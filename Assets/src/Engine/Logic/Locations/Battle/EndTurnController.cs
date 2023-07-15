using Engine.Data;
using Engine.EGUI;
using UnityEngine;

namespace Engine.Logic.Locations
{

    /// <summary>
    /// 
    /// Обработчик конца хода игрока
    /// ---
    /// Player end of turn handler
    /// 
    /// </summary>
    public class EndTurnController : Panel
    {

        public void DoEndTurnClick()
        {
            if(!Game.Instance.Runtime.BattleFlag || Game.Instance.Runtime.BattleContext.OrderIndex != OrderGroup.PlayerGroup)
            {
                Hide();
                return;
            }

            ObjectFinder.BattleManager.DoNextGroupTurn();
            Hide();
        }

    }

}
