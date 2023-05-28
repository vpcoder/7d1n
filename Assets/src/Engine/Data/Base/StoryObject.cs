using System;

namespace Engine.Data
{

    /// <summary>
    /// 
    /// Базовый класс объекта хранилища
    /// ---
    /// Storage object base class
    /// 
    /// </summary>
    [Serializable]
    public abstract class StoryObject : IStoryObject
    {

        ///<summary>
        ///     Идентификатор объекта
        ///     Обязан быть уникален в рамках своего типа
        ///     ---
        ///     Object identifier
        ///     Must be unique within its type
        ///</summary>
        public long ID { get; set; }

    }

}
