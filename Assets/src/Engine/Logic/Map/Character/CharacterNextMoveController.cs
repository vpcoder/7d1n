using Engine.Map;
using Mapbox.Unity.MeshGeneration.Data;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Engine.Logic.Map
{

    public class CharacterNextMoveController : MonoBehaviour,
                                               IPointerClickHandler,
                                               IPointerDownHandler
    {

        private MapCharacter character;
        private CharacterMoveEffectController characterEffect;
        private Vector2 mouseDownPos;
        private EnterBuildControll enterBuildControll;

        private void Awake()
        {
            character = ObjectFinder.Find<MapCharacter>();
            characterEffect = ObjectFinder.Find<CharacterMoveEffectController>();
            enterBuildControll = ObjectFinder.Find<EnterBuildControll>();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            enterBuildControll.Hide();
            character.Target = null;
            mouseDownPos = DeviceInput.TouchPosition;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (Vector3.Distance(DeviceInput.TouchPosition, mouseDownPos) >= 10f)
                return;

            var ray = Camera.main.ScreenPointToRay(DeviceInput.TouchPosition);
            var hitsData = Physics.RaycastAll(ray, 1000f);

            if (hitsData.Length == 0)
                return;

            var tileHitInfo = hitsData.Where(hit => hit.transform?.gameObject?.GetComponent<UnityTile>() != null)
                .FirstOrDefault();

            character.NextPosition = tileHitInfo.point;
            character.NeedMove = true;
            characterEffect.StartMove(character.NextPosition);
        }

    }

}
