using Engine.Data;
using UnityEngine;

namespace Engine.Character
{

	public class CharacterStateHealth : CharacterStateUI
	{
		
		public override void OnStart()
		{
            Value = Mathf.FloorToInt(100f / Game.Instance.Character.State.MaxHealth * Game.Instance.Character.State.Health);
		}
		
	}

}
