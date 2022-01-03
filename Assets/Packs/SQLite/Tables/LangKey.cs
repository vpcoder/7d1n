using System;

namespace Engine.DB.I18n
{

    [Serializable]
    public class LangKey
    {
        [Column("key")]
        [Unique]
        [PrimaryKey]
        public string Key { get; set; }

        [Column("value")]
        public string Value { get; set; }
    }

}
