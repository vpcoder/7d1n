using Engine.Data;
using Engine.Logic.Locations.Battle.Actions;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Engine.Logic.Locations
{

    /// <summary>
    ///
    /// Контроллер, обрабатывающий клики по предметам
    /// Данный компонент вешается на объект, на который планируется совершать нажатия, и выполнять заданные действия
    /// ---
    /// Controller handling object clicks
    /// This component hangs on the object you plan to click on and perform specified actions
    /// 
    /// </summary>
    [RequireComponent(typeof(LocationObjectItemBehaviour))]
    public class ItemSelectController : MonoBehaviour
    {

        /// <summary>
        ///     Время нажатия, чтобы рассчитать задержку нажатия
        ///     ---
        ///     Press time to calculate the press delay
        /// </summary>
        private float downTime;

        private void OnMouseDown()
        {
            if (!CheckDistance())
                return;

            downTime = Time.time;
        }

        private void OnMouseUp()
        {
            if (!CheckDistance())
                return;

            if (Time.time - downTime < 0.4f) // Это обычный клик на объекте
            {
                OnUseClick();
                return;
            }

            //if(Game.Instance.Runtime.Mode == Mode.Game) // Это клик с задержкой, нужно развернуть меню
            OnMenuClick();
        }

        /// <summary>
        ///     Меню объекта, разворачивается при длительном нажатии на объект
        ///     ---
        ///     Object menu, unfolds with a long press on the object
        /// </summary>
        public void OnMenuClick()
        {
            var item = GetComponent<LocationObjectItemBehaviour>();
            ObjectFinder.Find<ActionPanelController>().Show(item);
        }

        /// <summary>
        ///     Использование объекта, выполняется при коротком нажатии на объект
        ///     ---
        ///     Using an object, performed with a short click on the object
        /// </summary>
        public void OnUseClick()
        {
            if (Game.Instance.Runtime.Mode != Mode.Game
                && Game.Instance.Runtime.BattleContext.OrderIndex != OrderGroup.PlayerGroup) // Не наш ход
                return;
            
            var item = GetComponent<LocationObjectItemBehaviour>();
            var useItem = item.GetComponent<LocationObjectBattleUseController>();
            if(useItem != null)
            {
                var battleActions = ObjectFinder.Find<BattleActionsController>();

                battleActions.Show(); // Отображаем панель действия, чтобы пользователь сказал - совершать его или нет
                battleActions.NeedAP = useItem.AP;
                battleActions.Action = CharacterBattleAction.Use;
                battleActions.UseContext.UseItem = useItem;
                battleActions.UpdateState();
            }
        }

        /// <summary>
        ///     Определяет, насколько персонаж далеко от нажимаемого объекта, может персонаж не достаёт до объекта...
        ///     ---
        ///     Determines how far the character is from the pressed object, maybe the character does not reach the object...
        /// </summary>
        /// <returns>
        ///     true - если персонаж достаточно близко,
        ///     false - иначе
        ///     ---
        ///     true - if the character is close enough,
        ///     false - otherwise
        /// </returns>
        private bool CheckDistance()
        {
            var character = ObjectFinder.Find<LocationCharacter>();
            var distance = Vector3.Distance(transform.position, character.transform.position);
            return distance <= character.PickUpDistance * 3f;
        }

    }

}
