using System.Collections.Generic;
using UnityEngine;

namespace Engine
{
    
    [CreateAssetMenu(fileName = "new AudioSourceFadeData",menuName = "Data/AudioSourceFadeData")]
    public class AudioSourceFadeData : ScriptableObject
    {
        
        [SerializeField] private List<AnimationCurve> animationCurves;

        public List<AnimationCurve> AnimationCurves => animationCurves;

        public void DownloadFromSource(AudioSource source)
        {
            if (animationCurves == null)
                animationCurves = new List<AnimationCurve>();
            
            if(animationCurves.Count != 0)
                animationCurves.Clear();
            
            animationCurves.Add(source.GetCustomCurve(AudioSourceCurveType.CustomRolloff));
            animationCurves.Add(source.GetCustomCurve(AudioSourceCurveType.Spread));
            animationCurves.Add(source.GetCustomCurve(AudioSourceCurveType.SpatialBlend));
            animationCurves.Add(source.GetCustomCurve(AudioSourceCurveType.ReverbZoneMix));
        }

        public void UploadToSource(AudioSource source)
        {
            if (animationCurves == null || animationCurves.Count != 4)
            {
                Debug.LogError("data is empty!");
                return;
            }
            source.SetCustomCurve(AudioSourceCurveType.CustomRolloff, animationCurves[0]);
            source.SetCustomCurve(AudioSourceCurveType.Spread, animationCurves[1]);
            source.SetCustomCurve(AudioSourceCurveType.SpatialBlend, animationCurves[2]);
            source.SetCustomCurve(AudioSourceCurveType.ReverbZoneMix, animationCurves[3]);
        }

    }
    
}