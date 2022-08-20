using UnityEngine;
using System.Collections.Generic;

namespace Engine.Logic {
	
	public class MoveController : MonoBehaviour {
		
		#region Hidden Fields
		
		[SerializeField]
		private List<Vector3> path;
		
		#endregion
		
		#region Properties
		
		public List<Vector3> Path {
			get {
				return this.path;
			}
			set {
				this.path = value;
			}
		}
		
		#endregion
		
		#region Unity Events
		
		void Start() {
			
		}
		
		void Update() {
			//Vector3 currentPoint = Path.First();
			//transform.Position = Vector3.Lerp(transform.)
		}
		
		#endregion

	}
	
}
