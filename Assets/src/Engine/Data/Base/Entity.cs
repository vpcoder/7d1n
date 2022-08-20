using Engine.Data.Factories;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Engine.Data
{

    ///<summary>
    ///
    /// Базовая сущность предмета/объекта/ресурса
    /// ---
    /// The basic essence of the subject/object/resource
    /// 
    ///</summary>
    [Serializable]
    public abstract class Entity : IEntity
    {

        ///<summary>
        ///     Идентификатор предмета
        ///     Обязан быть уникален в рамках всех групп и типов
        ///     ---
        ///     Item identifier
        ///     Must be unique within all groups and types
        ///</summary>
        public virtual long ID { get; set; }

        ///<summary>
        ///     Группа предмета
        ///     ---
        ///     Item Group
        ///</summary>
        public virtual GroupType Type { get; set; }

        ///<summary>
        ///     Тип инструмента
        ///     ---
        ///     Type of tool
        ///</summary>
        public virtual ISet<ToolType> ToolType { get; set; }

        ///<summary>
        ///     Локализованное название предмета
        ///     ---
        ///     Localized subject name
        ///</summary>
        public virtual string Name { get; set; }

        ///<summary>
        ///     Локализованное описание предмета
        ///     ---
        ///     Localized description of the subject
        ///</summary>
        public virtual string Description { get; set; }

        ///<summary>
        ///     Внешний вид предмета/объекта/ресурса в инвентаре
        ///     ---
        ///     Appearance of item/object/resource in inventory
        ///</summary>
        public virtual Sprite Sprite { get { return SpriteFactory.Instance.Get(ID); } }

        /// <summary>
        ///     Объект предмета на локации
        ///     ---
        ///     The object of the item on the location
        /// </summary>
        public virtual GameObject Prefab { get { return ObjectFactory.Instance.Get(ID); } }

        /// <summary>
        ///     Копирует текущую сущность в новый экземпляр
        ///     ---
        ///     Copies the current entity into a new instance
        /// </summary>
        /// <returns>
        ///     Копия сущности
        ///     ---
        ///     Entity Copy
        /// </returns>
        public abstract IIdentity Copy();

        /// <summary>
        ///     Если true - вес не рассчитывается, а берётся из свойства (*data.xml) в описании предмета
        ///     ---
        ///     If true - the weight is not calculated, but taken from the property (*data.xml) in the item description
        /// </summary>
       
        public bool StaticWeight { get; set; }
        
        /// <summary>
        ///     Вес сущности.
        ///     Рассчитывается рекурсивно, если сущность состоить из нескольких частей, её вес будет равен сумме весов частей.
        ///     ---
        ///     Entity weight.
        ///     If the entity consists of several parts, its weight will be equal to the sum of the weights of the parts.
        /// </summary>
        public long Weight { get; set; }

    }

}
