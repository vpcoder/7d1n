using System;
using UnityEngine;
using UnityEngine.UI;

namespace Engine.Story
{
    
    public class QuestHintBlink : MonoBehaviour
    {

        [SerializeField] private Text textField;

        [SerializeField] private Color normalColor;
        [SerializeField] private Color emptyColor;
        
        private float timestamp;
        private float value;
        private bool direction;

        private void Update()
        {
            value = Mathf.Min(1f, value + (Time.time - timestamp) * 0.5f);
            timestamp = Time.time;

            textField.color = direction
                ? Color.Lerp(normalColor, emptyColor, value)
                : Color.Lerp(emptyColor, normalColor, value);

            if (value >= 1f)
            {
                direction = !direction;
                value = 0f;
            }
        }
    }
    
}