using Engine.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Engine.Character
{

	public class CharacterStateProtection : CharacterStateUI
	{
		
		public override void OnStart()
		{
            Value = Game.Instance.Character.State.Protection + Game.Instance.Character.Equipment.Protection;
        }
		
	}

}
