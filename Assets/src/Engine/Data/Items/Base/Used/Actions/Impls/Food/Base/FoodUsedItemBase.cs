namespace Engine.Data.Items.Used.Actions.Food
{

    public abstract class FoodUsedItemBase : UseItemActionBase
    {

        public abstract int Hunger { get; }

        public abstract int Health { get; }

        public override bool DoAction()
        {
            if (Game.Instance.Character.State.Hunger > 100000 - Hunger)
                return false;

            Game.Instance.Character.State.Hunger += Hunger;

            if (Game.Instance.Character.State.Health >= Game.Instance.Character.State.MaxHealth - Health)
            {
                Game.Instance.Character.State.Health += Health;
            }
            else
            {
                Game.Instance.Character.State.Health = Game.Instance.Character.State.MaxHealth;
            }

            return true;
        }

    }

}
