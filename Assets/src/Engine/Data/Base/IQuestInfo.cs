using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Engine.Data
{
    
    /// <summary>
    ///
    /// Квест.
    /// Базовые параметры и свойства квеста.
    /// ---
    /// Quest.
    /// Basic parameters and properties of the quest.
    /// 
    /// </summary>
    public interface IQuestInfo : ISerializable
    {
        
        /// <summary>
        ///     Статус квеста
        ///     ---
        ///     Quest Status
        /// </summary>
        QuestState State { get; set; }
        
        /// <summary>
        ///     Индекс текущей стадии квеста
        ///     ---
        ///     Index of the current stage of the quest
        /// </summary>
        int Stage { get; set; }

        /// <summary>
        ///     Набор данных относящихся к  квесту (заметки, теги и т.д.)
        ///     ---
        ///     Set of data related to the quest (notes, tags, etc.)
        /// </summary>
        ISet<string> HashData { get; set; }

        /// <summary>
        ///     true - если квест был взят и начат
        ///     false - иначе
        ///     ---
        ///     true - if the quest has been taken and started
        ///     false - otherwise
        /// </summary>
        bool IsStarted { get; }
        
        /// <summary>
        ///     true - если квест был взят и закончен
        ///     false - иначе
        ///     ---
        ///     true - if the quest has been taken and completed
        ///     false - otherwise
        /// </summary>
        bool IsCompleted { get; }

        /// <summary>
        ///     Наименование квеста
        ///     ---
        ///     Quest name
        /// </summary>
        string Name { get; }
        
        /// <summary>
        ///     Описание всех стадий квеста
        ///     ---
        ///     Description of all stages of the quest
        /// </summary>
        IList<string> getDescription();
        
        /// <summary>
        ///     Перечисление всех предыдущих стадий квеста
        ///     ---
        ///     Listing of all previous stages of the quest
        /// </summary>
        IEnumerable<string> getBeforeStageDescriptions();
        
        /// <summary>
        ///     Описание текущей стадии квеста
        ///     ---
        ///     Description of the current stage of the quest
        /// </summary>
        string getCurrentStageDescription();
    }
    
}