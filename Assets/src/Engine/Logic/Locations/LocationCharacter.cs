using Engine.Data;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Engine.Data.Factories;
using Engine.Scenes;
using UnityEngine.AI;
using Engine.Logic.Locations.Animation;

namespace Engine.Logic.Locations
{

    /// <summary>
    /// Персонаж игрока на локации
    /// </summary>
    public class LocationCharacter : EnemyItem
    {

#pragma warning disable IDE0044, IDE0051, IDE0059

        [SerializeField] private Transform directionObject;
        [SerializeField] private float pickUpDistance = 1f;
        [SerializeField] private float speed = 3f;
        [SerializeField] public MoveContext MoveContext;

        private NavMeshPath navMeshPath;

        /// <summary>
        /// Текущий путь, по которому надо пройти
        /// (может быть пустым или не заполненным, что значит что персонаж стоит на месте)
        /// </summary>
        private List<Vector3> path;

        /// <summary>
        /// Расстояние на которую персонаж может вытянуть руку - поднять предмет, использовать объект и т.д.
        /// </summary>
        public float PickUpDistance
        {
            get
            {
                return pickUpDistance;
            }
        }

        /// <summary>
        /// Положение персонажа в мире
        /// </summary>
        public Vector3 Position
        {
            get
            {
                return transform.position;
            }
        }

        /// <summary>
        /// Точка из которой персонаж начал движение
        /// </summary>
        public Vector3 StartPosition
        {
            get
            {
                return MoveContext.StartPosition;
            }
        }

        /// <summary>
        /// Точка, в которую идёт персонаж в текущий момент
        /// </summary>
        public Vector3 NextPosition
        {
            get
            {
                return MoveContext.NextPosition;
            }
            set
            {
                if(agent.CalculatePath(value, navMeshPath))
                {
                    path = navMeshPath.corners.ToList();
                }
                else
                {
                    path = null;
                }
                UpdatePoint();
            }
        }

        /// <summary>
        /// Добавление опыта персонажу
        /// </summary>
        /// <param name="value">Количество добавляемого опыта</param>
        public override void AddBattleExp(long value)
        {
            // Рассчитываем полученный опыт
            ExpCalculationService.AddExp(value, Game.Instance.Character.Exps.FightExperience, Game.Instance.Character.Exps.MainExperience);
        }

        /// <summary>
        /// Нашего персонажа завалили
        /// </summary>
        public override void Died()
        {
            SceneManager.Instance.Switch(SceneName.Map);
        }

        public void SetPath(List<Vector3> path)
        {
            this.path = path;
            UpdatePoint();
        }

        public override void OnStart()
        {
            navMeshPath = new NavMeshPath();
            enemy = new PlayerCharacter();
            UpdateBody();
        }

        public override void OnUpdate()
        {
            if (Enemy.Health <= 0)
                Died();

            if (Game.Instance.Runtime.Mode == Mode.Switch)
                return;

            if (path == null || path.Count == 0)
                return; // Путь пустой, значит персонаж стоит на месте

            // Получаем текущую точку, куда следуюет идти
            var point = path[0];

            // Рассчитываем прогресс движения и перемещаем персонажа
            float progress = (Time.time - MoveContext.ChangePositionTimestamp) * speed / MoveContext.Distance;
            transform.position = Vector3.Lerp(MoveContext.StartPosition, MoveContext.NextPosition, Mathf.Min(progress, 1f));
            transform.rotation = Quaternion.Lerp(MoveContext.StartRotation, MoveContext.NextRotation, Mathf.Min(progress * 4f, 1f));

            if (progress >= 1f)
            {
                path.RemoveAt(0); // Забываем эту точку
                if (path.Count == 0)
                {
                    StopMove(); // Персонаж прошёл весь путь, останавливаемся
                }
                else
                {
                    UpdatePoint(); // Рассчитыываем следующую точку относительно нового участка пути
                }
            }
        }

        /// <summary>
        /// Срабатывает в момент достижения конца одного из участков пути персонажа
        /// </summary>
        private void UpdatePoint()
        {
            if (path == null || path.Count == 0)
                return;

            Animator.SetInteger(AnimationKey.MoveSpeedKey, (int)MoveSpeedType.Run); // Меняем состояние на бег

            directionObject.LookAt(path[0]); // Выставляем параметры поворота персонажа в сторону точки куда он бежит
            MoveContext.NextRotation = Quaternion.Euler(0, directionObject.rotation.eulerAngles.y, 0);
            MoveContext.StartRotation = transform.rotation;

            MoveContext.NextPosition = path[0]; // Выставляем параметры следующей точки куда нужно бежать
            MoveContext.StartPosition = transform.position;
            MoveContext.ChangePositionTimestamp = Time.time;
            MoveContext.Distance = Vector3.Distance(StartPosition, NextPosition);
        }

        /// <summary>
        /// Срабатывает в момент остановки движения
        /// </summary>
        private void StopMove()
        {
            Animator.SetInteger(AnimationKey.MoveSpeedKey, (int)MoveSpeedType.Idle);
            path = null;
        }

#if UNITY_EDITOR && DEBUG

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, pickUpDistance);

            if (path != null && path.Count > 1)
            {
                Gizmos.color = Color.yellow;
                Vector3 prev = path[0];
                for (int i = 1; i < path.Count; i++)
                {
                    Vector3 curr = path[i];
                    Gizmos.DrawLine(prev, curr);
                    prev = curr;
                }
            }
        }

#endif

    }

}
