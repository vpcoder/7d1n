using Engine.Data;
using Engine.Logic.Locations.Objects;
using System.Collections.Generic;
using UnityEngine;

namespace Engine.Logic.Locations
{

    /// <summary>
    /// 
    /// Снаряд который выпустили из огнестрельного оружия, оружия дальнего боя, лука, арбалета и т.д.
    /// ---
    /// A projectile that was fired from a firearm, long-range weapon, bow, crossbow, etc.
    /// 
    /// </summary>
    public class BulletItem : MonoBehaviour
    {

        #region Hidden Fields

        /// <summary>
        ///     Скорость полёта снаряда
        ///     ---
        ///     Projectile flight speed
        /// </summary>
        [SerializeField] private float speed = 25f;

        /// <summary>
        ///     Дистанция на которой происходит нанесение урона от снаряда к цели в игровых метрах
        ///     ---
        ///     The distance at which damage occurs from the projectile to the target in game meters
        /// </summary>
        [SerializeField] private float reactDistance = 0.1f;

        /// <summary>
        ///     Максимальная дистанция полёта снаряда в игровых метрах
        ///     ---
        ///     Maximum projectile flight distance in game meters
        /// </summary>
        [SerializeField] private float maxDistance = 20f;

        /// <summary>
        ///     Персонаж, который стреляет данным снарядом
        ///     ---
        ///     The character who fires this projectile
        /// </summary>
        private IAttackObject source;

        /// <summary>
        ///     Точка от которой снаряд начинает свой полёт
        ///     ---
        ///     The point from which the projectile begins its flight
        /// </summary>
        private Vector3 targetPos;

        /// <summary>
        ///     Тока в которой снаряд заканчивает свой полёт
        ///     ---
        ///     The one in which the projectile ends its flight
        /// </summary>
        private Vector3 startPos;

        /// <summary>
        ///     Направление в котором движется снаряд
        ///     ---
        ///     The direction in which the projectile moves
        /// </summary>
        private Vector3 direction;

        /// <summary>
        ///     Оружие, из которого был произведёт выстрел
        ///     ---
        ///     The weapon from which the shot was fired
        /// </summary>
        private IFirearmsWeapon weapon;

        /// <summary>
        ///     Снаряд инициирован и готов к тому чтобы начать движение
        ///     ---
        ///     The projectile is initiated and ready to start moving
        /// </summary>
        private bool isInited = false;

        /// <summary>
        ///     Время относительно которого движется снаряд
        ///     ---
        ///     Time relative to which the projectile moves
        /// </summary>
        private float timestamp;

        /// <summary>
        ///     Список объектов через которые прошёл снаряд (которым он нанёс урон)
        ///     ---
        ///     List of objects through which the projectile passed (which it damaged)
        /// </summary>
        private IList<IDamagedObject> damagedObjects;

        /// <summary>
        ///     Следующий объект на пути снаряда, если такого нет - null
        ///     ---
        ///     The next object in the path of the projectile, if there is no such object - null
        /// </summary>
        private IDamagedObject next;

        /// <summary>
        ///     Последняя дистанция до следующего объекта
        ///     ---
        ///     The last distance to the next object
        /// </summary>
        private float nextDistance;

        /// <summary>
        ///     Спровоцировано ли застревание снаряда в объекте?
        ///     ---
        ///     Was the projectile stuck in the object provoked?
        /// </summary>
        private bool stopPenetration;

        #endregion

        #region Unity Events

        /// <summary>
        ///     Выполняет перемещение снаряда в пространстве до цели
        ///     ---
        ///     Moves the projectile in space up to the target
        /// </summary>
        private void Update()
        {
            if (!isInited)
                return;

            float deltaTime = (Time.time - timestamp);
            var beforeDistance = Vector3.Distance(this.transform.position, targetPos);
            transform.position += direction * (speed * deltaTime);
            var afterDistance = Vector3.Distance(this.transform.position, targetPos);

            timestamp = Time.time;

            if(next != null) // На пути следования снаряда есть объект?
            {
                var nextPointDistance = NextDistance;
                if(nextPointDistance < reactDistance || // Дистанция до объекта уже сопоставима с дистанцией-реакцией
                    nextDistance > nextPointDistance) // Дистанция начала расти, значит мы уже пролетели объект
                {

                    DoDamage(next); // Наносим урон тому до кого долетели
                    var destroyObject = next as IDestroyedObject;
                    if (destroyObject != null) // Если этот объект неживой, дополнительно проверяем его состояние
                        destroyObject.CheckDestroy();

                    this.next = UpdateNext(); // Переключаемся на следующую цель
                    if(this.next != null)
                        this.nextDistance = NextDistance;
                }
                else
                {
                    nextDistance = nextPointDistance; // Всё ещё движемся к цели...
                }
            }

            if (afterDistance > maxDistance || // Вылетели за пределы максимальной дистанции полёта снаряда
                afterDistance > beforeDistance || // Пролетели цель
                afterDistance <= reactDistance || // До цели осталось слишком маленькое расстояние - расстояние-реакции
                stopPenetration) // Снаряд застрял в ком то
            {
                isInited = false;
                GameObject.Destroy(this.gameObject);
            }
        }

