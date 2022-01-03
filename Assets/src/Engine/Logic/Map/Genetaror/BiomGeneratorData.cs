using System;
using System.Collections.Generic;
using UnityEngine;

namespace Engine.Logic.Map
{

    public class BiomGeneratorData : MonoBehaviour
    {

        [SerializeField] protected List<Color> biomList;

        [SerializeField] protected List<GameObject> waterGroundList;
        [SerializeField] protected GameObject waterPlane;

        [SerializeField] protected List<GameObject> sandGroundList;
        [SerializeField] protected GameObject sandPlane;

        [SerializeField] protected List<GameObject> woodedGroundList;
        [SerializeField] protected GameObject woodedPlane;

        [SerializeField] protected List<GameObject> exhaustedGroundList;
        [SerializeField] protected GameObject exhaustedPlane;

        public GameObject GetPlane(BiomType biom)
        {
            switch(biom)
            {
                case BiomType.Water:     return waterPlane;
                case BiomType.Sand:      return sandPlane;
                case BiomType.Wooden:    return woodedPlane;
                case BiomType.Exhausted: return exhaustedPlane;
                default:
                    return null;
            }
        }

        public List<GameObject> GetBiomObjects(BiomType biom)
        {
            switch (biom)
            {
                case BiomType.Water: return waterGroundList;
                case BiomType.Sand: return sandGroundList;
                case BiomType.Wooden: return woodedGroundList;
                case BiomType.Exhausted: return exhaustedGroundList;
                default:
                    throw new NotSupportedException();
            }
        }

        public Color GetBiomBackground(BiomType biom)
        {
            return biomList[(int)biom];
        }

    }

}
