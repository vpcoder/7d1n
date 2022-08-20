using System.Collections.Generic;
using UnityEngine;

namespace Engine.Logic
{

    /// <summary>
    /// Контроллер ячеек экипировки
    /// (всё куда вешается одежда персонажа)
    /// </summary>
    public class CharacterClothCellController : MonoBehaviour
    {

        /// <summary>
        /// Все поддерживаемые ячейки экипировки
        /// </summary>
        [SerializeField] private List<CharacterClothCell> cells;

        /// <summary>
        /// Выполняет обновление информации о ячейках на UI
        /// </summary>
        public void UpdateInfo()
        {
            foreach (var cell in cells)
                cell.UpdateInfo();
        }

    }

}
