using Engine.Data.Repositories;
using System;
using System.Linq;

namespace Engine.Data
{

    [Serializable]
    public class EquipmentRepositoryObject : IRepositoryObject
    {
        public long ID { get { return IDValue; } set { } }
        public long IDValue;

        public int HeadIndex;
        public int BodyIndex;
        public int HandIndex;
        public int LegsIndex;
        public int BootIndex;
        public int Use1Index;
        public int Use2Index;
    }

    public class Equipment : ICharacterStoredObjectSerializable<EquipmentRepositoryObject>
    {

        public ICloth Head { get; set; }
        public ICloth Body { get; set; }
        public ICloth Hand { get; set; }
        public ICloth Legs { get; set; }
        public ICloth Boot { get; set; }
        public IWeapon Use1 { get; set; }
        public IWeapon Use2 { get; set; }

        public bool TryRemoveItem(IItem item)
        {
            var result = false;

            if (Head == item)
            {
                result = true;
                Head = null;
            }

            if (Body == item)
            {
                result = true;
                Body = null;
            }

            if (Hand == item)
            {
                result = true;
                Hand = null;
            }

            if (Legs == item)
            {
                result = true;
                Legs = null;
            }

            if (Boot == item)
            {
                result = true;
                Boot = null;
            }

            if (Use1 == item)
            {
                result = true;
                Use1 = null;
            }

            if (Use2 == item)
            {
                result = true;
                Use2 = null;
            }

            if(result)
            {
                CharacterRepository.Instance.EquipmentRepository.Save(CreateData());
            }

            return result;
        }

        public int Protection
        {
            get
            {
                return (new[] { Head, Body, Hand, Legs, Boot }).Select(value => value?.Protection ?? 0).Sum();
            }
        }

        #region Serialization

        public EquipmentRepositoryObject CreateData()
        {
            var inventory = Game.Instance.Character.Inventory;
            var data = new EquipmentRepositoryObject
            {
                IDValue = Game.Instance.Runtime.PlayerID,
                HeadIndex = inventory.TryFindIndex(Head),
                BodyIndex = inventory.TryFindIndex(Body),
                HandIndex = inventory.TryFindIndex(Hand),
                LegsIndex = inventory.TryFindIndex(Legs),
                BootIndex = inventory.TryFindIndex(Boot),
                Use1Index = inventory.TryFindIndex(Use1),
                Use2Index = inventory.TryFindIndex(Use2)
            };
            return data;
        }

        public void LoadFromData(EquipmentRepositoryObject data)
        {
            var inventory = Game.Instance.Character.Inventory;
            Head = (ICloth) inventory.GetByIndex(data.HeadIndex);
            Body = (ICloth) inventory.GetByIndex(data.BodyIndex);
            Hand = (ICloth) inventory.GetByIndex(data.HandIndex);
            Legs = (ICloth) inventory.GetByIndex(data.LegsIndex);
            Boot = (ICloth) inventory.GetByIndex(data.BootIndex);
            Use1 = (IWeapon)inventory.GetByIndex(data.Use1Index);
            Use2 = (IWeapon)inventory.GetByIndex(data.Use2Index);
        }

        #endregion

    }

}
