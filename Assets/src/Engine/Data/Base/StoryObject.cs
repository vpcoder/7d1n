using System;

namespace Engine.Data
{

    /// <summary>
    /// Базовый класс объекта хранилища
    /// </summary>
    [Serializable]
    public abstract class StoryObject : IStoryObject
    {

        ///<summary>
        /// Идентификатор объекта
        /// Обязан быть уникален в рамках своего типа
        ///</summary>
        public long ID { get; set; }

    }

}
