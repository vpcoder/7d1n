using UnityEngine;
using UnityEngine.EventSystems;
using Engine.Data;


namespace Engine.Logic.Map
{

    public class LocationCameraController : MonoBehaviour, IDragHandler
    {

	    private const float START_CAMERA_HEIGHT = 15f;
        [SerializeField] private float panSpeed = 0.05f;
        [SerializeField] private Transform target;

        #region Map Zoom

        /// <summary>
        /// Скорость наращивания увеличения
        /// </summary>
        [SerializeField] private float zoomSpeed = 50f;

        /// <summary>
        /// Текущий зум
        /// </summary>
        [SerializeField] private float currentZoom;

        /// <summary>
        /// Допустимые границы увеличения
        /// </summary>
        [SerializeField] private Vector2 zoomZone = new Vector2(0f, 10f);

        #endregion

        [SerializeField] private Camera referenceCamera;

        private float angleX;
        private float angleY;

        private void HandleTouch()
		{
			switch (Input.touchCount)
			{
				case 1:
					HandleMouseAndKeyBoard();
					break;
				case 2:
					Touch touchZero = Input.GetTouch(0);
					Touch touchOne = Input.GetTouch(1);
					Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
					Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;
					float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
					float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;
					float zoomFactor = 0.01f * (touchDeltaMag - prevTouchDeltaMag);

					ZoomMapUsingTouchOrMouse(zoomFactor);
					break;
			}
		}

        /// <summary>
        /// Изменение приближения и отдаления карты
        /// </summary>
        private void DoZoom(float zoomFactor)
        {
            var deltaZoom = zoomFactor * zoomSpeed;

            if (zoomZone.x > currentZoom + deltaZoom)
                deltaZoom = 0;

            if (zoomZone.y < currentZoom + deltaZoom)
                deltaZoom = 0;

            currentZoom += deltaZoom;

            var cameraTransform = referenceCamera.transform;
            cameraTransform.position += cameraTransform.forward * deltaZoom;
        }

        private void ZoomMapUsingTouchOrMouse(float zoomFactor)
		{
            DoZoom(zoomFactor);
        }

        private void Awake()
        {
            UpdateCameraPos();
        }

        public void UpdateCameraPos()
        {
	        var targetPos = target.transform.position + new Vector3(0f, 2f, 0f); // Смотрим чуть выше груди персонажа
	        var cameraTransform = referenceCamera.transform;
	        
	        float radius = Vector3.Distance(cameraTransform.position, targetPos);
	        float x = radius * Mathf.Cos(angleX) * Mathf.Sin(angleY);
	        float z = radius * Mathf.Sin(angleX) * Mathf.Sin(angleY);

	        cameraTransform.position = new Vector3(x + targetPos.x,
												   START_CAMERA_HEIGHT + targetPos.y,
		        								   z + targetPos.z);

	        cameraTransform.LookAt(targetPos);
        } 
        
        private void UpdateCameraPos(float deltaX, float deltaY)
        {
            angleX -= deltaX;
            angleY = Mathf.Clamp(Mathf.Rad2Deg * (angleY - deltaY), -60, -5) * Mathf.Deg2Rad; // Разрешаем перемещаться по Y в отрезке от 5 до 60 градусов
            UpdateCameraPos();
        }

        private void HandleMouseAndKeyBoard()
		{
            if (DeviceInput.TouchCount == 1)
			{
                float deltaX = Input.GetAxis("Mouse X") * panSpeed;
                float deltaY = Input.GetAxis("Mouse Y") * panSpeed * 0.3f;
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
