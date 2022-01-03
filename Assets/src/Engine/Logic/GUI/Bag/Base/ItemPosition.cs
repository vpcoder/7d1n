using System;
using UnityEngine;

namespace Engine.Logic {

	[Serializable]
	public class ItemPosition {

		#region Hidden Fields
		
		[SerializeField] private int x;
		[SerializeField] private int y;
		
		#endregion
		
		#region Ctors

		public ItemPosition() : this(0,0) { }
		
		public ItemPosition(int x, int y) {
			this.x = x;
			this.y = y;
		}
		
		#endregion
		
		#region Properties
		
		public int X {
			get { return this.x; }
			set { this.x = value; }
		}
		
		public int Y {
			get { return this.y; }
			set { this.y = value; }
		}
		
		#endregion
		
	}
	
}
