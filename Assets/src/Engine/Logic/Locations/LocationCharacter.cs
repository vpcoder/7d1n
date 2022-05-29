using Engine.Data;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Engine.Scenes;
using UnityEngine.AI;
using Engine.Logic.Locations.Animation;
using Engine.Logic.Map;

namespace Engine.Logic.Locations
{

    /// <summary>
    /// Персонаж игрока на локации
    /// </summary>
    public class LocationCharacter : EnemyNpcBehaviour
    {

#pragma warning disable IDE0044, IDE0051, IDE0059

        [SerializeField] private float pickUpDistance = 1f;
        [SerializeField] private float speed = 3f;
        [SerializeField] private MoveContext moveContext;

        private NavMeshPath navMeshPath;

        /// <summary>
        /// Текущий путь, по которому надо пройти
        /// (может быть пустым или не заполненным, что значит что персонаж стоит на месте)
        /// </summary>
        private List<Vector3> path;

        private LocationCameraController cameraController;
        
        /// <summary>
        /// Расстояние на которую персонаж может вытянуть руку - поднять предмет, использовать объект и т.д.
        /// </summary>
        public float PickUpDistance => pickUpDistance;

        /// <summary>
        /// Положение персонажа в мире
        /// </summary>
        public Vector3 Position => transform.position;

        /// <summary>
        /// Точка из которой персонаж начал движение
        /// </summary>
        public Vector3 StartPosition => moveContext.StartPosition;

        /// <summary>
        /// Точка, в которую идёт персонаж в текущий момент
        /// </summary>
        public Vector3 NextPosition
        {
            get => moveContext.NextPosition;
            set
            {
                path = agent.CalculatePath(value, navMeshPath) ? navMeshPath.corners.ToList() : null;
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
            Enemy = new PlayerCharacter();
            cameraController = ObjectFinder.Find<LocationCameraController>();
        }

        public override void OnUpdate()
        {
            if (Enemy.Health <= 0)
                Died();

            if (Game.Instance.Runtime.Mode == Mode.Switch)
                return;

            if (Lists.IsEmpty(path))
                return; // Путь пустой, значит персонаж стоит на месте
            
            // Во время перемещения двигаем камеру за персонажем
            cameraController.UpdateCameraPos();
            
            // Рассчитываем прогресс движения и перемещаем персонажа
            float progress = (Time.time - moveContext.ChangePositionTimestamp) * speed / moveContext.Distance;
            transform.position = Vector3.Lerp(moveContext.StartPosition, moveContext.NextPosition, Mathf.Min(progress, 1f));
            transform.rotation = Quaternion.Lerp(moveContext.StartRotation, moveContext.NextRotation, Mathf.Min(progress * 4f, 1f));

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
            if (Lists.IsEmpty(path))
                return;

            Animator.SetInteger(AnimationKey.MoveSpeedKey, (int)MoveSpeedType.Run); // Меняем состояние на бег

            lookDirectionTransform.LookAt(path[0]); // Выставляем параметры поворота персонажа в сторону точки куда он бежит
            moveContext.NextRotation = Quaternion.Euler(0, lookDirectionTransform.rotation.eulerAngles.y, 0);
            moveContext.StartRotation = transform.rotation;

            moveContext.NextPosition = path[0]; // Выставляем параметры следующей точки куда нужно бежать
            moveContext.StartPosition = transform.position;
            moveContext.ChangePositionTimestamp = Time.time;
            moveContext.Distance = Vector3.Distance(StartPosition, NextPosition);
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
