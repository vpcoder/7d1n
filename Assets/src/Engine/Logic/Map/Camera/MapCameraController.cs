namespace Engine.Logic.Map
{
	using UnityEngine;
	using UnityEngine.EventSystems;
    using Mapbox.Unity.Location;
    using UnityEngine.UI;

    public class MapCameraController : MonoBehaviour,
                                       IDragHandler
    {

        [SerializeField] private Text txtDebugLog;
        [SerializeField] private AbstractLocationProvider _gps;
        [SerializeField] private float _panSpeed = 20f;
        [SerializeField] private Transform target;

        #region Map Zoom

        /// <summary>
        /// Скорость наращивания увеличения
        /// </summary>
        [SerializeField] private float _zoomSpeed = 50f;

        /// <summary>
        /// Текущий зум
        /// </summary>
        [SerializeField] private float _currentZoom = 0;

        /// <summary>
        /// Допустимые границы увеличения
        /// </summary>
        [SerializeField] private Vector2 _zoomZone = new Vector2(-10f, 10f);

        #endregion

        [SerializeField] private Camera _referenceCamera;

        private void HandleTouch()
		{
			float zoomFactor = 0.0f;
			//pinch to zoom. 
			switch (Input.touchCount)
			{
				case 1:
					{
						HandleMouseAndKeyBoard();
					}
					break;
				case 2:
					{
						Touch touchZero = Input.GetTouch(0);
						Touch touchOne = Input.GetTouch(1);
						Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
						Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;
						float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
						float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;
						zoomFactor = 0.01f * (touchDeltaMag - prevTouchDeltaMag);
					}
					ZoomMapUsingTouchOrMouse(zoomFactor);
					break;
				default:
					break;
			}
		}

        /// <summary>
        /// Изменение приближения и отдаления карты
        /// </summary>
        private void DoZoom(float zoomFactor)
        {
            var deltaZoom = zoomFactor * _zoomSpeed;

            if (_zoomZone.x > _currentZoom + deltaZoom)
                deltaZoom = 0;

            if (_zoomZone.y < _currentZoom + deltaZoom)
                deltaZoom = 0;

            _currentZoom += deltaZoom;
            _referenceCamera.fieldOfView = _currentZoom;
        }

        private void Update()
        {
            var location = _gps.CurrentLocation;
            var position = location.LatitudeLongitude;

            txtDebugLog.text = position.ToString() + "\n" + location.UserHeading + "\n" + location.IsLocationServiceEnabled + "\n" + location.IsLocationServiceInitializing;

            //_map.UpdateMap(position);
        }

        private void ZoomMapUsingTouchOrMouse(float zoomFactor)
		{
            DoZoom(zoomFactor);
        }

        private float angleX = 0;
        private float angleY = 0;

        private void HandleMouseAndKeyBoard()
		{
            if (DeviceInput.TouchCount == 1)
			{
                float radius = Vector3.Distance(_referenceCamera.transform.position, target.transform.position);

                float deltaX = Input.GetAxis("Mouse X") * _panSpeed;
                float deltaY = Input.GetAxis("Mouse Y") * _panSpeed * 0.3f;

                if (!(Mathf.Approximately(deltaX, 0) && Mathf.Approximately(deltaY, 0)))
                {
                    angleX += deltaX;
                    angleY = Mathf.Clamp(Mathf.Rad2Deg * (angleY + deltaY), -60, -5) * Mathf.Deg2Rad; // от 5 до 45 градусов

                    float x = radius * Mathf.Cos(angleX) * Mathf.Sin(angleY);
                    float z = radius * Mathf.Sin(angleX) * Mathf.Sin(angleY);
                    float y = radius * Mathf.Cos(angleY);

                    _referenceCamera.transform.position = new Vector3(x + target.transform.position.x,
                                                                      y + target.transform.position.y,
                                                                      z + target.transform.position.z);

                    _referenceCamera.transform.LookAt(target.transform.position);
                }
            }

            DoZoom(Input.GetAxis("Mouse ScrollWheel"));
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (Input.touchSupported && Input.touchCount > 0)
            {
                HandleTouch();
            }
            else
            {
                HandleMouseAndKeyBoard();
            }
        }

    }

}