        private float NextDistance
        {
            get
            {
                if (next == null || next.ToObject == null)
                    return 0f;
                return Vector3.Distance(transform.position, next.ToObject.transform.position);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Выполняет поиск следующего, наиболее близкого объекта на пути снаряда.
        ///     Если на пути снаряда больше нет объектов, вернёт null
        ///     ---
        ///     Searches for the next closest object in the path of the projectile.
        ///     If there are no more objects in the path of the projectile, returns null
        /// </summary>
        private IDamagedObject UpdateNext()
        {
            if (damagedObjects.Count == 0)
                return null;

            damagedObjects.RemoveAt(0);

            if (damagedObjects.Count == 0)
                return null;

            return damagedObjects[0];
        }

        /// <summary>
        ///     Выполняет расчёт траектории движения снаряда, а так же определяет какие объекты были пробиты или поражены, начинает визуальное движение снаряда, воспроизводит звук выстрела
        ///     ---
        ///     Calculates the trajectory of the projectile, as well as determines what objects have been penetrated or hit, starts the visual movement of the projectile, reproduces the sound of a gunshot
        /// </summary>
        /// <param name="source">
        ///     Персонаж, который совершает выстрел
        ///     ---
        ///     The character who takes the shot
        /// </param>
        /// <param name="startPos">
        ///     Точка откуда был выпущен снаряд
        ///     ---
        ///     The point from which the shell was fired
        /// </param>
        /// <param name="targetPos">
        ///     Точка, куда было выполнено прицеливание
        ///     ---
        ///     The point where the aiming was performed
        /// </param>
        /// <param name="weapon">
        ///     Оружие из которого был совершён выстрел
        ///     ---
        ///     The weapon from which the shot was fired
        /// </param>
        public void Init(IAttackObject source, Vector3 startPos, Vector3 targetPos, IFirearmsWeapon weapon)
        {
            this.transform.position = startPos;
            this.transform.LookAt(targetPos);
            this.startPos = startPos;
            this.direction = (targetPos - startPos).normalized;
            this.targetPos = startPos + direction * maxDistance;
            this.weapon = weapon;
            this.source = source;
            this.isInited = true;
            this.timestamp = Time.time;
            this.stopPenetration = false;
            this.nextDistance = maxDistance;
            this.damagedObjects = PrepareDamagedObjects();

            // Обновляем следующую цель снаряда
            this.next = damagedObjects != null && damagedObjects.Count > 0 ? damagedObjects[0] : null;
            this.nextDistance = NextDistance;

            AudioController.Instance.CreateTimedFragment(source.AttackCharacterObject.transform.position, MixerType.Sounds, weapon.ShootSoundType);
        }

        /// <summary>
        ///     Получает и рассчитывает список объектов на пути снаряда, так же вычисляет сколько объектов будет пробито насквозь (в последнем объекте - снаряд застрял)
        ///     ---
        ///     Obtains and calculates a list of objects in the path of the projectile, as well as calculates how many objects will be penetrated (in the last object - the shell is stuck)
        /// </summary>
        /// <returns>
        ///     Список объектов, которые получат урон этим снарядом
        ///     ---
        ///     A list of objects that will take damage with this projectile
        /// </returns>
        private IList<IDamagedObject> PrepareDamagedObjects()
        {
            var hits = Physics.RaycastAll(startPos, direction, maxDistance);
            if (hits == null || hits.Length == 0)
                return new List<IDamagedObject>();

            var result = new List<IDamagedObject>(hits.Length);
            // В результате рейкаста мы можем получить последовательность с повторными вхождениями одних и тех же объектов, когда луч прошивает их
            // Поэтому нужно отсеить такие дублирования, для этого использую Set и его Contains
            var checkedkHits = new HashSet<GameObject>();
            foreach(var item in hits)
            {
                var gameObject = item.collider.gameObject;
                if (gameObject == source.AttackCharacterObject || checkedkHits.Contains(gameObject))
                    continue;

                var fragment = gameObject.GetComponent<IFragmentDamaged>();
                if(fragment == null)
                    continue;

                result.Add(fragment.Damaged);
                checkedkHits.Add(gameObject); // Запоминаем что такой объект уже прошили насквозь
            }

            return result;
        }

        /// <summary>
        ///     Наносит урон всем объектам на пути снаряда, если такие объекты существуют
        ///     ---
        ///     Deals damage to all objects in the path of the projectile if such objects exist
        /// </summary>
        private void DoDamage(IDamagedObject damagedObject)
        {
            if (damagedObject == null)
                return;

            // Рассчитываем параметры для определения пробития объекта item
            var targetProtectionPercent = BattleCalculationService.GetProtectionPercent(damagedObject.Protection);
            var penetrationPercent = BattleCalculationService.GetPenetrationPerent(weapon.Penetration, targetProtectionPercent);
            stopPenetration = !BattleCalculationService.IsPenetration(penetrationPercent); // Пробиваем насквозь?

            BattleCalculationService.DoBulletDamage(source, damagedObject, weapon, this); // Наносим урон снарядом
        }

        #endregion

#if UNITY_EDITOR && DEBUG

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(startPos, targetPos);
        }

#endif

    }

}
