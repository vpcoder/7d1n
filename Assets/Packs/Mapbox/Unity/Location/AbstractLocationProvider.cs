namespace Mapbox.Unity.Location
{
	using System;
	using UnityEngine;

	public abstract class AbstractLocationProvider : MonoBehaviour, ILocationProvider
	{

		protected Location _currentLocation;

		/// <summary>
		/// Gets the last known location.
		/// </summary>
		/// <value>The current location.</value>
		public Location CurrentLocation
		{
			get
			{

#if UNITY_EDITOR && DEBUG
                _currentLocation.LatitudeLongitude = new Utils.Vector2d(54.800523d, 32.076018d);
                _currentLocation.IsLocationServiceEnabled = true;
                _currentLocation.IsLocationUpdated = true;
#endif
                return _currentLocation;
            }
		}

		public event Action<Location> OnLocationUpdated = delegate { };

		protected virtual void SendLocation(Location location)
		{
			OnLocationUpdated(location);
		}

	}

}
