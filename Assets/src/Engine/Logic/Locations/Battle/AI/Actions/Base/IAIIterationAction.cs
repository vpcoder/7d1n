
namespace Engine.Logic.Locations
{

    /// <summary>
    /// 
    /// Модуль действия ИИ существа, для определённого типа действия ActionType
    /// Задача итератора - выполнять необходимые операции для совершения действия.
    /// Примером итератора может быть - перемещение NPC. Для перемещения создаётся контекст действия в котором сказано как ИИ должен перемещаться.
    /// Далее контекст отдаётся итератору, который выполняет необходимые низкоуровневые операции над NPC, чтобы совершить указанное действие - двигает тело NPC, выполняет переключение анимаций, проверяет чтобы NPC ни с кем не столкнулся и т.д.
    /// ---
    /// Creature AI action module, for a certain ActionType
    /// The iterator's task is to perform the necessary operations to perform an action.
    /// An example of an iterator would be moving an NPC. For a move, an action context is created that specifies how the AI must move.
    /// Then the context is given to the iterator, which performs the necessary low-level operations on the NPC to perform the specified action - moves the NPC's body, performs switching animations, checks that the NPC doesn't collide with anyone, etc.
    /// 
    /// </summary>
    public interface IAiIterationAction
    {

        /// <summary>
        /// Тип действия для которого рассчитан жтот модуль
        /// ---
        /// The type of action for which this module is designed
        /// </summary>
        NpcActionType ActionType { get; }

        /// <summary>
        /// Метод вызывается в момент начала совершения действия ИИ существа
        /// ---
        /// The method is called at the moment the creature AI action starts
        /// </summary>
        /// <param name="npc">
        /// Сам NPC, который выполняет действие
        /// ---
        /// The NPC who performs the action
        /// </param>
        /// <param name="actionContext">
        /// Контекст операции, которая выполняется ИИ существа
        /// ---
        /// Context of the operation performed by the creature AI
        /// </param>
        void Start(EnemyNpcBehaviour npc, NpcBaseActionContext actionContext);

        /// <summary>
        /// Выполняет итерацию действия ИИ существа с учётом контекста операции
        /// ---
        /// Iterates the action of the creature AI, taking into account the context of the operation
        /// </summary>
        /// <param name="npc">
        /// Сам NPC, который выполняет действие
        /// ---
        /// The NPC who performs the action
        /// </param>
        /// <param name="actionContext">
        /// Контекст операции, которая выполняется ИИ существа
        /// ---
        /// Context of the operation performed by the creature AI
        /// </param>
        /// <param name="timestamp">
        /// Время начала выполнения действия ИИ существа
        /// ---
        /// Time of the start of the AI creature action
        /// </param>
        /// <returns>
        /// Возвращает:
        /// True - если в этой итерации удалось закончить действие,
        /// False - если действие ещё продолжается, и необходимо выполнить ещё итерации
        /// ---
        /// Returns:
        /// True - if in this iteration the action was completed,
        /// False - if the action is still in progress and more iterations are needed
        /// </returns>
        bool Iteration(EnemyNpcBehaviour npc, NpcBaseActionContext actionContext, float timestamp);

        /// <summary>
        /// The method is called when the action being performed is finished
        /// ---
        /// The method is called at the moment the creature AI action starts
        /// </summary>
        /// <param name="npc">
        /// Сам NPC, который выполняет действие
        /// ---
        /// The NPC who performs the action
        /// </param>
        /// <param name="actionContext">
        /// Контекст операции, которая выполняется ИИ существа
        /// ---
        /// Context of the operation performed by the creature AI
        /// </param>
        void End(EnemyNpcBehaviour npc, NpcBaseActionContext actionContext, float timestamp);

    }

}
