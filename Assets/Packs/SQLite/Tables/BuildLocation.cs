using Engine.Data;
using System;

namespace Engine.DB
{

	[Serializable]
	[Table("build_location")]
	public class BuildLocation : Dto, IRepositoryObject
    {

        [Column("timestamp")]
		public string Timestamp { get; set; }

        [Column("data")]
        public string Data { get; set; }

    }

}
