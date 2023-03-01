using Engine.Data;
using Engine.EGUI;
using Engine.Logic.Location;
using Engine.Logic.Locations.Animation;
using Engine.Logic.Locations.Battle.Actions;
using Engine.Logic.Locations.Generator;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Engine.Logic.Locations.Battle
{

    /// <summary>
    ///
    /// Контроллер управляющий маркером прицеливания
    /// Позволяет наводиться на маркер прицеливания для совершения атаки
    /// ---
    /// The controller that controls the aiming marker
    /// Allows aiming at the aiming marker to execute an attack
    /// 
    /// </summary>
    public class CharacterAimController : Panel,
                                          IDragHandler,
                                          IPointerDownHandler
    {

        #region Hidden Fields
        
        /// <summary>
        ///     Префаб маркера атаки
        ///     ---
        ///     Prefab of the attack marker
        /// </summary>
        [SerializeField] private GameObject attackMarkerPrefab;

        #endregion
        
        #region Properties
        
        /// <summary>
        ///     Радиус зоны прицеливания
        ///     ---
        ///     Radius of aiming zone
        /// </summary>
        public float Radius
        {
            get
            {
                return Body.transform.localScale.x;
            }
            set
            {
                Vector3 size = Vector3.one * value;
                size.y = 1;
                Body.transform.localScale = size;
            }
        }

        #endregion
        
        public void Show(float radius)
        {
            Radius = radius;
            if (radius <= 0)
            {
                Hide();
                return;
            }
            base.Show();
        }

        public override void Hide()
        {
            base.Hide();
            var controller = ObjectFinder.Find<BattleActionsController>();

            if (controller.AttackContext.AttackMarker != null)
                GameObject.Destroy(controller.AttackContext.AttackMarker);

            controller.AttackContext.AttackMarker = null;
        }

        private void UpdateEvent()
        {
            if (Game.Instance.Runtime.BattleContext.OrderIndex != OrderGroup.PlayerGroup) // Не ход игрока?
                return;

            if (Game.Instance.Runtime.ActionMode != ActionMode.Aim)
                return;

            var point = DeviceInput.TouchPosition;
            var ray = Camera.main.ScreenPointToRay(point);
            var hits = Physics.RaycastAll(ray, 100f);
            if (hits == null || hits.Length == 0)
                return;

            Vector3 floorHitPoint = TryFindFloor(hits);
            if (floorHitPoint == Vector3.zero)
                return;

            if (Vector3.Distance(floorHitPoint, ObjectFinder.Character.Position) > Radius)
                return;

            OnAimClick(floorHitPoint);
        }

        private Vector3 TryFindFloor(RaycastHit[] hits)
        {
            foreach(var hit in hits)
            {
                var floor = hit.transform.GetComponent<WalkableFloor>();
                if (floor != null)
                    return hit.point;
            }
            return Vector3.zero;
        }

        /// <summary>
        ///     Игрок нажал на действие, следующее нажатие игрока может быть установкой маркера атаки, если будет поднят checkFlag
        /// </summary>
        /// <param name="action">Действие которое будет совершаться</param>
        /// <param name="weapon">Оружие, которым совершается действие</param>
        public void DoHandsActionClick(HandActionType action, IWeapon weapon)
        {
            var controller = ObjectFinder.Find<BattleActionsController>();
            controller.AttackContext.Action = action;
            controller.AttackContext.Weapon = weapon;
            controller.Action = CharacterBattleAction.Attack;

            var character = ObjectFinder.Character;

            int ap = 0;
            float aimRadius = weapon.AimRadius;

            switch (action)
            {
                case HandActionType.AttackFirearms:
                    ap = weapon.UseAP;
                    break;
                case HandActionType.AttackEdged:
                    ap = weapon.UseAP;
                    break;
                case HandActionType.ReloadFirearms:
                    var firearms = (IFirearmsWeapon)weapon;
                    ap = firearms.ReloadAP;
                    aimRadius = 0;
                    break;
                case HandActionType.ThrowGrenade:
                    var grenade = (IGrenadeWeapon)weapon;
                    ap = grenade.UseAP;
                    break;
                case HandActionType.ThrowEdged:
                    var edged = (IEdgedWeapon)weapon;
                    ap = edged.ThrowAP;
                    aimRadius = edged.ThrowAimRadius;
                    break;
            }
            
            character.Animator.SetInteger(AnimationKey.WeaponEquipKey, (int)weapon.WeaponType);

            controller.NeedAP = ap;
            controller.UpdateState();
            controller.Show();

            if (Game.Instance.Runtime.ActionMode == ActionMode.Rotation) {
                var rotationController = ObjectFinder.Find<RotationActionModeController>();
                rotationController.PrevActionMode = ActionMode.Aim;
            }
            else
            {
                Game.Instance.Runtime.ActionMode = ActionMode.Aim;
            }

            Show(aimRadius);
        }

        public void OnAimClick(Vector3 floorHitPoint)
        {
            var character = ObjectFinder.Find<LocationCharacter>();
            character.transform.LookAt(floorHitPoint);

            var aimDirection = character.transform.rotation.eulerAngles;
            aimDirection.z = 0f;
            aimDirection.x = 0f;
            character.transform.rotation = Quaternion.Euler(aimDirection);

            var controller = ObjectFinder.Find<BattleActionsController>();

            if (controller.AttackContext.AttackMarker == null)
                controller.AttackContext.AttackMarker = GameObject.Instantiate(attackMarkerPrefab);

            controller.AttackContext.AttackMarker.transform.position = floorHitPoint;
            controller.AttackContext.AttackMarker.transform.rotation = character.transform.rotation;
            controller.UpdateState();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if(Game.Instance.Runtime.Mode != Mode.Game &&
               Game.Instance.Runtime.Mode != Mode.Battle)
                return;
            UpdateEvent();
        }

        public void OnDrag(PointerEventData eventData)
        {
            if(Game.Instance.Runtime.Mode != Mode.Game &&
               Game.Instance.Runtime.Mode != Mode.Battle)
                return;
            UpdateEvent();
        }

    }

}
