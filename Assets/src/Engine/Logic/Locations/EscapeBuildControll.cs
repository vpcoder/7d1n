using Engine.EGUI;
using Engine.Scenes;
using UnityEngine;

namespace Engine.Logic.Locations
{

    /// <summary>
    /// Контроллер выхода из локации на глобальную карту
    /// </summary>
    public class EscapeBuildControll : MonoBehaviour
    {

        #pragma warning disable IDE0044, IDE0051

        [SerializeField] private GameObject hintMessagePrefab;

        private void OnMouseDown()
        {
            var marker = ObjectFinder.Find<ExitMarker>();
            var character = ObjectFinder.Find<LocationCharacter>();

            if (Vector3.Distance(marker.transform.position, character.transform.position) <= character.PickUpDistance)
            {
                SceneManager.Instance.Switch(SceneName.Map);
            }
            else
            {
                UIHintMessageManager.Show(hintMessagePrefab, character.transform.position, Localization.Instance.Get("msg_error_cant_do_exit"));
            }
        }

    }

}
