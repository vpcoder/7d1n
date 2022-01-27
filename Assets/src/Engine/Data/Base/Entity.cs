using Engine.Data.Factories;
using System;
using UnityEngine;

namespace Engine.Data
{

    ///<summary>
    /// Базовая сущность предмета/обьекта/ресурса
    ///</summary>
    [Serializable]
    public abstract class Entity : IEntity
    {

        ///<summary>
        /// Идентификатор предмета
        /// Обязан быть уникален в рамках всех групп и типов
        ///</summary>
        public virtual long ID { get; set; }

        ///<summary>
        /// Группа предмета
        ///</summary>
        public virtual GroupType Type { get; set; }

        ///<summary>
        /// Тип инструмента
        ///</summary>
        public virtual ToolType ToolType { get; set; }

        ///<summary>
        /// Локализованное название предмета
        ///</summary>
        public virtual string Name { get; set; }

        ///<summary>
        /// Локализованное описание предмета
        ///</summary>
        public virtual string Description { get; set; }

        ///<summary>
        /// Внешний вид предмета/объекта/ресурса
        ///</summary>
        public virtual Sprite Sprite { get { return SpriteFactory.Instance.Get(ID); } }

        /// <summary>
        /// Объект предмета на локации
        /// ---
        /// The object of the item on the location
        /// </summary>
        public virtual GameObject Prefab { get { return ObjectFactory.Instance.Get(ID); } }

        /// <summary>
        /// Копирует текущую сущность в новый экземпляр
        /// </summary>
        /// <returns>Копия сущности</returns>
        public abstract IIdentity Copy();

        /// <summary>
        /// Вес
        /// </summary>
        public long Weight { get; set; }

    }

}
