using System;
using System.Collections.Generic;
using Engine.Logic.Locations;
using UnityEngine;

namespace UnityEditor.Menu
{

    public class FragmentInfo
    {
        public string Name { get; set; }
        public Vector3 Center { get; set; }
        public Vector3 Size { get; set; }
        public FragmentInfo(string name, Vector3 center, Vector3 size)
        {
            this.Name = name;
            this.Center = center;
            this.Size = size;
        }
    }
    
    public static class RootMeshBonesHumanoidActionMenuItem
    {

        private static readonly IDictionary<CharacterDamagedFragment, FragmentInfo> humanoidBonesName = new Dictionary<CharacterDamagedFragment, FragmentInfo>
        {
            { CharacterDamagedFragment.Head, new FragmentInfo("Head", new Vector3(0f, 0.05f, 0f), new Vector3(0.3f, 0.4f, 0.3f)) },
            { CharacterDamagedFragment.Body, new FragmentInfo("Spine_02", Vector3.zero, new Vector3(0.55f, 0.3f, 0.3f)) },
            { CharacterDamagedFragment.ShoulderLeft, new FragmentInfo("Shoulder_L", new Vector3(0f, -0.02f, 0f), new Vector3(0.3f, 0.15f, 0.15f)) },
            { CharacterDamagedFragment.ShoulderRight, new FragmentInfo("Shoulder_R", new Vector3(0f, 0.02f, 0f), new Vector3(0.3f, 0.15f, 0.15f)) },
            { CharacterDamagedFragment.ElbowLeft, new FragmentInfo("Elbow_L", new Vector3(-0.05f, 0f, 0f), new Vector3(0.5f, 0.15f, 0.15f)) },
            { CharacterDamagedFragment.ElbowRight, new FragmentInfo("Elbow_R", new Vector3(-0.05f, 0f, 0f), new Vector3(0.5f, 0.15f, 0.15f)) },
            { CharacterDamagedFragment.UpperLegLeft, new FragmentInfo("UpperLeg_L", new Vector3(0.1f, 0f, 0f), new Vector3(0.5f, 0.15f, 0.15f)) },
            { CharacterDamagedFragment.UpperLegRight, new FragmentInfo("UpperLeg_R", new Vector3(-0.1f, 0f, 0f), new Vector3(0.5f, 0.15f, 0.15f)) },
            { CharacterDamagedFragment.LowerLegLeft, new FragmentInfo("LowerLeg_L", new Vector3(0.2f, 0f, 0f), new Vector3(0.5f, 0.15f, 0.15f)) },
            { CharacterDamagedFragment.LowerLegRight, new FragmentInfo("LowerLeg_R", new Vector3(-0.2f, 0f, 0f), new Vector3(0.5f, 0.15f, 0.15f)) },
        };

        private static FragmentInfo EYES_INFO = new FragmentInfo("Eyes", new Vector3(0f, 0.03f, 0.2f), Vector3.zero);
        private const string ROOT_SKELETON_NAME = "Root";
        
        [MenuItem("7d1n/Root Bones/Add humanoid Fragment Damaged")]
        public static void AddDamagedFragment()
        {
            var root = TryFindRoot(Selection.activeTransform, ROOT_SKELETON_NAME);
            if (root == null)
                root = TryFindRootReverse(Selection.activeTransform, ROOT_SKELETON_NAME);
            if (root == null)
            {
                Debug.LogError("root skeleton not founded in selected object, you must select bone/skeleton!");
                return;
            }

            var damaged = FindDamaged(root);
            SetupCharacterProps(root);

            foreach (var fragment in humanoidBonesName)
                SetupFragmentSet(root, damaged, fragment.Key);

            Debug.Log("Setup fragment bones success!");
        }
        
        [MenuItem("7d1n/Root Bones/Remove humanoid Fragment Damaged")]
        public static void RemoveDamagedFragment()
        {
            var root = TryFindRoot(Selection.activeTransform, ROOT_SKELETON_NAME);
            if (root == null)
                root = TryFindRootReverse(Selection.activeTransform, ROOT_SKELETON_NAME);
            if (root == null)
            {
                Debug.LogError("root skeleton not founded in selected object, you must select bone/skeleton!");
                return;
            }

            foreach (var fragment in humanoidBonesName)
                RemoveFragmentSet(root, fragment.Key);
        }

