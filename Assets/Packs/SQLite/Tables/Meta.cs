using System;

namespace Engine.DB
{

    [Serializable]
    [Table("meta")]
    public class Meta : Dto
    {
        [Column("version")]
        public string Version { get; set; }
    }

}
