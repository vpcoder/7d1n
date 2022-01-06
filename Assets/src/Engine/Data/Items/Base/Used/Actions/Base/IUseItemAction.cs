using Engine.Data.Factories;

namespace Engine.Data.Items.Used
{

    /// <summary>
    /// 
    /// Действие при использовании используемого предмета IUsed
    /// ---
    /// Action when using an IUsed item
    /// 
    /// </summary>
    public interface IUseItemAction : IActionItem
    {

        /// <summary>
        /// Метод, выполняющий использование
        /// ---
        /// A method that performs the use of
        /// </summary>
        /// <returns></returns>
        bool DoAction();

    }

}
