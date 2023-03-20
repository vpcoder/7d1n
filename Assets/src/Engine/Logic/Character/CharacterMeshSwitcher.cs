using System.Collections.Generic;
using UnityEngine;

namespace Engine.Logic
{

    [ExecuteInEditMode]
    public class CharacterMeshSwitcher : MonoBehaviour
    {

        [SerializeField] private new SkinnedMeshRenderer renderer;
        [SerializeField] private List<Mesh> meshList;
        [SerializeField] private long selectedIndex;

        public SkinnedMeshRenderer Renderer
        {
            get { return renderer; }
            set { renderer = value; }
        }

        public long MeshIndex
        {
            get
            {
                return selectedIndex;
            }
            set
            {
                if (selectedIndex == value)
                    return;
                
                selectedIndex = value;
                
                if (selectedIndex < 0)
                    selectedIndex = meshList.Count - 1;
                
                if (selectedIndex >= meshList.Count)
                    selectedIndex = 0;
                
                UpdateMesh();
            }
        }

        private void UpdateMesh()
        {
            if(Lists.IsEmpty(meshList))
                return;
            Renderer.sharedMesh = meshList[(int)selectedIndex];
        }
        
#if UNITY_EDITOR

        private long prevIndex;
        
        private void OnValidate()
        {
            if (selectedIndex < 0)
                selectedIndex = meshList.Count - 1;
                
            if (selectedIndex >= meshList.Count)
                selectedIndex = 0;

            if (selectedIndex != prevIndex)
            {
                MeshIndex = selectedIndex;
                UpdateMesh();
                prevIndex = selectedIndex;
            }
        }

#endif
        
    }
    
}