using Engine.Data.Blueprints.Base;
using Engine.Data.Factories;
using UnityEngine;
using UnityEngine.UI;

namespace Engine.Logic
{
    
    public class CraftBlueprintItem : MonoBehaviour
    {

        #region Hidden Fields

        [SerializeField] private Image imgIcon;
        [SerializeField] private Text txtTitle;
        
        private IBlueprint blueprint;
        private Quaternion defaultRotation;
        private bool selected;
        private float timestamp;
        private bool needUpdate = false;

        #endregion
        
        public void Init(IBlueprint blueprint)
        {
            this.blueprint = blueprint;
            this.defaultRotation = Quaternion.Euler(0, 0, Random.Range(-4f, 4f));
            this.transform.localRotation = defaultRotation;
            UpdateInfo();
        }

        public void UpdateInfo()
        {
            txtTitle.text = Localization.Instance.Get(blueprint.Name);
            imgIcon.sprite = ItemFactory.Instance.Get(blueprint.ItemID).Sprite;
        }
        
        private void Update()
        {
            if (!needUpdate)
                return;
            float delta = Mathf.Min(Time.time - timestamp, 1f);
            transform.rotation = Quaternion.Lerp(transform.rotation, selected ? Quaternion.identity : defaultRotation, delta);
            if (delta >= 1f)
            {
                needUpdate = false;
            }
        }

        public void OnSelect()
        {
            if (selected)
                return;

            timestamp = Time.time;
            needUpdate = true;
            selected = true;

            ObjectFinder.Find<CraftController>().SelectBlueprint(this, blueprint);
        }

        public void OnDeselect()
        {
            if (!selected)
                return;

            timestamp = Time.time;
            needUpdate = true;
            selected = false;
        }

    }
    
}