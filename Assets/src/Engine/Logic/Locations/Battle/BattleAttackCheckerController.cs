using Engine.Data;
using Engine.Logic.Locations.Battle.Actions;
using UnityEngine;

namespace Engine.Logic.Locations
{

    /// <summary>
    /// Контроллер, необходимый для выполнения целеуказания атак игрока
    /// Общий принцип:
    ///     - игрок сначала жмёт на действие-команду (DoHandsActionClick)
    ///     - далее игрок жмёт на цель, куда проставляется маркер цели (SetupMarkerPos)
    /// </summary>
    public class BattleAttackCheckerController : MonoBehaviour
    {

        /// <summary>
        /// Префаб маркера атаки
        /// </summary>
        [SerializeField] private GameObject attackMarkerPrefab;

        /// <summary>
        /// Если этот флаг поднят, значит отпускание нажатого пальца спровоцирует поднятие флага needAddMark
        /// </summary>
        private bool checkFlag = false;

        /// <summary>
        /// Если этот флаг поднят, значит необходимо установить маркер атаки в точку нажатия
        /// </summary>
        private bool needAddMark = false;

        /// <summary>
        /// Тип последнего совершённого действия
        /// </summary>
        private HandActionType type;

        /// <summary>
        /// Следит за нажатиями пальцев, основываясь на флагах checkFlag и needAddMark
        /// </summary>
        private void Update()
        {
            // Нужно ли вообще следить за пальцами?
            if (Game.Instance.Runtime.Mode != Mode.Battle || (!checkFlag && !needAddMark))
                return; // Не нужно...

            // Смотрим на пальцы

            if(DeviceInput.GetTouchPhase == TouchPhase.Ended && checkFlag)
            {
                needAddMark = true;
                checkFlag = false;
            }

            if(DeviceInput.TouchCount > 0 && DeviceInput.GetTouchPhase == TouchPhase.Began && needAddMark)
            {
                SetupMarkerPos();
                needAddMark = false;
            }
        }

        /// <summary>
        /// Объект по которому можно ударить в ближней атаке, находящийся под курсором-маркером
        /// </summary>
        /// <returns>Первый попавшийся объект, сверху-вниз или null, если ничего не нашлось</returns>
        private IDamagedObject GetFirstSelectedDamagedObject(Vector3 worldPos)
        {
            var hits = Physics.RaycastAll(Camera.main.transform.position, (worldPos - Camera.main.transform.position).normalized, 100f);
            if (hits == null || hits.Length == 0)
                return null;

            foreach(var hit in hits)
            {
                var damagedObject = hit.collider.transform.GetComponent<IDamagedObject>();
                if (damagedObject != null)
                    return damagedObject;
            }

            return null;
        }

        /// <summary>
        /// Убирает маркер атаки
        /// </summary>
        private void HideMarker()
        {
            var controller = ObjectFinder.Find<BattleActionsController>();
            if (controller.AttackContext.AttackMarker != null)
                GameObject.Destroy(controller.AttackContext.AttackMarker);
            controller.AttackContext.AttackMarker = null;
            controller.UpdateState();
        }

        /// <summary>
        /// Устанавливает маркер атаки
        /// </summary>
        public void SetupMarkerPos()
        {
            var controller = ObjectFinder.Find<BattleActionsController>();
            var touchPos = DeviceInput.TouchPosition;
            var worldPos = Camera.main.ScreenToWorldPoint(touchPos);
            var selected = GetFirstSelectedDamagedObject(worldPos);
            if (type == HandActionType.AttackEdged)
            {
                if(selected == null)
                {
                    HideMarker();
                    return;
                }
                else
                {
                    var character = ObjectFinder.Find<LocationCharacter>();
                    var distance = Vector3.Distance(character.transform.position, selected.ToObject.transform.position);

                    if (distance > 0.8f) // Слишком далеко
                    {
                        HideMarker();
                        return;
                    }

                    worldPos = selected.ToObject.transform.position;
                    controller.AttackContext.Target = selected;
                }
            }

            if (controller.AttackContext.AttackMarker == null)
                controller.AttackContext.AttackMarker = GameObject.Instantiate<GameObject>(attackMarkerPrefab);

            controller.AttackContext.AttackMarker.transform.position = worldPos;
            controller.UpdateState();
        }

        /// <summary>
        /// Игрок нажал на действие, следующее нажатие игрока может быть установкой маркера атаки, если будет поднят checkFlag
        /// </summary>
        /// <param name="action">Действие которое будет совершаться</param>
        /// <param name="weapon">Оружие, которым совершается действие</param>
        public void DoHandsActionClick(HandActionType action, IWeapon weapon)
        {
            var controller = ObjectFinder.Find<BattleActionsController>();
            controller.AttackContext.Action = action;
            controller.AttackContext.Weapon = weapon;
            controller.Action = BattleAction.Attack;

            checkFlag = false;
            type = action;
            switch (action)
            {
                case HandActionType.AttackFirearms:
                    controller.NeedAP = weapon.UseAP;
                    needAddMark = false;
                    checkFlag = true;
                    break;
                case HandActionType.AttackEdged:
                    controller.NeedAP = weapon.UseAP;
                    needAddMark = false;
                    checkFlag = true;
                    break;
                case HandActionType.ReloadFirearms:
                    var firearms = (IFirearmsWeapon)weapon;
                    controller.NeedAP = firearms.ReloadAP;
                    break;
                case HandActionType.ThrowGrenade:
                    var grenade = (IGrenadeWeapon)weapon;
                    controller.NeedAP = grenade.UseAP;
                    needAddMark = false;
                    checkFlag = true;
                    break;
                case HandActionType.ThrowEdged:
                    var edged = (IEdgedWeapon)weapon;
                    controller.NeedAP = edged.ThrowAP;
                    needAddMark = false;
                    checkFlag = true;
                    break;
            }

            controller.UpdateState();
            controller.Show();

        }

    }

}
