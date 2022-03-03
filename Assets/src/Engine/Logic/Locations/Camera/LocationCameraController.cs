using UnityEngine;
using UnityEngine.EventSystems;
using Engine.Data;


namespace Engine.Logic.Map
{

    public class LocationCameraController : MonoBehaviour, IDragHandler
    {

        [SerializeField] private float _panSpeed = 0.05f;
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
        [SerializeField] private Vector2 _zoomZone = new Vector2(0f, 10f);

        #endregion

        [SerializeField] private Camera _referenceCamera;

        private float angleX = 0;
        private float angleY = 0;

        private void HandleTouch()
		{
			float zoomFactor = 0.0f;
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

            _referenceCamera.transform.position += _referenceCamera.transform.forward * deltaZoom;
        }

        private void ZoomMapUsingTouchOrMouse(float zoomFactor)
		{
            DoZoom(zoomFactor);
        }

        private void Awake()
        {
            UpdateCameraPos(0.001f, 0f);
        }

        private void UpdateCameraPos(float deltaX, float deltaY)
        {
            float radius = Vector3.Distance(_referenceCamera.transform.position, target.transform.position);

            if (!(Mathf.Approximately(deltaX, 0) && Mathf.Approximately(deltaY, 0)))
            {
                angleX -= deltaX;
                angleY = Mathf.Clamp(Mathf.Rad2Deg * (angleY - deltaY), -60, -5) * Mathf.Deg2Rad; // Разрешаем перемещаться по Y в отрезке от 5 до 60 градусов

                float x = radius * Mathf.Cos(angleX) * Mathf.Sin(angleY);
                float z = radius * Mathf.Sin(angleX) * Mathf.Sin(angleY);
                float y = radius * Mathf.Cos(angleY);

                _referenceCamera.transform.position = new Vector3(x + target.transform.position.x,
                                                                  y + target.transform.position.y,
                                                                  z + target.transform.position.z);

                _referenceCamera.transform.LookAt(target.transform.position);
            }
        }

        private void HandleMouseAndKeyBoard()
		{
            if (DeviceInput.TouchCount == 1)
			{
                float deltaX = Input.GetAxis("Mouse X") * _panSpeed;
                float deltaY = Input.GetAxis("Mouse Y") * _panSpeed * 0.3f;
                UpdateCameraPos(deltaX, deltaY);
            }

            DoZoom(Input.GetAxis("Mouse ScrollWheel"));
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (Game.Instance.Runtime.ActionMode != ActionMode.Rotation)
                return;

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
