using System;
using System.Collections.Generic;
using com.baensi.sdon.logic;

namespace com.baensi.sdon.protocol.entities
{

	// Autogen Transport Entity
	/// <summary>
    /// Транспортная сущность 'build_enemy'
    /// </summary>
    public class BuildEnemy : TransportEntity
    {
		
		#region Fields

		public int Id { get; set; }
		public int BuildId { get; set; }
		public int Enemy { get; set; }
		public int Health { get; set; }

		#endregion

		#region Ctor

		public BuildEnemy()
		{ }

		#endregion

    }
}
