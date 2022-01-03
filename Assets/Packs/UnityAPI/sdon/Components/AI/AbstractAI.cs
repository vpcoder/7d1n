using System;
using UnityEngine;
using System.Collections.Generic;

namespace Engine.Enemy {

	[RequireComponent(typeof(AIAnimator))]
	[RequireComponent(typeof(AIPather))]
	public class AbstractAI : MonoBehaviour, AI, IMonoBehaviourOverrideStartEvent, IMonoBehaviourOverrideUpdateEvent {


		#region Hidden Fields

		private AIAnimator animator;
		private AIPather   pather;

		#endregion

		#region Unity Events

		public virtual void OnStart()  { }
		public virtual void OnUpdate() { }

		private void Start() {
			this.animator = GetComponent<AIAnimator>();
			this.pather   = GetComponent<AIPather>();
			OnStart();
		}

		private void Update() {
			OnUpdate();
		}

		#endregion

		#region Properties

		public IAnimator Animator {
			get {
				return this.animator;
			}
		}

		public IPather Pather {
			get {
				return this.pather;
			}
		}

		#endregion


	}

}
