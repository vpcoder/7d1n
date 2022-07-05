using System.Collections.Generic;
using UnityEngine;

namespace Engine.Data
{

    ///<summary>
    ///
    /// Сущность предмета/объекта/ресурса
    /// ---
    /// Entity of the item/object/resource
    /// 
    ///</summary>
    public interface IEntity : IIdentity
    {

        /// <summary>
        ///     Тип инструмента
        ///     ---
        ///     Type of tool
        /// </summary>
        ISet<ToolType> ToolType { get; set; }

        ///<summary>
        ///     Группа предмета
        ///     ---
        ///     Item Group
        ///</summary>
        GroupType Type { get; set; }

        ///<summary>
        ///     Локализованное название предмета
        ///     ---
        ///     Localized item name
        ///</summary>
        string Name { get; set; }

        ///<summary>
        ///     Локализованное описание предмета
        ///     ---
        ///     Localized item description
        ///</summary>
        string Description { get; set; }

        ///<summary>
        ///     Внешний вид предмета/объекта/ресурса
        ///     ---
        ///     Appearance of the item/object/resource
        ///</summary>
        Sprite Sprite { get; }

        /// <summary>
        ///     Объект предмета на локации
        ///     ---
        ///     The object of the item on the location
        /// </summary>
        GameObject Prefab { get; }

        /// <summary>
        ///     Если true - вес не рассчитывается, а берётся из свойства (*data.xml) в описании предмета
        ///     ---
        ///     If true - the weight is not calculated, but taken from the property (*data.xml) in the item description
        /// </summary>
       
        bool StaticWeight { get; set; }
        
        /// <summary>
        ///     Вес сущности.
        ///     Рассчитывается рекурсивно, если сущность состоить из нескольких частей, её вес будет равен сумме весов частей.
        ///     ---
        ///     Entity weight.
        ///     If the entity consists of several parts, its weight will be equal to the sum of the weights of the parts.
        /// </summary>
        long Weight { get; set; }

    }

}
