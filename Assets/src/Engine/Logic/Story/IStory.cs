using Engine.Logic.Dialog;

namespace Engine.Story
{
    public interface IStory : IStoryActive
    {

        /// <summary>
        ///     Идентификатор истории.
        ///     Уникален в рамках всех историй!
        ///     ---
        ///     Story ID.
        ///     Unique within all stories!
        /// </summary>
        string StoryID { get; }

        /// <summary>
        ///     Вызывается когда необходимо инициализировать историю
        ///     Вызовется для любой истории, и той что уже была проиграна, и той что ещё не воспроизводилась
        ///     ---
        ///     Called when it is necessary to initialize story
        ///     Called for any story, both the one that has already been played and the one that has not yet been played
        /// </summary>
        void Init();

        /// <summary>
        ///     Вызывается когда история завершается
        ///     ---
        ///     Called when the story ends
        /// </summary>
        void Complete();

        /// <summary>
        ///     Вызывается сразу после инициализации у тех историй, которые ранее ни разу не выполнялись.
        ///     (выполняется Init->FirstInit), история завершилась (FirstComplete).
        ///     (первый запуск этой истории)
        ///     ---
        ///     Called immediately after initialization for those stories that have never been executed before
        ///     (the first start of this story)
        /// </summary>
        /// <returns>
        ///     Возвращает логическое значение "Нужно ли выполнять CreateDialog"?
        ///     true - необходимо выполнить историю и запустить CreateDialog
        ///     false - история не нужно выполнять, можно завершать работу
        ///     ---
        ///     Returns the boolean value "Is it necessary to execute CreateDialog"?
        ///     true - story should be executed and CreateDialog should be started
        ///     false - story does not need to be executed, you can terminate
        /// </returns>
        bool FirstInit();
        
        /// <summary>
        ///     Вызывается сразу после инициализации у тех историй, которые ранее были выполнены
        ///     Например, игрок впервые попадает на локацию, взаимодействует с историей (выполняется Init->FirstInit), история завершилась (FirstComplete).
        ///     Далее игрок покидает локацию, затем снова возвращается в локацию (выполняется Init->SecondInit), история завершилась (SecondComplete)
        ///     (последующие запуски этой истории)
        ///     ---
        ///     Called immediately after initialization for those stories that were previously executed
        ///     For example, the player enters the location, interacts with the story, the story ended.
        ///     Then the player leaves the location, then returns to the location again. Thus, the given story
        ///     appears in the location again, and can start again. For these cases, after initialization the
        ///     this method.
        ///     (subsequent runs of this story)
        /// </summary>
        /// <returns>
        ///     Возвращает логическое значение "Нужно ли выполнять CreateDialog"?
        ///     true - необходимо выполнить историю и запустить CreateDialog
        ///     false - история не нужно выполнять, можно завершать работу
        ///     ---
        ///     Returns the boolean value "Is it necessary to execute CreateDialog"?
        ///     true - story should be executed and CreateDialog should be started
        ///     false - story does not need to be executed, you can terminate
        /// </returns>
        bool SecondInit();

        /// <summary>
        ///     Выполняет формирование истории, для её последующего запуска
        ///     ---
        ///     Performs the formation of story, for its subsequent launch
        /// </summary>
        /// <param name="dlg">
        ///     Экземпляр очереди диалога для данной истории.
        ///     ---
        ///     A copy of the dialog queue for this story.
        /// </param>
        void CreateDialog(DialogQueue dlg);

        /// <summary>
        ///     Выполняется когда историю запускали в первый раз и она завершилась
        ///     ---
        ///     Executed when the story is run for the first time and it has ended
        /// </summary>
        void FirstComplete();
        
        /// <summary>
        ///     Выполняется когда история уже ранее запускалась и завершалась, тоесть в последующие разы,
        ///     когда история заканчивается.
        ///     ---
        ///     Executed when the story has already been run and finished, i.e. at subsequent times,
        ///     when the story ends.
        /// </summary>
        void SecondComplete();

        /// <summary>
        ///     Деструктор истории
        ///     ---
        ///     The Destructor of story
        /// </summary>
        void Destruct();

    }
    
}