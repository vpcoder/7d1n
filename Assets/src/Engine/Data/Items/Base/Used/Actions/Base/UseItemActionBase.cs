namespace Engine.Data.Items.Used
{

    public abstract class UseItemActionBase : IUseItemAction
    {

        public abstract string ID { get; }

        public abstract bool DoAction();

    }

}
