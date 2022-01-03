using System;
using System.Collections;
using UnityEngine;

namespace Engine.IO
{

	/// <summary>
	/// Базовый абстрактный класс получения данных с GPS (источник GPS-данных неважен)
	/// </summary>
	public abstract class IOGpsBase : MonoBehaviour,
									  IGps,
									  IMonoBehaviourOverrideStartEvent,
									  IMonoBehaviourOverrideUpdateEvent
	{

		#region Fields

		[SerializeField]
		protected float lastUpdateTime;

		[SerializeField]
		protected Vector3 currentPosition;

		[SerializeField]
		protected Vector3 lastPosition;

		[SerializeField]
		protected bool isEnabled = true;

		[SerializeField]
		protected GpsStatusType status;

		[SerializeField]
		protected float lastHAccuracy;

		[SerializeField]
		protected float currentHAccuracy;

		[SerializeField]
		protected float lastVAccuracy;

		[SerializeField]
		protected float currentVAccuracy;

		[SerializeField]
		protected float updateTime = 0.5f;

		[SerializeField]
		protected int maxTimeout = 20;

		#endregion

		#region Hidden Fields

		protected bool startedFlag = false;

		protected bool needStarted = false;

        #endregion

        #region Properties

        public Vector3 DebugPosition
        {
            get; set;
        } = new Vector3(54.55914f, 30.32488f, 0f);

        //48.55914f; //54.80842f;
        //39.32488f; //32.08611f;

        public virtual Vector3 LastPosition
		{
			get
			{
				return this.lastPosition;
			}
		}
		
		public virtual Vector3 CurrentPosition
		{
			get
			{
#if UNITY_EDITOR && DEBUG
                currentPosition = DebugPosition;
#endif
				return this.currentPosition;
			}
		}

		public virtual float LastHAccuracy
		{
			get
			{
				return this.lastHAccuracy;
			}
		}

		public virtual float CurrentVAccuracy
		{
			get
			{
				return this.currentVAccuracy;
			}
		}

		public virtual float LastVAccuracy
		{
			get
			{
				return this.lastVAccuracy;
			}
		}

		public virtual float CurrentHAccuracy
		{
			get
			{
				return this.currentHAccuracy;
			}
		}

		public virtual GpsStatusType Status
		{
			get
			{
				return status;
			}
		}

		public virtual float LastUpdateTime
		{
			get
			{
				return Time.time - lastUpdateTime;
			}
		}

		public virtual bool IsEnabled
		{
			get
			{
#if UNITY_EDITOR && DEBUG
                return true;
#else
                return isEnabled;
#endif
			}
		}

		public virtual bool IsStarted
		{
			get
			{
#if UNITY_EDITOR && DEBUG
                return true;
#else
                return startedFlag;
#endif
			}
		}

#endregion

#region Unity Events

		public virtual void OnStart()
		{
		}

		public virtual void OnUpdate()
		{
		}

		private void Start()
		{
			OnStart();
		}

		private void Update()
		{
			OnUpdate();

			if (needStarted)
			{
				DoStartGpsCoroutine();
				return;
			}

			if (!IsStarted)
				return;

			DoUpdateGpsCoroutine();
		}

#endregion

#region Methods

		public virtual void StartGps()
		{
			needStarted = true;

			if (!IsEnabled)
			{
				startedFlag = false;
				return;
			}
		}

		public virtual void StopGps()
		{
			needStarted = false;
			startedFlag = false;
		}

        public virtual void SetPos(Vector3 pos)
        {
            currentPosition = pos;
            lastPosition = pos;
        }

		/// <summary>
		/// Инициализирует GPS
		/// </summary>
		protected abstract void DoStartGpsCoroutine();

		/// <summary>
		/// Обновляет данные полученные от GPS
		/// </summary>
		protected abstract void DoUpdateGpsCoroutine();

#endregion

		public override string ToString()
		{
			return "gps enabled: " + IsEnabled + "\n"
				+ "started: " + IsStarted + "\n"
				+ "state: " + Status + "\n"
				+ "pos: " + CurrentPosition.ToFltString() + "\n"
				+ "h acc: " + CurrentHAccuracy + "\n"
				+ "v acc: " + CurrentVAccuracy + "\n"
				+ "delay: " + LastUpdateTime + "\n";
		}

	}

}
