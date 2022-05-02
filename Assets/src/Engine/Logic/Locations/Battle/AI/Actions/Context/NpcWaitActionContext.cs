
namespace Engine.Logic.Locations
{

    /// <summary>
    /// 
    /// Контекст ожидания/бездействия NPC,
    /// необходим для создания пауз и задержек в действиях NPC, чтобы со стороны NPC был более естественен
    /// ---
    /// The NPC's wait/no-action context,
    /// necessary to create pauses and delays in the NPC's actions, so that the NPC's side is more natural
    /// 
    /// </summary>
    public class NpcWaitActionContext : NpcBaseActionContext
    {

        /// <summary>
        /// Количество секунд, которые NPC должен будет ожидать/бездействовать
        /// ---
        /// The number of seconds the NPC will have to wait/not act
        /// </summary>
        public float WaitDelay { get; set; } = 0f;

    }

}
