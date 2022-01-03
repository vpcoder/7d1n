using UnityEngine;

namespace Engine.Data
{

    /// <summary>
    /// Фактор, влияющий на персонажа
    /// </summary>
    public class Factor : IFactor
    {
        
        /// <summary>
        /// Идентификатор фактора
        /// </summary>
        public long ID { get; set; }

        /// <summary>
        /// Количество итераций на которые фактор ещё будет действовать
        /// (когда станет равно или меньше 0 - фактор исчезнет)
        /// </summary>
        public int Iteration { get; set; }

        /// <summary>
        /// Время длительности фактора в итерациях
        /// </summary>
        public int DurationCount { get; set; }

        /// <summary>
        /// Краткое название фактора
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Описание фактора
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Изображение фактора на панели факторов
        /// </summary>
        public Sprite Sprite { get; set; }

        /// <summary>
        /// Срабатывает 1 раз во время добавления фактора
        /// </summary>
        public FactorActionDelegate StartFactor { get; set; }

        /// <summary>
        /// Срабатывает один раз во время удаления фактора
        /// </summary>
        public FactorActionDelegate EndFactor { get; set; }

        /// <summary>
        /// Срабатывает каждую секунду, пока действует фактор
        /// </summary>
        public FactorActionDelegate DoFactor { get; set; }


        public IIdentity Copy()
        {
            return new Factor()
            {
                ID = ID,
                Name = Name,
                Description = Description,
                Iteration = Iteration,
                DurationCount = DurationCount,
                Sprite = Sprite,
                StartFactor = StartFactor,
                EndFactor = EndFactor,
                DoFactor = DoFactor,
            };
        }

    }

}
