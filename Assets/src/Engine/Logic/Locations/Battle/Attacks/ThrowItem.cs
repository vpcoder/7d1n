using Engine.Data;
using Engine.Logic.Locations.Objects;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Engine.Logic.Locations
{

    /// <summary>
    /// Метательный предмет
    /// </summary>
    public class ThrowItem : MonoBehaviour
    {

        /// <summary>
        /// Префаб предмета, который появится на земле после того как упадёт, если null - ничего не появится
        /// </summary>
        [SerializeField] private LocationDroppedItemBehaviour droppedPrefab;

        [SerializeField] private float speed = 5f;
        [SerializeField] private float reactDistance = 0.1f;
        [SerializeField] private float maxDistance = 20f;
        private IAttackCharacter source;

        private Vector3 targetPos;
        private Vector3 startPos;
        private IEdgedWeapon weapon;

        private bool isInited = false;

        private float timestamp;

        public void Init(IAttackCharacter source, Vector3 startPos, Vector3 targetPos, IEdgedWeapon weapon)
        {
            this.transform.position = startPos;
            this.transform.LookAt(targetPos);
            this.targetPos = targetPos;
            this.startPos = startPos;
            this.weapon = weapon;
            this.source = source;
            this.isInited = true;
            timestamp = Time.time;
        }

        private void Update()
        {
            if (!isInited)
                return;

            float delta = (Time.time - timestamp);
            var prevDistance = Vector3.Distance(this.transform.position, targetPos);
            this.transform.position += (this.transform.forward * speed) * delta;
            var distance = Vector3.Distance(this.transform.position, targetPos);

            timestamp = Time.time;

            if (distance > prevDistance || distance <= reactDistance)
            {
                DoDamage();
                GameObject.Destroy(this.gameObject);
                return;
            }

            if(distance > maxDistance)
            {
                GameObject.Destroy(gameObject);
            }
        }

        private List<GameObject> FindDamagedObjects() {
            var hits = Physics.RaycastAll(startPos, (targetPos - startPos).normalized, maxDistance);
            if (hits == null || hits.Length == 0)
                return new List<GameObject>();
            return hits.Where(item => item.collider.gameObject != source.ToObject
                            && item.collider.gameObject.GetComponent<IDamagedObject>() != null)
                       .Select(item => item.collider.gameObject)
                       .ToList();
        }

        /// <summary>
        /// Предмет долетел до цели, просчитываем во что он попал
        /// </summary>
        private void DoDamage()
        {
            var damagedObjects = FindDamagedObjects();
            if (damagedObjects.Count == 0)
                return;

            IDamagedObject enemy = damagedObjects.Select(item => item.transform.GetComponent<IDamagedObject>())
                                                 .Where(item => item != null)
                                                 .FirstOrDefault();

            if (enemy == null)
                return; // Не дамажный объект, пролетаем насквозь

            // Наносим урон
            BattleCalculationService.DoEdgedThrowDamage(source, enemy, weapon, this);

            // Предмет после попадания падает на землю, или дематериализуется?
            if (droppedPrefab != null)
            {
                // Выкидываем предмет на карту
                var dropped = GameObject.Instantiate<LocationDroppedItemBehaviour>(droppedPrefab);
                dropped.Init(ItemSerializator.Convert(weapon), targetPos);
            }

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
