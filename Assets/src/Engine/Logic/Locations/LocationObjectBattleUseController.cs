using UnityEngine;

namespace Engine.Logic.Locations
{

    /// <summary>
    /// Предмет можно использовать в бою за ОД
    /// </summary>
    public class LocationObjectBattleUseController : MonoBehaviour
    {

        [SerializeField] private int ap = 1;

        /// <summary>
        /// Стоимость использования
        /// </summary>
        public int AP
        {
            get
            {
                return ap;
            }
        }

    }

}
