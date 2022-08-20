using UnityEngine;
using Engine.Data;
using Engine.Logic.Locations;

namespace Engine.Map
{

    [ExecuteInEditMode]
	public class LocationMapController : MonoBehaviour
	{

		#region Hidden Fields

		[SerializeField] private float x;
		[SerializeField] private float y;

		[SerializeField] private float zoom = 70;
		[SerializeField] private float cameraMaxDistance = 1.4f;

		[SerializeField] private float subSpeedK = 0.155f;
		[SerializeField] private float speed = -0.0456f;

		[SerializeField] private Vector3 minMoveZone; // -583200, 0,      20
        [SerializeField] private Vector3 maxMoveZone; //  583200, 145800, 90

        [SerializeField] private new Camera camera;

        [SerializeField] LocationCharacter player;

		private Vector2 prevPoint = Vector2.zero;
		private Transform selectedTmp = null;
        private RaycastHit selectedHit;

		#endregion

		#region Events Fields

		//public event ClickedEvent Clicked;

		//public event ZoomedEvent Zoomed;

        //public StateEvent State = StateEvent.None;

        public float CameraMaxDistance { get { return cameraMaxDistance; } }

        #endregion

		private void OnZoomed(float value)
		{
			zoom += value*0.7f;
			zoom = zoom.InFrame(minMoveZone.z, maxMoveZone.z);
			Zoom = zoom;
		}

		#region Unity Events

		private void Start()
		{
			//this.Zoomed += OnZoomed;
            //this.Clicked += OnClicked;
		}

        private void OnClicked(Transform selected, RaycastHit hit)
        {
            if (selected == null)
                return;

            player.NextPosition = hit.point;
        }

        private void Update()
		{
            if (DeviceInput.IsUiTouch())
                return;

            var mode = Game.Instance.Runtime.Mode;
            if (mode != Mode.Game &&
                mode != Mode.Battle)
                return;

            camera.orthographicSize = zoom;

			float subSpeed = (1f / (maxMoveZone.z - minMoveZone.z) * zoom) * subSpeedK;

            if (DeviceInput.TouchCount == 0)
            {
                //State = StateEvent.None;
            }

            if (DeviceInput.TouchCount == 0 && (mode == Mode.Game || mode == Mode.Battle))
			{
				//if (selectedTmp != null && State == StateEvent.None)
				//{
					//Clicked?.Invoke(selectedTmp, selectedHit);
				//	selectedTmp = null;
                    //State = StateEvent.Select;
                //}
                return;
			}

			if (DeviceInput.TouchCount == 2)
			{
			    Touch touchZero = Input.GetTouch(0);
				Touch touchOne = Input.GetTouch(1);

				// Find the position in the previous frame of each touch.
				Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
				Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

				// Find the magnitude of the vector (the distance) between the touches in each frame.
				float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
				float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

				// Find the difference in the distances between each frame.
				float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

				//Zoomed?.Invoke(deltaMagnitudeDiff);
                //State = StateEvent.Zoom;
                return;
			}

            if (DeviceInput.GetTouchPhase == TouchPhase.Began)
            {
                prevPoint = DeviceInput.TouchPosition;
                //State = StateEvent.None;
            }

            if (DeviceInput.GetTouchPhase == TouchPhase.Began && mode == Mode.Game)
			{
				RaycastHit hit;
                Ray ray = camera.ScreenPointToRay(prevPoint);
				if (Physics.Raycast(ray, out hit, 100f))
				{
					selectedTmp = hit.transform;
                    selectedHit = hit;
                }
            }

			if (DeviceInput.GetTouchPhase == TouchPhase.Moved)
			{
				Vector2 touchDeltaPosition = DeviceInput.TouchPosition - prevPoint;

				if (touchDeltaPosition.magnitude > 0.1f)
					selectedTmp = null;

                x += touchDeltaPosition.x * speed * subSpeed;
				x = x.InFrame(minMoveZone.x, maxMoveZone.x);

                y += touchDeltaPosition.y * speed * subSpeed;
				y = y.InFrame(minMoveZone.y, maxMoveZone.y);

                camera.transform.localPosition = new Vector3(x, y, -3f);
				prevPoint = DeviceInput.TouchPosition;
                //State = StateEvent.Move;
            }

        }

		#endregion


		#region Properties

		private void UpdateCameraPosition()
		{
            camera.transform.localPosition = new Vector3(x, y, -3);
		}

		public float X
		{
			get
			{
				return this.x;
			}
			set
			{
				this.x = value.InFrame(minMoveZone.x, maxMoveZone.x);
				UpdateCameraPosition();
			}
		}

		public float Y
		{
			get
			{
				return this.y;
			}
			set
			{
				this.y = value.InFrame(minMoveZone.y, maxMoveZone.y);
				UpdateCameraPosition();
			}
		}

		public float Zoom
		{
			get
			{
				return this.zoom;
			}
			set
			{
				this.zoom = value.InFrame(minMoveZone.z, maxMoveZone.z);
				UpdateCameraPosition();
			}
		}

		#endregion

		#region EDITOR

#if UNITY_EDITOR

		private void OnValidate()
		{

			this.x = x.InFrame(minMoveZone.x, maxMoveZone.x);
			this.y = y.InFrame(minMoveZone.y, maxMoveZone.y);
			this.zoom = zoom.InFrame(minMoveZone.z, maxMoveZone.z);

			UpdateCameraPosition();

		}

		private void OnDrawGizmos()
		{

			Gizmos.color = Color.green;
			Gizmos.DrawWireCube(Vector3.Lerp(maxMoveZone, minMoveZone, 0.5f), maxMoveZone - minMoveZone);

			Gizmos.color = Color.yellow;
			Gizmos.DrawWireCube(new Vector3(x, y, zoom), new Vector3(1, 1, 0.1f));
			
		}


#endif

		#endregion

	}

}
