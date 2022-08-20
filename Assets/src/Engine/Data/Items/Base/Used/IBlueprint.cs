namespace Engine.Data.Blueprints.Base
{
    
    /// <summary>
    /// 
    /// Чертёж, по которому можно создать предмет
    /// ---
    /// A blueprint from which you can create an object
    /// 
    /// </summary>
    public interface IBlueprint : IUsed
    {
        
        /// <summary>
        ///     Группа в которой находится чертёж
        ///     ---
        ///     Group in which the blueprint is located
        /// </summary>
        BlueprintGroupType BlueprintGroupType { get; set; }
        
        /// <summary>
        ///     Фильтры, которым соответствует чертёж
        ///     ---
        ///     Filters to which the blueprint corresponds
        /// </summary>
        BlueprintFilterType BlueprintFilter { get; set; }

        /// <summary>
        ///     Идентификатор создаваемого предмета
        ///     ---
        ///     The identifier of the item to be created
        /// </summary>
        long ItemID { get; set; }

    }
    
}