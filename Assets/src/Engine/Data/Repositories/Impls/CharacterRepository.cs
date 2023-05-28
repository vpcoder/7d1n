
using System;

namespace Engine.Data.Repositories
{

    public class CharacterRepository
    {

        #region Singleton

        private static readonly Lazy<CharacterRepository> instance = new Lazy<CharacterRepository>(() => new CharacterRepository());
        public static CharacterRepository Instance => instance.Value;
        private CharacterRepository() { }

        #endregion

        #region Stories

        public CharAccountRepository    AccountRepository    = new CharAccountRepository();
        public CharEquipmentRepository  EquipmentRepository  = new CharEquipmentRepository();
        public CharExpsRepository       ExpsRepository       = new CharExpsRepository();
        public CharInventoryRepository  InventoryRepository  = new CharInventoryRepository();
        public CharParametersRepository ParametersRepository = new CharParametersRepository();
        public CharSkillsRepository     SkillsRepository     = new CharSkillsRepository();
        public CharStateRepository      StateRepository      = new CharStateRepository();
        public CharQuestRepository      QuestRepository      = new CharQuestRepository();

        #endregion

        public void Delete(long id)
        {
            AccountRepository.Delete(id);
            InventoryRepository.Delete(id);
            EquipmentRepository.Delete(id);
            ExpsRepository.Delete(id);
            ParametersRepository.Delete(id);
            SkillsRepository.Delete(id);
            StateRepository.Delete(id);
            QuestRepository.Delete(id);
        }

        public void SaveAll(Character character)
        {
            AccountRepository.Save(character.Account.CreateData());
            InventoryRepository.Save(character.Inventory.CreateData());
            EquipmentRepository.Save(character.Equipment.CreateData());
            ExpsRepository.Save(character.Exps.CreateData());
            ParametersRepository.Save(character.Parameters.CreateData());
            SkillsRepository.Save(character.Skills.CreateData());
            StateRepository.Save(character.State.CreateData());
            QuestRepository.Save(character.Quest.CreateData());
        }

        public void LoadAll(Character character)
        {
            var id = Game.Instance.Runtime.PlayerID;
            character.Account.LoadFromData(AccountRepository.Get(id));
            character.Inventory.LoadFromData(InventoryRepository.Get(id));
            character.Exps.LoadFromData(ExpsRepository.Get(id));
            character.Parameters.LoadFromData(ParametersRepository.Get(id));
            character.Skills.LoadFromData(SkillsRepository.Get(id));
            character.State.LoadFromData(StateRepository.Get(id));
            character.Equipment.LoadFromData(EquipmentRepository.Get(id));
        }

    }

    public class CharAccountRepository : RepositoryBase<AccountRepositoryObject, CharAccountProxy>
    { }

    public class CharEquipmentRepository : RepositoryBase<EquipmentRepositoryObject, CharEquipmentProxy>
    { }

    public class CharExpsRepository : RepositoryBase<ExpDataRepositoryObject, CharExpsProxy>
    { }

    public class CharInventoryRepository : RepositoryBase<InventoryRepositoryObject, CharInventoryProxy>
    { }

    public class CharParametersRepository : RepositoryBase<ParametersRepositoryData, CharParametersProxy>
    { }

    public class CharSkillsRepository : RepositoryBase<SkillsRepositoryObject, CharSkillsProxy>
    { }

    public class CharStateRepository : RepositoryBase<StateRepositoryObject, CharStateProxy>
    { }

    public class CharQuestRepository : RepositoryBase<QuestRepositoryObject, CharQuestProxy>
    { }

}
