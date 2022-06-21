using System.Collections.Generic;
using Engine.Data;
using Engine.EGUI;
using Engine.Logic.Scrap;
using UnityEngine;
using UnityEngine.UI;

namespace Engine.Logic.Locations
{

    /// <summary>
    /// 
    /// Панель разбора объектов в локации
    /// ---
    /// Panel of additional actions on objects in the location
    /// 
    /// </summary>
    public class ScrapPanelController : Panel
    {

        #region Hidden Fields

        /// <summary>
        ///     Расстояние между двумя блоками итемов
        ///     ---
        ///     Distance between two blocks of items
        /// </summary>
        private const float ITEM_Y_OFFSET = 8f;
        
        /// <summary>
        ///     Префаб итема. Он должен содержать разметку для одной части, из которой состоит объект.
        ///     ---
        ///     Prefab of an item. It must contain the markup for the one part that makes up the object.
        /// </summary>
        [SerializeField] private ScrapPartItem itemPrefab;
        
        /// <summary>
        ///     Ссылка на контент, на котором располагаются итемы частей объекта
        ///     ---
        ///     Link to the content on which the items of the object's parts are located
        /// </summary>
        [SerializeField] private RectTransform content;
        
        /// <summary>
        ///     Заголовок панели разбора
        ///     ---
        ///     Disassembly panel header
        /// </summary>
        [SerializeField] private Text txtTitle;
        
        /// <summary>
        ///     Список ранее созданных UI объектов для визуализации разбираемых частей
        ///     ---
        ///     List of previously created UI objects to visualize the parts to be disassembled
        /// </summary>
        private List<ScrapPartItem> items = new List<ScrapPartItem>();
        
        /// <summary>
        ///     Объект который выбрали для того чтобы разобрать
        ///     ---
        ///     The object that was chosen to disassemble
        /// </summary>
        private LocationObjectItemBehaviour selectedItemBehaviour;
        
        /// <summary>
        ///     Панель с которой был вызван текущий интерфейс разбора
        ///     ---
        ///     The panel from which the current parsing interface was called
        /// </summary>
        private ActionPanelController actionPanelController;

        #endregion
        
        #region Properties

        /// <summary>
        ///     Список ранее созданных UI объектов для визуализации разбираемых частей
        ///     ---
        ///     List of previously created UI objects to visualize the parts to be disassembled
        /// </summary>
        public List<ScrapPartItem> Items => items;
        
        #endregion
        
        public void Show(LocationObjectItemBehaviour selectedItemBehaviour, ActionPanelController actionPanelController)
        {
            Game.Instance.Runtime.Mode = Mode.GUI;
            this.selectedItemBehaviour = selectedItemBehaviour;
            this.actionPanelController = actionPanelController;

            base.Show();

            txtTitle.text = Localization.Instance.Get(selectedItemBehaviour.Item.Name);
            
            var parts = selectedItemBehaviour.Item.Parts;
            foreach (var part in parts)
                AddItem(part);

            var itemSize = ((RectTransform)itemPrefab.transform).sizeDelta;
            var contentSize = content.sizeDelta;
            contentSize.y = content.childCount * itemSize.y + ITEM_Y_OFFSET * content.childCount;
            content.sizeDelta = contentSize;
        }

        private void AddItem(Part part)
        {
            var index = content.childCount;
            var size = ((RectTransform)itemPrefab.transform).sizeDelta;
            
            var item = Instantiate(itemPrefab, content);
            var rect = (RectTransform)item.transform;

            var pos = rect.localPosition;
            pos.y = -index * size.y - ITEM_Y_OFFSET * index;
            rect.localPosition = pos;
            
            item.Init(part, this);
            items.Add(item);
        }

        public override void Hide()
        {
            foreach (var item in items)
            {
                item.Clean();
                Destroy(item);
            }

            items.Clear();
            actionPanelController = null;
            base.Hide();
        }

        #region Methods
        
        /// <summary>
        ///     Определяет в целом - возможен ли разбор объекта?
        ///     ---
        ///     Determines in general - is it possible to disassemble the object?
        /// </summary>
        /// <returns>
        ///     true - если разбор объекта возможен (хотябы одна часть объекта будет успешно разобрана)
        ///     false - не удасться разобрать ни одной части объекта, разбор невозможен
        ///     ---
        ///     true - if the disassembly is possible (at least one part of the object will be successfully disassembled)
        ///     false - no part of the object can be disassembled, disassembly is impossible
        /// </returns>
        private bool CanScrap()
        {
            foreach (var item in items)
            {
                if (!item.IsCanScrap())
                    return false;
            }
            return true;
        }

        /// <summary>
        ///     Логика разбора объекта на составляющие компоненты, с учётом навыков пресонажа
        ///     ---
        ///     The logic of decomposing an object into its components, taking into account the skills of the character
        /// </summary>
        private void DoScrap()
        {
            if (!CanScrap())
                return;
            foreach (var item in items)
            {
                var part = item.Part;
                var character = Game.Instance.Character;
                var count = ScrapCalculationService.CalcResourceCount(part); // Вычисляем полученные части
                if (count == 0)
                    continue;
                var exps = character.Exps;
                ExpCalculationService.AddExp(part.Difficulty, exps.ScrapExperience, exps.MainExperience); // Увеличиваем опыт разборки, в зависимости от количества частей и сложности их извлечения
                character.Inventory.Add(part.ResourceID, count);
            }
            Destroy(selectedItemBehaviour.gameObject);
        }
        
        #endregion
        
        #region Events
        
        /// <summary>
        ///     Клик на кнопке разбора
        ///     ---
        ///     Click on the disassemble button
        /// </summary>
        public void OnScrapClick()
        {
            if (!CanScrap())
                return;

            DoScrap();
            
            // Уничтожаем разбираемый объект
            Destroy(selectedItemBehaviour.gameObject);
            
            // Прячем интерфейс взаимодействия с объектом
            actionPanelController.Hide();
            
            // Прячем интерфейс разбора
            Hide();
        }

        /// <summary>
        ///     Клик на кнопке отмены
        ///     ---
        ///     Clicking the cancel button
        /// </summary>
        public void OnCloseClick()
        {
            Hide();
        }
        
        #endregion
        
    }

}
