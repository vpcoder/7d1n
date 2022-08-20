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
        ///     Метод, выполняющий использование
        ///     ---
        ///     A method that performs the use of
        /// </summary>
        /// <returns>
        ///     true - если предмет был одноразовым, и он уничтожился
        ///     false - если предмет многоразовый, и остаётся в инветаре
        ///     ---
        ///     true - if the item was single-use and it is destroyed
        ///     false - if the item is reusable and stays in the inventory
        /// </returns>
        bool DoAction();

    }

}
