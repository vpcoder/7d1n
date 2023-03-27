using Engine.Logic.Locations.Animation;
using UnityEngine;

namespace Engine.Logic.Map
{

    public class CharacterMoveEffectController : MonoBehaviour
    {
        [SerializeField] private Animator characterAnimator;

        [SerializeField] private GameObject waterEffect;
        [SerializeField] private GameObject derbisEffect;
        [SerializeField] private GameObject dustEffect;

        private bool move;
        private BiomType biom;
        private Vector3 movePoint;

        public void StartMove(Vector3 movePoint)
        {
            this.movePoint = movePoint;
            move = true;
            UpdateInfo();
            characterAnimator.SetCharacterMoveSpeedType(MoveSpeedType.Walk);
            characterAnimator.transform.LookAt(movePoint);
        }

        public void StopMove()
        {
            move = false;
            UpdateInfo();

            characterAnimator.SetCharacterMoveSpeedType(MoveSpeedType.Idle);
        }

        public void SwitchBiom(BiomType biom)
        {
            this.biom = biom;
            if (move)
                UpdateInfo();
        }

        private void UpdateInfo()
        {
            if (!move)
            {
                waterEffect.SetActive(false);
                derbisEffect.SetActive(false);
                dustEffect.SetActive(false);
                return;
            }

            switch(biom)
            {
                case BiomType.Water:
                    waterEffect.SetActive(true);
                    derbisEffect.SetActive(false);
                    dustEffect.SetActive(false);
                    break;
                case BiomType.Sand:
                    waterEffect.SetActive(false);
                    derbisEffect.SetActive(false);
                    dustEffect.SetActive(true);
                    break;
                case BiomType.Exhausted:
                    waterEffect.SetActive(false);
                    derbisEffect.SetActive(false);
                    dustEffect.SetActive(true);
                    break;
                case BiomType.Wooden:
                    waterEffect.SetActive(false);
                    derbisEffect.SetActive(true);
                    dustEffect.SetActive(false);
                    break;
            }
        }

    }

}
