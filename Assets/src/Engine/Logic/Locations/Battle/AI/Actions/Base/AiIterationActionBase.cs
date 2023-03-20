
namespace Engine.Logic.Locations
{

    /// <summary>
    /// 
    /// Базовый модуль действия ИИ NPC, для определённого типа действия ActionType
    /// Задача итератора - выполнять необходимые операции для совершения действия.
    /// Примером итератора может быть - перемещение NPC. Для перемещения создаётся контекст действия в котором сказано как ИИ должен перемещаться.
    /// Далее контекст отдаётся итератору, который выполняет необходимые низкоуровневые операции над NPC, чтобы совершить указанное действие - двигает тело NPC, выполняет переключение анимаций, проверяет чтобы NPC ни с кем не столкнулся и т.д.
    /// ---
    /// 
    /// The basic NPC AI action module, for a certain ActionType
    /// The iterator's task is to perform the necessary operations to perform the action.
    /// An example of an iterator would be moving an NPC. For a move, an action context is created that specifies how the AI should move.
    /// Then the context is given to an iterator, which performs the necessary low-level operations on the NPC to perform the action - move the NPC's body, switch animations, make sure the NPC doesn't collide with anyone, etc.
    /// 
    /// </summary>
    public abstract class AiIterationActionBase<T> : IAiIterationAction where T : NpcBaseActionContext
    {

        /// <summary>
        ///     Тип действия для которого рассчитан жтот модуль
        ///     ---
        ///     The type of action for which this module is designed
        /// </summary>
        public abstract NpcActionType ActionType { get; }

        /// <summary>
        ///     Метод вызывается в момент начала совершения действия ИИ существа
        ///     ---
        ///     The method is called at the moment the creature AI action starts
        /// </summary>
        /// <param name="npc">
        ///     Сам NPC, который выполняет действие
        ///     ---
        ///     The NPC who performs the action
        /// </param>
        /// <param name="actionContext">
        ///     Контекст операции, которая выполняется ИИ существа
        ///     ---
        ///     Context of the operation performed by the creature AI
        /// </param>
        public abstract void Start(CharacterNpcBehaviour npc, T actionContext);

        /// <summary>
        ///     Метод вызывается когда совершаемое действие закончено
        ///     ---
        ///     The method is called when the action being performed is finished
        /// </summary>
        /// <param name="npc">
        ///     Сам NPC, который выполняет действие
        ///     ---
        ///     The NPC who performs the action
        /// </param>
        /// <param name="actionContext">
        ///     Контекст операции, которая выполняется ИИ существа
        ///     ---
        ///     Context of the operation performed by the creature AI
        /// </param>
        public abstract void End(CharacterNpcBehaviour npc, T actionContext, float timestamp);

        /// <summary>
        ///     Выполняет итерацию действия ИИ существа с учётом контекста операции
        ///     ---
        ///     Iterates the action of the creature AI, taking into account the context of the operation
        /// </summary>
        /// <param name="npc">
        ///     Сам NPC, который выполняет действие
        ///     ---
        ///     The NPC who performs the action
        /// </param>
        /// <param name="actionContext">
        ///     Контекст операции, которая выполняется ИИ существа
        ///     ---
        ///     Context of the operation performed by the creature AI
        /// </param>
        /// <param name="timestamp">
        ///     Время начала выполнения действия ИИ существа
        ///     ---
        ///     Time of the start of the AI creature action
        /// </param>
        /// <returns>
        ///     Возвращает:
        ///     True - если в этой итерации удалось закончить действие,
        ///     False - если действие ещё продолжается, и необходимо выполнить ещё итерации
        ///     ---
        ///     Returns:
        ///     True - if in this iteration the action was completed,
        ///     False - if the action is still in progress and more iterations are needed
        /// </returns>
        public abstract bool Iteration(CharacterNpcBehaviour npc, T actionContext, float timestamp);

        /// <summary>
        ///     Выполняет итерацию действия ИИ существа с учётом контекста операции
        ///     ---
        ///     Iterates the action of the creature AI, taking into account the context of the operation
        /// </summary>
        /// <param name="npc">
        ///     Сам NPC, который выполняет действие
        ///     ---
        ///     The NPC who performs the action
        /// </param>
        /// <param name="actionContext">
        ///     Контекст операции, которая выполняется ИИ существа
        ///     ---
        ///     Context of the operation performed by the creature AI
        /// </param>
        /// <param name="timestamp">
        ///     Время начала выполнения действия ИИ существа
        ///     ---
        ///     Time of the start of the AI creature action
        /// </param>
        /// <returns>
        ///     Возвращает:
        ///     True - если в этой итерации удалось закончить действие,
        ///     False - если действие ещё продолжается, и необходимо выполнить ещё итерации
        ///     ---
        ///     Returns:
        ///     True - if in this iteration the action was completed,
        ///     False - if the action is still in progress and more iterations are needed
        /// </returns>
        public bool Iteration(CharacterNpcBehaviour npc, NpcBaseActionContext actionContext, float timestamp)
        {
            return Iteration(npc, (T)actionContext, timestamp);
        }

        /// <summary>
        ///     Метод вызывается в момент начала совершения действия ИИ существа
        ///     ---
        ///     The method is called at the moment the creature AI action starts
        /// </summary>
        /// <param name="npc">
        ///     Сам NPC, который выполняет действие
        ///     ---
        ///     The NPC who performs the action
        /// </param>
        /// <param name="actionContext">
        ///     Контекст операции, которая выполняется ИИ существа
        ///     ---
        ///     Context of the operation performed by the creature AI
        /// </param>
        public void Start(CharacterNpcBehaviour npc, NpcBaseActionContext actionContext)
        {
            Start(npc, (T)actionContext);
        }

        /// <summary>
        ///     Метод вызывается когда совершаемое действие закончено
        ///     ---
        ///     The method is called when the action being performed is finished
        /// </summary>
        /// <param name="npc">
        ///     Сам NPC, который выполняет действие
        ///     ---
        ///     The NPC who performs the action
        /// </param>
        /// <param name="actionContext">
        ///     Контекст операции, которая выполняется ИИ существа
        ///     ---
        ///     Context of the operation performed by the creature AI
        /// </param>
        public void End(CharacterNpcBehaviour npc, NpcBaseActionContext actionContext, float timestamp)
        {
            End(npc, (T)actionContext, timestamp);
        }

    }

}
