using UnityEngine;

namespace Engine.Logic.Locations.Generator.Markers
{

    /// <summary>
    /// 
    /// Общая информация для всех типов маркеров
    /// ---
    /// 
    /// 
    /// </summary>
    public interface IMarker
    {

        GameObject ToObject { get; }

        Vector3 Rotation { get; set; }

        Vector3 Position { get; set; }

        Vector3 Bounds { get; }

    }

}
