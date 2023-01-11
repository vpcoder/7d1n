
using Engine.Data;

namespace Engine.Logic.Locations
{
    
    /// <summary>
    ///
    /// Предиктор, который будет формировать список действий для NPC в зависимости от его состояния
    /// ---
    /// Predictor that will generate a list of actions for an NPC depending on its state
    /// 
    /// </summary>
    public interface IPredictor
    {

        /// <summary>
        ///     Название предиктора
        ///		Уникальное в рамках всех предикторов!
        ///     ---
        ///     Predictor name
        ///		Unique within all predictors!
        /// </summary>
        public string Name { get; }

        /// <summary>
        ///     Метод должен сформировать список действий для NPC
        ///     ---
        ///     The method should generate a list of actions for NPCs
        /// </summary>
        /// <param name="context">
        ///     Контекст предиктора, в котором находятся все необходимые для анализа действий NPC данные
        ///     ---
        ///     Predictor context, which contains all the data needed to analyze NPC actions
        /// </param>
        public void CreateStrategyForNpc(PredictorContext context);

    }
    
}