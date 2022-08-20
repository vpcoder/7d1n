using System;

namespace Engine.DB
{

    [Serializable]
    [Table("player")]
    public class Player : Dto
    {
        [Column("name")]
        public string Name { get; set; }

        [Column("body")]
        public long BodyID { get; set; }
    }

}
