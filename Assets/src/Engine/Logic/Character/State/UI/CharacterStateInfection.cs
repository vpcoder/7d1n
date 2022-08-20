using Engine.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Engine.Character
{

	public class CharacterStateInfection : CharacterStateUI
	{
		
		public override void OnUpdate()
		{
            Value = Game.Instance.Character.State.Infection / 1000;
        }
		
	}

}
