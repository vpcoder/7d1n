using System.Linq;

namespace Engine.Data.Blueprints.Base
{
    
    /// <summary>
    /// 
    /// Чертёж, по которому можно создать предмет
    /// ---
    /// A blueprint from which you can create an object
    /// 
    /// </summary>
    public class Blueprint : Used, IBlueprint
    {
        
        //  ToolType
        //
        //  Инструменты, необходимые для того чтобы собрать предмет по этому чертежу
        //  ---
        //  Tools needed to assemble the object according to this blueprint
        
        //  Parts
        //  
        //  Ресурсы, необходимые для того чтобы собрать предмет по этому чертежу
        //  ---
        //  The resources needed to assemble an item from this blueprint are
        
        /// <summary>
        ///     Группа в которой находится чертёж
        ///     ---
        ///     Group in which the blueprint is located
        /// </summary>
        public BlueprintGroupType BlueprintGroupType { get; set; }
        
        /// <summary>
        ///     Фильтры, которым соответствует чертёж
        ///     ---
        ///     Filters to which the blueprint corresponds
        /// </summary>
        public BlueprintFilterType BlueprintFilter { get; set; }
        
        /// <summary>
        ///     Идентификатор создаваемого предмета
        ///     ---
        ///     The identifier of the item to be created
        /// </summary>
        public long ItemID { get; set; }
        
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
        public override IIdentity Copy()
        {
            return new Blueprint()
            {
                ID = ID,
                ToolType = ToolType,
                Type = Type,
                Name = Name,
                Description = Description,
                Count = Count,
                StackSize = StackSize,
                StaticWeight = StaticWeight,
                Weight = Weight,
                Parts = Parts?.ToList(),
                Level = Level,
                Author = Author,

                UseAction = UseAction,
                UseSoundType = UseSoundType,
                
                BlueprintGroupType = BlueprintGroupType,
                BlueprintFilter = BlueprintFilter,
                ItemID = ItemID,
            };
        }
        
    }
    
}