using Engine.Data;
using Engine.Data.Factories;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Engine.Logic.Locations
{

    /// <summary>
    /// Граната
    /// </summary>
    public class GrenadeItem : MonoBehaviour
    {

        [SerializeField] private float speed = 25f;
        [SerializeField] private float reactDistance = 0.1f;
        [SerializeField] private float maxDistance = 20f;
        private IAttackObject source;

        private Vector3 targetPos;
        private Vector3 startPos;
        private IGrenadeWeapon weapon;

        private bool isInited = false;

        private float timestamp;

        public void Init(IAttackObject source, Vector3 startPos, Vector3 targetPos, IGrenadeWeapon weapon)
        {
            this.transform.position = startPos;
            this.transform.LookAt(targetPos);
            this.targetPos = targetPos;
            this.startPos = startPos;
            this.weapon = weapon;
            this.source = source;
            this.isInited = true;
            timestamp = Time.time;

            AudioController.Instance.CreateTimedFragment(transform.position, MixerType.Sounds, weapon.ThrowSoundType);
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
                DoDestroy();
                return;
            }

            if(distance > maxDistance)
            {
                DoDestroy();
            }
        }

        private List<GameObject> FindDamagedObjects() {
            var hits = Physics.SphereCastAll(transform.position, weapon.Radius, transform.forward);
            if (hits == null || hits.Length == 0)
                return new List<GameObject>();
            return hits.Where(item => item.collider.gameObject.GetComponent<IDamagedObject>() != null)
                       .Select(item => item.collider.gameObject)
                       .ToList();
        }

        private void DoDestroy()
        {
            GrenadeEffectFactory.Instance.CreatePrefabInstance(weapon.ExplodeEffectType, transform.position);
            AudioController.Instance.CreateTimedFragment(transform.position, MixerType.Sounds, weapon.ExplodeSoundType);
            Destroy(gameObject);
        }

        /// <summary>
        /// Предмет долетел до цели, просчитываем что он задел взрывом
        /// </summary>
        private void DoDamage()
        {
            var damagedObjects = FindDamagedObjects();
            if (damagedObjects.Count == 0)
                return;

            foreach (var item in damagedObjects) // Передаём урон всем кто попал под сферокаст
            {
                var enemy = item.transform.GetComponent<IDamagedObject>();
                if (enemy == null)
                    continue; // Этому объекту нельзя нанести урон

                // Наносим урон взрывом
                BattleCalculationService.DoGrenadeDamage(source, enemy, weapon, this);
            }
        }

#if UNITY_EDITOR && DEBUG

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(startPos, targetPos);

            if(weapon != null)
                Gizmos.DrawWireSphere(targetPos, weapon.Radius);
        }

#endif

    }

}
