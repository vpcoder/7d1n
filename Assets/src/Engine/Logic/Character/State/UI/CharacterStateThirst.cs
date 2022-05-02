using Engine.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Engine.Character
{

	public class CharacterStateThirst : CharacterStateUI
	{
		
		public override void OnUpdate()
		{
            Value = Game.Instance.Character.State.Thirst / 1000;
        }
		
	}

}
