using System;
using UnityEngine;
using System.Collections;

namespace Engine.Enemy {

	public class ZombieAI : AbstractAI {

		public override void OnStart() {
			base.OnStart();

			//Pather.Haunt(PlayerData.getInstance().getPlayer().transform);

		}

	}

}
