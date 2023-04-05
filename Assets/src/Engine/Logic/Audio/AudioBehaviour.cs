using UnityEngine;

namespace Engine
{
    
    public class AudioBehaviour : MonoBehaviour
    {

        [SerializeField] [Range(0f, 1f)] private float level = 1f;
        [SerializeField] private MixerType type;
        [SerializeField] private AudioSource source;
        
        private void Awake()
        {
            var controller = AudioController.Instance;
            source.outputAudioMixerGroup = controller.GetMixer(type);
            source.volume = (controller.GetLowLevel(type) * level);
        }
        
    }
    
}