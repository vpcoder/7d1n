using Engine.DB;
using System;

namespace Engine
{

    [Serializable]
    [Table("settings")]
    public class Settings : Dto
    {

        [Column("musics_volume")]
        public int MusicsVolume { get; set; }

        [Column("sounds_volume")]
        public int SoundsVolume { get; set; }

        [Column("musics_enabled")]
        public bool MusicsEnabled { get; set; }

        [Column("sounds_enabled")]
        public bool SoundsEnabled { get; set; }

        [Column("language")]
        public int Language { get; set; }

        [Column("player_id")]
        public long PlayerID { get; set; }

    }

}
