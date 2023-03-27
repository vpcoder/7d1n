using Engine.Data;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Engine.Logic.Locations
{

    /// <summary>
    /// 
    /// Метательный предмет
    /// Примером может быть нож, сюрикен, копьё, дротик и т.д.
    /// В конечном итоге, при выставленном dropToGround предмет окажется на локации (при выключенном dropToGround предмет уничтожится)
    /// В момент инициализации метательный предмет определяет ПЕРВУЮ цель на пути полёта, и ЗАСТРЕВАЕТ в ней
    /// ---
    /// A throwing object.
    /// Examples are knife, shuriken, spear, dart, etc.
    /// Eventually, if dropToGround is set, the item will end up in the location (if dropToGround is set off, the item will be destroyed)
    /// At the moment of initialization, the throwing object detects the FIRST target on the flight path, and STAYS in it
    /// 
    /// </summary>
    public class ThrowItem : MonoBehaviour
    {

        #region Hidden Fields

        /// <summary>
        ///     Скорость полёта предмета
        ///     ---
        ///     The speed of an object
        /// </summary>
        [SerializeField] private float speed = 5f;

        /// <summary>
        ///     Расстояние до цели, при котором считается что снаряд долетел
        ///     ---
        ///     Distance to the target at which the object is considered to have reached
        /// </summary>
        [SerializeField] private float reactDistance = 0.001f;

        /// <summary>
        ///     Максимально возможное расстояние полёта предмета (по нему считается Raycast)
        ///     ---
        ///     Maximum possible distance of an object's flight (the Raycast is calculate by it)
        /// </summary>
        [SerializeField] private float maxDistance = 100f;

        /// <summary>
        ///     Определяет что происходит с предметом в итоге:
        ///         true - Сбрасывается на землю
        ///         false - Уничтожается без следа
        ///     ---
        ///     Determines what happens to the item as a result:
        ///         true - Dropped to the ground
        ///         false - Destroyed without trace
        /// </summary>
        [SerializeField] private bool dropToGround = true;

        private IAttackObject source;
        private Vector3 targetPos;
        private Vector3 startPos;
        private IEdgedWeapon weapon;
        private bool isInited = false;
        private float timestamp;
        private Vector3 forward;
        private float pathLength;
        private float distance;
        private GameObject damagedObject;

        #endregion

        #region Unity Events

        private void Update()
        {
            if (!isInited)
                return;

            float delta = (Time.time - timestamp);
            //var prevDistance = Vector3.Distance(this.transform.position, targetPos);
            var offset = speed * delta;
            pathLength += offset;
            this.transform.position += forward * offset;
            var distance = Vector3.Distance(this.transform.position, targetPos);

            timestamp = Time.time;

            if (pathLength > this.distance || distance <= reactDistance)
            {
                DoDamage();
                DoDestory();
                return;
            }

            if (distance > maxDistance)
            {
                DoDestory();
            }
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="startPos"></param>
        /// <param name="targetPos"></param>
        /// <param name="weapon"></param>
        public void Init(IAttackObject source, Vector3 startPos, Vector3 targetPos, IEdgedWeapon weapon)
        {
            this.transform.position = startPos;
            this.transform.LookAt(targetPos);
            this.pathLength = 0f;
            this.startPos = startPos;
            this.distance = Mathf.Min(maxDistance, ThrowCalculationService.GetThrowDistance(weapon));
            this.forward = (targetPos - startPos).normalized;
            this.targetPos = forward * distance;
            this.weapon = weapon;
            this.source = source;
            this.isInited = true;
            this.timestamp = Time.time;
            this.damagedObject = FindDamagedObject();
        }

        /// <summary>
        /// 
        /// </summary>
        private void DoDestory()
        {
            DoDrop();
            GameObject.Destroy(gameObject);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private GameObject FindDamagedObject() {
            var hits = Physics.RaycastAll(startPos, forward, maxDistance);
            if (hits == null || hits.Length == 0)
                return null;
            
            var hit = hits.Where(item =>
            {
                var target = item.collider.gameObject.GetComponent<IFragmentDamaged>();
                return target != null && target.Damaged != source.Damaged;
            }).FirstOrDefault();
            if (hit.collider?.gameObject == null)
                return null;

            GameObject first = hit.collider.gameObject;
            this.targetPos = hit.point;
            this.distance = Vector3.Distance(targetPos, startPos);

            return first;
        }

        /// <summary>
        /// Предмет долетел до цели, просчитываем во что он попал
        /// </summary>
        private void DoDamage()
        {
            if (damagedObject == null)
                return; // Ни во что не врезались

            IFragmentDamaged fragment = damagedObject.transform.GetComponent<IFragmentDamaged>();
            IDamagedObject damaged = null;

            if (fragment != null)
                damaged = fragment.Damaged;
            
            if (damaged == null)
                return; // Не дамажный объект, пролетаем насквозь

            // Наносим урон
            BattleCalculationService.DoEdgedThrowDamage(source, damaged, weapon, this);
        }

        /// <summary>
        /// 
        /// </summary>
        private void DoDrop()
        {
            if (!dropToGround)
                return;

            // Выкидываем предмет на карту
            ObjectFinder.Find<ItemsDropController>().Drop(targetPos, false, ItemSerializator.Convert(weapon));
        }

#if UNITY_EDITOR && DEBUG

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(startPos, targetPos);
        }

#endif

    }

}
