using UnityEngine;

namespace Engine.Logic.Locations
{
    
    /// <summary>
    ///
    /// Фрагмент персонажа для получения урона.
    /// Это может быть рука, голова, живот и прочие части тела.
    /// Задача каждой части тела в конечном итоге передать весь урон в агрегатор урона - IDamagedObject
    /// ---
    /// A fragment of the character to take damage.
    /// This can be an arm, head, stomach, or other body part.
    /// The task of each body part is to eventually pass all the damage to the damage aggregator - IDamagedObject
    /// 
    /// </summary>
    public class FragmentCharacterDamagedBehaviour : FragmentDamagedBase
    {

        [SerializeField] private CharacterDamagedFragment fragment;

        public CharacterDamagedFragment CharacterFragment => fragment;
        
    }

}