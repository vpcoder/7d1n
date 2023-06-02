using System.Collections.Generic;
using Engine.Data;
using Engine.Logic;
using UnityEngine;

namespace Engine.Story.Chagegrad
{
    
    public class SelectCharacterBodyController : MonoBehaviour
    {

        [SerializeField] private List<GenderType> bodyGender;
        [SerializeField] private CharacterMeshSwitcher meshSwitcher;

        public void OnLeftClick()
        {
            AudioController.Instance.PlaySound("ui/button");
            meshSwitcher.MeshIndex++;
            Game.Instance.Character.Account.SpriteID = meshSwitcher.MeshIndex;
            Game.Instance.Character.Account.Gender = bodyGender[(int)meshSwitcher.MeshIndex];
        }

        public void OnRightClick()
        {
            AudioController.Instance.PlaySound("ui/button");
            meshSwitcher.MeshIndex--;
            Game.Instance.Character.Account.SpriteID = meshSwitcher.MeshIndex;
            Game.Instance.Character.Account.Gender = bodyGender[(int)meshSwitcher.MeshIndex];
        }
        
    }
    
}