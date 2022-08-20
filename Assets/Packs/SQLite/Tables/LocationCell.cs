using Engine.Data;
using System;

namespace Engine.DB
{
    
	[Serializable]
	[Table("location_cell")]
	public class LocationCell : Dto, IStoryObject
    {

        [Column("pos_x")]
        public int PosX { get; set; }

        [Column("pos_y")]
        public int PosY { get; set; }

        [Column("biom_pos_x")]
        public int BiomPosX { get; set; }

        [Column("biom_pos_y")]
        public int BiomPosY { get; set; }

        [Column("data")]
        public string Data { get; set; }

    }

}
