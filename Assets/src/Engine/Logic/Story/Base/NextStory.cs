using System;
using UnityEngine;

namespace Engine.Story
{
    
    [Serializable]
    public class NextStory
    {
        [SerializeField] public StoryBase Story;
        [SerializeField] public bool SwitchActive;
        [SerializeField] public bool NeedRun;
    }
    
}