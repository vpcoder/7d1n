using Engine.Data.Factories;

namespace Engine.Data.Items.Used
{

    public interface IUseItemAction : IActionItem
    {

        bool DoAction();

    }

}
