using Engine.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Engine.Character
{

	public class CharacterStateWeight : CharacterStateUI
	{
		
		public override void OnUpdate()
		{
            Value = Mathf.FloorToInt(100f / Game.Instance.Character.State.MaxWeight * Game.Instance.Character.Inventory.Weight);
        }
		
	}

}
