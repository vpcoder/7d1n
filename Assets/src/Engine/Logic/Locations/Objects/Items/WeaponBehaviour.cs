using UnityEngine;

namespace Engine.Logic.Locations.Objects
{

    public class WeaponBehaviour : MonoBehaviour, IWeaponBehaviour
    {

        #region Hidden Fields

        [SerializeField] private Vector3 positionOffset;
        [SerializeField] private Vector3 rotationOffset;

        #endregion

        #region Properties

        public Vector3 PositionOffset => positionOffset;

        public Vector3 RotationOffset => rotationOffset;

        #endregion

    }

}
