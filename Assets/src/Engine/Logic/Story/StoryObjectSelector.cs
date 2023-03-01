using Engine.Data;
using Engine.Logic.Locations;
using UnityEngine;

namespace Engine.Story
{
    
    [RequireComponent(typeof(IStorySelectCatcher))]
    public class StoryObjectSelector : MonoBehaviour
    {

        /// <summary>
        ///     Время нажатия, чтобы рассчитать задержку нажатия
        ///     ---
        ///     Press time to calculate the press delay
        /// </summary>
        private float downTime;

        private LocationCharacter character;
        
        private void OnMouseDown()
        {
            if (Game.Instance.Runtime.Mode != Mode.Game)
                return;

            downTime = Time.time;
        }

        private void OnMouseUp()
        {
            if (Game.Instance.Runtime.Mode != Mode.Game)
                return;

            if (Time.time - downTime < 0.4f) // Это обычный клик на объекте
            {
                if(ObjectFinder.Character?.IsBusy ?? true)
                    return; // Персонаж щас чем то занят или его вообще нет в сцене, ничего не делаем
                
                var component = GetComponent<IStorySelectCatcher>();
                if (CheckDistance())
                {
                    component?.SelectInDistance();
                }
                else
                {
                    component?.SelectOutDistance();
                }
            }
        }

        /// <summary>
        ///     Определяет, насколько персонаж далеко от нажимаемого объекта, может персонаж не достаёт до объекта...
        ///     ---
        ///     Determines how far the character is from the pressed object, maybe the character does not reach the object...
        /// </summary>
        /// <returns>
        ///     true - если персонаж достаточно близко,
        ///     false - иначе
        ///     ---
        ///     true - if the character is close enough,
        ///     false - otherwise
        /// </returns>
        private bool CheckDistance()
        {
            if (character == null)
                character = ObjectFinder.Character;

            var distance = Vector3.Distance(transform.position, character.transform.position);
            return distance <= character.PickUpDistance * 2f;
        }
        
    }
    
}