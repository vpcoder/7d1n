using Engine.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Engine.Character
{

	public class CharacterStateHunger : CharacterStateUI
	{
		
		public override void OnStart()
		{
            Value = Game.Instance.Character.State.Hunger / 1000;
        }
		
	}

}
