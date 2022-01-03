using UnityEngine;
using UnityEngine.UI;

namespace Engine.Components
{

    public class I18nText : MonoBehaviour
    {

        [SerializeField]
        public string TextKey;

        private void Start()
        {
            var textField = GetComponent<Text>();

            if (textField == null)
            {
                Debug.LogError("text field not founded!");
                return;
            }

            textField.text = Localization.Instance.Get(TextKey);
        }

    }

}
