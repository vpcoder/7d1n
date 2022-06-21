using System.Collections.Generic;
using Engine.Data;
using Engine.Data.Factories;
using Engine.Logic.Locations;
using UnityEngine;
using UnityEngine.UI;

namespace Engine.Logic.Scrap
{
    
    public class ScrapPartItem : MonoBehaviour
    {

        private const float TOOL_Y_OFFSET = 4f;
        
        #region Hidden Fields

        [SerializeField] private ScrapPartToolItem toolPrefab;
        
        [SerializeField] private Image imgIcon;
        [SerializeField] private Text txtWeight;
        [SerializeField] private Text txtCount;
        
        [SerializeField] private Text txtTitle;
        [SerializeField] private Text txtDifficulty;

        [SerializeField] private RectTransform toolsContentRect;

        private ScrapPanelController parent;
        private Part part;
        private List<ScrapPartToolItem> toolsItems = new List<ScrapPartToolItem>();
        
        #endregion

        public Part Part => part;

        public void Init(Part part, ScrapPanelController parent)
        {
            this.part   = part;
            this.parent = parent;
            
            UpdateInfo();
        }

        public void Clean()
        {
            foreach (var item in toolsItems)
                Destroy(item);
            toolsItems.Clear();
        }

        private void AddItem(ToolType tool)
        {
            var index = toolsContentRect.childCount;
            var size = ((RectTransform)toolPrefab.transform).sizeDelta;
            
            var item = Instantiate(toolPrefab, toolsContentRect);
            var rect = (RectTransform)item.transform;

            var pos = rect.localPosition;
            pos.y = -index * size.y - TOOL_Y_OFFSET * index;
            rect.localPosition = pos;
            
            item.Init(tool);
            toolsItems.Add(item);
        }

        private void UpdateInfo()
        {
            var item = ItemFactory.Instance.Get(part.ResourceID);
            imgIcon.sprite = item.Sprite;
            txtWeight.text = WeightCalculationService.GetWeightFormat(item.Weight);
            txtCount.text = part.ResourceCount.ToString();
            txtTitle.text = Localization.Instance.Get(item.Name);

            if (Sets.IsEmpty(part.NeededTools))
                return;
            
            foreach (var tool in part.NeededTools)
                AddItem(tool);
            
            var itemSize = ((RectTransform)toolPrefab.transform).sizeDelta;
            var contentSize = toolsContentRect.sizeDelta;
            contentSize.y = toolsContentRect.childCount * itemSize.y + TOOL_Y_OFFSET * toolsContentRect.childCount;
            toolsContentRect.sizeDelta = contentSize;
        }

        public bool IsCanScrap()
        {
            if (Sets.IsEmpty(part.NeededTools)) // Можно разобрать без инструментов
                return true;
            return Game.Instance.Character.Inventory.HasAnyTools(part.NeededTools); // Ищем в инвентаре необходимые инструменты
        }
        
    }
    
}