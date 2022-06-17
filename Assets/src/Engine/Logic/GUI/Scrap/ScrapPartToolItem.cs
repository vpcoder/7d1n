using Engine.Data;
using Engine.Data.Factories;
using UnityEngine;
using UnityEngine.UI;

namespace Engine.Logic.Scrap
{
    
    public class ScrapPartToolItem : MonoBehaviour
    {

        #region Hidden Fields

        [SerializeField] private Color foundedTool;
        [SerializeField] private Color absentTool;
        
        [SerializeField] private Image imgTool;
        [SerializeField] private Image imgItem;
        [SerializeField] private Text txtDescription;

        #endregion

        public void Init(ToolType tool)
        {
            var itemTool = Game.Instance.Character.Inventory.GetFirstByToolType(tool);
            if (itemTool == null)
            {
                imgTool.color = absentTool;
                txtDescription.color = absentTool;
            }
            else
            {
                imgTool.color = foundedTool;
                txtDescription.color = foundedTool;
                imgItem.sprite = itemTool.Sprite;
            }
            txtDescription.text = ToolFactory.Instance.GetName(tool);
            imgTool.sprite = ToolFactory.Instance.Get(tool);
        }

    }
    
}