        private static DamagedBase FindDamaged(Transform root)
        {
            var damaged = TryFindByChild<DamagedBase>(root);
            if (damaged == null)
                throw new Exception("DamagedBase not founded on root.parent!");
            return damaged;
        }
        
        private static void SetupCharacterProps(Transform root)
        {
            var character = TryFindByChild<EnemyNpcBehaviour>(root);
            if (character == null)
                throw new Exception("EnemyNpcBehaviour not founded on root.parent!");

            var eyes = TryFindRoot(root, EYES_INFO.Name);
            if (eyes == null)
                throw new Exception("skeleton hasn't contains eyes bone with name '" + EYES_INFO.Name + "'!");

            var eye = eyes.Find("Eye")?.gameObject;
            if (eye == null)
            {
                eye = new GameObject();
                eye.name = "Eye";
                eye.transform.SetParent(eyes);
            }

            eye.transform.localScale = Vector3.one;
            eye.transform.localPosition = EYES_INFO.Center;
            eye.transform.localRotation = Quaternion.identity;
            character.Eye = eye.transform;
        }
        
        private static void RemoveFragmentSet(Transform root, CharacterDamagedFragment fragmentType)
        {
            var info = humanoidBonesName[fragmentType];
            var fragment = TryFindFragment(root, info);
            if(fragment == null)
                return;
            fragment.gameObject.GetComponent<FragmentCharacterDamagedBehaviour>()?.Destroy();
            fragment.gameObject.GetComponent<BoxCollider>()?.Destroy();
        }
        
        private static void SetupFragmentSet(Transform root, DamagedBase damagedBase, CharacterDamagedFragment fragmentType)
        {
            var scale = new Vector3(1f / root.localScale.x, 
                                    1f / root.localScale.y, 
                                    1f / root.localScale.z);
            
            var info = humanoidBonesName[fragmentType];
            
            var fragment = TryFindFragment(root, info);

            var collider = fragment.GetComponent<BoxCollider>();
            if (collider == null)
                collider = fragment.gameObject.AddComponent<BoxCollider>();

            collider.center = info.Center;
            collider.size = new Vector3(info.Size.x * scale.x, 
                                        info.Size.y * scale.y,
                                        info.Size.z * scale.z);

            Debug.Log("fragment '" + fragmentType.ToString() + "' added collider");
            
            var damaged = fragment.gameObject.GetComponent<FragmentCharacterDamagedBehaviour>();
            if(damaged == null)
                damaged = fragment.gameObject.AddComponent<FragmentCharacterDamagedBehaviour>();
            
            damaged.CharacterFragment = fragmentType;
            damaged.Damaged = damagedBase;
            
            Debug.Log("fragment '" + fragmentType.ToString() + "' added damaged fragment");
        }

        private static T TryFindByChild<T>(Transform child) where T : MonoBehaviour
        {
            if (child == null)
                return null;
            var component = child.GetComponent<T>();
            if (component != null)
                return component;
            
            return TryFindByChild<T>(child.parent);
        }
        
        private static Transform TryFindRootReverse(Transform child, string name)
        {
            if (child == null)
                return null;
            if (child.name == name)
                return child;
            return TryFindRootReverse(child.parent, name);
        }
        
        private static Transform TryFindFragment(Transform root, FragmentInfo fragmentInfo)
        {
            var fragment = TryFindRoot(root, fragmentInfo.Name);
            if (fragment == null)
                throw new Exception("fragment '" + fragmentInfo.Name + "' not founded in root!");
            return fragment;
        }
        
        private static Transform TryFindRoot(Transform parent, string name)
        {
            if (parent == null)
                return null;
            
            if (parent.name == name)
                return parent;

            foreach (var child in parent.Childs())
            {
                var findByChild = TryFindRoot(child, name);
                if (findByChild != null)
                    return findByChild;
            }
            
            return null;
        }
        
    }
    
}