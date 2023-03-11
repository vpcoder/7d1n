using Engine.Data;
using Engine.Logic;
using UnityEngine;

namespace Engine.Story.Tutorial
{
    
    public class SelectCharacterBodyController : MonoBehaviour
    {

        [SerializeField] private CharacterMeshSwitcher meshSwitcher;

        public void OnLeftClick()
        {
            meshSwitcher.MeshIndex++;
            Game.Instance.Character.Account.SpriteID = meshSwitcher.MeshIndex;
        }

        public void OnRightClick()
        {
            meshSwitcher.MeshIndex--;
            Game.Instance.Character.Account.SpriteID = meshSwitcher.MeshIndex;
        }
        
    }
    
}