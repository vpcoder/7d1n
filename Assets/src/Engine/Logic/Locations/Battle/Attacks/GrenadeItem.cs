using Engine.Data;
using Engine.Data.Factories;
using System.Collections.Generic;
using UnityEngine;

namespace Engine.Logic.Locations
{

    /// <summary>
    /// 
    /// Снаряд который представляет собой гранату, которая должна будет взорваться в конце пути
    /// ---
    /// A projectile which is a grenade that will have to explode at the end of the path
    /// 
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
        private bool isInited;
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

            // Звук броска гранаты
            AudioController.Instance.CreateTimedFragment(transform.position, MixerType.Sounds, weapon.ThrowSoundType);
        }

        private void Update()
        {
            if (!isInited)
                return;

            float delta = (Time.time - timestamp);
            var prevDistance = Vector3.Distance(this.transform.position, targetPos);
            this.transform.position += this.transform.forward * (speed * delta);
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

        /// <summary>
        ///     Поиск всех кто попадает под взрывную волну и кто может получать урон
        ///     ---
        ///     Search for anyone who is hit by the blast wave and who can take damage
        /// </summary>
        /// <returns>
        ///     Список объектов, которые попали под удар
        ///     ---
        ///     List of facilities that have been hit
        /// </returns>
        private List<GameObject> FindDamagedObjects() {
            var list = new List<GameObject>();
            
            var colliders =  Physics.OverlapSphere(transform.position, weapon.Radius);
            if (colliders.Length == 0)
                return list;
            
            for (int i = colliders.Length - 1; i >=0; i--)
            {
                var damaged = colliders[i];
                if (damaged.gameObject.GetComponent<IDamagedObject>() != null)
                    list.Add(damaged.gameObject);
            }
            
            return list;
        }

        /// <summary>
        ///     Взрываем гранату и уничтожаем текущий объект
        ///     ---
        ///     Detonate the grenade and destroy the current object
        /// </summary>
        private void DoDestroy()
        {
            // Эффект взрыва
            GrenadeEffectFactory.Instance.CreatePrefabInstance(weapon.ExplodeEffectType, transform.position);
            
            // Звук взрыва
            AudioController.Instance.CreateTimedFragment(transform.position, MixerType.Sounds, weapon.ExplodeSoundType);
            
            // Уничтожаем текущий объект
            Destroy(gameObject);
        }

        /// <summary>
        ///     Раскидываем урон всем кому прилетело
        ///     ---
        ///     Spread the damage to everyone who is hit
        /// </summary>
        private void DoDamage()
        {
            var damagedObjects = FindDamagedObjects();
            if (damagedObjects.Count == 0)
                return;

            foreach (var item in damagedObjects) // Передаём урон всем кто попал под сферокаст
            {
                var character = item.transform.GetComponent<IDamagedObject>();
                if (character == null)
                    continue; // Этому объекту нельзя нанести урон

                // Наносим урон взрывом
                BattleCalculationService.DoGrenadeDamage(source, character, weapon, this);
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
