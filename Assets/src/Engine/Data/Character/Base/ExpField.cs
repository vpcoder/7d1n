using System;

namespace Engine.Data
{

	/// <summary>
	///
	/// Поле опыта.
	/// Содержит общую информацию об абстрактном опыте.
	/// ---
	/// The field of experience.
	/// Contains general information about the abstract experience.
	/// 
	/// </summary>
	[Serializable]
	public class ExpField
    {
		
		#region Hidden Fields
		
		public long experience;
		public long maxExperience;
		public long level;
		public long points;
		
		#endregion
		
		#region Ctors
		
		public ExpField() { }
		
		public ExpField(long experience, long maxExperience, long level, long points) {
			this.experience    = experience;
			this.maxExperience = maxExperience;
			this.level         = level;
			this.points        = points;
		}
		
		#endregion
		
		#region Properties
		
		/// <summary>
		/// 	Текущее количество опыта
		/// 	---
		/// 	Current amount of experience
		/// </summary>
		public long Experience {
			get { return this.experience; }
			set { this.experience = value; }
		}
		
		/// <summary>
		/// 	Максимальное количество опыта на текущем уровне
		/// 	---
		/// 	Maximum amount of experience at the current level
		/// </summary>
		public long MaxExperience {
			get { return this.maxExperience; }
			set { this.maxExperience = value; }
		}
		
		/// <summary>
		/// 	Текущий уровень
		/// 	---
		///		Current level
		/// </summary>
		public long Level {
			get { return this.level; }
			set { this.level = value; }
		}
		
		/// <summary>
		/// 	Количество свободных очков опыта
		/// 	---
		/// 	Number of free experience points
		/// </summary>
		public long Points {
			get { return this.points; }
			set { this.points = value; }
		}
		
		#endregion
		
	}
	
}
