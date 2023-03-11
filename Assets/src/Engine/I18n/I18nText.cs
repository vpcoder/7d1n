using System;
using UnityEngine;
using UnityEngine.UI;

namespace Engine.Components
{

    [RequireComponent(typeof(Text))]
    public class I18nText : MonoBehaviour
    {

        [SerializeField]
        public string TextKey;

        private void OnEnable()
        {
            UpdateInfo();
        }

        private void Awake()
        {
            UpdateInfo();
        }

        public void UpdateInfo()
        {
            var textField = GetComponent<Text>();
            textField.text = Localization.Instance.Get(TextKey);
        }

    }

}
