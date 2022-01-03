using UnityEngine;
using UnityEngine.AI;

namespace Engine.Enemy {
	
	[RequireComponent(typeof(NavMeshAgent))]
	public partial class AIPather : MonoBehaviour, IPather {

		#region Shared Fields

		[Caption("Скорость хотьбы")]
		[Comments("Скорость передвижения при статусе 'Ходьба'")]
		[SerializeField] private float speedWalk = 0.05f;

		[Caption("Скорость бега")]
		[Comments("Скорость передвижения при статусе 'Бег'")]
		[SerializeField] private float speedRun  = 0.09f;

		[SerializeField] private float height = 1f;

		#endregion
         
		#region Hidden Fields

		private MoveType     moveType;
		private Vector3      point;
		private Quaternion   look;

		[SerializeField]
		private Transform    target = null;
		private NavMeshAgent navMeshAgent;

		private float        timeTargetSetup;

		[SerializeField]
		private float SENSITIVE_DISTANCE = 0.1f;

		#endregion

		#region Unity Events

		private void Start() {
			this.navMeshAgent = GetComponent<NavMeshAgent>();
			this.point        = transform.position;
			this.look         = Quaternion.identity;
			this.MoveType     = MoveType.WALK;
		}

		private void DoMoveIteration() {
			/*
			Vector3[] pos = this.Path;
			if (pos == null) {
				return;
			}
			
			Vector3 nextPoint  = pos[1];
			transform.position = Vector3.MoveTowards(transform.position, nextPoint, CurrentSpeed*Time.deltaTime);

			if (Vector3.Distance(ThisPosition, nextPoint) <= SENSITIVE_DISTANCE) {
				nextPoint = pos[pointIteration + 1];
				transform.position = Vector3.MoveTowards(transform.position, nextPoint, CurrentSpeed * Time.deltaTime);
			}

			this.look = Quaternion.LookRotation(nextPoint - transform.position);
			transform.rotation = Quaternion.Lerp(transform.rotation, this.look, Time.deltaTime * 2f);
			*/

		}

		private void Update() {

			if (Target != null) {
				navMeshAgent.SetDestination(Target.transform.position);
				//navMeshAgent.CalculatePath(Target.position, this.path);
				DoMoveIteration();
				return;
			}

			//navMeshAgent.CalculatePath(Point, this.path);
			navMeshAgent.SetDestination(Point);
			DoMoveIteration();

		}
		
		#endregion

		#region Methods

		/// <summary>
		/// Указывает идти в точку point
		/// </summary>
		/// <param name="point">Точка, куда необходимо идти</param>
		public void Move(Vector3 point) {
			this.Point = point;
		}

		/// <summary>
		/// Меняет тип хотьбы
		/// </summary>
		/// <param name="type">Новый тип хотьбы</param>
		public void SetMoveType(MoveType type) {

		}

		/// <summary>
		/// Указывает цель для преследования
		/// </summary>
		/// <param name="target">Цель преследования</param>
		public void Haunt(Transform target) {
			this.Target = target;
		}

		/// <summary>
		/// Возвращает логическое значение - преследует ли текущий AI цель или нет
		/// </summary>
		/// <returns>true - если target задан, и AI преследует цель</returns>
		public bool isHaunted() {
			return target != null;
		}

		#endregion

		#region Properties

		private Vector3 ThisPosition {
			get {
				Vector3 pos = transform.position;
				pos.y -= height;
				return pos;
			}
		}

		/// <summary>
		/// Возвращает время, когда был установлен target. Если цели не было установлено, вернёт 0
		/// </summary>
		public float TimeTargetSetup {
			get {
				return this.timeTargetSetup;
			}
		}

		/// <summary>
		/// Возвращает время, в течение которого AI приследует цель с момента установки цели. Если цели не было установлено, вернёт 0
		/// </summary>
		public float TimeTargetHunted {
			get {
				if (target == null) {
					return 0;
				}
				return Time.time - this.timeTargetSetup;
			}
		}

		/// <summary>
		/// Возвращает/Устанавливает тип передвижения
		/// </summary>
		public MoveType MoveType {
			get {
				return this.moveType;
			}
			set {
				this.moveType = value;

				switch (moveType) {
					case MoveType.WALK:
						navMeshAgent.speed = SpeedWalk;
						break;
					case MoveType.RUN:
						navMeshAgent.speed = SpeedRun;
						break;
				}

			}
		}
		
		/// <summary>
		/// Возвращает/Устанавливает точку, в которую необходимо двигаться AI
		/// </summary>
		public Vector3 Point {
			get {
				return this.point;
			}
			set {
				this.point = value;
			}
		}

		/// <summary>
		/// Возвращает/Устанавливает цель, за которой надо "охотиться" AI
		/// </summary>
		public Transform Target {
			get {
				return this.target;
			}
			set {
				this.target = value;
				if (target == null) {
					this.timeTargetSetup = 0;
					return;
				}
				this.timeTargetSetup = Time.time;
			}
		}

		/// <summary>
		/// Устанавливает/Возвращает текущий угол обзора AI
		/// </summary>
		public Quaternion Look {
			get {
				return this.look;
			}
			set {
				this.look = value;
			}
		}

		/// <summary>
		/// Возвращает текущий массив точек пути (null, если стоит на месте)
		/// </summary>
		public Vector3[] Path {
			get {
				if (this.navMeshAgent.path == null || this.navMeshAgent.path.corners==null || this.navMeshAgent.path.corners.Length==0) {
					return null;
				}
				return this.navMeshAgent.path.corners;
			}
		}

		#endregion

		#region Characteristics

		/// <summary>
		/// Утанавливает/Возвращает скорость хотьбы
		/// </summary>
		public float SpeedWalk {
			get {
				return this.speedWalk;
			}
			set {
				this.speedWalk = value;
			}
		}

		/// <summary>
		/// Устанавливает/Возвращает скорость бега
		/// </summary>
		public float SpeedRun {
			get {
				return this.speedRun;
			}
			set {
				this.speedRun = value;
			}
		}

		/// <summary>
		/// Возвращает текущую скорость передвижения AI
		/// </summary>
		public float CurrentSpeed {
			get {
				return this.navMeshAgent.speed;
			}
		}

		#endregion


		#region Editor

#if UNITY_EDITOR

		private static Color PATH_COLOR = new Color(0, 1, 0);
		private static Color PATH_POINT = new Color(1, 1, 0, 0.4f);

		private void OnDrawGizmos() {

			Gizmos.color = new Color(1,0,0,0.3f);
			Gizmos.DrawSphere(ThisPosition, SENSITIVE_DISTANCE);

			if (!Application.isPlaying || Path == null) {
				return;
			}

			Vector3 prev = transform.position;
			foreach (Vector3 point in Path) {

				Gizmos.DrawLine(prev, point);
				Gizmos.color = PATH_COLOR;

				Gizmos.color = PATH_POINT;
				Gizmos.DrawCube(point, Vector3.one * Vector3.Distance(Camera.main.transform.position, point) * 0.01f);

				prev = point;
			}

		}

#endif

		#endregion

	}

}
