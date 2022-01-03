using System;
using UnityEngine;

namespace Engine.Data {

	public interface ISkill
    {
		
		string ID          { get; }
		string Name        { get; }
		string Description { get; }
        Sprite Sprite      { get; }
		
	}
	
}
