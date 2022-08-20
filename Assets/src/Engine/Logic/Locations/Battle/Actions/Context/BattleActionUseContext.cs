
namespace Engine.Logic.Locations.Battle.Actions
{

    /// <summary>
    ///
    /// Контекст взаимодействия персонажа с объектом в сцене (например открывание двери)
    /// ---
    /// Context of the character's interaction with an object in the scene (e.g. opening a door)
    /// 
    /// </summary>
    public class BattleActionUseContext : BattleActionContext
    {

        /// <summary>
        ///     Объект с которым персонаж взаимодействует
        ///     ---
        ///     The object with which the character interacts
        /// </summary>
        public LocationObjectBattleUseController UseItem { get; set; }

    }

}
