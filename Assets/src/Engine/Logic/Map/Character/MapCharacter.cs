using Engine.Data;
using Engine.Logic;
using Engine.Logic.Map;
using Mapbox.Unity.MeshGeneration.Data;
using System.Linq;
using UnityEngine;

namespace Engine.Map
{

    /// <summary>
    /// 
    /// Управление персонажем на глобальной карте
    /// Осуществляет управление самим персонажем (перемещение в границе окружности)
    /// ---
    /// 
    /// 
    /// </summary>
    public class MapCharacter : MonoBehaviour
    {

        #region Hidden Fields

        /// <summary>
        /// Безопасная зона за окружностью, чтобы игрок "не залип" на границе окружности
        /// </summary>
        [SerializeField] private float safeDistanceZone = 0.1f;

        /// <summary>
        /// 
        /// </summary>
        [SerializeField] private MapHuman human;

        /// <summary>
        /// 
        /// </summary>
        [SerializeField] private CharacterMoveEffectController characterEffect;

        /// <summary>
        /// 
        /// </summary>
        [SerializeField] private LineRenderer pathLine;

        /// <summary>
        /// 
        /// </summary>
        [SerializeField] private Transform body;

        #endregion

        #region Properties

        public UnityTile CurrentTile { get; private set; }

        public MoveContext MoveContext { get; set; } = new MoveContext();

        private Vector3 Position
        {
            get
            {
                return transform.position;
            }
            set
            {
                transform.position = value;
            }
        }

        public Vector3 NextPosition
        {
            get
            {
                return MoveContext.NextPosition;
            }
            set
            {
                var pos = value;
                pos.y = 0f;
                MoveContext.NextPosition = pos;
                MoveContext.StartPosition = Position;
                MoveContext.ChangePositionTimestamp = Time.time;
                MoveContext.Distance = Vector3.Distance(StartPosition, NextPosition);
            }
        }

        public bool NeedMove { get; set; }

        public GameObject Target { get; set; }

        public Vector3 StartPosition { get { return MoveContext.StartPosition; } }

        /// <summary>
        /// Прогресс перемещения между двумя точками
        /// </summary>
        private float MoveProgress
        {
            get
            {
                return Mathf.Min((Time.time - MoveContext.ChangePositionTimestamp) * human.MoveSpeed / MoveContext.Distance, 1f);
            }
        }

        #endregion

        #region Unity Events

        private void Start()
        {
            NeedMove = true;
            NextPosition = Position;
            pathLine.positionCount = 2;
            ResetPath();
        }

        /// <summary>
        /// Непрерывное позиционирование персонажа
        /// </summary>
        private void Update()
        {
            if (Game.Instance.Runtime.Mode == Mode.Switch)
                return;

            CheckTile();

            Vector3 circleCenter = human.Position;
            Vector3 point = Position - circleCenter;

            float currentDistance = Vector3.Distance(Position, circleCenter); // Текущее расстояние от центра позиции игрока, до текущей позиции персонажа

            if (!NeedMove) // Персонаж не двигается
            {
                if (currentDistance > human.MoveRadius) // При этом, персонаж дальше радиуса
                {
                    Position = CirclePos(point, circleCenter);
                    StopMove();
                    ResetPath();
                }
                return;
            }

            // Прогресс движения по траектории
            float progress = MoveProgress;

            Vector3 nextPosition = !NeedMove ? Vector3.zero : Vector3.Lerp(StartPosition, NextPosition, progress);
            float nextDistance = Vector3.Distance(nextPosition, circleCenter);
            bool safeDistanceFail = currentDistance - human.MoveRadius > safeDistanceZone;

            // Игрок убегает за пределы активной зоны?
            if (currentDistance > human.MoveRadius && (nextDistance > currentDistance || safeDistanceFail))
            {
                Position = CirclePos(point, circleCenter); // Проецируем его позицию на границу окружности
                StopMove();
                ResetPath();
                return;
            }

            Position = nextPosition;

            if (progress >= 1f)
            {
                StopMove();
                DoEndEvent();
            }
            else
            {
                UpdatePath();
            }
        }

        #endregion

        #region Methods

        public void GoToCenter()
        {
            Position = NextPosition = human.Position;
            StopMove();
            ResetPath();
        }

        private void CheckTile()
        {
            var needUpdateTile = CurrentTile == null;
            if (!needUpdateTile)
            {
                // Расстояние между тайлами
                var offset = CurrentTile.transform.position - transform.position;
                var tileSize = TerrainGenerator.Instance.TileHalfSize;
                needUpdateTile = Mathf.Abs(offset.x) > tileSize.x || Mathf.Abs(offset.z) > tileSize.y;
            }

            if (!needUpdateTile)
                return;

            var hitsData = Physics.RaycastAll(transform.position + Vector3.up * 2, Vector3.down, 8f);
            if (hitsData.Length == 0)
            {
                CurrentTile = null;
                return;
            }

            CurrentTile = hitsData.Where(hit => hit.transform?.gameObject?.GetComponent<UnityTile>() != null)
                .FirstOrDefault().transform?.GetComponent<UnityTile>();

            characterEffect.SwitchBiom(CurrentTile.Biom);
        }

        private Vector3 CirclePos(Vector3 point, Vector3 circleCenter)
        {
            float d = Mathf.Sqrt(point.x * point.x + point.z * point.z) / human.MoveRadius;
            return new Vector3(point.x / d, 0, point.z / d) + circleCenter; // Проецируем его позицию на границу окружности
        }

        private void DoEndEvent()
        {
            if (Target == null)
                return;

            var build = Target.GetComponent<Building>();
            ObjectFinder.Find<EnterBuildControll>().Show(build.Info);
        }

        private void StopMove()
        {
            NeedMove = false;
            characterEffect.StopMove();
        }

        private void ResetPath()
        {
            pathLine.SetPositions(new[] { Vector3.zero, Vector3.zero });
        }

        /// <summary>
        /// Перерисовываем линию пути
        /// </summary>
        private void UpdatePath()
        {
            var offset = body.forward * 2f;
            var pos = transform.position + offset;

            if (Vector3.Distance(Vector3.zero, offset) >= Vector3.Distance(pos, MoveContext.NextPosition))
            {
                pathLine.SetPositions(new[] { Vector3.zero, Vector3.zero });
            }
            else
            {
                var pos0 = new Vector3(pos.x, 0.8f, pos.z);
                var pos1 = new Vector3(MoveContext.NextPosition.x, 0.8f, MoveContext.NextPosition.z);
                pathLine.SetPositions(new[] { pos0, pos1 });
            }
        }

        #endregion

        #region Editor

#if UNITY_EDITOR && DEBUG

        private void OnDrawGizmos()
        {
            var center = human.Position;
            var left = center + new Vector3(human.MoveRadius, 0f, 0f);
            Gizmos.color = Color.green;
            Gizmos.DrawLine(center, left);
        }

#endif

        #endregion

    }

}
