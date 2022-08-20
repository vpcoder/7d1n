using System.Collections.Generic;
using System.Linq;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UnityEngine
{

	public static class DeviceInput
	{

        private static int UI_LAYOUT = LayerMask.NameToLayer("UI");
		private static int currentTouchIndex = 0;

        public static bool IsUiTouch()
        {
            bool uiFlag = false;
            //if ((TouchCount == 1 && GetTouchPhase == TouchPhase.Began) || GetTouchPhase == TouchPhase.Ended)
            //{
            //   TouchPad
            //    var data = new PointerEventData(EventSystem.current);
            //    data.position = UITouchPosition;
            //    List<RaycastResult> results = new List<RaycastResult>();
            //    ObjectFinder.Find<GraphicRaycaster>().Raycast(data, results);
            //
            //    if (results.Count > 0)
            //    {
            //        Debug.Log(string.Join("\r\n->", results.Select(o => o.gameObject.transform.name)));
            //        //uiFlag = hits.Where(hit => hit.collider?.gameObject.GetComponent<IPointerClickHandler>() != null).Any();
            //    }
            //}
            if(uiFlag)
            {
                Debug.Log("tap on UI");
            }
            return uiFlag;
        }

		public static int CurrentTouchIndex
		{
			get
			{
				return currentTouchIndex;
			}
			set
			{
				currentTouchIndex = value;
			}
		}

		public static TouchPhase GetTouchPhase
		{

			get
			{

#if UNITY_STANDALONE || UNITY_EDITOR

				if (Input.GetMouseButtonDown(0))
					return TouchPhase.Began;

				if (Input.GetMouseButton(0))
					return TouchPhase.Moved;

				if (Input.GetMouseButtonUp(0))
					return TouchPhase.Ended;

				return TouchPhase.Stationary;

#else
                if(Input.touchCount > 0)
				    return Input.GetTouch(currentTouchIndex).phase;
                else
                    return TouchPhase.Canceled;

#endif

            }

        }

		public static int TouchCount
		{

			get
			{

#if UNITY_STANDALONE || UNITY_EDITOR

				return Input.GetMouseButton(0) ? 1 : 0;

#else

				return Input.touchCount;

#endif

			}

		}

		public static Vector2 UITouchPosition
		{

			get
			{

#if UNITY_STANDALONE || UNITY_EDITOR

				return new Vector2(Input.mousePosition.x,
								   Screen.height - Input.mousePosition.y);

#else

				return Input.GetTouch(currentTouchIndex).position;

#endif

			}

		}

		public static Vector3 TouchPosition3D
		{

			get
			{

#if UNITY_STANDALONE || UNITY_EDITOR

				return Input.mousePosition;


#else

				Vector2 pos = Input.GetTouch(currentTouchIndex).position;
				return new Vector3(pos.x,pos.y,0);

#endif

			}
		}

		public static Vector2 GetTouchPosition(int index)
		{

#if UNITY_STANDALONE || UNITY_EDITOR

				return new Vector2(Input.mousePosition.x,
								   Input.mousePosition.y);

#else

				return Input.GetTouch(index).position;

#endif

		}

		public static Vector2 TouchPosition
		{

			get
			{

#if UNITY_STANDALONE || UNITY_EDITOR

				return new Vector2(Input.mousePosition.x,
								   Input.mousePosition.y);

#else

				return Input.GetTouch(currentTouchIndex).position;

#endif

			}

		}

		public static Vector2 TouchDelta
		{

			get
			{

#if UNITY_STANDALONE || UNITY_EDITOR

				return new Vector2(Input.mouseScrollDelta.x,
								   Input.mouseScrollDelta.y);

#else

				return Input.GetTouch(currentTouchIndex).deltaPosition;

#endif

			}

		}

	}

}
