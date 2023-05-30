using UnityEngine;

namespace Engine.Story.Tutorial
{

    public class WTLedBlinker : MonoBehaviour
    {

        [SerializeField] private Renderer renderer;
        [SerializeField] private bool blink = false;

        public bool Blink
        {
            get { return blink; }
            set
            {
                this.blink = value;
                if(!value)
                    renderer.material.SetColor("_EmissionColor", Color.black);
            }
        }
        
        void Update()
        {
            if(!blink)
                return;
            
            float emission = Mathf.PingPong(Time.time * 1.5f, 1.0f);
            renderer.material.SetColor ("_EmissionColor", Color.white * Mathf.LinearToGammaSpace(emission));
        }
    }

}
