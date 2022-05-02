using Engine.Data;
using Engine.Logic.Locations.Battle.Actions;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Engine.Logic.Locations
{

    /// <summary>
    /// Контроллер, обрабатывающий клики по предметам
    /// Данный компонент вешается на объект, на который планируется совершать нажатия, и выполнять заданные действия
    /// </summary>
    [RequireComponent(typeof(LocationObjectItem))]
    public class ItemSelectController : MonoBehaviour
    {

        /// <summary>
        /// Время нажатия, чтобы рассчитать задержку нажатия
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
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            if (!CheckDistance())
                return;

            if (Time.time - downTime < 0.4f) // Это обычный клик на объекте
            {
                OnUseClick();
                return;
            }

            if(Game.Instance.Runtime.Mode == Mode.Game) // Это клик с задержкой, нужно развернуть меню
                OnMenuClick();
        }

        /// <summary>
        /// Меню объекта, разворачивается при длительном нажатии на объект
        /// </summary>
        public void OnMenuClick()
        {
            var item = GetComponent<LocationObjectItem>();
            ObjectFinder.Find<ActionPanelController>().Show(item);
        }

        /// <summary>
        /// Использование объекта, выполняется при коротком нажатии на объект
        /// </summary>
        public void OnUseClick()
        {
            var item = GetComponent<LocationObjectItem>();

            switch (Game.Instance.Runtime.Mode) {
                case Mode.Game:

                    var useObject = item.GetComponent<IUseObjectController>();
                    if (useObject != null)
                        useObject.DoUse();

                    break;
                case Mode.Battle:

                    if (Game.Instance.Runtime.BattleContext.OrderIndex != EnemyGroup.PlayerGroup) // Не наш ход
                        return;

                    var useItem = item.GetComponent<LocationObjectBattleUseController>();
                    if(useItem != null)
                    {
                        var battleManager = ObjectFinder.Find<BattleManager>();
                        var battleActions = battleManager.BattleActions;

                        battleActions.Show(); // Отображаем панель действия, чтобы пользователь сказал - совершать его или нет
                        battleActions.NeedAP = useItem.AP;
                        battleActions.Action = CharacterBattleAction.Use;
                        battleActions.UseContext.UseItem = useItem;
                        battleActions.UpdateState();
                    }

                    break;
            }

        }

        /// <summary>
        /// Определяет, насколько персонаж далеко от нажимаего объекта, может персонаж не достаёт до объекта...
        /// </summary>
        /// <returns>true - если персонаж достаточно близко, иначе - false</returns>
        private bool CheckDistance()
        {
            var character = ObjectFinder.Find<LocationCharacter>();
            var distance = Vector3.Distance(transform.position, character.transform.position);
            return distance <= character.PickUpDistance;
        }

    }

}
