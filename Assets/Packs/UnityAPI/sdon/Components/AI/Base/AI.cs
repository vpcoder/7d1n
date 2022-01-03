
using System;
using UnityEngine;

namespace Engine.Enemy {

	public interface AI {

		IAnimator Animator { get; }
		IPather   Pather   { get; }

	}

}
