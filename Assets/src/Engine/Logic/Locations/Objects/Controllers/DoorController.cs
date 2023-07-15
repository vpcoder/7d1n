using Engine.Data;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Engine.Logic.Locations
{
    
    /// <summary>
    /// Контекст состояния двери
    /// Информация о положении двери, повороте и т.д.
    /// </summary>
    [Serializable]
    public class DoorStateContext
    {
        [SerializeField] public Vector3 Position;
        [SerializeField] public Vector3 Rotation;
        [SerializeField] public List<Vector2Int> PathFinderPoints;
    }

    /// <summary>
    /// Состояние двери
    /// </summary>
    public enum DoorState : byte
    {
        /// <summary>
        /// Открыта
        /// </summary>
        OPENED,

        /// <summary>
        /// Закрыта
        /// </summary>
        CLOSED,
    }

    public class DoorController : MonoBehaviour, IUseObjectController
    {

        [SerializeField] private float speed = 0.5f;
        [SerializeField] private DoorStateContext openState;
        [SerializeField] private DoorStateContext closeState;

        [SerializeField]
        private DoorState state = DoorState.OPENED;

        private Vector3 startPosition;
        private Vector3 endPosition;
        private Quaternion startRotation;
        private Quaternion endRotation;
        private bool needUpdate = false;

        private float timestamp;

        public DoorState State
        {
            get
            {
                return state;
            }
            set
            {
                if (state == value)
                    return;
                this.state = value;
                UpdateState();
            }
        }

        public GameObject ToObj { get { return gameObject; } }

        /// <summary>
        /// Меняет состояние двери
        /// </summary>
        public void DoUse()
        {
            State = State == DoorState.OPENED ? DoorState.CLOSED : DoorState.OPENED;
        }

        private void Start()
        {
            UpdateState();
        }

        private void Update()
        {
            if (!needUpdate)
                return;

            var progress = Mathf.Min((Time.time - timestamp) * speed, 1f);

            transform.localPosition = Vector3.Lerp(startPosition, endPosition, progress);
            transform.localRotation = Quaternion.Lerp(startRotation, endRotation, progress);

            if(progress >= 1f)
                needUpdate = false;
        }

        private void UpdateState()
        {
            var nextState = State == DoorState.OPENED ? openState : closeState;
            //foreach (var point in nextState.PathFinderPoints)
            //    Matrix.Set(point.x, point.y, State == DoorState.OPENED);
            
            if(Game.Instance.Runtime.Mode == Mode.Battle)
            {
                var battleManager = ObjectFinder.BattleManager;
                if(State == DoorState.OPENED)
                {

                    //battleManager.AddPathCellsToBattle(nextState.PathFinderPoints.ToArray());
                }
                else
                {
                    // battleManager.RemovePathCellsFromBattle(nextState.PathFinderPoints.ToArray());
                }
            }

            startPosition = transform.localPosition;
            startRotation = transform.localRotation;
            endPosition = nextState.Position;
            endRotation = Quaternion.Euler(nextState.Rotation);
            timestamp = Time.time;
            this.needUpdate = true;
        }

    }

}
