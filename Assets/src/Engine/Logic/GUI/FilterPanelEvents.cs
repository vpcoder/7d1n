using UnityEngine;

namespace Engine.Logic
{

    public class FilterPanelEvents : MonoBehaviour
    {

        private CharacterBag Inventory { get { return ObjectFinder.Find<CharacterBag>(); } }

        [SerializeField] private FilterButtonAction buttonAll;
        [SerializeField] private FilterButtonAction buttonResources;
        [SerializeField] private FilterButtonAction buttonCloths;
        [SerializeField] private FilterButtonAction buttonWeapons;
        [SerializeField] private FilterButtonAction buttonMedics;
        [SerializeField] private FilterButtonAction buttonFoods;
        [SerializeField] private FilterButtonAction buttonBuilds;

        private FilterButtonAction selected;

        public void SetFilterAll()
        {
            Select(buttonAll);
            SetFilter(ItemFilterType.All);
        }

        public void SetFilterResources()
        {
            Select(buttonResources);
            SetFilter(ItemFilterType.Resources);
        }

        public void SetFilterCloths()
        {
            Select(buttonCloths);
            SetFilter(ItemFilterType.Cloths);
        }

        public void SetFilterWeapons()
        {
            Select(buttonWeapons);
            SetFilter(ItemFilterType.Weapons);
        }

        public void SetFilterMedics()
        {
            Select(buttonMedics);
            SetFilter(ItemFilterType.Medics);
        }
        
        public void SetFilterFood()
        {
            Select(buttonFoods);
            SetFilter(ItemFilterType.Foods);
        }

        public void SetFilterBuilds()
        {
            Select(buttonBuilds);
            SetFilter(ItemFilterType.Builds);
        }

        private void Select(FilterButtonAction selectedButton)
        {
            if (selected == selectedButton)
                return;

            if (selected == null)
                selected = buttonAll;

            selected.DoDeselect();

            selected = selectedButton;
            selected.DoSelect();
        }

        private void SetFilter(ItemFilterType filter)
        {
            var inventory = Inventory;

            if (inventory.Filter == filter)
                return;

            inventory.Filter = filter;
            inventory.Show();
        }

    }

}
