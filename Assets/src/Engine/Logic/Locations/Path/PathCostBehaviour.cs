using Engine.EGUI;
using UnityEngine;
using UnityEngine.UI;

namespace Engine.Logic.Locations
{
    
    public class PathCostBehaviour : Panel
    {

        [SerializeField] private Text txtCost;

        public void Setup(string text, Vector3 position)
        {
            Show();
            txtCost.text = text;
            transform.position = position;
            
            var rotation = transform.rotation.eulerAngles;
            rotation.y = Camera.main.transform.rotation.eulerAngles.y;
            transform.rotation = Quaternion.Euler(rotation);
        }

    }
    
}