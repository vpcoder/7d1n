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

            // Это обычный клик на объекте
            // This is a normal click on an object
            if (Time.time - downTime < 0.4f)
            {
                var character = ObjectFinder.Character;
                // Если персонаж щас чем то занят или его вообще нет в сцене, ничего не делаем
                // If a character is busy with something or is not in the scene at all, we do nothing.
                if(character == null || character.IsBusy)
                    return;
                
                var component = GetComponent<IStorySelectCatcher>();
                if (CheckDistance(character))
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
        private bool CheckDistance(LocationCharacter character)
        {
            var distance = Vector3.Distance(transform.position, character.transform.position);
            return distance <= character.PickUpDistance * 2f;
        }
        
    }
    
}