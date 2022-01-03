using Engine.Data;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Engine.Logic.Locations
{

    /// <summary>
    /// Снаряд
    /// </summary>
    public class BulletItem : MonoBehaviour
    {

        [SerializeField] private float speed = 25f;
        [SerializeField] private float reactDistance = 0.1f;
        [SerializeField] private float maxDistance = 20f;

        private IAttackCharacter source;

        private Vector3 targetPos;
        private Vector3 startPos;
        private IFirearmsWeapon weapon;

        private bool isInited = false;

        private float timestamp;

        public void Init(IAttackCharacter source, Vector3 startPos, Vector3 targetPos, IFirearmsWeapon weapon)
        {
            this.transform.position = startPos;
            this.transform.LookAt(targetPos);
            this.targetPos = targetPos;
            this.startPos = startPos;
            this.weapon = weapon;
            this.source = source;
            this.isInited = true;
            timestamp = Time.time;

            AudioController.Instance.CreateTimedFragment(source.ToObject.transform.position, MixerType.Sounds, weapon.ShootSoundType);
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

        private void DoDamage()
        {
            var damagedObjects = FindDamagedObjects();
            if (damagedObjects.Count == 0)
                return;

            foreach (var item in damagedObjects) // Пробиваем список объектов
            {
                var enemy = item.transform.GetComponent<IDamagedObject>();
                if (enemy == null)
                    continue; // Этому объекту нельзя нанести урон

                // Наносим урон снарядом
                BattleCalculationService.DoBulletDamage(source, enemy, weapon, this);

                var penetration = Mathf.Max(weapon.Penetration - enemy.Protection, 0f);
                var isPenetrated = Random.Range(0f, 100f) <= penetration;

                if (!isPenetrated) // Пробиваем насквозь?
                    break; // Не пробили, прекращаем пробивать объекты
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
