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
                GenerationInfo = new CharacterLootGeneration()
                {
                    IsRandomItemsGeneration = GenerationInfo.IsRandomItemsGeneration,
                    IsRandomWeaponsGeneration = GenerationInfo.IsRandomWeaponsGeneration,
                    ItemsForGeneration = GenerationInfo.ItemsForGeneration?.ToList(),
                    ItemsMaxCountForGeneration = GenerationInfo.ItemsMaxCountForGeneration,
                    WeaponsForGeneration = GenerationInfo.WeaponsForGeneration?.ToList(),
                    WeaponsMaxCountForGeneration = GenerationInfo.WeaponsMaxCountForGeneration,
                },
                Items = Items?.ToList(),
                Weapons = Weapons?.ToList(),
            };
        }

    }

}
