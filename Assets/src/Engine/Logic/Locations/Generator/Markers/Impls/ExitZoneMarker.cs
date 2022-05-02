using System;
using UnityEngine;

namespace Engine.Logic.Locations.Generator.Markers
{

    [RequireComponent(typeof(BoxCollider))]
    public class ExitZoneMarker : MarkerBase
    {

        public override Vector3 Bounds => GetComponent<BoxCollider>().bounds.size;


        #region Editor

#if UNITY_EDITOR && DEBUG

        private void OnDrawGizmos()
        {
            
        }

#endif

        #endregion


    }

}
