using System;
using System.Collections.Generic;

namespace Engine.Logic.Locations
{

    [Serializable]
    public class ObjectDataSet
    {
        public long   ID;
        public string Position;
        public string Rotation;
    }

    [Serializable]
    public class EnemyWeaponDataSet
    {
        public long ID;
        public long AmmoCount;
    }

    [Serializable]
    public class EnemyItemDataSet
    {
        public long ID;
        public long Count;
    }

    [Serializable]
    public class EnemyDataSet
    {
        public long ID;
        public int Health;
        public int Protection;
        public List<EnemyWeaponDataSet> Weapons;
        public List<EnemyItemDataSet> Items;
        public string Position;
        public string Rotation;
    }

    [Serializable]
    public class LocationDataSet
    {
        public List<EnemyDataSet> Enemies;
        public List<ObjectDataSet> Objects;
    }

}
