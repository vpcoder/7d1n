using UnityEditor.Animations;
using UnityEngine;

namespace Engine.Data
{

    public class EnemyBody : MonoBehaviour
    {

        [SerializeField] private AnimatorController controller;
        [SerializeField] private Avatar avatar;

        public AnimatorController Controller { get { return controller; } }
        public Avatar Avatar { get { return avatar; } }

    }

}
