
using System;

namespace Engine.Data.Stories
{

    public class CharacterStory
    {

        #region Singleton

        private static readonly Lazy<CharacterStory> instance = new Lazy<CharacterStory>(() => new CharacterStory());
        public static CharacterStory Instance => instance.Value;
        private CharacterStory() { }

        #endregion

        #region Stories

        public CharAccountStory    AccountStory    = new CharAccountStory();
        public CharEquipmentStory  EquipmentStory  = new CharEquipmentStory();
        public CharExpsStory       ExpsStory       = new CharExpsStory();
        public CharInventoryStory  InventoryStory  = new CharInventoryStory();
        public CharParametersStory ParametersStory = new CharParametersStory();
        public CharSkillsStory     SkillsStory     = new CharSkillsStory();
        public CharStateStory      StateStory      = new CharStateStory();

        #endregion

        public void Delete(long id)
        {
            AccountStory.Delete(id);
            InventoryStory.Delete(id);
            EquipmentStory.Delete(id);
            ExpsStory.Delete(id);
            ParametersStory.Delete(id);
            SkillsStory.Delete(id);
            StateStory.Delete(id);
        }

        public void SaveAll(Character character)
        {
            AccountStory.Save(character.Account.CreateData());
            InventoryStory.Save(character.Inventory.CreateData());
            EquipmentStory.Save(character.Equipment.CreateData());
            ExpsStory.Save(character.Exps.CreateData());
            ParametersStory.Save(character.Parameters.CreateData());
            SkillsStory.Save(character.Skills.CreateData());
            StateStory.Save(character.State.CreateData());
        }

        public void LoadAll(Character character)
        {
            var id = Game.Instance.Runtime.PlayerID;
            character.Account.LoadFromData(AccountStory.Get(id));
            character.Inventory.LoadFromData(InventoryStory.Get(id));
            character.Exps.LoadFromData(ExpsStory.Get(id));
            character.Parameters.LoadFromData(ParametersStory.Get(id));
            character.Skills.LoadFromData(SkillsStory.Get(id));
            character.State.LoadFromData(StateStory.Get(id));
            character.Equipment.LoadFromData(EquipmentStory.Get(id));
        }

    }

    public class CharAccountStory : StoryBase<AccountStoryObject, CharAccountProxy>
    { }

    public class CharEquipmentStory : StoryBase<EquipmentStoryObject, CharEquipmentProxy>
    { }

    public class CharExpsStory : StoryBase<ExpDataStoryObject, CharExpsProxy>
    { }

    public class CharInventoryStory : StoryBase<InventoryStoryObject, CharInventoryProxy>
    { }

    public class CharParametersStory : StoryBase<ParametersStoryData, CharParametersProxy>
    { }

    public class CharSkillsStory : StoryBase<SkillsStoryObject, CharSkillsProxy>
    { }

    public class CharStateStory : StoryBase<StateStoryObject, CharStateProxy>
    { }

}
