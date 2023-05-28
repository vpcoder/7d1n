using UnityEngine;

namespace Engine
{
    
    public class AudioBehaviour : MonoBehaviour
    {

        [SerializeField] [Range(0f, 1f)] private float level = 1f;
        [SerializeField] private MixerType type;
        [SerializeField] private AudioSource source;

        [SerializeField] private AudioSourceFadeData fadeData;
        
        public AudioSource Source => source;
        public AudioSourceFadeData FadeData => fadeData;
        
        private void Awake()
        {
            var controller = AudioController.Instance;
            source.outputAudioMixerGroup = controller.GetMixer(type);
            source.volume = (controller.GetLowLevel(type) * level);

            source.spatialBlend = 1f;
            source.spread = 0;

            source.rolloffMode = AudioRolloffMode.Custom;
            source.maxDistance = 30;
            if (fadeData == null)
                fadeData = Resources.Load<AudioSourceFadeData>("Audio/AudioObjectFade");
            
            fadeData.UploadToSource(source);
        }
        
    }
    
}