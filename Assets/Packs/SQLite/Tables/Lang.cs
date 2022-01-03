using System;

namespace Engine.DB.I18n
{

    [Serializable]
    [Table("i18n_lang")] // Известный язык из базы (русский, английский и т.д.)
    public class Lang : Dto
    {
        [Column("code")]
        public string Code { get; set; }

        [Column("name")]
        public string Name { get; set; }
    }

}
