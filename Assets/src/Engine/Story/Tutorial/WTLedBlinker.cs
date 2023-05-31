using UnityEngine;

namespace Engine.Story.Tutorial
{

    public class WTLedBlinker : MonoBehaviour
    {

        [SerializeField] private Renderer rendererComponent;
        [SerializeField] private bool blink = false;
        
        public bool Blink
        {
            get { return blink; }
            set
            {
                this.blink = value;
                if(!value)
                    rendererComponent.material.SetEmissionColor(Color.black);
            }
        }
        
        void Update()
        {
            if(!blink)
                return;
            
            float emission = Mathf.PingPong(Time.time * 1.5f, 1.0f);
            rendererComponent.material.SetEmissionColor(Color.white * Mathf.LinearToGammaSpace(emission));
        }
    }

}
