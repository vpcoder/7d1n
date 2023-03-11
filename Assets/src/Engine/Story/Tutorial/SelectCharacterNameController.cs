using Engine.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Engine.Story.Tutorial
{
    
    public class SelectCharacterNameController : MonoBehaviour
    {

        [SerializeField] private Text txtField;

        public void OnTextChange()
        {
            var name = txtField.text;
            if (string.IsNullOrWhiteSpace(name))
                name = Game.Instance.Character.Account.Name;
            Game.Instance.Character.Account.Name = name;
        }
        
    }
    
}