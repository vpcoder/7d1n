using System.Linq;

namespace Engine.Data
{

    public class NPC : CharacterBase
    {

        public override IIdentity Copy()
        {
            return new NPC()
            {
                ID = ID,
                AP = AP,
                Exp = Exp,
                OrderGroup = OrderGroup,
                Protection = Protection,
                Health = Health,
                ItemsForGeneration = ItemsForGeneration?.ToList(),
                ItemsMaxCountForGeneration = ItemsMaxCountForGeneration,
                WeaponsForGeneration = WeaponsForGeneration?.ToList(),
                WeaponsMaxCountForGeneration = WeaponsMaxCountForGeneration,
                Items = Items?.ToList(),
                Weapons = Weapons?.ToList(),
            };
        }

    }

}
