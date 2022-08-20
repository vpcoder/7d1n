using UnityEngine;
using Engine.Data;
using Mapbox.Unity.Location;
using Mapbox.Unity.Utilities;
using Mapbox.Unity.Map;

namespace Engine.Map
{
	
    /// <summary>
    /// Окружность-ходьбы вокруг человека
    /// </summary>
	public class MapHuman : MonoBehaviour
	{

        #region Serializable Fields

        /// <summary>
        /// Размер спрайта зоны в пикселях (спрайт должен быть квадратным)
        /// </summary>
        [SerializeField] private float zonePixelSize = 512f;

        /// <summary>
        /// Доступный радиус перемещения персонажа
        /// </summary>
		[SerializeField] private float moveRadius = 2f;

        /// <summary>
        /// Скорость перемещения персонажа
        /// </summary>
        [SerializeField] private float moveSpeed = 1f;

        /// <summary>
        /// Максимальная скорость перемещения зоны (игрока), то что выше - телепортация
        /// </summary>
        [SerializeField] private float zoneMoveSpeed = 50f;

        /// <summary>
        /// Объект зоны перемещения
        /// </summary>
        [SerializeField] private Transform zone;

        
        [SerializeField] private AbstractLocationProvider gps;
        [SerializeField] private AbstractMap map;

        public MoveContext MoveContext { get; set; } = new MoveContext();

        private float changeGpsPointTimestamp = 0;

        #endregion

        #region Shared Properties

        public Vector3 Position
		{
			get
			{
				return zone.position;
			}
		}

        /// <summary>
        /// Текущий радиус доступной зоны перемещения
        /// </summary>
		public float MoveRadius
		{
			get
			{
                var pixelScaleFactor = this.zonePixelSize * 0.005f; // расчитываем коэффициент масштабирования спрайта в мире для заданного размера изображения в пикселях
                return this.moveRadius * pixelScaleFactor;
			}
            set
            {
                this.moveRadius = value;
                DoScaleZone();
            }
		}

        /// <summary>
        /// Текущая сокрость перемещения персонажа
        /// </summary>
        public float MoveSpeed
        {
            get
            {
                return moveSpeed;
            }
            set
            {
                this.moveSpeed = value;
            }
        }

		#endregion

		private void Start()
		{

        }

        private void FixedUpdate()
		{
            if (Game.Instance.Runtime.Mode == Mode.Switch)
                return;

            float distance = Vector3.Distance(MoveContext.NextPosition, transform.localPosition);

            if (distance >= 20f)
            {
                transform.localPosition = MoveContext.NextPosition;
                MoveContext.StartPosition = MoveContext.NextPosition;
                changeGpsPointTimestamp = Time.time;
                MoveContext.ChangePositionTimestamp = Time.time;
                MoveContext.Distance = 1f;
                return;
            }

            if (Time.time - changeGpsPointTimestamp > 2f)
            {
                changeGpsPointTimestamp = Time.time;
                MoveContext.ChangePositionTimestamp = Time.time;
                Vector3 unityPos = Conversions.GeoToWorldPosition(gps.CurrentLocation.LatitudeLongitude, map.CenterMercator, map.WorldRelativeScale).ToVector3xz();
                unityPos.y = unityPos.z;
                unityPos.z = 0;

                MoveContext.NextPosition = unityPos;
                MoveContext.StartPosition = transform.localPosition;
                MoveContext.Distance = Vector3.Distance(MoveContext.StartPosition, MoveContext.NextPosition);
            }
            float progress = MoveContext.Distance == 0 ? 1f : (Time.time - MoveContext.ChangePositionTimestamp) * zoneMoveSpeed / MoveContext.Distance;
            transform.localPosition = Vector3.Lerp(MoveContext.StartPosition, MoveContext.NextPosition, Mathf.Min(progress, 1f));
        }


        float radius = 25f;
        public void SwitchDebugZoneSize()
        {
            if (radius == 25f)
            {
                MoveRadius = 250f;
                radius = 250f;
            }
            else
            {
                MoveRadius = 25f;
                radius = 25f;
            }
        }

		private void DoScaleZone()
		{
            zone.localScale = Vector3.one * moveRadius;
        }

		#region Editor

#if UNITY_EDITOR

		private void OnValidate()
		{
			if (moveRadius < 0.01f)
				moveRadius = 0.01f;
            DoScaleZone();
        }

#endif

		#endregion

	}

}
