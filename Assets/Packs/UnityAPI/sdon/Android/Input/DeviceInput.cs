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
