using System;

namespace Engine.Data
{

    /// <summary>
    /// Блок инфомрации о крафтовых предметах
    /// </summary>
    [Serializable]
    public class CraftableInfo
    {
        public long   Level;
        public string Author;
    }

}
