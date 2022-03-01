using Engine.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Engine.Character
{

	public class CharacterStateProtection : CharacterStateUI
	{
		
		public override void OnUpdate()
		{
            Value = CurrentCharacterCalculationService.CurrentProtection();
        }
		
	}

}
