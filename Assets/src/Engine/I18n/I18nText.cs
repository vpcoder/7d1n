using UnityEngine;
using UnityEngine.UI;

namespace Engine.Components
{

    [RequireComponent(typeof(Text))]
    public class I18nText : MonoBehaviour
    {

        [SerializeField]
        public string TextKey;

        private void Awake()
        {
            var textField = GetComponent<Text>();
            textField.text = Localization.Instance.Get(TextKey);
        }

    }

}
