using System;

namespace UnityEditor {

	/// <summary>
	/// Интерфейс определяющий часть необходимых методов MonoBehaviour
	/// </summary>
	public interface IMonoBehaviourReflection {
	
		void Awake();

		void FixedUpdate();
	
		void LateUpdate();
	
		void OnDestroy();
	
		void OnDisable();
	
		void OnDrawGizmos();
	
		void OnDrawGizmosSelected();
	
		void OnEnable();
	
		void OnGUI();
	
		void OnValidate();
	
		void Reset();
	
		void Start();
	
		void Update();
	
	}

}
