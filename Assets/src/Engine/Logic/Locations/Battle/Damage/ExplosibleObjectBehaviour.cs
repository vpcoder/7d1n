using UnityEngine;

namespace Engine.Logic.Locations.Objects
{

    /// <summary>
    /// 
    /// Базовый неживой объект, который может быть уничтожен
    /// ---
    /// Basic inanimate object that can be destroyed
    /// 
    /// </summary>
    [RequireComponent(typeof(MeshExploder))]
    public class ExplosibleObjectBehaviour : DestroyedBase
    {

        #region Methods

        /// <summary>
        ///     Выполняет взрыв меша в процессе уничтожения объекта
        ///     ---
        ///     Blows up a mesh in the process of destroying an object
        /// </summary>
        public override void DoDestroy()
        {
            GetComponent<MeshExploder>().Explode();
            base.DoDestroy();
        }

        #endregion

    }

}
