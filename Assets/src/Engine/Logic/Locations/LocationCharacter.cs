using Engine.Data;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Engine.Scenes;
using UnityEngine.AI;
using Engine.Logic.Locations.Animation;
using Engine.Logic.Locations.Char.Impls;
using Engine.Logic.Map;

namespace Engine.Logic.Locations
{

    /// <summary>
    ///
    /// Персонаж игрока на локации
    /// ---
    /// Player character on location
    /// 
    /// </summary>
    public class LocationCharacter : CharacterNpcBehaviour
    {

        #region Hidden Fields

        [SerializeField] private float pickUpDistance = 1f;
        [SerializeField] private float speed = 3f;
        [SerializeField] private MoveContext moveContext;
        
        private NavMeshPath navMeshPath;
        private List<Vector3> path;
        private LocationCameraController cameraController;
        
        #endregion
        
        
        #region Properties

        /// <summary>
        ///     Текущий путь, по которому надо пройти
        ///     (может быть пустым или не заполненным, что значит что персонаж стоит на месте)
        ///     ---
        ///     The current path to follow
        ///     (can be empty or unfilled, which means that the character is standing still)
        /// </summary>
        public List<Vector3> Path => path;
        
        /// <summary>
        ///     Расстояние на которое персонаж может вытянуть руку - поднять предмет, использовать объект, взаимодейстовать с окружением и т.д.
        ///     ---
        ///     The distance to which a character can extend his arm to lift an object, use an object, interact with the environment, etc.
        /// </summary>
        public float PickUpDistance => pickUpDistance;

        /// <summary>
        ///     Глобальное положение персонажа в мире
        ///     ---
        ///     Global position of the character in the world
        /// </summary>
        public Vector3 Position => transform.position;

        /// <summary>
        ///     Точка из которой персонаж начал движение
        ///     ---
        ///     The point from which the character began to move
        /// </summary>
        public Vector3 StartPosition => moveContext.StartPosition;

        /// <summary>
        ///     Точка, в которую идёт персонаж в текущий момент
        ///     ---
        ///     The point at which the character is currently going
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
        ///     Персонаж занят чем то?
        ///     ---
        ///     Is the character doing something?
        /// </summary>
        public bool IsBusy => Path != null;
        
        #endregion

        /// <summary>
        ///     Добавление боевого опыта персонажу
        ///     ---
        ///     Adding combat experience to a character
        /// </summary>
        /// <param name="value">
        ///     Количество добавляемого опыта
        ///     ---
        ///     Amount of experience to be added
        /// </param>
        public override void AddBattleExp(long value)
        {
            // Рассчитываем полученный опыт
            // Calculate the resulting experience
            ExpCalculationService.AddExp(value, Game.Instance.Character.Exps.FightExperience, Game.Instance.Character.Exps.MainExperience);
        }

        /// <summary>
        ///     Нашего персонажа завалили
        ///     ---
        ///     Our character was died.
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
            Character = new PlayerCharacter();
            cameraController = ObjectFinder.Find<LocationCameraController>();
            MeshSwitcher.MeshIndex = Game.Instance.Character.Account.SpriteID;
        }

        public override void OnUpdate()
        {
            if (Character.Health <= 0)
                Died();

            if (Game.Instance.Runtime.Mode == Mode.Switch)
                return;

            // Путь пустой, значит персонаж стоит на месте
            // The path is empty, so the character is standing still.
            if (Lists.IsEmpty(path))
                return;
            
            // Во время перемещения двигаем камеру за персонажем
            // While moving, move the camera behind the character
            cameraController.UpdateCameraPos();
            
            // Рассчитываем прогресс движения и перемещаем персонажа
            // Calculate movement progress and move the character
            float progress = (Time.time - moveContext.ChangePositionTimestamp) * speed / moveContext.Distance;
            transform.position = Vector3.Lerp(moveContext.StartPosition, moveContext.NextPosition, Mathf.Min(progress, 1f));
            transform.rotation = Quaternion.Lerp(moveContext.StartRotation, moveContext.NextRotation, Mathf.Min(progress * 4f, 1f));

            if (progress >= 1f)
            {
                // Забываем эту точку
                // We forget this point
                path.RemoveAt(0);
                if (path.Count == 0)
                {
                    // Персонаж прошёл весь путь, останавливаемся
                    // The character has gone all the way, we stop
                    StopMove();
                }
                else
                {
                    // Рассчитыываем следующую точку относительно нового участка пути
                    // Calculate the next point relative to the new track section
                    UpdatePoint();
                }
            }
        }

        /// <summary>
        ///     Срабатывает в момент достижения конца одного из участков пути персонажа
        ///     ---
        ///     Triggers at the moment you reach the end of one part of the character's path
        /// </summary>
        private void UpdatePoint()
        {
            if (Lists.IsEmpty(path))
                return;

            Animator.SetCharacterMoveSpeedType(MoveSpeedType.Run);

            // Выставляем параметры поворота персонажа в сторону точки куда он бежит
            // Set the parameters for turning the character in the direction of the point where he is running
            lookDirectionTransform.LookAt(path[0]);
            moveContext.NextRotation = Quaternion.Euler(0, lookDirectionTransform.rotation.eulerAngles.y, 0);
            moveContext.StartRotation = transform.rotation;

            // Выставляем параметры следующей точки куда нужно бежать
            // Set the parameters of the next point to run to
            moveContext.NextPosition = path[0];
            moveContext.StartPosition = transform.position;
            moveContext.ChangePositionTimestamp = Time.time;
            moveContext.Distance = Vector3.Distance(StartPosition, NextPosition);
        }

        /// <summary>
        ///     Срабатывает в момент остановки движения
        ///     ---
        ///     Triggers when move stops
        /// </summary>
        private void StopMove()
        {
            Animator.SetCharacterMoveSpeedType(MoveSpeedType.Idle);
            path = null;
        }

#if UNITY_EDITOR && DEBUG

        private void OnDrawGizmos()
        {
            return;
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
