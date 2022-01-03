
using UnityEngine;

namespace Engine.Logic.Locations
{

    /// <summary>
    /// Объект, который можно использовать
    /// </summary>
    public interface IUseObjectController
    {

        /// <summary>
        /// Выполняет действие над предметом
        /// </summary>
        void DoUse();

        /// <summary>
        /// Возвращает объект Unity
        /// </summary>
        GameObject ToObj { get; }

    }

}
