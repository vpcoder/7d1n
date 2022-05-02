using System.Collections.Generic;
using UnityEngine;

namespace Engine.Logic.Locations
{

    /// <summary>
    /// 
    /// Информация о траектории пути персонажа, рассчитанного на основании действий игрока
    /// ---
    /// Information about the trajectory of the character, calculated on the basis of the player's actions
    /// 
    /// </summary>
    public struct SmartPath
    {

        #region Shared Fields

        /// <summary>
        ///     Фрагмент пути, на который у персонажа достаточно ОД
        ///     ---
        ///     A fragment of the path for which the character has enough APs
        /// </summary>
        public List<Vector3> ActivePath;

        /// <summary>
        ///     Фрагмент пути, на который у персонажа не хватает ОД
        ///     ---
        ///     A fragment of the path for which the character does not have enough APs
        /// </summary>
        public List<Vector3> ErrorPath;

        /// <summary>
        ///     Точка в которой заканчивается часть доступного пути и начинается часть пути на которую не хватает доступных ОД
        ///     ---
        ///     The point at which part of the available path ends and part of the path for which there are not enough available APs begins
        /// </summary>
        public ActivePoint EdgePoint;

        #endregion

        #region Properties

        /// <summary>
        ///     Сколько потребуется потратить ОД чтобы совершить активную часть пути
        ///     ---
        ///     How much APs will it take to do the active part of the journey
        /// </summary>
        public int ActivePathAP
        {
            get
            {
                if (EdgePoint.IsFullPath)
                    return (int)PathHelper.CalcLength(ActivePath) + 1;
                return (int)EdgePoint.ActivePathInMeters;
            }
        }

        /// <summary>
        ///     Сколько бы потребовалось ОД чтобы совершить весь путь
        ///     ---
        ///     How much APs would it take to go all the way
        /// </summary>
        public int FullPathAP
        {
            get
            {
                if (EdgePoint.IsFullPath)
                    return ActivePathAP;
                return (int)(EdgePoint.ActivePathInMeters + EdgePoint.ErrorPathInMeters + 1);
            }
        }

        #endregion

    }

}
