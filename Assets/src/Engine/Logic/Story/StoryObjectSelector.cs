using Engine.Data;
using Engine.Logic.Locations;
using UnityEngine;

namespace Engine.Story
{
    
    /// <summary>
    ///
    /// Селектор истории.
    /// Выполняет отлавливание нажатия на данный селектор, после чего выполняет
    /// вызов связанной с селектором истории, находящейся на этом же GameObject.
    /// ---
    /// Story selector.
    /// Catches a click on the given selector, and then performs a
    /// call the story associated with the selector, located on the same GameObject.
    /// 
    /// </summary>
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
            if (Game.Instance.Runtime.Mode != Mode.Game
                || Game.Instance.Runtime.ActionMode == ActionMode.Rotation)
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
                
                // Истории может не быть на объекте, что странно, а так же история может быть выключена, в таких случаях ничего не запускаем
                // Story may not be on the object, which is strange, and also history may be turned off, in such cases do not launch anything
                if(component == null || !component.IsActive)
                    return;
                
                if (CheckDistance(character))
                {
                    component.SelectInDistance();
                    return;
                }
                
                // История есть, она включена, но персонаж находится слишком далеко чтобы с ней взаимодействовать
                // The story is there, it's on, but the character is too far away to interact with it
                component.SelectOutDistance();
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