namespace Engine.Story
{
    
    /// <summary>
    ///
    /// Объект истории, с которым персонаж игрока может взаимодействовать (получать нажатия)
    /// ---
    /// Story object with which the player's character can interact (receive clicks)
    /// 
    /// </summary>
    public interface IStorySelectCatcher : IStoryActive
    {

        /// <summary>
        ///     Выполняется когда выделенный объект находится в области досягаемости персонажа игрока
        ///     ---
        ///     Executed when the highlighted object is in range of the player's character
        /// </summary>
        void SelectInDistance();
        
        /// <summary>
        ///     Выполняется когда выделенный объект находится слишком далеко от персонажа игрока
        ///     ---
        ///     Executed when the highlighted object is too far from the player's character
        /// </summary>
        void SelectOutDistance();

    }
    
}