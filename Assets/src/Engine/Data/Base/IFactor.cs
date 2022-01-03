using System;
using UnityEngine;

namespace Engine.Data
{

    /// <summary>
    /// Фактор, влияющий на персонажей
    /// </summary>
    public interface IFactor : IIdentity
    {

        /// <summary>
        /// Краткое название фактора
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Описание фактора
        /// </summary>
        string Description { get; set; }

        /// <summary>
        /// Изображение фактора на панели факторов
        /// </summary>
        Sprite Sprite { get; set; }


        /// <summary>
        /// Количество итераций на которые фактор ещё будет действовать
        /// (когда станет равно или меньше 0 - фактор исчезнет)
        /// </summary>
        int Iteration { get; set; }

        /// <summary>
        /// Время длительности фактора в итерациях
        /// </summary>
        int DurationCount { get; set; }

        /// <summary>
        /// Срабатывает 1 раз во время добавления фактора
        /// </summary>
        FactorActionDelegate StartFactor { get; set; }

        /// <summary>
        /// Срабатывает один раз во время удаления фактора
        /// </summary>
        FactorActionDelegate EndFactor { get; set; }

        /// <summary>
        /// Срабатывает каждую секунду, пока действует фактор
        /// </summary>
        FactorActionDelegate DoFactor { get; set; }

    }

}
