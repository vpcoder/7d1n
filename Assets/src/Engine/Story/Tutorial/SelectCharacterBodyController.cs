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
            AudioController.Instance.PlaySound("ui/button");
            meshSwitcher.MeshIndex++;
            Game.Instance.Character.Account.SpriteID = meshSwitcher.MeshIndex;
        }

        public void OnRightClick()
        {
            AudioController.Instance.PlaySound("ui/button");
            meshSwitcher.MeshIndex--;
            Game.Instance.Character.Account.SpriteID = meshSwitcher.MeshIndex;
        }
        
    }
    
}