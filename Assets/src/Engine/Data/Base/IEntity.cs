using UnityEngine;

namespace Engine.Data
{

    ///<summary>
    /// Сущность предмета/обьекта/ресурса
    ///</summary>
    public interface IEntity : IIdentity
    {

        /// <summary>
        /// Тип инструмента
        /// </summary>
        ToolType ToolType { get; set; }

        ///<summary>
        /// Группа предмета
        ///</summary>
        GroupType Type { get; set; }

        ///<summary>
        /// Локализованное название предмета
        ///</summary>
        string Name { get; set; }

        ///<summary>
        /// Локализованное описание предмета
        ///</summary>
        string Description { get; set; }

        ///<summary>
        /// Внешний вид предмета/объекта/ресурса
        ///</summary>
        Sprite Sprite { get; }

        /// <summary>
        /// Вес
        /// </summary>
        long Weight { get; set; }

    }

}
