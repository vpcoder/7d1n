namespace Mapbox.Unity.MeshGeneration.Modifiers
{
    using UnityEngine;
    using Mapbox.Unity.MeshGeneration.Data;
    using Engine.Logic.Map;

    /// <summary>
    /// Модификатор зданий
    /// Выполняется в момент инициализации здания на карте
    /// </summary>
    [CreateAssetMenu(menuName = "Mapbox/Modifiers/Building Modifier")]
    public class BuildingModifier : GameObjectModifier
    {

        public override void Run(VectorEntity ve, UnityTile tile)
        {
            if(ve.GameObject.GetComponent<Building>() == null)
                ve.GameObject.AddComponent<Building>().Init(ve, tile);
        }

    }

}
