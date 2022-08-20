using System;
using UnityEngine;

namespace UnityEngine
{

	public static class DeviceRaycast
	{

		public static bool Raycast(out RaycastHit hit, Vector3 point, float distance)
		{
			Ray ray = Camera.main.ScreenPointToRay(point);
			return Physics.Raycast(ray, out hit, distance);
		}

		public static RaycastHit[] RaycastAll(Vector3 point, float distance)
		{
			Ray ray = Camera.main.ScreenPointToRay(point);
			return Physics.RaycastAll(ray, distance);
		}

	}

}

