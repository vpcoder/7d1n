using UnityEngine;

namespace Engine.Data
{

    /// <summary>
    ///
    /// Фактор, влияющий на персонажей
    /// ---
    /// Factor that affects the characters
    /// 
    /// </summary>
    public interface IFactor : IIdentity
    {

        /// <summary>
        ///     Краткое название фактора
        ///     ---
        ///     Short name of the factor
        /// </summary>
        string Name { get; set; }

        /// <summary>
        ///     Описание фактора
        ///     ---
        ///     Factor description
        /// </summary>
        string Description { get; set; }

        /// <summary>
        ///     Изображение фактора на панели факторов
        ///     ---
        ///     Picture of the factor on the factor panel
        /// </summary>
        Sprite Sprite { get; set; }


        /// <summary>
        ///     Количество итераций на которые фактор ещё будет действовать
        ///     (когда станет равно или меньше 0 - фактор исчезнет)
        ///     ---
        ///     The number of iterations for which the factor will still act
        ///     (when it becomes equal or less than 0, the factor will disappear)
        /// </summary>
        int Iteration { get; set; }

        /// <summary>
        ///     Время длительности фактора в итерациях
        ///     ---
        ///     Factor duration time in iterations
        /// </summary>
        int DurationCount { get; set; }

        /// <summary>
        ///     Срабатывает 1 раз во время добавления фактора
        ///     ---
        ///     Triggers once during the addition of a factor
        /// </summary>
        FactorActionDelegate StartFactor { get; set; }

        /// <summary>
        ///     Срабатывает один раз во время удаления фактора
        ///     ---
        ///     Triggers once during factor removal
        /// </summary>
        FactorActionDelegate EndFactor { get; set; }

        /// <summary>
        ///     Срабатывает каждую секунду, пока действует фактор
        ///     ---
        ///     It is triggered every second as long as the factor
        /// </summary>
        FactorActionDelegate DoFactor { get; set; }

    }

}
