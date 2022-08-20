using UnityEngine;
using UnityEngine.UI;

namespace Engine.Logic
{

    [RequireComponent(typeof(Image))]
    public class FilterButtonAction : MonoBehaviour
    {

        [SerializeField] private Color selectedColor = Color.white;
        [SerializeField] private Color deselectedColor = Color.gray;

        private float timestamp;
        private bool isCompleted = false;

        [SerializeField] public bool IsSelected = false;
        [SerializeField] public float Speed = 1f;

        private Image image;

        private void Start()
        {
            image = GetComponent<Image>();
        }

        public void DoSelect()
        {
            IsSelected = true;
            isCompleted = false;
            timestamp = Time.time;
        }

        public void DoDeselect()
        {
            IsSelected = false;
            isCompleted = false;
            timestamp = Time.time;
        }

        private void Update()
        {
            if (isCompleted)
                return;

            var progress = Mathf.Min((Time.time - timestamp * Speed), 1f);
            image.color = Color.Lerp(image.color, IsSelected ? selectedColor : deselectedColor, progress);
            if(progress >=1f)
            {
                isCompleted = true;
            }
        }

    }

}
