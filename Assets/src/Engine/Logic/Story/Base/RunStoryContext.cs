using System;
using Engine.Data;
using UnityEngine;

namespace Engine.Story
{
    
    [Serializable]
    public class RunStoryContext
    {
        [SerializeField] public Vector3 PlayerEyePos;
        [SerializeField] public GameObject TopPanel;
        [SerializeField] public GameObject PlayerCharacter;
        [SerializeField] public StoryDataRepoObject StoryInfo;
        [SerializeField] public int StartFloor;
        [SerializeField] public float StartFov;
        [SerializeField] public TransformPair StartTransformPair;
    }
    
}