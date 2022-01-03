using Engine.Data;
using UnityEngine;

namespace Engine.Logic.Locations
{

    /// <summary>
    /// Обработчик конца хода игрока
    /// </summary>
    public class EndStepController : MonoBehaviour
    {

        [SerializeField] private GameObject body;

        public void Show()
        {
            body.SetActive(true);
        }

        public void Hide()
        {
            body.SetActive(false);
        }


        public void DoEndStepClick()
        {
            if(Game.Instance.Runtime.BattleContext.OrderIndex != EnemyGroup.PlayerGroup)
            {
                Hide();
                return;
            }

            var manager = ObjectFinder.Find<BattleManager>();
            manager.DoNextOrder();
            Hide();
        }

    }

}
